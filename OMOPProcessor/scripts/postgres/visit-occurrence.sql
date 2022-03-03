/** VisitOccurrence */
INSERT INTO {sc}.visit_occurrence
	(person_id, visit_concept_id, visit_start_date, visit_start_datetime, visit_end_date, visit_end_datetime, visit_type_concept_id, provider_id, care_site_id, visit_source_value, 
	visit_source_concept_id, admitted_from_concept_id, admitted_from_source_value, discharged_to_concept_id, discharged_to_source_value, preceding_visit_occurrence_id)
	SELECT 
	person_id, 9202, visit_detail_start_date, visit_detail_start_date::timestamp, visit_detail_start_date, visit_detail_start_date::timestamp, 32827, max(provider_id) provider_id, max(care_site_id) care_site_id, max(visit_detail_source_value) visit_detail_source_value, 
	0, null, null, null, null, null
	FROM {sc}._chunk ch join {sc}.visit_detail v ON ch.patient_id = v.person_id WHERE ch.ordinal = {ch} GROUP BY v.person_id, v.visit_detail_start_date;


