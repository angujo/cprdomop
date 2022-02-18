WITH consults AS (
	SELECT consid, patid, constype FROM {sc}._chunk join {ss}.consultation ON patient_id = patid where ordinal = {ch}
),
clinical_source AS (
	SELECT s.patid, s.eventdate, s.consid, s.staffid, cs.constype FROM {ss}.clinical s JOIN consults cs ON cs.consid=s.consid WHERE s.eventdate IS NOT null
),
referral_source AS (
	SELECT s.patid, s.eventdate, s.consid, s.staffid, cs.constype FROM {ss}.referral s JOIN consults cs ON cs.consid=s.consid WHERE s.eventdate IS NOT null
),
test_source AS (
	SELECT s.patid, s.eventdate, s.consid, s.staffid, cs.constype FROM {ss}.test s JOIN consults cs ON cs.consid=s.consid WHERE s.eventdate IS NOT null
),
immunization_source AS (
	SELECT s.patid, s.eventdate, s.consid, s.staffid, cs.constype FROM {ss}.immunisation s JOIN consults cs ON cs.consid=s.consid WHERE s.eventdate IS NOT null
),
therapy_source AS (
	SELECT s.patid, s.eventdate, s.consid, s.staffid, cs.constype FROM {ss}.therapy s JOIN consults cs ON cs.consid=s.consid WHERE s.eventdate IS NOT null
),
union_source AS (
	SELECT * FROM clinical_source
	UNION
	SELECT * FROM referral_source
	UNION
	SELECT * FROM test_source
	UNION
	SELECT * FROM immunization_source
	UNION
	SELECT * FROM therapy_source
)
INSERT INTO {sc}.visit_detail 
	(person_id, visit_detail_concept_id, visit_detail_start_date, visit_detail_start_datetime, visit_detail_end_date, visit_detail_end_datetime, visit_detail_type_concept_id, 
	provider_id, care_site_id, visit_detail_source_value, visit_detail_source_concept_id,  
	preceding_visit_detail_id, parent_visit_detail_id, visit_occurrence_id)
	SELECT 
	patid, 9202 visit_detail_concept_id, eventdate::date, eventdate::timestamp, eventdate::date, eventdate::timestamp, 32827,
	staffid, right(patid::varchar,5)::numeric as care_site_id, constype, 0,
	NULL,NULL,null
	FROM union_source ORDER BY patid, eventdate::date;

-- populate the preceding entries
WITH vdetails AS (SELECT visit_detail_id, lag(visit_detail_id, 1) over(partition BY person_id ORDER BY visit_detail_start_date) AS prev_id FROM {sc}._chunk ch JOIN {sc}.visit_detail on ch.patient_id = person_id WHERE ch.ordinal = {ch} ORDER BY person_id, visit_detail_start_date asc)
UPDATE {sc}.visit_detail v SET preceding_visit_detail_id = d.prev_id, parent_visit_detail_id = d.prev_id FROM vdetails d WHERE v.visit_detail_id=d.visit_detail_id;
	
-- NOTES
-- populate visit_occurrence_id column with value from visit_occurance table