WITH voccur AS (SELECT person_id, visit_start_date, visit_occurrence_id FROM {sc}._chunk join {sc}.visit_occurrence on patient_id = person_id where ordinal = {ch}),
	ssource AS (SELECT source_concept_id, source_code, source_vocabulary_id, source_valid_start_date, source_valid_end_date FROM {sc}.source_to_source WHERE source_vocabulary_id in ('Read','gemscript','Gemscript')),
	sstandard AS (SELECT source_concept_id, source_code, source_vocabulary_id, source_domain_id, source_valid_start_date, source_valid_end_date FROM {sc}.source_to_standard WHERE source_vocabulary_id in ('Read', 'JNJ_CPRD_TEST_ENT', 'JNJ_CPRD_ADD_ENTTYPE','gemscript','Gemscript', 'LOINC') AND target_standard_concept = 'S' and target_invalid_reason is NULL),
	concs AS (SELECT concept_id, domain_id, concept_name, concept_code, vocabulary_id FROM {vs}.concept WHERE standard_concept = 'S' and invalid_reason is NULL and (domain_id IN ('Meas Value','Meas Value Operator') OR vocabulary_id IN ('UCUM','LOINC'))),
	meds AS (SELECT medcode, read_code FROM {ss}.medical),
	
	i_clinicals AS (
		insert into {sc}.stem_table 
		(domain_id, person_id, visit_occurrence_id, provider_id, start_datetime, concept_id, source_value, source_concept_id, type_concept_id, 
		start_date, operator_concept_id, unit_concept_id, unit_source_value, end_date, sig, range_high, range_low, value_as_number, value_as_concept_id, 
		value_source_value, value_as_string)
		SELECT
		CASE WHEN cn.concept_id IS NULL OR 0 = cn.concept_id THEN 'Observation' else cn.domain_id END AS domain_id, 
		c.patid person_id, vo.visit_occurrence_id, c.staffid provider_id, c.eventdate::timestamp start_datetime, 
		st.source_concept_id concept_id, m.read_code source_value, ss.source_concept_id, 32827 type_concept_id, c.eventdate start_date,
		NULL operator_concept_id,NULL unit_concept_id,NULL unit_source_value, null end_date, NULL sig, 
		NULL range_high, NULL range_low, NULL value_as_number, null value_as_concept_id, null value_source_value, NULL value_as_string
		FROM {sc}._chunk JOIN {ss}.clinical c on patient_id = c.patid
		JOIN meds m ON c.medcode = m.medcode
		JOIN voccur vo ON vo.person_id = c.patid AND vo.visit_start_date = c.eventdate
		LEFT JOIN concs cn ON cn.concept_code = m.read_code
		LEFT JOIN ssource ss ON ss.source_code = m.read_code
		LEFT JOIN sstandard st ON st.source_code = m.read_code AND st.source_vocabulary_id = 'Read'
		WHERE ordinal={ch}
	),
	
	i_referrals AS (
		insert into {sc}.stem_table 
		(domain_id, person_id, visit_occurrence_id, provider_id, start_datetime, concept_id, source_value, source_concept_id, type_concept_id, 
		start_date, operator_concept_id, unit_concept_id, unit_source_value, end_date, sig, range_high, range_low, value_as_number, value_as_concept_id, 
		value_source_value, value_as_string)
		SELECT
		CASE WHEN cn.concept_id IS NULL OR 0 = cn.concept_id THEN 'Observation' else cn.domain_id END AS domain_id, 
		r.patid person_id, vo.visit_occurrence_id, r.staffid provider_id, r.eventdate::timestamp start_datetime, 
		st.source_concept_id concept_id, m.read_code source_value, ss.source_concept_id, 32842 type_concept_id, r.eventdate start_date,
		NULL operator_concept_id,NULL unit_concept_id,NULL unit_source_value, null end_date, NULL sig, 
		NULL range_high, NULL range_low, NULL value_as_number, null value_as_concept_id, null value_source_value, NULL value_as_string
		FROM {sc}._chunk JOIN {ss}.referral r on patient_id = r.patid
		JOIN meds m ON r.medcode = m.medcode
		JOIN voccur vo ON vo.person_id = r.patid AND vo.visit_start_date = r.eventdate
		LEFT JOIN concs cn ON cn.concept_code = m.read_code
		LEFT JOIN ssource ss ON ss.source_code = m.read_code
		LEFT JOIN sstandard st ON st.source_code = m.read_code AND st.source_vocabulary_id = 'Read'
		WHERE ordinal={ch}
	),
	
	i_immunes AS (
		insert into {sc}.stem_table 
		(domain_id, person_id, visit_occurrence_id, provider_id, start_datetime, concept_id, source_value, source_concept_id, type_concept_id, 
		start_date, operator_concept_id, unit_concept_id, unit_source_value, end_date, sig, range_high, range_low, value_as_number, value_as_concept_id, 
		value_source_value, value_as_string)
		SELECT
		CASE WHEN cn.concept_id IS NULL OR 0 = cn.concept_id THEN 'Observation' else cn.domain_id END AS domain_id,  
		r.patid person_id, vo.visit_occurrence_id, r.staffid provider_id, r.eventdate::timestamp start_datetime, 
		st.source_concept_id concept_id, m.read_code source_value, ss.source_concept_id, 32827 type_concept_id, r.eventdate start_date,
		NULL operator_concept_id,NULL unit_concept_id,NULL unit_source_value, null end_date, NULL sig, 
		NULL range_high, NULL range_low, NULL value_as_number, null value_as_concept_id, null value_source_value, NULL value_as_string
		FROM {sc}._chunk JOIN {ss}.immunisation r on patient_id = r.patid
		JOIN meds m ON r.medcode = m.medcode
		JOIN voccur vo ON vo.person_id = r.patid AND vo.visit_start_date = r.eventdate
		LEFT JOIN concs cn ON cn.concept_code = m.read_code
		LEFT JOIN ssource ss ON ss.source_code = m.read_code
		LEFT JOIN sstandard st ON st.source_code = m.read_code AND st.source_vocabulary_id = 'Read'
		WHERE ordinal={ch}
	),
	
	i_tests AS (
		insert into {sc}.stem_table 
		(domain_id, person_id, visit_occurrence_id, provider_id, start_datetime, concept_id, source_value, source_concept_id, type_concept_id, 
		start_date, operator_concept_id, unit_concept_id, unit_source_value, end_date, sig, range_high, range_low, value_as_number, value_as_concept_id, 
		value_source_value, value_as_string)
		SELECT
		CASE WHEN cn.concept_id IS NULL OR 0 = cn.concept_id THEN 'Observation' else cn.domain_id END AS domain_id, 
		ti.patid person_id, vo.visit_occurrence_id, ti.staffid provider_id, ti.eventdate::timestamp AS start_datetime, 
		st.source_concept_id concept_id, ti.read_code source_value, ss.source_concept_id, 32856 type_concept_id, ti.eventdate start_date, 
		CASE WHEN cop.concept_id IS NULL THEN 0 ELSE cop.concept_id end operator_concept_id,	cu.concept_id unit_concept_id,ti.unit unit_source_value,
		null end_date, NULL sig, ti.range_high, ti.range_low, ti.value_as_number, cv.concept_id value_as_concept_id, ti.value_as_concept_id value_source_value, NULL value_as_string
		FROM {sc}._chunk JOIN {sc}.test_int ti on patient_id = ti.patid
		JOIN voccur vo ON vo.person_id = ti.patid AND vo.visit_start_date = ti.eventdate
		LEFT JOIN concs cn ON cn.concept_code = ti.read_code
		LEFT JOIN concs cop ON ti.operator = cop.concept_name AND cop.domain_id IN ('Meas Value Operator')
		LEFT JOIN ssource ss ON ss.source_code = ti.read_code
		LEFT JOIN sstandard st ON st.source_code = ti.read_code AND st.source_vocabulary_id = 'JNJ_CPRD_TEST_ENT'
		LEFT JOIN concs cu ON cu.concept_code = ti.unit AND cu.vocabulary_id = 'UCUM'
		LEFT JOIN concs cv ON cv.concept_name = ti.value_as_concept_id AND cv.domain_id IN ('Meas Value')		
		WHERE ordinal={ch}
	),
	
	i_addins AS (
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
			when ad.qualifier_source_value IS not null THEN (SELECT concept_id FROM concs WHERE source_code = ad.qualifier_source_value AND domain_id = 'Meas Value' AND vocabulary_id = 'LOINC' LIMIT 1)
		END value_as_concept_id,
		ad.qualifier_source_value value_source_value,
		ad.value_as_string
		FROM {sc}._chunk JOIN {sc}.add_in ad on patient_id = ad.patid
		JOIN voccur vo ON vo.person_id = ad.patid AND vo.visit_start_date = ad.eventdate
		LEFT JOIN concs cn ON cn.concept_code = ad.source_value
		LEFT JOIN sstandard st ON st.source_code = ad.source_value AND st.source_vocabulary_id = 'JNJ_CPRD_ADD_ENTTYPE'
		LEFT JOIN concs cu ON cu.concept_code = ad.unit_source_value AND cu.vocabulary_id = 'UCUM'
		WHERE ordinal={ch}
	)
