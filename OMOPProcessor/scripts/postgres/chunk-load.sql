INSERT INTO {sc}."_chunk"
(ordinal, patient_id)
select {ch} AS ord, patid from {ss}.patient LIMIT {lmt} OFFSET {ost};