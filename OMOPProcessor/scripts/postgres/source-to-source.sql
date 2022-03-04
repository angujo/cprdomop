/** SourceToSource */
INSERT INTO {sc}.source_to_source
(source_code, source_concept_id, source_code_description, source_vocabulary_id, source_domain_id, source_concept_class_id, 
source_valid_start_date, source_valid_end_date, source_invalid_reason, target_concept_id, target_concept_name, 
target_vocabulary_id, target_domain_id, target_concept_class_id, target_invalid_reason, target_standard_concept)
(
	WITH CONC AS (SELECT domain_id, valid_start_date,valid_end_date, concept_name, concept_class_id, invalid_reason, standard_concept, concept_id, concept_code, vocabulary_id FROM {vs}.concept),
	CTE_VOCAB_MAP AS (
	       SELECT c.concept_code AS source_code, c.concept_id AS source_concept_id, c.CONCEPT_NAME AS source_code_description, 
				c.vocabulary_id AS source_vocabulary_id, c.domain_id AS source_domain_id, c.concept_class_id AS source_concept_class_id, 
	            c.VALID_START_DATE AS source_valid_start_date, c.VALID_END_DATE AS source_valid_end_date, c.invalid_reason AS source_invalid_reason, 
	            c.concept_ID as target_concept_id, c.concept_name AS target_concept_name, c.vocabulary_id AS target_vocabulary_id, c.domain_id AS target_domain_id, 
				c.concept_class_id AS target_concept_class_id, c.INVALID_REASON AS target_invalid_reason, 
	            c.STANDARD_CONCEPT AS target_standard_concept
	       FROM CONC c
	       UNION
	       SELECT source_code, source_concept_id, source_code_description, source_vocabulary_id, c1.domain_id AS source_domain_id, c2.CONCEPT_CLASS_ID AS source_concept_class_id, 
				c1.VALID_START_DATE AS source_valid_start_date, c1.VALID_END_DATE AS source_valid_end_date,stcm.INVALID_REASON AS source_invalid_reason,
				target_concept_id, c2.CONCEPT_NAME AS target_concept_name, target_vocabulary_id, c2.domain_id AS target_domain_id, c2.concept_class_id AS target_concept_class_id, 
				c2.INVALID_REASON AS target_invalid_reason, c2.standard_concept AS target_standard_concept
	       FROM {ss}.source_to_concept_map stcm
	              LEFT OUTER JOIN CONC c1 ON c1.concept_id = stcm.source_concept_id
	              LEFT OUTER JOIN CONC c2 ON c2.CONCEPT_ID = stcm.target_concept_id
	       WHERE stcm.INVALID_REASON IS NULL
	)
	SELECT * FROM CTE_VOCAB_MAP
);


CREATE INDEX idx_source_vocab_map_source_code ON {sc}.source_to_source USING btree (source_code);
CREATE INDEX idx_source_vocab_map_source_vocab_id ON {sc}.source_to_source USING btree (source_vocabulary_id);