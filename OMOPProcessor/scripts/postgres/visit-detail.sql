/** VisitDetail */
WITH clinical_source AS (
	SELECT s.patid, s.eventdate, s.consid, s.staffid FROM {sc}._chunk join {ss}.clinical s ON patient_id = patid AND s.eventdate IS NOT null where ordinal = {ch}
),
referral_source AS (
	SELECT s.patid, s.eventdate, s.consid, s.staffid FROM {sc}._chunk join {ss}.referral s ON patient_id = patid AND s.eventdate IS NOT null where ordinal = {ch}
),
test_source AS (
	SELECT s.patid, s.eventdate, s.consid, s.staffid FROM {sc}._chunk join {ss}.test s ON patient_id = patid AND s.eventdate IS NOT null where ordinal = {ch}
),
immunization_source AS (
	SELECT s.patid, s.eventdate, s.consid, s.staffid FROM {sc}._chunk join {ss}.immunisation s ON patient_id = patid AND s.eventdate IS NOT null where ordinal = {ch}
),
therapy_source AS (
	SELECT s.patid, s.eventdate, s.consid, s.staffid FROM {sc}._chunk join {ss}.therapy s ON patient_id = patid AND s.eventdate IS NOT null where ordinal = {ch}
),
union_source AS (
	SELECT * FROM clinical_source
	UNION ALL
	SELECT * FROM referral_source
	UNION ALL
	SELECT * FROM test_source
	UNION ALL
	SELECT * FROM immunization_source
	UNION ALL
	SELECT * FROM therapy_source
)
INSERT INTO {sc}.visit_detail 
	(person_id, visit_detail_concept_id, visit_detail_start_date, visit_detail_start_datetime, visit_detail_end_date, visit_detail_end_datetime, visit_detail_type_concept_id, 
	provider_id, care_site_id, visit_detail_source_value, visit_detail_source_concept_id,  
	preceding_visit_detail_id, parent_visit_detail_id, visit_occurrence_id)
	SELECT 
	u.patid, 9202 visit_detail_concept_id, u.eventdate::date, u.eventdate::timestamp, u.eventdate::date, u.eventdate::timestamp, 32827,
	u.staffid, right(u.patid::varchar,5)::numeric as care_site_id, cs.constype, 0,
	NULL,NULL,null
	FROM {sc}._chunk 
	join {ss}.consultation cs ON patient_id = cs.patid 
	JOIN union_source u ON cs.patid=u.patid AND cs.consid = u.consid AND cs.eventdate = u.eventdate
	where ordinal = {ch}

	
-- NOTES
-- populate visit_occurrence_id column with value from visit_occurance table