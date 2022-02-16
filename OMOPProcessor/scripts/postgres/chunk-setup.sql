-- Drop table

DROP TABLE IF EXISTS {sc}."_chunk";

CREATE TABLE {sc}."_chunk" (
	id bigserial NOT NULL,
	ordinal int8 NOT NULL,
	patient_id int8 NULL,
	loaded bool NULL DEFAULT false,
	processed bool NULL,
	load_time timestamp NULL,
	end_time timestamp NULL
);