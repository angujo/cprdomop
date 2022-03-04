/** Person #{ch} */
INSERT INTO {sc}.person
(person_id, gender_concept_id, year_of_birth, month_of_birth, day_of_birth, birth_datetime, race_concept_id, ethnicity_concept_id, location_id, provider_id, 
care_site_id, person_source_value, gender_source_value, gender_source_concept_id, race_source_value, race_source_concept_id, ethnicity_source_value, ethnicity_source_concept_id)
SELECT 
patid,case gender::int WHEN 1 THEN 8507 ELSE 8532 END, yob, mob, null, null, 0, 0, null, null, 
RIGHT(patid::varchar,5)::bigint, patid, gender, 0, null, 0, null, 0
FROM {sc}._chunk JOIN {ss}.patient ON patient_id = patid AND accept = 1 AND gender::int IN (1,2) AND (case when 4 > char_length(yob::varchar) then 1800+yob else yob end) > 1875 AND (deathdate IS NULL OR deathdate >= crd)
WHERE ordinal = {ch};