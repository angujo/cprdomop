/** StemTest **/
WITH concs AS (SELECT concept_id, domain_id, concept_name, concept_code, vocabulary_id FROM {vs}.concept WHERE standard_concept = 'S' and invalid_reason is NULL and (domain_id IN ('Meas Value','Meas Value Operator') OR vocabulary_id IN ('UCUM','LOINC')))
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
JOIN {sc}.visit_occurrence vo ON vo.person_id = ti.patid AND vo.visit_start_date = ti.eventdate
LEFT JOIN concs cn ON cn.concept_code = ti.read_code
LEFT JOIN concs cop ON ti.operator = cop.concept_name AND cop.domain_id IN ('Meas Value Operator')
LEFT JOIN {sc}.source_to_source ss ON ss.source_code = ti.read_code and ss.source_vocabulary_id='Read'
LEFT JOIN {sc}.source_to_standard st ON st.source_code = ti.read_code AND st.source_vocabulary_id = 'JNJ_CPRD_TEST_ENT' AND st.target_standard_concept = 'S' and st.target_invalid_reason is NULL
LEFT JOIN concs cu ON cu.concept_code = ti.unit AND cu.vocabulary_id = 'UCUM'
LEFT JOIN concs cv ON cv.concept_name = ti.value_as_concept_id AND cv.domain_id IN ('Meas Value')		
WHERE ordinal={ch}