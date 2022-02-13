-- {sc}.additional definition

-- Drop table

-- DROP TABLE {sc}.additional;

CREATE TABLE IF NOT EXISTS {sc}.additional (
	id bigserial NOT NULL,
	patid int8 NOT NULL,
	enttype int4 NOT NULL,
	adid int4 NOT NULL,
	data1 varchar(20) NULL DEFAULT NULL::character varying,
	data2 varchar(20) NULL DEFAULT NULL::character varying,
	data3 varchar(20) NULL DEFAULT NULL::character varying,
	data4 varchar(20) NULL DEFAULT NULL::character varying,
	data5 varchar(20) NULL DEFAULT NULL::character varying,
	data6 varchar(20) NULL DEFAULT NULL::character varying,
	data7 varchar(20) NULL DEFAULT NULL::character varying,
	data8 varchar(20) NULL DEFAULT NULL::character varying,
	data9 varchar(20) NULL DEFAULT NULL::character varying,
	data10 varchar(20) NULL DEFAULT NULL::character varying,
	data11 varchar(20) NULL DEFAULT NULL::character varying,
	data12 varchar(20) NULL DEFAULT NULL::character varying,
	CONSTRAINT additional_pkey PRIMARY KEY (id)
);


-- {sc}.clinical definition

-- Drop table

-- DROP TABLE {sc}.clinical;

CREATE TABLE IF NOT EXISTS {sc}.clinical (
	id bigserial NOT NULL,
	patid int8 NOT NULL,
	eventdate date NULL,
	"sysdate" date NULL,
	constype int2 NULL,
	consid int4 NOT NULL,
	medcode int8 NOT NULL,
	sctid varchar(64)  NULL,
	sctdescid varchar(64)  NULL,
	sctexpression varchar(64)  NULL,
	sctmaptype int2  NULL,
	sctmapversion int4  NULL,
	sctisindicative boolean  NULL,
	sctisassured boolean  NULL,
	staffid int8 NULL,
	episode int4  NULL,
	enttype int4  NULL,
	adid int4 NOT NULL,
	CONSTRAINT clinical_pkey PRIMARY KEY (id)
);

-- {sc}.commondosages definition

-- Drop table

-- DROP TABLE {sc}.commondosages;

CREATE TABLE IF NOT EXISTS {sc}.commondosages (
	id bigserial NOT NULL,
	dosageid varchar(64) NOT NULL,
	dosage_text varchar(1000) NULL DEFAULT NULL::character varying,
	daily_dose numeric(15, 3) NULL DEFAULT NULL::numeric,
	dose_number numeric(15, 3) NULL DEFAULT NULL::numeric,
	dose_unit varchar(7) NULL DEFAULT NULL::character varying,
	dose_frequency numeric(15, 3) NULL DEFAULT NULL::numeric,
	dose_interval numeric(15, 3) NULL DEFAULT NULL::numeric,
	choice_of_dose int2 NULL,
	dose_max_average int2 NULL,
	change_dose int2 NULL,
	dose_duration numeric(15, 3) NULL DEFAULT NULL::numeric,
	CONSTRAINT commondosages_pkey PRIMARY KEY (id)
);

-- {sc}.consultation definition

-- Drop table

-- DROP TABLE {sc}.consultation;

CREATE TABLE IF NOT EXISTS {sc}.consultation (
	id bigserial NOT NULL,
	patid int8 NOT NULL,
	eventdate date NULL,
	"sysdate" date NULL,
	constype int2 NULL,
	consid int4 NOT NULL,
	staffid int8 NOT NULL,
	duration int8 NOT NULL,
	CONSTRAINT consultation_pkey PRIMARY KEY (id)
);

-- {sc}.covid_test_mappings definition

-- Drop table

-- DROP TABLE {sc}.covid_test_mappings;

CREATE TABLE IF NOT EXISTS {sc}.covid_test_mappings (
	id bigserial NOT NULL,
	measurement_source_value varchar(250) NOT NULL,
	concept_id int4 NULL,
	value_as_concept_id int4 NOT NULL,
	CONSTRAINT covid_test_mappings_pkey PRIMARY KEY (id)
);