insert into {sc}.stem_table 
(domain_id, person_id, visit_occurrence_id, provider_id, start_datetime, concept_id, source_value, source_concept_id, type_concept_id, 
start_date, operator_concept_id, unit_concept_id, unit_source_value, end_date, sig, range_high, range_low, value_as_number, value_as_concept_id, 
value_source_value, value_as_string)
SELECT
CASE WHEN cn.concept_id IS NULL OR 0 = cn.concept_id THEN 'Observation' else cn.domain_id END AS domain_id,  
r.patid person_id, vo.visit_occurrence_id, r.staffid provider_id, r.eventdate::timestamp start_datetime, 
st.source_concept_id concept_id, m.gemscriptcode source_value, ss.source_concept_id, 32838 type_concept_id, r.eventdate start_date,
NULL operator_concept_id,NULL unit_concept_id,NULL unit_source_value, 
r.eventdate::date + coalesce(case when r.numdays = 0 or r.numdays > 365 then null else r.numdays end, dd.numdays, dm.numdays, 1) end_date, cd.dosage_text sig, 
NULL range_high, NULL range_low, NULL value_as_number, null value_as_concept_id, null value_source_value, NULL value_as_string
FROM {sc}._chunk JOIN {ss}.therapy r on patient_id = r.patid
JOIN {ss}.product m ON r.prodcode = m.prodcode
JOIN voccur vo ON vo.person_id = r.patid AND vo.visit_start_date = r.eventdate
JOIN {ss}.commondosages cd ON cd.dosageid = r.dosageid
LEFT JOIN concs cn ON cn.concept_code = m.gemscriptcode
LEFT JOIN ssource ss ON ss.source_code = m.gemscriptcode AND ss.source_vocabulary_id = 'gemscript' AND r.eventdate between ss.source_valid_start_date and ss.source_valid_end_date
LEFT JOIN sstandard st ON st.source_code = m.gemscriptcode AND st.source_vocabulary_id = 'gemscript' AND r.eventdate between st.source_valid_start_date and st.source_valid_end_date
LEFT join {ss}.daysupply_decodes dd on r.prodcode = dd.prodcode and dd.daily_dose = coalesce(cd.daily_dose, 0) and dd.qty = coalesce(case when r.qty < 0 then null else r.qty end, 0) and dd.numpacks = coalesce(r.numpacks, 0)
left join {ss}.daysupply_modes dm on r.prodcode = dm.prodcode
		WHERE ordinal={ch};