/** CreateTables */
-- {sc}.add_in definition

-- Drop table

DROP TABLE IF EXISTS {sc}.add_in;

CREATE TABLE {sc}.add_in (
	patid int8 NULL,
	eventdate date NULL,
	constype int2 NULL,
	consid int4 NULL,
	staffid int8 NULL,
	enttype int4 NULL,
	category varchar(30) NULL,
	description varchar(60) NULL,
	"data" varchar(50) NULL,
	value_as_number varchar NULL,
	value_as_string varchar NULL,
	value_as_date varchar NULL,
	unit_source_value text NULL,
	qualifier_source_value text NULL,
	read_code_description varchar(100) NULL,
	gemscript_description varchar(500) NULL,
	data_fields int2 NULL,
	source_value text NULL
);
-- {sc}.test_int definition

-- Drop table

DROP TABLE IF EXISTS {sc}.test_int;

CREATE TABLE {sc}.test_int (
	patid int8 NULL,
	eventdate date NULL,
	consid int4 NULL,
	staffid int8 NULL,
	read_code varchar(20) NULL,
	medcode int8 NULL,
	read_description varchar(650) NULL,
	map_value text NULL,
	enttype int4 NULL,
	enttype_desc varchar(800) NULL,
	data_fields int2 NULL,
	"operator" varchar NULL,
	value_as_number varchar(20) NULL,
	unit varchar NULL,
	value_as_concept_id varchar NULL,
	range_low varchar(20) NULL,
	range_high varchar(20) NULL
);
-- public.stem_table definition

-- Drop table

DROP TABLE IF EXISTS {sc}.stem_table;

CREATE TABLE IF NOT EXISTS {sc}.stem_table (
	id bigserial primary key,
	domain_id varchar NULL,
	person_id int8 NULL,
	visit_occurrence_id int8 NULL,
	provider_id int8 NULL,
	start_datetime timestamp NULL,
	concept_id int4 NULL,
	source_value varchar(450) NULL,
	source_concept_id int4 NULL,
	type_concept_id int4 NULL,
	start_date date NULL,
	operator_concept_id text NULL,
	unit_concept_id text NULL,
	unit_source_value text NULL,
	end_date text NULL,
	sig text NULL,
	range_high text NULL,
	range_low text NULL,
	value_as_number text NULL,
	value_as_concept_id text NULL,
	value_source_value text NULL,
	value_as_string text NULL
);

-- {sc}.attribute_definition definition

-- Drop table

DROP TABLE IF EXISTS {sc}.attribute_definition;

CREATE TABLE IF NOT EXISTS {sc}.attribute_definition (
	attribute_definition_id int8 NOT NULL,
	attribute_name varchar(255) NOT NULL,
	attribute_description text NULL,
	attribute_type_concept_id int4 NOT NULL,
	attribute_syntax text NULL
);


-- {sc}.care_site definition

-- Drop table

DROP TABLE IF EXISTS {sc}.care_site;

CREATE TABLE IF NOT EXISTS {sc}.care_site (
	care_site_id int8 NOT NULL,
	care_site_name varchar(255) NULL,
	place_of_service_concept_id int4 NULL,
	location_id int8 NULL,
	care_site_source_value varchar(50) NULL,
	place_of_service_source_value varchar(50) NULL
);


-- {sc}.cdm_domain_meta definition

-- Drop table

DROP TABLE IF EXISTS {sc}.cdm_domain_meta;

CREATE TABLE IF NOT EXISTS {sc}.cdm_domain_meta (
	domain_id varchar(20) NULL,
	description varchar(4000) NULL
);


-- {sc}.cdm_source definition

-- Drop table

DROP TABLE IF EXISTS {sc}.cdm_source;

CREATE TABLE IF NOT EXISTS {sc}.cdm_source (
	cdm_source_name varchar(255) NOT NULL,
	cdm_source_abbreviation varchar(25) NULL,
	cdm_holder varchar(255) NULL,
	source_description text NULL,
	source_documentation_reference varchar(255) NULL,
	cdm_etl_reference varchar(255) NULL,
	source_release_date date NULL,
	cdm_release_date date NULL,
	cdm_version varchar(10) NULL,
	vocabulary_version varchar(20) NULL
);


-- {sc}.cohort definition

-- Drop table

DROP TABLE IF EXISTS {sc}.cohort;

