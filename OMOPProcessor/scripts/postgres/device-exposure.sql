INSERT INTO {sc}.device_exposure 
(device_type_concept_id, device_exposure_id, person_id, device_concept_id, device_exposure_start_date, 
device_exposure_start_datetime, device_exposure_end_date, device_exposure_end_datetime, unique_device_id, 
quantity, provider_id, visit_occurrence_id, visit_detail_id, device_source_concept_id, device_source_value) 
SELECT 
type_concept_id device_type_concept_id, id device_exposure_id, person_id, concept_id device_concept_id, start_date device_exposure_start_date, 
start_datetime device_exposure_start_datetime, end_date device_exposure_end_date, end_date::timestamp device_exposure_end_datetime, NULL unique_device_id, 
NULL quantity, provider_id, visit_occurrence_id, NULL visit_detail_id, source_concept_id device_source_concept_id, source_value device_source_value  
FROM {sc}._chunk JOIN {sc}.stem_table ON patient_id = person_id AND domain_id IN ('Device') WHERE ordinal = {ch}