-- {sc}.daysupply_decodes definition

-- Drop table

-- DROP TABLE {sc}.daysupply_decodes;

CREATE TABLE IF NOT EXISTS {sc}.daysupply_decodes (
	id bigserial NOT NULL,
	prodcode int4 NOT NULL,
	daily_dose numeric(15, 3) NULL,
	qty numeric(9, 2) NULL,
	numpacks int4 NULL,
	numdays int2 NULL,
	CONSTRAINT daysupply_decodes_pkey PRIMARY KEY (id)
);


-- {sc}.daysupply_modes definition

-- Drop table

-- DROP TABLE {sc}.daysupply_modes;

CREATE TABLE IF NOT EXISTS {sc}.daysupply_modes (
	id bigserial NOT NULL,
	prodcode int4 NOT NULL,
	numdays int2 NULL,
	CONSTRAINT daysupply_modes_pkey PRIMARY KEY (id)
);


-- {sc}.entity definition

-- Drop table

-- DROP TABLE {sc}.entity;

CREATE TABLE IF NOT EXISTS {sc}.entity (
	id bigserial NOT NULL,
	enttype int4 NOT NULL,
	description varchar(60) NOT NULL,
	filetype varchar(8) NOT NULL,
	category varchar(30) NULL DEFAULT NULL::character varying,
	data_fields int2 NOT NULL,
	data1 varchar(50) NULL DEFAULT NULL::character varying,
	data1_lkup varchar(20) NULL DEFAULT NULL::character varying,
	data2 varchar(50) NULL DEFAULT NULL::character varying,
	data2_lkup varchar(20) NULL DEFAULT NULL::character varying,
	data3 varchar(50) NULL DEFAULT NULL::character varying,
	data3_lkup varchar(20) NULL DEFAULT NULL::character varying,
	data4 varchar(50) NULL DEFAULT NULL::character varying,
	data4_lkup varchar(20) NULL DEFAULT NULL::character varying,
	data5 varchar(50) NULL DEFAULT NULL::character varying,
	data5_lkup varchar(20) NULL DEFAULT NULL::character varying,
	data6 varchar(50) NULL DEFAULT NULL::character varying,
	data6_lkup varchar(20) NULL DEFAULT NULL::character varying,
	data7 varchar(50) NULL DEFAULT NULL::character varying,
	data7_lkup varchar(20) NULL DEFAULT NULL::character varying,
	data8 varchar(50) NULL DEFAULT NULL::character varying,
	data8_lkup varchar(20) NULL DEFAULT NULL::character varying,
	data9 varchar(50) NULL DEFAULT NULL::character varying,
	data9_lkup varchar(20) NULL DEFAULT NULL::character varying,
	data10 varchar(50) NULL DEFAULT NULL::character varying,
	data10_lkup varchar(20) NULL DEFAULT NULL::character varying,
	data11 varchar(50) NULL DEFAULT NULL::character varying,
	data11_lkup varchar(20) NULL DEFAULT NULL::character varying,
	data12 varchar(50) NULL DEFAULT NULL::character varying,
	data12_lkup varchar(20) NULL DEFAULT NULL::character varying,
	CONSTRAINT entity_pkey PRIMARY KEY (id)
);


-- {sc}.immunisation definition

-- Drop table

-- DROP TABLE {sc}.immunisation;

CREATE TABLE IF NOT EXISTS {sc}.immunisation (
	id bigserial NOT NULL,
	patid int8 NOT NULL,
	eventdate date NULL,
	"sysdate" date NULL,
	constype int4  NULL,
	consid int4 NOT NULL,
	medcode int8 NOT NULL,
	sctid varchar(64) NULL,
	sctdescid varchar(64) NULL,
	sctexpression varchar(64) NULL,
	sctmaptype int2 NULL,
	sctmapversion int4 NULL,
	sctisindicative boolean NULL,
	sctisassured boolean NULL,
	staffid int8 NOT NULL,
	immstype int8 NOT NULL,
	stage int8 NOT NULL,
	"status" int8 NOT NULL,
	compound int8 NOT NULL,
	source int8 NOT NULL,
	reason int8 NOT NULL,
	method int8 NOT NULL,
	batch int8 NOT NULL,
	CONSTRAINT immunisation_pkey PRIMARY KEY (id)
);

