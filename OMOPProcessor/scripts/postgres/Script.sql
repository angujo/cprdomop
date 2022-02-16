WITH adds AS (
		SELECT patid, adid, a.enttype, a.data1, a.data2, a.data3, a.data4, a.data5, a.data6, a.data7 FROM _chunk ch join SOURCE.additional a on ch.person_id = a.patid 
		where ch.chunkId = {0} 
		),
		
	clinic AS (
		SELECT c.eventdate,c.constype,c.consid,c.staffid,c.adid,c.patid FROM SOURCE.clinical c JOIN adds a ON a.adid = c.adid and c.patid = a.patid 
		group by c.eventdate,c.constype,c.consid,c.staffid,c.adid,c.patid
		),
		
	entities AS (
		SELECT e.enttype, e.data_fields, e.category, e.description, e.data1, e.data2, e.data3, e.data4, e.data5, e.data6, e.data7, e.data1_lkup, e.data2_lkup, e.data3_lkup, e.data4_lkup, e.data5_lkup, e.data6_lkup, e.data7_lkup
		FROM SOURCE.entity e JOIN adds a ON a.enttype = e.enttype
		group by e.enttype, e.data_fields, e.category, e.description, e.data1, e.data2, e.data3, e.data4, e.data5, e.data6, e.data7, e.data1_lkup, e.data2_lkup, e.data3_lkup, e.data4_lkup, e.data5_lkup, e.data6_lkup, e.data7_lkup
		),
		
	consult AS (
		SELECT con.patid, con.consid, con.constype
		FROM SOURCE.consultation cons JOIN clinic c ON c.patid = con.patid and c.consid = con.consid
		group by con.patid, con.consid, con.constype
		),
		
	looktype AS (
		SELECT lt.name, lt.lookup_type_id FROM SOURCE.lookuptype lt 
		JOIN entities e ON lt.name IN (e.data1_lkup, e.data2_lkup, e.data3_lkup, e.data4_lkup, e.data5_lkup, e.data6_lkup, e.data7_lkup)
		group by lt.name, lt.lookup_type_id
		),
		
	looks AS (
		SELECT lu.lookup_type_id, lu.text, lu.code FROM SOURCE.lookup lu 
		JOIN looktype lt on lt.lookup_type_id = lu.lookup_type_id 
		JOIN adds a ON lu.code::text IN (a.data1, a.data2, a.data3, a.data4, a.data5, a.data6, a.data7)
		group by lu.lookup_type_id, lu.text, lu.code
		),
		
	meds AS (
		SELECT m.medcode, m.read_code, m.desc FROM SOURCE.medical m 
		JOIN adds a on m.medcode::text IN (a.data1, a.data2, a.data3, a.data4, a.data5, a.data6, a.data7) 
		JOIN entities e on a.enttype = e.enttype AND 'Medical Dictionary' IN (e.data1_lkup, e.data2_lkup, e.data3_lkup, e.data4_lkup, e.data5_lkup, e.data6_lkup, e.data7_lkup)
		group by m.medcode, m.read_code, m.desc
		),
		
	products AS (
		SELECT p.prodcode, p.gemscriptcode, p.productname FROM SOURCE.product p
		JOIN adds a on p.prodcode::text IN (a.data1, a.data2, a.data3, a.data4, a.data5, a.data6, a.data7) 
		JOIN entities e on a.enttype = e.enttype AND 'Product Dictionary' IN (e.data1_lkup, e.data2_lkup, e.data3_lkup, e.data4_lkup, e.data5_lkup, e.data6_lkup, e.data7_lkup)
		group by p.prodcode, p.gemscriptcode, p.productname
	)
	
