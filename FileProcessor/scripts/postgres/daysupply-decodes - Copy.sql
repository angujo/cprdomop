insert into {sc}.daysupply_decodes 
select b.prodcode, b.daily_dose, b.qty, b.numpacks, b.numdays 
from (select *, ROW_NUMBER() over (partition by prodcode, daily_dose, qty, numpacks order by daycount desc) AS RowNumber 
from (select prodcode, 
    case when c.daily_dose is null then 0 else c.daily_dose end as daily_dose, 
    case when qty is null then 0 else qty end as qty, 
    case when numpacks is null then 0 
    else numpacks end as numpacks, 
    numdays, 
    COUNT(prodcode) as daycount 
from {sc}.therapy t left join {sc}.commondosages c on t.dosageid = c.dosageid 
where (numdays > 0 and numdays <=365) and prodcode>1 
group by prodcode, 
    case when c.daily_dose is null then 0 else c.daily_dose end, 
    case when qty is null then 0 else qty end, 
    case when numpacks is null then 0 else numpacks end, numdays) a ) b 
where RowNumber=1;