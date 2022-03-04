/** SourceToStandard */
INSERT INTO {sc}.source_to_standard
(source_code, source_concept_id, source_code_description, source_vocabulary_id, source_domain_id, source_concept_class_id, source_valid_start_date, 
source_valid_end_date, source_invalid_reason, target_concept_id, target_concept_name, target_vocabulary_id, target_domain_id, target_concept_class_id, 
target_invalid_reason, target_standard_concept)
(
	WITH CONC AS (
		SELECT * FROM {vs}.concept WHERE invalid_reason IS NULL 
	),
	CTE_VOCAB_MAP AS (
       SELECT c.concept_code AS source_code, c.concept_id AS source_concept_id, c.concept_name AS source_code_description, c.vocabulary_id AS source_vocabulary_id, 
			c.domain_id AS source_domain_id, c.CONCEPT_CLASS_ID AS source_concept_class_id, 
			c.VALID_START_DATE AS source_valid_start_date, c.VALID_END_DATE AS source_valid_end_date, c.INVALID_REASON AS source_invalid_reason, 
			c1.concept_id AS target_concept_id, c1.concept_name AS target_concept_name, c1.VOCABULARY_ID AS target_vocabulary_id, c1.domain_id AS target_domain_id, c1.concept_class_id AS target_concept_class_id, 
			c1.INVALID_REASON AS target_invalid_reason, c1.standard_concept AS target_standard_concept
       FROM CONC C
		JOIN {vs}.CONCEPT_RELATIONSHIP CR ON C.CONCEPT_ID = CR.CONCEPT_ID_1 AND CR.invalid_reason IS NULL AND lower(cr.relationship_id) = 'maps to'
		JOIN CONC C1 ON CR.CONCEPT_ID_2 = C1.CONCEPT_ID AND C1.INVALID_REASON IS NULL
       UNION ALL
       SELECT source_code, source_concept_id, source_code_description, source_vocabulary_id, c1.domain_id AS source_domain_id, c2.CONCEPT_CLASS_ID AS source_concept_class_id,
			c1.VALID_START_DATE AS source_valid_start_date, c1.VALID_END_DATE AS source_valid_end_date, 
			stcm.INVALID_REASON AS source_invalid_reason,target_concept_id, c2.CONCEPT_NAME AS target_concept_name, target_vocabulary_id, c2.domain_id AS target_domain_id, c2.concept_class_id AS target_concept_class_id, 
			c2.INVALID_REASON AS target_invalid_reason, c2.standard_concept AS target_standard_concept
       FROM {ss}.source_to_concept_map stcm
		LEFT OUTER JOIN CONC c1 ON c1.concept_id = stcm.source_concept_id
		LEFT OUTER JOIN CONC c2 ON c2.CONCEPT_ID = stcm.target_concept_id
       WHERE stcm.INVALID_REASON IS NULL
	)
	SELECT * FROM CTE_VOCAB_MAP
);


CREATE INDEX idx_sstandard_source_code ON {sc}.source_to_standard USING btree (source_code);
CREATE INDEX idx_sstandard_source_vocab_id ON {sc}.source_to_standard USING btree (source_vocabulary_id);
CREATE INDEX idx_sstandard_target_inv_reason ON {sc}.source_to_standard USING btree (target_invalid_reason);
CREATE INDEX idx_sstandard_target_stconcept ON {sc}.source_to_standard USING btree (target_standard_concept);