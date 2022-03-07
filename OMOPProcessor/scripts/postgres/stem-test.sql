/** StemTest #{ch}**/
insert into {sc}.stem_table 
(domain_id, person_id, visit_occurrence_id, provider_id, start_datetime, concept_id, source_value, source_concept_id, type_concept_id, 
start_date, operator_concept_id, unit_concept_id, unit_source_value, end_date, sig, range_high, range_low, value_as_number, value_as_concept_id, 
value_source_value, value_as_string)
SELECT
CASE WHEN cn.concept_id IS NULL OR 0 = cn.concept_id THEN 'Observation' else cn.domain_id END AS domain_id, 
ti.patid person_id, vo.visit_occurrence_id, ti.staffid provider_id, ti.eventdate::timestamp AS start_datetime, 
st.source_concept_id concept_id, ti.read_code source_value, ss.source_concept_id, 32856 type_concept_id, ti.eventdate start_date, 
CASE ti.operator WHEN '<=' THEN 4171754 WHEN '>=' THEN 4171755 WHEN '<' THEN 4171756 WHEN '=' THEN 4172703 WHEN '>' THEN 4172704 END operator_concept_id, 
cu.concept_id unit_concept_id,ti.unit unit_source_value,
null end_date, NULL sig, ti.range_high, ti.range_low, ti.value_as_number, cv.concept_id value_as_concept_id, ti.value_as_concept_id value_source_value, NULL value_as_string
FROM {sc}._chunk JOIN {sc}.test_int ti on patient_id = ti.patid
JOIN {sc}.visit_occurrence vo ON vo.person_id = ti.patid AND vo.visit_start_date = ti.eventdate
JOIN {sc}.source_to_standard st ON st.source_code = ti.read_code AND st.source_vocabulary_id = 'JNJ_CPRD_TEST_ENT' AND st.target_standard_concept = 'S' and st.target_invalid_reason is NULL
LEFT JOIN {vs}.concept cn ON cn.concept_code = ti.read_code AND cn.standard_concept = 'S' and cn.invalid_reason is NULL
LEFT JOIN {sc}.source_to_source ss ON ss.source_code = ti.read_code and ss.source_vocabulary_id='Read'
LEFT JOIN {vs}.concept cu ON cu.concept_code = ti.unit AND cu.vocabulary_id = 'UCUM' AND cu.standard_concept = 'S' and cu.invalid_reason is NULL
LEFT JOIN {vs}.concept cv ON cv.concept_name = ti.value_as_concept_id AND cv.domain_id IN ('Meas Value') AND cv.standard_concept = 'S' AND cv.invalid_reason IS NULL		
WHERE ordinal={ch}