CREATE TABLE IF NOT EXISTS {sc}.cohort (
	cohort_definition_id int4 NOT NULL,
	subject_id int4 NOT NULL,
	cohort_start_date date NOT NULL,
	cohort_end_date date NOT NULL
);


-- {sc}.cohort_attribute definition

-- Drop table

DROP TABLE IF EXISTS {sc}.cohort_attribute;

CREATE TABLE IF NOT EXISTS {sc}.cohort_attribute (
	cohort_definition_id int4 NOT NULL,
	subject_id int4 NOT NULL,
	cohort_start_date date NOT NULL,
	cohort_end_date date NOT NULL,
	attribute_definition_id int4 NOT NULL,
	value_as_number numeric NULL,
	value_as_concept_id int4 NULL
);


-- {sc}.cohort_definition definition

-- Drop table

DROP TABLE IF EXISTS {sc}.cohort_definition;

CREATE TABLE IF NOT EXISTS {sc}.cohort_definition (
	cohort_definition_id int4 NOT NULL,
	cohort_definition_name varchar(255) NOT NULL,
	cohort_definition_description text NULL,
	definition_type_concept_id int4  NULL,
	cohort_definition_syntax text NULL,
	subject_concept_id int4  NULL,
	cohort_initiation_date date NULL
);


-- {sc}.condition_era definition

-- Drop table

DROP TABLE IF EXISTS {sc}.condition_era;

CREATE TABLE IF NOT EXISTS {sc}.condition_era (
	condition_era_id bigserial NOT NULL,
	person_id int8 NOT NULL,
	condition_concept_id int4 NOT NULL,
	condition_era_start_date date NOT NULL,
	condition_era_end_date date NOT NULL,
	condition_occurrence_count int4 NULL
);


-- {sc}.condition_occurrence definition

-- Drop table

DROP TABLE IF EXISTS {sc}.condition_occurrence;

CREATE TABLE IF NOT EXISTS {sc}.condition_occurrence (
	condition_occurrence_id int8 NOT NULL,
	person_id int8 NOT NULL,
	condition_concept_id int4 NOT NULL,
	condition_start_date date NOT NULL,
	condition_start_datetime timestamp NULL,
	condition_end_date date NULL,
	condition_end_datetime timestamp NULL,
	condition_type_concept_id int4 NOT NULL,
	stop_reason varchar(20) NULL,
	provider_id int8 NULL,
	visit_occurrence_id int8 NULL,
	visit_detail_id int8 NULL,
	condition_status_concept_id int4 NULL,
	condition_source_value varchar(450) NULL,
	condition_source_concept_id int4 NULL,
	condition_status_source_value varchar(50) NULL
);


-- {sc}."cost" definition

-- Drop table

DROP TABLE IF EXISTS {sc}."cost";

CREATE TABLE IF NOT EXISTS {sc}."cost" (
	cost_id int8 NOT NULL,
	cost_event_id int8 NOT NULL,
	cost_domain_id varchar(20) NOT NULL,
	cost_type_concept_id int4 NOT NULL,
	currency_concept_id int4 NULL,
	total_charge numeric NULL,
	total_cost numeric NULL,
	total_paid numeric NULL,
	paid_by_payer numeric NULL,
	paid_by_patient numeric NULL,
	paid_patient_copay numeric NULL,
	paid_patient_coinsurance numeric NULL,
	paid_patient_deductible numeric NULL,
	paid_by_primary numeric NULL,
	paid_ingredient_cost numeric NULL,
	paid_dispensing_fee numeric NULL,
	payer_plan_period_id int8 NULL,
	amount_allowed numeric NULL,
	revenue_code_concept_id int4 NULL,
	revenue_code_source_value varchar(50) NULL,
	drg_concept_id int4 NULL,
	drg_source_value varchar(3) NULL
);


-- {sc}.death definition

-- Drop table

DROP TABLE IF EXISTS {sc}.death;

CREATE TABLE IF NOT EXISTS {sc}.death (
	person_id int8 NOT NULL,
	death_date date NOT NULL,
	death_datetime timestamp NULL,
	death_type_concept_id int4 NOT NULL,
	cause_concept_id int4 NULL,
	cause_source_value varchar(50) NULL,
	cause_source_concept_id int4 NULL
);


-- {sc}.device_exposure definition

-- Drop table

DROP TABLE IF EXISTS {sc}.device_exposure;

