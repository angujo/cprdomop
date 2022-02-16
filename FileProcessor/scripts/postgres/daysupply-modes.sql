insert into {sc}.daysupply_modes
select b.prodcode, b.numdays as dayssupply 
from (select a.prodcode, a.numdays, a.daycount, ROW_NUMBER() over (partition by a.prodcode order by a.prodcode, a.daycount desc) AS RowNumber
    from (select prodcode, numdays, count(patid) as daycount 
        from {sc}.therapy 
        where (numdays > 0 and numdays <=365) and prodcode>1 
        group by prodcode, numdays) a ) b 
where RowNumber=1
