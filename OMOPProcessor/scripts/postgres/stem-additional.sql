/** StemAdditional */
WITH concs AS (SELECT concept_id, domain_id, concept_name, concept_code, vocabulary_id 
		FROM {vs}.concept WHERE standard_concept = 'S' and invalid_reason is NULL and vocabulary_id IN ('UCUM','LOINC')),
	sstandard AS (SELECT source_concept_id, source_code, source_vocabulary_id, source_domain_id, source_valid_start_date, source_valid_end_date
		FROM {sc}.source_to_standard WHERE source_vocabulary_id in ('Read', 'JNJ_CPRD_ADD_ENTTYPE','Gemscript') AND target_standard_concept = 'S' and target_invalid_reason is NULL)
		insert into {sc}.stem_table 
		(domain_id, person_id, visit_occurrence_id, provider_id, start_datetime, concept_id, source_value, source_concept_id, type_concept_id, 
		start_date, operator_concept_id, unit_concept_id, unit_source_value, end_date, sig, range_high, range_low, value_as_number, value_as_concept_id, 
		value_source_value, value_as_string)
		SELECT 
		CASE WHEN cn.concept_id IS NULL OR 0 = cn.concept_id THEN 'Observation' else cn.domain_id END AS domain_id, 
		ad.patid person_id,
		vo.visit_occurrence_id,
		ad.staffid provider_id,
		ad.eventdate::timestamp start_datetime,
		st.source_concept_id AS concept_id, 
		ad.source_value,
		0 source_concept_id,
		32851 AS type_concept_id, 
		ad.eventdate start_date,
		NULL operator_concept_id,
		cu.concept_id unit_concept_id,
		ad.unit_source_value,
		ad.eventdate end_date,
		NULL sig, NULL range_high, NULL range_low,
		ad.value_as_number,
		CASE 
			when ad."data" = 'Read code for condition' THEN (SELECT source_concept_id FROM sstandard WHERE source_code = ad.value_as_string AND source_vocabulary_id = 'Read' LIMIT 1)
			when ad."data" = 'Drug code' THEN (SELECT source_concept_id FROM sstandard WHERE source_code = ad.value_as_string AND source_vocabulary_id = 'Gemscript' LIMIT 1)
			when ad.qualifier_source_value IS not null AND source_code = ad.qualifier_source_value THEN (SELECT concept_id FROM concs WHERE domain_id = 'Meas Value' AND vocabulary_id = 'LOINC' LIMIT 1)
		END value_as_concept_id,
		ad.qualifier_source_value value_source_value,
		ad.value_as_string
		FROM {sc}._chunk JOIN {sc}.add_in ad on patient_id = ad.patid
		JOIN {sc}.visit_occurrence vo ON vo.person_id = ad.patid AND vo.visit_start_date = ad.eventdate
		LEFT JOIN {vs}.concept cn ON cn.concept_code = ad.source_value
		LEFT JOIN sstandard st ON st.source_code = ad.source_value AND st.source_vocabulary_id = 'JNJ_CPRD_ADD_ENTTYPE' 
		LEFT JOIN concs cu ON cu.concept_code = ad.unit_source_value AND cu.vocabulary_id = 'UCUM'
		WHERE ordinal={ch}