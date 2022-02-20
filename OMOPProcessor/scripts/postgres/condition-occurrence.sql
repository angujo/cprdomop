INSERT INTO {sc}.condition_occurrence 
(condition_status_source_value, provider_id, visit_occurrence_id, visit_detail_id, condition_status_concept_id, condition_occurrence_id, 
condition_source_value, person_id, condition_concept_id, condition_start_date, condition_source_concept_id, 
condition_start_datetime, condition_end_date, condition_end_datetime, condition_type_concept_id, stop_reason) 
SELECT 
NULL condition_status_source_value, provider_id, visit_occurrence_id, null visit_detail_id, NULL condition_status_concept_id, stem_table.id condition_occurrence_id, 
source_value condition_source_value, person_id, concept_id condition_concept_id, start_date condition_start_date, source_concept_id condition_source_concept_id, 
start_datetime condition_start_datetime, end_date::date condition_end_date, end_date::timestamp condition_end_datetime, type_concept_id condition_type_concept_id, NULL stop_reason  
FROM {sc}._chunk JOIN {sc}.stem_table ON patient_id = person_id and domain_id IN ('Condition','Condition Status') where ordinal = {ch}