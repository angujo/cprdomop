-- populate visit_details' visit_occurrence_id
UPDATE {sc}.visit_detail SET visit_occurrence_id = o.visit_occurrence_id FROM {sc}._chunk ch JOIN {sc}.visit_occurrence o ON ch.patient_id = o.person_id where ch.ordinal = {ch} and visit_detail.visit_detail_start_date = o.visit_start_date;

-- populate the preceding entries
WITH vdetails AS (SELECT visit_detail_id, lag(visit_detail_id, 1) over(partition BY person_id ORDER BY visit_detail_start_date) AS prev_id FROM {sc}._chunk ch JOIN {sc}.visit_detail on ch.patient_id = person_id WHERE ch.ordinal = {ch} ORDER BY person_id, visit_detail_start_date asc)
UPDATE {sc}.visit_detail v SET preceding_visit_detail_id = d.prev_id, parent_visit_detail_id = d.prev_id FROM vdetails d WHERE v.visit_detail_id=d.visit_detail_id;