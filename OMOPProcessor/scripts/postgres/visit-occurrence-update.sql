/** VisitDetailOccurrenceUpdate */
-- populate the preceding entries
WITH vodetails AS (
	SELECT visit_occurrence_id, visit_detail_start_date, lag(visit_occurrence_id, 1) over(partition BY person_id ORDER BY visit_detail_start_date) AS prev_id 
	FROM {sc}.visit_detail ORDER BY person_id, visit_detail_start_date asc
	)
UPDATE {sc}.visit_occurrence SET preceding_visit_occurrence_id = d.prev_id FROM vodetails d WHERE visit_occurrence.visit_occurrence_id=d.visit_occurrence_id;
