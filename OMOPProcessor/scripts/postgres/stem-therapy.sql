/** StemTherapy **/
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
JOIN {sc}.visit_occurrence vo ON vo.person_id = r.patid AND vo.visit_start_date = r.eventdate
JOIN {ss}.commondosages cd ON cd.dosageid = r.dosageid
LEFT JOIN {vs}.concept cn ON standard_concept = 'S' and invalid_reason is NULL and cn.concept_code = m.gemscriptcode
LEFT JOIN {sc}.source_to_source ss ON ss.source_code = m.gemscriptcode AND ss.source_vocabulary_id = 'gemscript'
LEFT JOIN {sc}.source_to_standard st ON st.source_code = m.gemscriptcode AND st.source_vocabulary_id = 'gemscript' AND st.target_standard_concept = 'S' and st.target_invalid_reason is NULL
LEFT join {ss}.daysupply_decodes dd on r.prodcode = dd.prodcode and dd.daily_dose = coalesce(cd.daily_dose, 0) and dd.qty = coalesce(case when r.qty < 0 then null else r.qty end, 0) and dd.numpacks = coalesce(r.numpacks, 0)
left join {ss}.daysupply_modes dm on r.prodcode = dm.prodcode
WHERE ordinal={ch} AND r.eventdate between ss.source_valid_start_date and ss.source_valid_end_date AND r.eventdate between st.source_valid_start_date and st.source_valid_end_date;