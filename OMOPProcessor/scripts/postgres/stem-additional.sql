/** StemAdditional */
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
sd.source_concept_id AS concept_id, 
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
null value_as_concept_id,
ad.qualifier_source_value value_source_value,
ad.value_as_string
FROM {sc}._chunk JOIN {sc}.add_in ad on patient_id = ad.patid
JOIN {sc}.visit_occurrence vo ON vo.person_id = ad.patid AND vo.visit_start_date = ad.eventdate
LEFT JOIN {vs}.concept cn ON cn.concept_code = ad.source_value
left join {sc}.source_to_standard sd ON sd.source_code = ad.source_value AND sd.source_vocabulary_id in ('JNJ_CPRD_ADD_ENTTYPE') AND sd.target_standard_concept = 'S' and sd.target_invalid_reason is NULL
LEFT JOIN {vs}.concept cu ON cu.concept_code = ad.unit_source_value AND cu.vocabulary_id = 'UCUM' and cu.standard_concept = 'S' and cu.invalid_reason is NULL
WHERE ordinal={ch}