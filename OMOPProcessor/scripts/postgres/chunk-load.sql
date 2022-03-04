/** ChunkLoad */
-- Clean it up 
truncate table {sc}."_chunk" ;
-- Now setup chunks
INSERT INTO {sc}."_chunk"
(ordinal, patient_id)
select ceil((t.rid-1)/{lmt}), t.patid  from (select row_number() over(order by patid) rid, patid  from {ss}.patient p) t;