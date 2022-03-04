/** Observation #{ch} */
INSERT INTO {sc}.observation 
(observation_date, observation_datetime, observation_type_concept_id, unit_source_value, qualifier_source_value, value_as_string, 
observation_source_concept_id, visit_detail_id, visit_occurrence_id, provider_id, observation_source_value, unit_concept_id, qualifier_concept_id, 
value_as_concept_id, value_as_number, observation_id, person_id, observation_concept_id) 
SELECT 
start_date observation_date, start_datetime observation_datetime, type_concept_id observation_type_concept_id, unit_source_value,  NULL qualifier_source_value, value_as_string, 
source_concept_id observation_source_concept_id, NULL visit_detail_id, visit_occurrence_id, provider_id, source_value observation_source_value, unit_concept_id::numeric, NULL qualifier_concept_id, 
value_as_concept_id::numeric, value_as_number::numeric, stem_table.id observation_id, person_id, concept_id observation_concept_id 
FROM {sc}._chunk JOIN {sc}.stem_table ON patient_id = person_id AND domain_id IN ('Observation') WHERE ordinal = {ch}