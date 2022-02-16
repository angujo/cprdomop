INSERT INTO {sc}.procedure_occurrence 
(procedure_source_value, procedure_occurrence_id, person_id, procedure_concept_id, procedure_date, procedure_datetime, 
procedure_type_concept_id, modifier_concept_id, quantity, provider_id, visit_occurrence_id, visit_detail_id, 
procedure_source_concept_id, modifier_source_value) 
SELECT 
(source_value procedure_source_value,id procedure_occurrence_id, person_id, concept_id procedure_concept_id, start_date procedure_date, start_datetime procedure_datetime, 
type_concept_id procedure_type_concept_id, null modifier_concept_id, null quantity, provider_id, visit_occurrence_id, null visit_detail_id, 
source_concept_id procedure_source_concept_id, null modifier_source_value)  
FROM {sc}._chunk JOIN {sc}.stem_table ON patient_id = person_id and domain_id IN ('Procedure') where ordinal = {ch}