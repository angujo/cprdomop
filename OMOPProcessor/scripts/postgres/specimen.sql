/** Specimen #{ch} */
INSERT INTO {sc}.specimen 
(disease_status_source_value, unit_source_value, disease_status_concept_id, specimen_source_value, anatomic_site_concept_id, unit_concept_id, quantity, 
specimen_datetime, specimen_date, specimen_type_concept_id, specimen_concept_id, specimen_source_id, person_id, specimen_id, 
anatomic_site_source_value) 
SELECT 
NULL disease_status_source_value, unit_source_value, NULL disease_status_concept_id, source_value specimen_source_value, NULL anatomic_site_concept_id, unit_concept_id::numeric, NULL quantity, 
start_datetime specimen_datetime, start_date specimen_date, type_concept_id specimen_type_concept_id, concept_id specimen_concept_id, NULL specimen_source_id, person_id,stem_table.id  specimen_id, 
NULL anatomic_site_source_value  
FROM {sc}._chunk JOIN {sc}.stem_table ON patient_id = person_id AND domain_id IN ('Specimen') WHERE ordinal = {ch}