CREATE TABLE IF NOT EXISTS {sc}.device_exposure (
	device_exposure_id int8 NOT NULL,
	person_id int8 NOT NULL,
	device_concept_id int4 NOT NULL,
	device_exposure_start_date date NOT NULL,
	device_exposure_start_datetime timestamp NULL,
	device_exposure_end_date date NULL,
	device_exposure_end_datetime timestamp NULL,
	device_type_concept_id int4 NOT NULL,
	unique_device_id varchar(50) NULL,
	quantity int4 NULL,
	provider_id int8 NULL,
	visit_occurrence_id int8 NULL,
	visit_detail_id int8 NULL,
	device_source_value varchar(100) NULL,
	device_source_concept_id int4 NULL
);


-- {sc}.dose_era definition

-- Drop table

DROP TABLE IF EXISTS {sc}.dose_era;

CREATE TABLE IF NOT EXISTS {sc}.dose_era (
	dose_era_id int8 NOT NULL,
	person_id int4 NOT NULL,
	drug_concept_id int4 NOT NULL,
	unit_concept_id int4 NOT NULL,
	dose_value numeric NOT NULL,
	dose_era_start_date date NOT NULL,
	dose_era_end_date date NOT NULL
);


-- {sc}.drug_era definition

-- Drop table

DROP TABLE IF EXISTS {sc}.drug_era;

CREATE TABLE IF NOT EXISTS {sc}.drug_era (
	drug_era_id serial8 NOT NULL,
	person_id int8 NOT NULL,
	drug_concept_id int4 NOT NULL,
	drug_era_start_date date NOT NULL,
	drug_era_end_date date NOT NULL,
	drug_exposure_count int4 NULL,
	gap_days int4 NULL
);


-- {sc}.drug_exposure definition

-- Drop table

DROP TABLE IF EXISTS {sc}.drug_exposure;

CREATE TABLE IF NOT EXISTS {sc}.drug_exposure (
	drug_exposure_id int8 NOT NULL,
	person_id int8 NOT NULL,
	drug_concept_id int4 NOT NULL,
	drug_exposure_start_date date NOT NULL,
	drug_exposure_start_datetime timestamp NULL,
	drug_exposure_end_date date NOT NULL,
	drug_exposure_end_datetime timestamp NULL,
	verbatim_end_date date NULL,
	drug_type_concept_id int4 NOT NULL,
	stop_reason varchar(20) NULL,
	refills int4 NULL,
	quantity numeric NULL,
	days_supply int4 NULL,
	sig text NULL,
	route_concept_id int4 NULL,
	lot_number varchar(50) NULL,
	provider_id int8 NULL,
	visit_occurrence_id int8 NULL,
	visit_detail_id int8 NULL,
	drug_source_value varchar(50) NULL,
	drug_source_concept_id int4 NULL,
	route_source_value varchar(50) NULL,
	dose_unit_source_value varchar(50) NULL
);


-- {sc}.fact_relationship definition

-- Drop table

DROP TABLE IF EXISTS {sc}.fact_relationship;

CREATE TABLE IF NOT EXISTS {sc}.fact_relationship (
	domain_concept_id_1 int4 NOT NULL,
	fact_id_1 int4 NOT NULL,
	domain_concept_id_2 int4 NOT NULL,
	fact_id_2 int4 NOT NULL,
	relationship_concept_id int4 NOT NULL
);


-- {sc}."location" definition

-- Drop table

DROP TABLE IF EXISTS {sc}."location";

CREATE TABLE IF NOT EXISTS {sc}."location" (
	location_id int8 NOT NULL,
	address_1 varchar(50) NULL,
	address_2 varchar(50) NULL,
	city varchar(50) NULL,
	state varchar(2) NULL,
	zip varchar(9) NULL,
	county varchar(20) NULL,
	location_source_value varchar(50) NULL
);


-- {sc}.measurement definition

-- Drop table

DROP TABLE IF EXISTS {sc}.measurement;

CREATE TABLE IF NOT EXISTS {sc}.measurement (
	measurement_id int8 NOT NULL,
	person_id int8 NOT NULL,
	measurement_concept_id int4 NOT NULL,
	measurement_date date NOT NULL,
	measurement_datetime timestamp NULL,
	measurement_time varchar(10) NULL,
	measurement_type_concept_id int4 NOT NULL,
	operator_concept_id int4 NULL,
	value_as_number numeric NULL,
	value_as_concept_id varchar(120) NULL,
	unit_concept_id int4 NULL,
	range_low numeric NULL,
	range_high numeric NULL,
	provider_id int8 NULL,
	visit_occurrence_id int8 NULL,
	visit_detail_id int8 NULL,
	measurement_source_value varchar(100) NULL,
	measurement_source_concept_id int4 NULL,
	unit_source_value varchar(50) NULL,
	value_source_value varchar(2500) NULL
);


