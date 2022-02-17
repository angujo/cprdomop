INSERT INTO {sc}.observation_period
(person_id, observation_period_start_date, observation_period_end_date, period_type_concept_id)
SELECT 
p.patid, greatest(p.frd,r.uts), least(p.tod,r.lcd, p.crd), 32880
FROM {sc}._chunk JOIN {ss}.patient p ON p.patid = patient_id JOIN {ss}.practice r ON RIGHT(p.patid::varchar,5)::numeric = r.pracid WHERE ordinal = {ch}