-- {sc}.lookup definition

-- Drop table

-- DROP TABLE {sc}.lookup;

CREATE TABLE IF NOT EXISTS {sc}.lookup (
	lookup_id serial8 NOT NULL,
	lookup_type_id int8 NULL,
	code int2 NOT NULL,
	"text" varchar(100) NULL DEFAULT NULL::character varying,
	CONSTRAINT lookup_pkey PRIMARY KEY (lookup_id)
);

-- {sc}.lookuptype definition

-- Drop table

-- DROP TABLE {sc}.lookuptype;

CREATE TABLE IF NOT EXISTS {sc}.lookuptype (
	lookup_type_id bigserial NOT NULL,
	"name" varchar(3) NOT NULL,
	description varchar(3) NOT NULL,
	CONSTRAINT lookuptype_pkey PRIMARY KEY (lookup_type_id)
);


-- {sc}.medical definition

-- Drop table

-- DROP TABLE {sc}.medical;

CREATE TABLE IF NOT EXISTS {sc}.medical (
	id bigserial NOT NULL,
	medcode int8 NOT NULL,
	readcode varchar(7) NULL DEFAULT NULL::character varying,
	"desc" varchar(100) NULL DEFAULT NULL::character varying,
	CONSTRAINT medical_pkey PRIMARY KEY (id)
);


-- {sc}.patient definition

-- Drop table

-- DROP TABLE {sc}.patient;

CREATE TABLE IF NOT EXISTS {sc}.patient (
	id bigserial NOT NULL,
	patid int8 NOT NULL,
	famnum int8 NOT NULL,
	vmid int8 NOT NULL,
	pracid int4 NULL,
	gender int2 NOT NULL,
	yob int2 NOT NULL,
	mob int2 NOT NULL,
	chsreg int2 NOT NULL,
	marital int2 NOT NULL,
	chsdate date NULL,
	prescr int4 NULL,
	capsup int4 NULL,
	regstat int4 NULL,
	reggap int4 NULL,
	internal int4 NULL,
	frd date NULL,
	tod date NULL,
	toreason int2 NOT NULL,
	crd date NULL,
	deathdate date NULL,
	accept int2 NOT NULL,
	CONSTRAINT patient_pkey PRIMARY KEY (id)
);



-- {sc}.practice definition

-- Drop table

-- DROP TABLE {sc}.practice;

CREATE TABLE IF NOT EXISTS {sc}.practice (
	id bigserial NOT NULL,
	pracid int4 NOT NULL,
	region int2 NOT NULL,
	lcd date NOT NULL,
	uts date NOT NULL,
	CONSTRAINT practice_pkey PRIMARY KEY (id)
);


-- {sc}.product definition

-- Drop table

-- DROP TABLE {sc}.product;

CREATE TABLE IF NOT EXISTS {sc}.product (
	id bigserial NOT NULL,
	prodcode int8 NOT NULL,
	dmdcode varchar(20) NULL DEFAULT NULL::character varying,
	gemscriptcode varchar(8) NULL DEFAULT NULL::character varying,
	productname varchar(500) NULL DEFAULT NULL::character varying,
	drugsubstance varchar(1500) NULL DEFAULT NULL::character varying,
	strength varchar(1100) NULL DEFAULT NULL::character varying,
	formulation varchar(100) NULL DEFAULT NULL::character varying,
	route varchar(100) NULL DEFAULT NULL::character varying,
	bnfcode varchar(100) NULL DEFAULT NULL::character varying,
	bnfchapter varchar(500) NULL DEFAULT NULL::character varying,
	CONSTRAINT product_pkey PRIMARY KEY (id)
);


-- {sc}.referral definition