-- {sc}.metadata definition

-- Drop table

DROP TABLE IF EXISTS {sc}.metadata;

CREATE TABLE IF NOT EXISTS {sc}.metadata (
	metadata_concept_id int4 NOT NULL,
	metadata_type_concept_id int4 NOT NULL,
	"name" varchar(250) NOT NULL,
	value_as_string text NULL,
	value_as_concept_id int4 NULL,
	metadata_date date NULL,
	metadata_datetime timestamp NULL
);


-- {sc}.metadata_tmp definition

-- Drop table

DROP TABLE IF EXISTS {sc}.metadata_tmp;

CREATE TABLE IF NOT EXISTS {sc}.metadata_tmp (
	person_id int8 NOT NULL,
	"name" varchar(250) NOT NULL
);


-- {sc}.note definition

-- Drop table

DROP TABLE IF EXISTS {sc}.note;

CREATE TABLE IF NOT EXISTS {sc}.note (
	note_id int8 NOT NULL,
	person_id int4 NOT NULL,
	note_date date NOT NULL,
	note_datetime timestamp NULL,
	note_type_concept_id int4 NOT NULL,
	note_class_concept_id int4 NOT NULL,
	note_title varchar(250) NULL,
	note_text text NULL,
	encoding_concept_id int4 NOT NULL,
	language_concept_id int4 NOT NULL,
	provider_id int4 NULL,
	visit_occurrence_id int8 NULL,
	visit_detail_id int4 NULL,
	note_source_value varchar(50) NULL
);


-- {sc}.note_nlp definition

-- Drop table

DROP TABLE IF EXISTS {sc}.note_nlp;

CREATE TABLE IF NOT EXISTS {sc}.note_nlp (
	note_nlp_id int8 NOT NULL,
	note_id int4 NOT NULL,
	section_concept_id int4 NULL,
	snippet varchar(250) NULL,
	"offset" varchar(250) NULL,
	lexical_variant varchar(250) NOT NULL,
	note_nlp_concept_id int4 NULL,
	note_nlp_source_concept_id int4 NULL,
	nlp_system varchar(250) NULL,
	nlp_date date NOT NULL,
	nlp_datetime timestamp NULL,
	term_exists varchar(1) NULL,
	term_temporal varchar(50) NULL,
	term_modifiers varchar(2000) NULL
);


-- {sc}.observation definition

-- Drop table

DROP TABLE IF EXISTS {sc}.observation;

CREATE TABLE IF NOT EXISTS {sc}.observation (
	observation_id int8 NOT NULL,
	person_id int8 NOT NULL,
	observation_concept_id int4 NULL,
	observation_date date NOT NULL,
	observation_datetime timestamp NULL,
	observation_type_concept_id int4 NOT NULL,
	value_as_number numeric NULL,
	value_as_string varchar(2000) NULL,
	value_as_concept_id varchar(120) NULL,
	qualifier_concept_id int4 NULL,
	unit_concept_id int4 NULL,
	provider_id int8 NULL,
	visit_occurrence_id int8 NULL,
	visit_detail_id int8 NULL,
	observation_source_value varchar(250) NULL,
	observation_source_concept_id int4 NULL,
	unit_source_value varchar(250) NULL,
	qualifier_source_value varchar(250) NULL
);


-- {sc}.observation_period definition

-- Drop table

DROP TABLE IF EXISTS {sc}.observation_period;

CREATE TABLE IF NOT EXISTS {sc}.observation_period (
	observation_period_id serial8 NOT NULL,
	person_id int8 NOT NULL,
	observation_period_start_date date NOT NULL,
	observation_period_end_date date NOT NULL,
	period_type_concept_id int4 NOT NULL
);


-- {sc}.payer_plan_period definition

-- Drop table

DROP TABLE IF EXISTS {sc}.payer_plan_period;

