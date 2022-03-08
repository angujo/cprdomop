/** StemImmunization  #{ch}*/
insert into {sc}.stem_table 
		(domain_id, person_id, visit_occurrence_id, provider_id, start_datetime, concept_id, source_value, source_concept_id, type_concept_id, 
		start_date, operator_concept_id, unit_concept_id, unit_source_value, end_date, sig, range_high, range_low, value_as_number, value_as_concept_id, 
		value_source_value, value_as_string)
		SELECT
		CASE WHEN cn.concept_id IS NULL OR 0 = cn.concept_id THEN 'Observation' else cn.domain_id END AS domain_id,  
		r.patid person_id, null visit_occurrence_id, r.staffid provider_id, r.eventdate::timestamp start_datetime, 
		st.source_concept_id concept_id, m.read_code source_value, ss.source_concept_id, 32827 type_concept_id, r.eventdate start_date,
		NULL operator_concept_id,NULL unit_concept_id,NULL unit_source_value, null end_date, NULL sig, 
		NULL range_high, NULL range_low, NULL value_as_number, null value_as_concept_id, null value_source_value, NULL value_as_string
		FROM {sc}._chunk JOIN {ss}.immunisation r on patient_id = r.patid
		JOIN {ss}.medical m ON r.medcode = m.medcode
		JOIN {sc}.source_to_standard st ON st.source_code = m.read_code AND st.source_vocabulary_id = 'Read' and st.target_standard_concept='S' and st.target_invalid_reason IS NULL
		LEFT JOIN {vs}.concept cn ON cn.standard_concept = 'S' and cn.invalid_reason is NULL and cn.concept_code = m.read_code
		LEFT JOIN {sc}.source_to_source ss ON ss.source_code = m.read_code AND ss.source_vocabulary_id='Read'
		WHERE ordinal={ch}