-- Drop table

-- DROP TABLE {sc}.referral;

CREATE TABLE IF NOT EXISTS {sc}.referral (
	id bigserial NOT NULL,
	patid int8 NOT NULL,
	eventdate date NULL,
	"sysdate" date NULL,
	consid int4 NOT NULL,
	constype int4  NULL,
	medcode int8 NOT NULL,
	sctid varchar(64) NULL,
	sctdescid varchar(64) NULL,
	sctexpression varchar(64) NULL,
	sctmaptype int2 NULL,
	sctmapversion int4 NULL,
	sctisindicative boolean NULL,
	sctisassured boolean NULL,
	staffid int8 NULL,
	source int4 NULL,
	nhsspec int4 NULL,
	fhsaspec int4 NULL,
	inpatient int2 NULL,
	attendance int2 NULL,
	urgency int2 NULL,
	CONSTRAINT referral_pkey PRIMARY KEY (id)
);


-- {sc}.scoringmethod definition

-- Drop table

-- DROP TABLE {sc}.scoringmethod;

CREATE TABLE IF NOT EXISTS {sc}.scoremethod (
	id bigserial NOT NULL,
	code int4 NOT NULL,
	scoringmethod varchar(20) NULL DEFAULT NULL::character varying,
	CONSTRAINT scoringmethod_pkey PRIMARY KEY (id)
);


-- {sc}.staff definition

-- Drop table

-- DROP TABLE {sc}.staff;

CREATE TABLE IF NOT EXISTS {sc}.staff (
	id bigserial NOT NULL,
	staffid int8 NOT NULL,
	gender int2 NOT NULL,
	"role" int2 NOT NULL,
	CONSTRAINT staff_pkey PRIMARY KEY (id)
);


-- {sc}.test definition

-- Drop table

-- DROP TABLE {sc}.test;

CREATE TABLE IF NOT EXISTS {sc}.test (
	id bigserial NOT NULL,
	patid int8 NOT NULL,
	eventdate date NULL,
	"sysdate" date NULL,
	constype int4  NULL,
	consid int4 NOT NULL,
	medcode int8 NOT NULL,
	sctid varchar(64) NULL,
	sctdescid varchar(64) NULL,
	sctexpression varchar(64) NULL,
	sctmaptype int2 NULL,
	sctmapversion int4 NULL,
	sctisindicative boolean NULL,
	sctisassured boolean NULL,
	staffid int8 NULL,
	enttype int4 NOT NULL,
	data1 varchar(20) NULL DEFAULT NULL::character varying,
	data2 varchar(20) NULL DEFAULT NULL::character varying,
	data3 varchar(20) NULL DEFAULT NULL::character varying,
	data4 varchar(20) NULL DEFAULT NULL::character varying,
	data5 varchar(20) NULL DEFAULT NULL::character varying,
	data6 varchar(20) NULL DEFAULT NULL::character varying,
	data7 varchar(20) NULL DEFAULT NULL::character varying,
	data8 varchar(20) NULL DEFAULT NULL::character varying,
	CONSTRAINT test_pkey PRIMARY KEY (id)
);


-- {sc}.therapy definition

-- Drop table

-- DROP TABLE {sc}.therapy;

CREATE TABLE IF NOT EXISTS {sc}.therapy (
	id bigserial NOT NULL,
	patid int8 NOT NULL,
	eventdate date NULL,
	"sysdate" date NULL,
	consid int4 NOT NULL,
	prodcode int8 NOT NULL,
	drugdmd varchar(64) NULL,
	staffid int8 NOT NULL,
	dosageid varchar(64) NULL DEFAULT NULL::character varying,
	bnfcode int4 NOT NULL,
	qty numeric(11, 3) NULL DEFAULT NULL::numeric,
	numdays int4 NULL,
	numpacks numeric(10, 3) NULL DEFAULT NULL::numeric,
	packtype int4 NULL,
	issueseq int4 NULL,
	prn boolean NULL,
	CONSTRAINT therapy_pkey PRIMARY KEY (id)
);