CREATE TABLE IF NOT EXISTS {sc}.payer_plan_period (
	payer_plan_period_id int8 NOT NULL,
	person_id int8 NOT NULL,
	payer_plan_period_start_date date NOT NULL,
	payer_plan_period_end_date date NOT NULL,
	payer_concept_id int4 NULL,
	payer_source_value varchar(50) NULL,
	payer_source_concept_id int4 NULL,
	plan_concept_id int4 NULL,
	plan_source_value varchar(50) NULL,
	plan_source_concept_id int4 NULL,
	sponsor_concept_id int4 NULL,
	sponsor_source_value varchar(50) NULL,
	sponsor_source_concept_id int4 NULL,
	family_source_value varchar(50) NULL,
	stop_reason_concept_id int4 NULL,
	stop_reason_source_value varchar(50) NULL,
	stop_reason_source_concept_id int4 NULL
);


-- {sc}.person definition

-- Drop table

DROP TABLE IF EXISTS {sc}.person;

CREATE TABLE IF NOT EXISTS {sc}.person (
	person_id int8 NOT NULL,
	gender_concept_id int4 NOT NULL,
	year_of_birth int4 NOT NULL,
	month_of_birth int4 NULL,
	day_of_birth int4 NULL,
	birth_datetime timestamp NULL,
	race_concept_id int4 NOT NULL,
	ethnicity_concept_id int4 NOT NULL,
	location_id int8 NULL,
	provider_id int8 NULL,
	care_site_id int8 NULL,
	person_source_value varchar(50) NULL,
	gender_source_value varchar(50) NULL,
	gender_source_concept_id int4 NULL,
	race_source_value varchar(50) NULL,
	race_source_concept_id int4 NULL,
	ethnicity_source_value varchar(50) NULL,
	ethnicity_source_concept_id int4 NULL
);


-- {sc}.procedure_occurrence definition

-- Drop table

DROP TABLE IF EXISTS {sc}.procedure_occurrence;

CREATE TABLE IF NOT EXISTS {sc}.procedure_occurrence (
	procedure_occurrence_id int8 NOT NULL,
	person_id int8 NOT NULL,
	procedure_concept_id int4 NOT NULL,
	procedure_date date NOT NULL,
	procedure_datetime timestamp NULL,
	procedure_type_concept_id int4 NOT NULL,
	modifier_concept_id int4 NULL,
	quantity int4 NULL,
	provider_id int8 NULL,
	visit_occurrence_id int8 NULL,
	visit_detail_id int8 NULL,
	procedure_source_value varchar(50) NULL,
	procedure_source_concept_id int4 NULL,
	modifier_source_value varchar(50) NULL
);


-- {sc}.provider definition

-- Drop table

DROP TABLE IF EXISTS {sc}.provider;

CREATE TABLE IF NOT EXISTS {sc}.provider (
	provider_id int8 NOT NULL,
	provider_name varchar(255) NULL,
	npi varchar(20) NULL,
	dea varchar(20) NULL,
	specialty_concept_id int4 NULL,
	care_site_id int8 NULL,
	year_of_birth int4 NULL,
	gender_concept_id int4 NULL,
	provider_source_value varchar(50) NULL,
	specialty_source_value varchar(50) NULL,
	specialty_source_concept_id int4 NULL,
	gender_source_value varchar(50) NULL,
	gender_source_concept_id int4 NULL
);


-- {sc}.source_to_concept_map definition

-- Drop table

DROP TABLE IF EXISTS {sc}.source_to_concept_map;

CREATE TABLE IF NOT EXISTS {sc}.source_to_concept_map (
	source_code varchar(255) NOT NULL,
	source_concept_id int4 NOT NULL,
	source_vocabulary_id varchar(20) NOT NULL,
	source_code_description varchar(255) NULL,
	target_concept_id int4 NOT NULL,
	target_vocabulary_id varchar(20) NULL,
	valid_start_date date NOT NULL,
	valid_end_date date NOT NULL,
	invalid_reason varchar(1) NULL
);


-- {sc}.source_to_source definition

-- Drop table

DROP TABLE IF EXISTS {sc}.source_to_source;

CREATE TABLE IF NOT EXISTS {sc}.source_to_source (
	source_code varchar NULL,
	source_concept_id int4 NULL,
	source_code_description varchar(255) NULL,
	source_vocabulary_id varchar(20) NULL,
	source_domain_id varchar(20) NULL,
	source_concept_class_id varchar(20) NULL,
	source_valid_start_date date NULL,
	source_valid_end_date date NULL,
	source_invalid_reason varchar(1) NULL,
	target_concept_id int4 NULL,
	target_concept_name varchar(255) NULL,
	target_vocabulary_id varchar(20) NULL,
	target_domain_id varchar(20) NULL,
	target_concept_class_id varchar(20) NULL,
	target_invalid_reason varchar(1) NULL,
	target_standard_concept varchar(1) NULL
);


