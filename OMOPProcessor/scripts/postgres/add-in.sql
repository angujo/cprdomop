/** AddIn #{ch} */
INSERT INTO {sc}.add_in 
(patid, eventdate, constype, consid, staffid, enttype, category, description, data, value_as_number, value_as_string, value_as_date, unit_source_value, qualifier_source_value, read_code_description, gemscript_description, data_fields, source_value)
(WITH corecte AS(
		SELECT a.patid, a.adid, a.enttype, a.data1, a.data2, a.data3, a.data4, a.data5, a.data6, a.data7,
			c.eventdate,c.constype,c.consid,c.staffid,
			e.data_fields, e.category, e.description, e.data1 e_data1, e.data2 e_data2, e.data3 e_data3, e.data4 e_data4, e.data5 e_data5, e.data6 e_data6, e.data7 e_data7, e.data1_lkup, e.data2_lkup, e.data3_lkup, e.data4_lkup, e.data5_lkup, e.data6_lkup, e.data7_lkup
			FROM {sc}._chunk ck 
			join {ss}.additional a ON ck.patient_id = a.patid 
			join {ss}.clinical c on c.adid = a.adid and c.patid = a.patid
			join {ss}.entity e on a.enttype = e.enttype
			WHERE ck.ordinal = {ch}
	),
	lookups AS (
			SELECT lu.text, lu.code, lt.name FROM {ss}.lookup lu JOIN {ss}.lookuptype lt ON lt.lookup_type_id = lu.lookup_type_id
		),
	meds AS (
		SELECT m.medcode, m.read_code, m.desc FROM {ss}.medical m 
		),
	products AS (
		SELECT p.prodcode, p.gemscriptcode, p.productname FROM {ss}.product p
	)
	
	select cc.patid,
	       cc.eventdate, --Join on line 32 to the Clinical table to get this information
	       cc.constype,
	       cc.consid,
	       cc.staffid, 
	       cc.enttype, 
	       cc.category,
	       cc.description, 
	       cc.e_data1 as data,       
	       case when cc.data1_lkup in ('Medical Dictionary', 'Product Dictionary') then null else cc.data1 end as value_as_number, 
	       case when cc.data1_lkup = 'Medical Dictionary' then m.read_code when cc.data1_lkup = 'Product Dictionary' then p.gemscriptcode else lu.text end as value_as_string,
	       case when cc.data1_lkup = 'dd/mm/yyyy' then cc.data1 end as value_as_date,
	       case when cc.enttype in (13,488) then 'kg' --enttype 13 is weight, enttype 488 is weight loss
	        when cc.enttype = 476 then 'cm' --enttype 476 is waist circumference
	        when cc.enttype in (119,61,60,120) then 'week' --These enttypes relate to weeks gestation of fetus
	        when cc.enttype = 14 then 'm' --enttype 14 is height
	        else null --These units have to be hard coded as they do not have a unit lookup
	       end as unit_source_value,
	       null as qualifier_source_value,
	       m.desc as read_code_description,
	       p.productname as gemscript_description,
	       cc.data_fields,
	       cc.enttype::varchar||'-'||cc.category||'-'||cc.description||'-'||cc.e_data1  as source_value
	from corecte cc 
	left join lookups lt on cc.data1_lkup = lt.name and lt.code::text = cc.data1
	  left join meds m on m.medcode::text = cc.data1 and cc.data1_lkup = 'Medical Dictionary'
	  left join products p on p.prodcode::text = cc.data1 and cc.data1_lkup = 'Product Dictionary'
	where cc.data_fields > 0 and cc.enttype not in (72,116,372,78) --These are enttypes where data1 is the value and data2 is the unit
	
	UNION
	
	select cc.patid,
	       cc.eventdate,
	       cc.constype,
	       cc.consid,
	       cc.staffid, 
	       cc.enttype, 
	       cc.category,
	       cc.description, 
	       cc.e_data2 as data,       
	       case when cc.data2_lkup in ('Medical Dictionary', 'Product Dictionary') then null
	        else cc.data2 
	       end as value_as_number, 
	       case when cc.data2_lkup = 'Medical Dictionary' then m.read_code 
	        when cc.data2_lkup = 'Product Dictionary' then p.gemscriptcode
	        else lt.text 
	       end as value_as_string,
	       case when cc.data2_lkup = 'dd/mm/yyyy' then cc.data2
	       end as value_as_date,
	       case when cc.enttype = 52 then 'hour' --enttype 52 is sleep pattern and this is asking average hours of sleep
	        when cc.enttype = 69 then 'week' --enttype 69 is weeks post-natal
	        when cc.enttype = 150 then 'day' --enttype 150 is days post-natal
	        else null 
	       end as unit_source_value,
	       null as qualifier_source_value,
	       m.desc as read_code_description,
	       p.productname as gemscript_description,
	       cc.data_fields,
	       cc.enttype::varchar||'-'||cc.category||'-'||cc.description||'-'||cc.e_data2  as source_value
	from corecte cc 
	left join lookups lt on cc.data2_lkup = lt.name and lt.code::text =  cc.data2
	  left join meds m on m.medcode::text = cc.data2 and cc.data2_lkup = 'Medical Dictionary'
	  left join products p on p.prodcode::text = cc.data2 and cc.data2_lkup = 'Product Dictionary'
	where cc.data_fields > 1 and cc.enttype not in (72,116,78,60,119,120) 
	        
	UNION
	
	select cc.patid,
	       cc.eventdate,
	       cc.constype,
	       cc.consid,
	       cc.staffid, 
	       cc.enttype, 
	       cc.category,
	       cc.description, 
	       cc.e_data3 as data,       
	       case when cc.data3_lkup in ('Medical Dictionary', 'Product Dictionary') then null
	        else cc.data3 
	       end as value_as_number, 
	       case when cc.data3_lkup = 'Medical Dictionary' then m.read_code 
	        when cc.data3_lkup = 'Product Dictionary' then p.gemscriptcode
	        else lt.text 
	       end as value_as_string,
	       case when cc.data3_lkup = 'dd/mm/yyyy' then cc.data3
	       end as value_as_date,
	       null as unit_source_value,
	       null as qualifier_source_value,
	       m.desc as read_code_description,
	       p.productname as gemscript_description,
	       cc.data_fields,
	       cc.enttype::varchar||'-'||cc.category||'-'||cc.description||'-'||cc.e_data3  as source_value
	from corecte cc 
	left join lookups lt on cc.data3_lkup = lt.name and lt.code::text = cc.data3
	  left join meds m on m.medcode::text = cc.data3 and cc.data3_lkup = 'Medical Dictionary'
	  left join products p on p.prodcode::text = cc.data3 and cc.data3_lkup = 'Product Dictionary'
	where cc.data_fields > 2 and cc.enttype not in (372,78,126) --For these enttypes data3 and data4 are coupled and handled farther down in the query
	
	UNION
	
	select cc.patid,
	       cc.eventdate,
	       cc.constype,
	       cc.consid,
	       cc.staffid, 
	       cc.enttype, 
	       cc.category,
	       cc.description, 
	       cc.e_data4 as data,       
	       case when cc.data4_lkup in ('Medical Dictionary', 'Product Dictionary') then null
	        else cc.data4 
	       end as value_as_number, 
	       case when cc.data4_lkup = 'Medical Dictionary' then m.read_code 
	        when cc.data4_lkup = 'Product Dictionary' then p.gemscriptcode
	        else lt.text 
	       end as value_as_string,
	       case when cc.data4_lkup = 'dd/mm/yyyy' then cc.data4
	       end as value_as_date,
	       null as unit_source_value,
	       null as qualifier_source_value,
	       m.desc as read_code_description,
	       p.productname as gemscript_description,
	       cc.data_fields,
	       cc.enttype::varchar||'-'||cc.category||'-'||cc.description||'-'||cc.e_data4  as source_value
	from corecte cc 
	left join lookups lt on cc.data4_lkup = lt.name and lt.code::text = cc.data4
	  left join meds m on m.medcode::text = cc.data4 and cc.data4_lkup = 'Medical Dictionary'
	  left join products p on p.prodcode::text = cc.data4 and cc.data4_lkup = 'Product Dictionary'
	where cc.data_fields > 3 and cc.enttype not in (372,78,126) --For these enttypes data3 and data4 are coupled and handled farther down in the query
	        
	UNION
	
	select cc.patid,
	       cc.eventdate,
	       cc.constype,
	       cc.consid,
	       cc.staffid, 
	       cc.enttype, 
	       cc.category,
	       cc.description, 
	       cc.e_data5 as data,       
	       case when cc.data5_lkup in ('Medical Dictionary', 'Product Dictionary') then null
	        else cc.data5 
	       end as value_as_number, 
	       case when cc.data5_lkup = 'Medical Dictionary' then m.read_code 
	        when cc.data5_lkup = 'Product Dictionary' then p.gemscriptcode
	        else lt.text 
	       end as value_as_string,
	       case when cc.data5_lkup = 'dd/mm/yyyy' then cc.data5
	       end as value_as_date,
	       null as unit_source_value,
	       null as qualifier_source_value,
	       m.desc as read_code_description,
	       p.productname as gemscript_description,
	       cc.data_fields,
	       cc.enttype::varchar||'-'||cc.category||'-'||cc.description||'-'||cc.e_data5  as source_value
	from corecte cc 
	left join lookups lt on cc.data5_lkup = lt.name and lt.code::text = cc.data5
	  left join meds m on m.medcode::text = cc.data5 and cc.data5_lkup = 'Medical Dictionary'
	  left join products p on p.prodcode::text = cc.data5 and cc.data5_lkup = 'Product Dictionary'
	where cc.data_fields > 4 and cc.enttype not in (78) --For this enttype data5 and data6 are coupled and handled farther down in the query
	        
	UNION
	
	select cc.patid,
	       cc.eventdate,
	       cc.constype,
	       cc.consid,
	       cc.staffid, 
	       cc.enttype, 
	       cc.category,
	       cc.description, 
	       cc.e_data6 as data,       
	       case when cc.data6_lkup in ('Medical Dictionary', 'Product Dictionary') then null
	        else cc.data6 
	       end as value_as_number, 
	       case when cc.data6_lkup = 'Medical Dictionary' then m.read_code 
	        when cc.data6_lkup = 'Product Dictionary' then p.gemscriptcode
	        else lt.text 
	       end as value_as_string,
	       case when cc.data6_lkup = 'dd/mm/yyyy' then cc.data6
	       end as value_as_date,
	       null as unit_source_value,
	       null as qualifier_source_value,
	       m.desc as read_code_description,
	       p.productname as gemscript_description,
	       cc.data_fields,
	       cc.enttype::varchar||'-'||cc.category||'-'||cc.description||'-'||cc.e_data6  as source_value
	from corecte cc 
	left join lookups lt on cc.data6_lkup = lt.name and lt.code::text = cc.data6
	  left join meds m on m.medcode::text = cc.data6 and cc.data6_lkup = 'Medical Dictionary'
	  left join products p on p.prodcode::text = cc.data6 and cc.data6_lkup = 'Product Dictionary'
	where cc.data_fields > 5 and cc.enttype not in (78) --For this enttype data5 and data6 are coupled and handled farther down in the query
	        
	UNION
	
	select cc.patid,
	       cc.eventdate,
	       cc.constype,
	       cc.consid,
	       cc.staffid, 
	       cc.enttype, 
	       cc.category,
	       cc.description, 
	       cc.e_data7 as data,       
	       case when cc.data7_lkup in ('Medical Dictionary', 'Product Dictionary') then null
	        else cc.data7 
	       end as value_as_number, 
	       case when cc.data7_lkup = 'Medical Dictionary' then m.read_code 
	        when cc.data7_lkup = 'Product Dictionary' then p.gemscriptcode
	        else lt.text 
	       end as value_as_string,
	       case when cc.data7_lkup = 'dd/mm/yyyy' then cc.data7
	       end as value_as_date,
	       null as unit_source_value,
	       null as qualifier_source_value,
	       m.desc as read_code_description,
	       p.productname as gemscript_description,
	       cc.data_fields,
	       cc.enttype::varchar||'-'||cc.category||'-'||cc.description||'-'||cc.e_data7  as source_value
	from corecte cc 
	left join lookups lt on cc.data7_lkup = lt.name and lt.code::text = cc.data7
	  left join meds m on m.medcode::text = cc.data7 and cc.data7_lkup = 'Medical Dictionary'
	  left join products p on p.prodcode::text = cc.data7 and cc.data7_lkup = 'Product Dictionary'
	where cc.data_fields > 6
	        
	UNION
	
	select cc.patid,
	       cc.eventdate,
	       cc.constype,
	       cc.consid,
	       cc.staffid, 
	       cc.enttype, 
	       cc.category,
	       cc.description, 
	       cc.e_data1 as data,       
	       case when cc.data1_lkup in ('Medical Dictionary', 'Product Dictionary') then null
	        else cc.data1 
	       end as value_as_number, 
	       case when cc.data1_lkup = 'Medical Dictionary' then m.read_code 
	        when cc.data1_lkup = 'Product Dictionary' then p.gemscriptcode
	        else lt.text 
	       end as value_as_string,
	       case when cc.data1_lkup = 'dd/mm/yyyy' then cc.data1
	       end as value_as_date,
	       lt2.text as unit_source_value,
	       null as qualifier_source_value,
	       m.desc as read_code_description,
	       p.productname as gemscript_description,
	       cc.data_fields,
	       cc.enttype::varchar||'-'||cc.category||'-'||cc.description||'-'||cc.e_data1  as source_value
	from corecte cc 
	left join lookups lt on cc.data1_lkup = lt.name and lt.code::text = cc.data1
	left join lookups lt2 on cc.data2_lkup = lt2.name and lt2.code::text =  cc.data2
	  left join meds m on m.medcode::text = cc.data1 and cc.data1_lkup = 'Medical Dictionary'
	  left join products p on p.prodcode::text = cc.data1 and cc.data1_lkup = 'Product Dictionary'
	where cc.enttype in (72,116,78) --For these enttypes data1 is the value and data2 is the unit
	        
	UNION
	
	select cc.patid,
	       cc.eventdate,
	       cc.constype,
	       cc.consid,
	       cc.staffid, 
	       cc.enttype, 
	       cc.category,
	       cc.description, 
	       cc.e_data3 as data,       
	       case when cc.data3_lkup in ('Medical Dictionary', 'Product Dictionary') then null
	        else cc.data3 
	       end as value_as_number, 
	       case when cc.data3_lkup = 'Medical Dictionary' then m.read_code 
	        when cc.data3_lkup = 'Product Dictionary' then p.gemscriptcode
	        else lt.text 
	       end as value_as_string,
	       case when cc.data3_lkup = 'dd/mm/yyyy' then cc.data3
	       end as value_as_date,
	       lt2.text as unit_source_value,
	       null as qualifier_source_value,
	       m.desc as read_code_description,
	       p.productname as gemscript_description,
	       cc.data_fields,
	       cc.enttype::varchar||'-'||cc.category||'-'||cc.description||'-'||cc.e_data3  as source_value
	from corecte cc 
	left join lookups lt on cc.data3_lkup = lt.name and lt.code::text = cc.data3
	left join lookups lt2 on cc.data4_lkup = lt2.name and lt2.code::text = cc.data4
	  left join meds m on m.medcode::text = cc.data3 and cc.data1_lkup = 'Medical Dictionary'
	  left join products p on p.prodcode::text = cc.data3 and cc.data1_lkup = 'Product Dictionary'
	where cc.enttype in (126,78) --For these datatypes data3 is the value and data4 is the unit
	        
	UNION
	
	select cc.patid,
	       cc.eventdate,
	       cc.constype,
	       cc.consid,
	       cc.staffid, 
	       cc.enttype, 
	       cc.category,
	       cc.description, 
	       cc.e_data5 as data,       
	       case when cc.data5_lkup in ('Medical Dictionary', 'Product Dictionary') then null
	        else cc.data5 
	       end as value_as_number, 
	       case when cc.data5_lkup = 'Medical Dictionary' then m.read_code 
	        when cc.data5_lkup = 'Product Dictionary' then p.gemscriptcode
	        else lt.text 
	       end as value_as_string,
	       case when cc.data5_lkup = 'dd/mm/yyyy' then cc.data5
	       end as value_as_date,
	       lt2.text as unit_source_value,
	       null as qualifier_source_value,
	       m.desc as read_code_description,
	       p.productname as gemscript_description,
	       cc.data_fields,
	       cc.enttype::varchar||'-'||cc.category||'-'||cc.description||'-'||cc.e_data5  as source_value
	from corecte cc 
	left join lookups lt on cc.data5_lkup = lt.name and lt.code::text = cc.data5
	left join lookups lt2 on cc.data6_lkup = lt2.name and lt2.code::text = cc.data6
	  left join meds m on m.medcode::text = cc.data5 and cc.data1_lkup = 'Medical Dictionary'
	  left join products p on p.prodcode::text = cc.data5 and cc.data1_lkup = 'Product Dictionary'
	where cc.enttype in (78) --For this enttype data5 is the value and data6 is the unit
	        
	UNION
	
	select cc.patid,
	       cc.eventdate,
	       cc.constype,
	       cc.consid,
	       cc.staffid,  
	       cc.enttype, 
	       cc.category,
	       cc.description, 
	       sm.scoringmethod as data,       
	       cc.data1 as value_as_number, 
	       null as value_as_string,
	       case when cc.data1_lkup = 'dd/mm/yyyy' then cc.data1
	       end as value_as_date,
	       null as unit_source_value,
	       lt.text as qualifier_source_value,
	       null as read_code_description,
	       null as gemscript_description,
	       cc.data_fields,
	       cc.enttype::varchar||'-'||cc.category||'-'||cc.description||'-'||sm.scoringmethod  as source_value
	from corecte cc 
	left join lookups lt on cc.data4_lkup = lt.name and lt.code::text = cc.data4
	  left join {ss}.scoremethod sm on cc.data3 = sm.code::text
	where cc.enttype in (372) --This enttype is for the results of scores and questionnaires
);