select a.patid,
a.eventdate,
a.staffid,
a.value_as_string,
a.value_as_number,
a.value_as_date,
a.unit_source_value,
right(cast(a.patid as varchar),5) as care_site_id,
a.constype,
concat(a.patid,to_char(a.eventdate,'YYYYMMDD'))::numeric as visit_occurrence_id,
qualifier_source_value,
case
when a.qualifier_source_value = 'Not applicable' then 45882470
when a.qualifier_source_value = 'Abnormal' then 45878745
when a.qualifier_source_value = 'Absent' then 45884086
when a.qualifier_source_value = 'High' then 45876384
when a.qualifier_source_value = 'Low' then 45881666
when a.qualifier_source_value = 'Negative' then 45878583
when a.qualifier_source_value = 'Normal' then 45884153
when a.qualifier_source_value = 'Positive' then 45884084
when a.qualifier_source_value = 'Present' then 45879438
when a.qualifier_source_value = 'Trace' then 45881796
when a.qualifier_source_value = 'Unknown' then 45877986
when a.qualifier_source_value = 'Very high' then 45879181
when a.qualifier_source_value = 'Very low' then 45879182
end as qualifier_concept_id,
data,
concat(cast(enttype as varchar), '-', category, '-', description, '-', data)  as source_value
from
(

select a.patid,
c.eventdate, --Join on line 32 to the Clinical table to get this information
con.constype,
c.consid,
c.staffid,
a.enttype,
e.category,
e.description,
e.data1 as data,
case when e.data1_lkup in ('Medical Dictionary', 'Product Dictionary') then null
else a.data1 --A couple of values in data1 are either read codes or drug codes
end as value_as_number,
case when e.data1_lkup = 'Medical Dictionary' then m.read_code
when e.data1_lkup = 'Product Dictionary' then p.gemscriptcode
else lu.text
end as value_as_string,
case when e.data1_lkup = 'dd/mm/yyyy' then a.data1
end as value_as_date,
case when a.enttype in (13,488) then 'kg' --enttype 13 is weight, enttype 488 is weight loss
when a.enttype = 476 then 'cm' --enttype 476 is waist circumference
when a.enttype in (119,61,60,120) then 'week' --These enttypes relate to weeks gestation of fetus
when a.enttype = 14 then 'm' --enttype 14 is height
else '' --These units have to be hard coded as they do not have a unit lookup
end as unit_source_value,
'' as qualifier_source_value,
m.desc as read_code_description,
p.productname as gemscript_description,
e.data_fields
from adds a ON a.patid = ch.PERSON_ID
join entities e on a.enttype = e.enttype AND e.data_fields &gt; 0
join clinic c on c.patid = a.patid and c.adid = a.adid
left join looktype lt on e.data1_lkup = lt.name
left join looks lu on lt.lookup_type_id = lu.lookup_type_id and lu.code::text =  a.data1
left join meds m on m.medcode::text = a.data1 and e.data1_lkup = 'Medical Dictionary'
left join products p on p.prodcode::text = a.data1 and e.data1_lkup = 'Product Dictionary'
left join consult con on a.patid = con.patid and c.consid = con.consid
where a.enttype not in (72,116,372,78)  --These are enttypes where data1 is the value and data2 is the unit
group by a.patid, c.eventdate, con.constype, c.consid, c.staffid, e.category, a.enttype, e.description, e.data_fields, e.data1,
a.data1, lu.text, e.data1_lkup, m.read_code, m.desc, a.data1,
p.productname, p.gemscriptcode

UNION

select a.patid,
c.eventdate,
con.constype,
c.consid,
c.staffid,
a.enttype,
e.category,
e.description,
e.data2 as data,
case when e.data2_lkup in ('Medical Dictionary', 'Product Dictionary') then null
else  a.data2
end as value_as_number,
case when e.data2_lkup = 'Medical Dictionary' then m.read_code
when e.data2_lkup = 'Product Dictionary' then p.gemscriptcode
else lu.text
end as value_as_string,
case when e.data2_lkup = 'dd/mm/yyyy' then a.data2
end as value_as_date,
case when a.enttype = 52 then 'hour' --enttype 52 is sleep pattern and this is asking average hours of sleep
when a.enttype = 69 then 'week' --enttype 69 is weeks post-natal
when a.enttype = 150 then 'day' --enttype 150 is days post-natal
else ''
end as unit_source_value,
'' as qualifier_source_value,
m.desc as read_code_description,
p.productname as gemscript_description,
e.data_fields
from adds a
join entities e on a.enttype = e.enttype AND e.data_fields &gt; 1
left join clinic c on c.patid = a.patid and c.adid = a.adid
left join looktype lt on e.data2_lkup = lt.name
left join looks lu on lt.lookup_type_id = lu.lookup_type_id and lu.code::text =  a.data2
left join meds m on m.medcode::text =  a.data2 and e.data2_lkup = 'Medical Dictionary'
left join products p on p.prodcode::text =  a.data2  and e.data2_lkup = 'Product Dictionary'
left join consult con on a.patid = con.patid and c.consid = con.consid
where a.enttype not in (72,116,78,60,119,120)
group by a.patid, c.eventdate, con.constype, c.consid, c.staffid, e.category, a.enttype, e.description, e.data_fields, e.data2,
a.data2, lu.text, e.data2_lkup, m.read_code, m.desc, a.data2,
p.productname, p.gemscriptcode

UNION

select a.patid,
c.eventdate,
con.constype,
c.consid,
c.staffid,
a.enttype,
e.category,
e.description,
e.data3 as data,
case when e.data3_lkup in ('Medical Dictionary', 'Product Dictionary') then null
else  a.data3
end as value_as_number,
case when e.data3_lkup = 'Medical Dictionary' then m.read_code
when e.data3_lkup = 'Product Dictionary' then p.gemscriptcode
else lu.text
end as value_as_string,
case when e.data3_lkup = 'dd/mm/yyyy' then a.data3
end as value_as_date,
'' as unit_source_value,
'' as qualifier_source_value,
m.desc as read_code_description,
p.productname as gemscript_description,
e.data_fields
from adds a
join entities e on a.enttype = e.enttype AND e.data_fields &gt; 2
left join clinic c on c.patid = a.patid and c.adid = a.adid
left join looktype lt on e.data3_lkup = lt.name
left join looks lu on lt.lookup_type_id = lu.lookup_type_id and lu.code::text =  a.data3
left join meds m on m.medcode::text =  a.data3 and e.data3_lkup = 'Medical Dictionary'
left join products p on p.prodcode::text =  a.data3 and e.data3_lkup = 'Product Dictionary'
left join consult con on a.patid = con.patid and c.consid = con.consid 
where a.enttype not in (372,78,126) --For these enttypes data3 and data4 are coupled and handled farther down in the query
group by a.patid, c.eventdate, con.constype, c.consid, c.staffid, e.category, a.enttype, e.description, e.data_fields, e.data3,
a.data3, lu.text, e.data3_lkup, m.read_code, m.desc, a.data3,
p.productname, p.gemscriptcode

UNION

select a.patid,
c.eventdate,
con.constype,
c.consid,
c.staffid,
a.enttype,
e.category,
e.description,
e.data4 as data,
case when e.data4_lkup in ('Medical Dictionary', 'Product Dictionary') then null
else  a.data4
end as value_as_number,
case when e.data4_lkup = 'Medical Dictionary' then m.read_code
when e.data4_lkup = 'Product Dictionary' then p.gemscriptcode
else lu.text
end as value_as_string,
case when e.data4_lkup = 'dd/mm/yyyy' then a.data4
end as value_as_date,
'' as unit_source_value,
'' as qualifier_source_value,
m.desc as read_code_description,
p.productname as gemscript_description,
e.data_fields
from adds a 
join entities e on a.enttype = e.enttype AND e.data_fields &gt; 3
left join clinic c on c.patid = a.patid and c.adid = a.adid
left join looktype lt on e.data4_lkup = lt.name
left join looks lu on lt.lookup_type_id = lu.lookup_type_id and lu.code::text =  a.data4
left join meds m on m.medcode::text =  a.data4 and e.data4_lkup = 'Medical Dictionary'
left join products p on p.prodcode::text =  a.data4 and e.data4_lkup = 'Product Dictionary'
left join consult con on a.patid = con.patid and c.consid = con.consid   
where a.enttype not in (372,78,126) --For these enttypes data3 and data4 are coupled and handled farther down in the query
group by a.patid, c.eventdate, con.constype, c.consid, c.staffid, e.category, a.enttype, e.description, e.data_fields, e.data4,
a.data4, lu.text, e.data4_lkup, m.read_code, m.desc, a.data4, p.productname, p.gemscriptcode

UNION

select a.patid,
c.eventdate,
con.constype,
c.consid,
c.staffid,
a.enttype,
e.category,
e.description,
e.data5 as data,
case when e.data5_lkup in ('Medical Dictionary', 'Product Dictionary') then null
else  a.data5
end as value_as_number,
case when e.data5_lkup = 'Medical Dictionary' then m.read_code
when e.data5_lkup = 'Product Dictionary' then p.gemscriptcode
else lu.text
end as value_as_string,
case when e.data5_lkup = 'dd/mm/yyyy' then a.data5
end as value_as_date,
'' as unit_source_value,
'' as qualifier_source_value,
m.desc as read_code_description,
p.productname as gemscript_description,
e.data_fields
from adds a
left join clinic c on c.patid = a.patid and c.adid = a.adid
join entities e on a.enttype = e.enttype AND e.data_fields &gt; 4
left join looktype lt on e.data5_lkup = lt.name
left join looks lu on lt.lookup_type_id = lu.lookup_type_id and lu.code::text =  a.data5
left join meds m on m.medcode::text =  a.data5 and e.data5_lkup = 'Medical Dictionary'
left join products p on p.prodcode::text =  a.data5 and e.data5_lkup = 'Product Dictionary'
left join consult con on a.patid = con.patid and c.consid = con.consid  
where a.enttype not in (78) --For this enttype data5 and data6 are coupled and handled farther down in the query
group by a.patid, c.eventdate, con.constype, c.consid, c.staffid, e.category, a.enttype, e.description, e.data_fields, e.data5,
a.data5, lu.text, e.data5_lkup, m.read_code, m.desc, a.data5, p.productname, p.gemscriptcode

UNION

select a.patid,
c.eventdate,
con.constype,
c.consid,
c.staffid,
a.enttype,
e.category,
e.description,
e.data6 as data,
case when e.data6_lkup in ('Medical Dictionary', 'Product Dictionary') then null
else  a.data6
end as value_as_number,
case when e.data6_lkup = 'Medical Dictionary' then m.read_code
when e.data6_lkup = 'Product Dictionary' then p.gemscriptcode
else lu.text
end as value_as_string,
case when e.data6_lkup = 'dd/mm/yyyy' then a.data6
end as value_as_date,
'' as unit_source_value,
'' as qualifier_source_value,
m.desc as read_code_description,
p.productname as gemscript_description,
e.data_fields
from adds a
join entities e on a.enttype = e.enttype AND e.data_fields &gt; 5
left join clinic c on c.patid = a.patid and c.adid = a.adid
left join looktype lt on e.data6_lkup = lt.name
left join looks lu on lt.lookup_type_id = lu.lookup_type_id and lu.code::text =  a.data6
left join meds m on m.medcode::text =  a.data6 and e.data6_lkup = 'Medical Dictionary'
left join products p on p.prodcode::text =  a.data6 and e.data6_lkup = 'Product Dictionary'
left join consult con on a.patid = con.patid and c.consid = con.consid 
where a.enttype not in (78) --For this enttype data5 and data6 are coupled and handled farther down in the query
group by a.patid, c.eventdate, con.constype, c.consid, c.staffid, e.category, a.enttype, e.description, e.data_fields, e.data6,
a.data6, lu.text, e.data6_lkup, m.read_code, m.desc, a.data6, p.productname, p.gemscriptcode

UNION

select a.patid,
c.eventdate,
con.constype,
c.consid,
c.staffid,
a.enttype,
e.category,
e.description,
e.data7 as data,
case when e.data7_lkup in ('Medical Dictionary', 'Product Dictionary') then null
else  a.data7
end as value_as_number,
case when e.data7_lkup = 'Medical Dictionary' then m.read_code
when e.data7_lkup = 'Product Dictionary' then p.gemscriptcode
else lu.text
end as value_as_string,
case when e.data7_lkup = 'dd/mm/yyyy' then a.data7
end as value_as_date,
'' as unit_source_value,
'' as qualifier_source_value,
m.desc as read_code_description,
p.productname as gemscript_description,
e.data_fields
from adds a
join entities e on a.enttype = e.enttype AND e.data_fields &gt; 6
left join clinic c on c.patid = a.patid and c.adid = a.adid
left join looktype lt on e.data7_lkup = lt.name
left join looks lu on lt.lookup_type_id = lu.lookup_type_id and lu.code::text =  a.data7
left join meds m    on m.medcode::text =  a.data7 and e.data7_lkup = 'Medical Dictionary'
left join products p on p.prodcode::text =  a.data7 and e.data7_lkup = 'Product Dictionary'
left join consult con on a.patid = con.patid and c.consid = con.consid  
group by a.patid, c.eventdate, con.constype, c.consid, c.staffid, e.category, a.enttype, e.description, e.data_fields, e.data7,
a.data7, lu.text, e.data7_lkup, m.read_code, m.desc, a.data7,
p.productname, p.gemscriptcode

UNION

select a.patid,
c.eventdate,
con.constype,
c.consid,
c.staffid,
a.enttype,
e.category,
e.description,
e.data1 as data,
case when e.data1_lkup in ('Medical Dictionary', 'Product Dictionary') then null
else  a.data1
end as value_as_number,
case when e.data1_lkup = 'Medical Dictionary' then m.read_code
when e.data1_lkup = 'Product Dictionary' then p.gemscriptcode
else lu.text
end as value_as_string,
case when e.data1_lkup = 'dd/mm/yyyy' then a.data1
end as value_as_date,
lu2.text as unit_source_value,
'' as qualifier_source_value,
m.desc as read_code_description,
p.productname as gemscript_description,
e.data_fields
from adds a
join entities e on a.enttype = e.enttype
left join clinic c on c.patid = a.patid and c.adid = a.adid
left join looktype lt on e.data1_lkup = lt.name
left join looks lu on lt.lookup_type_id = lu.lookup_type_id and lu.code::text =  a.data1
left join looktype lt2 on e.data2_lkup = lt2.name
left join looks lu2 on lt2.lookup_type_id = lu2.lookup_type_id and lu2.code::text =  a.data2
left join meds m on m.medcode::text =  a.data1 and e.data1_lkup = 'Medical Dictionary'
left join products p on p.prodcode::text =  a.data1 and e.data1_lkup = 'Product Dictionary'
left join consult con on a.patid = con.patid and c.consid = con.consid
where a.enttype in (72,116,78) --For these enttypes data1 is the value and data2 is the unit
group by a.patid, c.eventdate, con.constype, c.consid, c.staffid, e.category, a.enttype, e.description, e.data_fields, e.data1,
a.data1, lu.text, e.data1_lkup, m.read_code, m.desc, a.data1,
p.productname, p.gemscriptcode, lu2.text

UNION

select a.patid,
c.eventdate,
con.constype,
c.consid,
c.staffid,
a.enttype,
e.category,
e.description,
e.data3 as data,
case when e.data3_lkup in ('Medical Dictionary', 'Product Dictionary') then null
else  a.data3
end as value_as_number,
case when e.data3_lkup = 'Medical Dictionary' then m.read_code
when e.data3_lkup = 'Product Dictionary' then p.gemscriptcode
else lu.text
end as value_as_string,
case when e.data3_lkup = 'dd/mm/yyyy' then a.data3
end as value_as_date,
lu2.text as unit_source_value,
'' as qualifier_source_value,
m.desc as read_code_description,
p.productname as gemscript_description,
e.data_fields
from adds a 
join entities e on a.enttype = e.enttype
left join clinic c on c.patid = a.patid and c.adid = a.adid
left join looktype lt on e.data3_lkup = lt.name
left join looks lu on lt.lookup_type_id = lu.lookup_type_id and lu.code::text =  a.data3
left join looktype lt2 on e.data4_lkup = lt2.name
left join looks lu2 on lt2.lookup_type_id = lu2.lookup_type_id and lu2.code::text =  a.data4
left join meds m on m.medcode::text =  a.data3 and e.data1_lkup = 'Medical Dictionary'
left join products p on p.prodcode::text =  a.data3 and e.data1_lkup = 'Product Dictionary'
left join consult con on a.patid = con.patid and c.consid = con.consid
where a.enttype in (126,78) --For these datatypes data3 is the value and data4 is the unit
group by a.patid, c.eventdate, con.constype, c.consid, c.staffid, e.category, a.enttype, e.description, e.data_fields, e.data3,
a.data3, lu.text, e.data3_lkup, m.read_code, m.desc, a.data3,
p.productname, p.gemscriptcode, lu2.text

UNION

select a.patid,
c.eventdate,
con.constype,
c.consid,
c.staffid,
a.enttype,
e.category,
e.description,
e.data5 as data,
case when e.data5_lkup in ('Medical Dictionary', 'Product Dictionary') then null
else  a.data5
end as value_as_number,
case when e.data5_lkup = 'Medical Dictionary' then m.read_code
when e.data5_lkup = 'Product Dictionary' then p.gemscriptcode
else lu.text
end as value_as_string,
case when e.data5_lkup = 'dd/mm/yyyy' then a.data5
end as value_as_date,
lu2.text as unit_source_value,
'' as qualifier_source_value,
m.desc as read_code_description,
p.productname as gemscript_description,
e.data_fields
from adds a 
join entities e on a.enttype = e.enttype and e.data1_lkup = 'Medical Dictionary' and e.data1_lkup = 'Product Dictionary'
left join clinic c on c.patid = a.patid and c.adid = a.adid
left join looktype lt on e.data5_lkup = lt.name
left join looks lu on lt.lookup_type_id = lu.lookup_type_id and lu.code::text =  a.data5
left join looktype lt2 on e.data6_lkup = lt2.name
left join looks lu2 on lt2.lookup_type_id = lu2.lookup_type_id and lu2.code::text =  a.data6
left join meds m on m.medcode::text =  a.data5
left join products p on p.prodcode::text =  a.data5
left join consult con on a.patid = con.patid and c.consid = con.consid
where a.enttype in (78) --For this enttype data5 is the value and data6 is the unit
group by a.patid, c.eventdate, con.constype, c.consid, c.staffid, e.category, a.enttype, e.description, e.data_fields, e.data5,
a.data5, lu.text, e.data5_lkup, m.read_code, m.desc, a.data5,
p.productname, p.gemscriptcode, lu2.text

UNION

select a.patid,
c.eventdate,
con.constype,
c.consid,
c.staffid,
a.enttype,
e.category,
e.description,
sm.scoringmethod as data,
a.data1 as value_as_number,
'' as value_as_string,
case when e.data1_lkup = 'dd/mm/yyyy' then a.data1
end as value_as_date,
'' as unit_source_value,
lu4.text as qualifier_source_value,
'' as read_code_description,
'' as gemscript_description,
e.data_fields
from adds a
join entities e on a.enttype = e.enttype
join clinic c on c.patid = a.patid and c.adid = a.adid
left join {sc}.scoringmethod sm on a.data3 = sm.code::text
left join looktype lt4 on e.data4_lkup = lt4.name
left join looks lu4 on lt4.lookup_type_id = lu4.lookup_type_id and lu4.code::text =  a.data4
left join consult con on a.patid = con.patid and c.consid = con.consid
where a.enttype in (372) --This enttype is for the results of scores and questionnaires
group by a.patid, c.eventdate, con.constype, c.consid, c.staffid, e.category, a.enttype, e.description, e.data_fields, sm.scoringmethod,
a.data1, lu4.text, e.data4_lkup, a.data1, e.data1_lkup
) as a
where a.eventdate is not NULL order by a.patid