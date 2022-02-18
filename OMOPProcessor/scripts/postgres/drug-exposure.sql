INSERT INTO {sc}.drug_exposure 
(sig, drug_exposure_start_date, drug_exposure_start_datetime, drug_exposure_end_date, drug_exposure_end_datetime, 
visit_occurrence_id, visit_detail_id, drug_source_concept_id, stop_reason, provider_id, route_concept_id, days_supply, quantity, 
dose_unit_source_value, route_source_value, drug_source_value, refills, drug_type_concept_id, verbatim_end_date, lot_number, 
drug_concept_id, drug_exposure_id, person_id) 
SELECT 
sig, start_date::date drug_exposure_start_date, start_datetime::timestamp drug_exposure_start_datetime, end_date::date drug_exposure_end_date, end_date::timestamp drug_exposure_end_datetime, 
visit_occurrence_id, null visit_detail_id, source_concept_id drug_source_concept_id, null stop_reason, provider_id,null route_concept_id, null days_supply, NULL quantity, 
NULL dose_unit_source_value, null route_source_value, source_value drug_source_value, NULL refills, type_concept_id drug_type_concept_id, null verbatim_end_date, NULL lot_number, 
concept_id drug_concept_id, id drug_exposure_id, person_id  
FROM {sc}._chunk JOIN {sc}.stem_table ON patient_id = person_id AND domain_id IN ('Drug') WHERE ordinal = {ch}