-- {sc}.concept definition

-- Drop table

-- DROP TABLE {sc}.concept;

CREATE TABLE IF NOT EXISTS {sc}.concept (
	concept_id int4 NOT NULL,
	concept_name text NOT NULL,
	domain_id varchar(20) NOT NULL,
	vocabulary_id varchar(20) NOT NULL,
	concept_class_id varchar(20) NOT NULL,
	standard_concept varchar(1) NULL,
	concept_code varchar(50) NOT NULL,
	valid_start_date date NOT NULL,
	valid_end_date date NOT NULL,
	invalid_reason varchar(1) NULL,
	CONSTRAINT xpk_concept PRIMARY KEY (concept_id)
);


-- {sc}.concept_ancestor definition

-- Drop table

-- DROP TABLE {sc}.concept_ancestor;

CREATE TABLE IF NOT EXISTS {sc}.concept_ancestor (
	ancestor_concept_id int4 NOT NULL,
	descendant_concept_id int4 NOT NULL,
	min_levels_of_separation int4 NOT NULL,
	max_levels_of_separation int4 NOT NULL,
	CONSTRAINT xpk_concept_ancestor PRIMARY KEY (ancestor_concept_id, descendant_concept_id)
);


-- {sc}.concept_class definition

-- Drop table

-- DROP TABLE {sc}.concept_class;

CREATE TABLE IF NOT EXISTS {sc}.concept_class (
	concept_class_id varchar(20) NOT NULL,
	concept_class_name varchar(255) NOT NULL,
	concept_class_concept_id int4 NOT NULL,
	CONSTRAINT xpk_concept_class PRIMARY KEY (concept_class_id)
);


-- {sc}.concept_relationship definition

-- Drop table

-- DROP TABLE {sc}.concept_relationship;

CREATE TABLE IF NOT EXISTS {sc}.concept_relationship (
	concept_id_1 int4 NOT NULL,
	concept_id_2 int4 NOT NULL,
	relationship_id varchar(20) NOT NULL,
	valid_start_date date NOT NULL,
	valid_end_date date NOT NULL,
	invalid_reason varchar(1) NULL,
	CONSTRAINT xpk_concept_relationship PRIMARY KEY (concept_id_1, concept_id_2, relationship_id)
);


-- {sc}.concept_synonym definition

-- Drop table

-- DROP TABLE {sc}.concept_synonym;

CREATE TABLE IF NOT EXISTS {sc}.concept_synonym (
	id bigserial NOT NULL,
	concept_id int4 NOT NULL,
	concept_synonym_name varchar(1000) NOT NULL,
	language_concept_id int4 NOT NULL,
	CONSTRAINT concept_synonym_pkey PRIMARY KEY (id)
);


-- {sc}."domain" definition

-- Drop table

-- DROP TABLE {sc}."domain";

CREATE TABLE IF NOT EXISTS {sc}."domain" (
	domain_id varchar(20) NOT NULL,
	domain_name varchar(255) NOT NULL,
	domain_concept_id int4 NOT NULL,
	CONSTRAINT xpk_domain PRIMARY KEY (domain_id)
);


-- {sc}.drug_strength definition

-- Drop table

-- DROP TABLE {sc}.drug_strength;

CREATE TABLE IF NOT EXISTS {sc}.drug_strength (
	drug_concept_id int4 NOT NULL,
	ingredient_concept_id int4 NOT NULL,
	amount_value numeric NULL,
	amount_unit_concept_id int4 NULL,
	numerator_value numeric NULL,
	numerator_unit_concept_id int4 NULL,
	denominator_value numeric NULL,
	denominator_unit_concept_id int4 NULL,
	box_size int4 NULL,
	valid_start_date date NOT NULL,
	valid_end_date date NOT NULL,
	invalid_reason varchar(1) NULL,
	CONSTRAINT xpk_drug_strength PRIMARY KEY (drug_concept_id, ingredient_concept_id)
);


-- {sc}.relationship definition

-- Drop table

-- DROP TABLE {sc}.relationship;

CREATE TABLE IF NOT EXISTS {sc}.relationship (
	relationship_id varchar(20) NOT NULL,
	relationship_name varchar(255) NOT NULL,
	is_hierarchical varchar(1) NOT NULL,
	defines_ancestry varchar(1) NOT NULL,
	reverse_relationship_id varchar(20) NOT NULL,
	relationship_concept_id int4 NOT NULL,
	CONSTRAINT xpk_relationship PRIMARY KEY (relationship_id)
);


-- {sc}.vocabulary definition

-- Drop table

-- DROP TABLE {sc}.vocabulary;

CREATE TABLE IF NOT EXISTS {sc}.vocabulary (
	vocabulary_id varchar(20) NOT NULL,
	vocabulary_name varchar(255) NOT NULL,
	vocabulary_reference varchar(255) NOT NULL,
	vocabulary_version varchar(255) NULL,
	vocabulary_concept_id int4 NOT NULL,
	CONSTRAINT xpk_vocabulary PRIMARY KEY (vocabulary_id)
);