-- {sc}.source_to_standard definition

-- Drop table

DROP TABLE IF EXISTS {sc}.source_to_standard;

CREATE TABLE IF NOT EXISTS {sc}.source_to_standard (
	source_code varchar NULL,
	source_concept_id int4 NULL,
	source_code_description varchar(255) NULL,
	source_vocabulary_id varchar(20) NULL,
	source_domain_id varchar(20) NULL,
	source_concept_class_id varchar(20) NULL,
	source_valid_start_date date NULL,
	source_valid_end_date date NULL,
	source_invalid_reason varchar(1) NULL,
	target_concept_id int4 NULL,
	target_concept_name varchar(255) NULL,
	target_vocabulary_id varchar(20) NULL,
	target_domain_id varchar(20) NULL,
	target_concept_class_id varchar(20) NULL,
	target_invalid_reason varchar(1) NULL,
	target_standard_concept varchar(1) NULL
);


-- {sc}.specimen definition

-- Drop table

DROP TABLE IF EXISTS {sc}.specimen;

CREATE TABLE IF NOT EXISTS {sc}.specimen (
	specimen_id int8 NOT NULL,
	person_id int8 NOT NULL,
	specimen_concept_id int4 NOT NULL,
	specimen_type_concept_id int4 NOT NULL,
	specimen_date date NOT NULL,
	specimen_datetime timestamp NULL,
	quantity numeric NULL,
	unit_concept_id int4 NULL,
	anatomic_site_concept_id int4 NULL,
	disease_status_concept_id int4 NULL,
	specimen_source_id varchar(50) NULL,
	specimen_source_value varchar(50) NULL,
	unit_source_value varchar(50) NULL,
	anatomic_site_source_value varchar(50) NULL,
	disease_status_source_value varchar(50) NULL
);


-- {sc}.visit_detail definition

-- Drop table

DROP TABLE IF EXISTS {sc}.visit_detail;

CREATE TABLE IF NOT EXISTS {sc}.visit_detail (
	visit_detail_id bigserial NOT NULL,
	person_id int8 NOT NULL,
	visit_detail_concept_id int4 NOT NULL,
	visit_detail_start_date date NOT NULL,
	visit_detail_start_datetime timestamp NULL,
	visit_detail_end_date date NOT NULL,
	visit_detail_end_datetime timestamp NULL,
	visit_detail_type_concept_id int4 NOT NULL,
	provider_id int8 NULL,
	care_site_id int8 NULL,
	admitting_source_concept_id int4 NULL,
	discharge_to_concept_id int4 NULL,
	preceding_visit_detail_id int8 NULL,
	visit_detail_source_value varchar(50) NULL,
	visit_detail_source_concept_id int4 NULL,
	admitting_source_value varchar(50) NULL,
	discharge_to_source_value varchar(50) NULL,
	parent_visit_detail_id int8 NULL,
	visit_occurrence_id int8 NULL
);


-- {sc}.visit_occurrence definition

-- Drop table

DROP TABLE IF EXISTS {sc}.visit_occurrence;

CREATE TABLE IF NOT EXISTS {sc}.visit_occurrence (
	visit_occurrence_id bigserial primary key,
	person_id int8 NOT NULL,
	visit_concept_id int4 NOT NULL,
	visit_start_date date NOT NULL,
	visit_start_datetime timestamp NULL,
	visit_end_date date NOT NULL,
	visit_end_datetime timestamp NULL,
	visit_type_concept_id int4 NOT NULL,
	provider_id int8 NULL,
	care_site_id int8 NULL,
	visit_source_value varchar(50) NULL,
	visit_source_concept_id int4 NULL,
	admitting_source_concept_id int4 NULL,
	admitting_source_value varchar(50) NULL,
	discharge_to_concept_id int4 NULL,
	discharge_to_source_value varchar(50) NULL,
	preceding_visit_occurrence_id int8 NULL,
	admitted_from_concept_id int8 NULL,
	admitted_from_source_value varchar NULL,
	discharged_to_concept_id int8 NULL,
	discharged_to_source_value varchar NULL
);