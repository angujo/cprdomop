CREATE INDEX idx_concept_class_id ON {sc}.concept USING btree (concept_class_id);

CREATE INDEX idx_concept_code ON {sc}.concept USING btree (concept_code);

CREATE UNIQUE INDEX idx_concept_concept_id ON {sc}.concept USING btree (concept_id);

CREATE INDEX idx_concept_domain_id ON {sc}.concept USING btree (domain_id);

CREATE INDEX idx_concept_id_varchar ON {sc}.concept USING btree (((concept_id)::character varying));

CREATE INDEX idx_concept_vocabluary_id ON {sc}.concept USING btree (vocabulary_id);

CREATE UNIQUE INDEX idx_xpk_concept ON {sc}.concept USING btree (concept_id);

CREATE INDEX idx_concept_ancestor_id_1 ON {sc}.concept_ancestor USING btree (ancestor_concept_id);

CREATE INDEX idx_concept_ancestor_id_2 ON {sc}.concept_ancestor USING btree (descendant_concept_id);

CREATE UNIQUE INDEX idx_xpk_concept_ancestor ON {sc}.concept_ancestor USING btree (ancestor_concept_id, descendant_concept_id);

CREATE UNIQUE INDEX idx_concept_class_class_id ON {sc}.concept_class USING btree (concept_class_id);

CREATE UNIQUE INDEX idx_xpk_concept_class ON {sc}.concept_class USING btree (concept_class_id);

CREATE INDEX idx_concept_relationship_id_1 ON {sc}.concept_relationship USING btree (concept_id_1);

CREATE INDEX idx_concept_relationship_id_2 ON {sc}.concept_relationship USING btree (concept_id_2);

CREATE INDEX idx_concept_relationship_id_3 ON {sc}.concept_relationship USING btree (relationship_id);

CREATE UNIQUE INDEX idx_xpk_concept_relationship ON {sc}.concept_relationship USING btree (concept_id_1, concept_id_2, relationship_id);

CREATE INDEX idx_concept_synonym_id ON {sc}.concept_synonym USING btree (concept_id);

CREATE UNIQUE INDEX idx_domain_domain_id ON {sc}.domain USING btree (domain_id);

CREATE UNIQUE INDEX idx_xpk_domain ON {sc}.domain USING btree (domain_id);

CREATE INDEX idx_drug_strength_id_1 ON {sc}.drug_strength USING btree (drug_concept_id);

CREATE INDEX idx_drug_strength_id_2 ON {sc}.drug_strength USING btree (ingredient_concept_id);

CREATE UNIQUE INDEX idx_xpk_drug_strength ON {sc}.drug_strength USING btree (drug_concept_id, ingredient_concept_id);

CREATE UNIQUE INDEX idx_relationship_rel_id ON {sc}.relationship USING btree (relationship_id);

CREATE UNIQUE INDEX idx_xpk_relationship ON {sc}.relationship USING btree (relationship_id);

CREATE UNIQUE INDEX idx_vocabulary_vocabulary_id ON {sc}.vocabulary USING btree (vocabulary_id);

CREATE UNIQUE INDEX idx_xpk_vocabulary ON {sc}.vocabulary USING btree (vocabulary_id);
