/** Measurement #{ch} */
INSERT INTO {sc}.measurement 
(measurement_id, measurement_source_value, unit_source_value, value_source_value, measurement_time, person_id, measurement_concept_id, 
measurement_date, measurement_datetime, measurement_type_concept_id, operator_concept_id, value_as_number, value_as_concept_id, 
unit_concept_id, range_low, range_high, provider_id, visit_occurrence_id, visit_detail_id, measurement_source_concept_id) 
SELECT 
stem_table.id measurement_id, null measurement_source_value, unit_source_value, value_source_value, null measurement_time, person_id, concept_id measurement_concept_id, 
start_date measurement_date, start_datetime measurement_datetime, type_concept_id measurement_type_concept_id, operator_concept_id::numeric, value_as_number::numeric, value_as_concept_id::numeric, 
unit_concept_id::numeric, range_low::numeric, range_high::numeric, provider_id::numeric, visit_occurrence_id, null visit_detail_id, source_concept_id measurement_source_concept_id  
FROM {sc}._chunk JOIN {sc}.stem_table ON patient_id = person_id AND domain_id IN ('Measurement') WHERE ordinal = {ch}