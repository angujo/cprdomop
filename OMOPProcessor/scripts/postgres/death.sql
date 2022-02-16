INSERT INTO {sc}.death
(person_id, death_date, death_datetime, death_type_concept_id, cause_concept_id, cause_source_value, cause_source_concept_id)
SELECT 
patid, CASE WHEN deathdate IS NULL THEN tod ELSE deathdate END, deathdate::timestamp, 32815, 0, 0, 0
FROM {sc}._chunk JOIN {ss}.patient ON patient_id = person_id and accept = 1 AND gender::int IN (1,2) AND yob > 1875 AND ((deathdate IS NOT null AND deathdate >= crd) OR (tod IS NOT NULL AND toreason IN (1,'death')))
WHERE ordinal = {ch};