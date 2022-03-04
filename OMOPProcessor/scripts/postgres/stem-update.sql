/** StemUpdate **/
with sstandard AS (
	select 
			max(case source_vocabulary_id when 'Read' then source_concept_id end) read_source_concept_id,
			max(case source_vocabulary_id when 'Gemscript' then source_concept_id end) gems_source_concept_id,
			source_code 
			from target.source_to_standard sd WHERE sd.source_vocabulary_id in ('Read', 'Gemscript') AND sd.target_standard_concept = 'S' and sd.target_invalid_reason is NULL
			group by source_code
),
upd_measurement As (
	UPDATE measurement 
	set value_as_concept_id = case when value_as_concept_id = 'Read' then read_source_concept_id when 'Gemscript' = value_as_concept_id then gems_source_concept_id when true=textregexeq(value_as_concept_id,'^[[:digit:]]+$') then value_as_concept_id end
	from sstandard ss 
	-- join concept cn ON measurement.
	where measurement.value_as_concept_id is not null and measurement.measurement_source_value = ss.source_code
)
UPDATE observation 
set value_as_concept_id = case when value_as_concept_id = 'Read' then read_source_concept_id when 'Gemscript' = value_as_concept_id then gems_source_concept_id when true=textregexeq(value_as_concept_id,'^[[:digit:]]+$') then value_as_concept_id end
from sstandard ss 
where observation.value_as_concept_id is not null and observation.observation_source_value = ss.source_code;