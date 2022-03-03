-- public.cdmtimer definition

-- Drop table

-- DROP TABLE public.cdmtimer;

CREATE TABLE public.cdmtimer (
	id bigserial NOT NULL,
	"name" varchar(250) NULL,
	chunkid int4 NULL DEFAULT 0,
	query text NULL,
	starttime timestamp NULL,
	endtime timestamp NULL,
	workloadid int8 NOT NULL,
	status int4 NULL,
	errorlog text NULL,
	CONSTRAINT cdmtimer_id_key UNIQUE (id)
);
CREATE UNIQUE INDEX cdmtimer_unique ON public.cdmtimer USING btree (name, chunkid, workloadid);


-- public.chunktimer definition

-- Drop table

-- DROP TABLE public.chunktimer;

CREATE TABLE public.chunktimer (
	id bigserial NOT NULL,
	chunkid int4 NULL,
	starttime timestamp NULL,
	endtime timestamp NULL,
	touched int4 NULL DEFAULT 0,
	workloadid int4 NULL,
	status int4 NULL,
	errorlog text NULL,
	CONSTRAINT chunktimer_id_key UNIQUE (id)
);
CREATE UNIQUE INDEX chunktimer_unique ON public.chunktimer USING btree (chunkid, workloadid);


-- public.queue definition

-- Drop table

-- DROP TABLE public.queue;

CREATE TABLE public.queue (
	id bigserial NOT NULL,
	workqueueid int4 NULL,
	taskindex int4 NULL,
	parallelindex int4 NULL,
	filepath text NULL,
	filecontent text NULL,
	starttime timestamp NULL,
	endtime timestamp NULL,
	status int4 NULL,
	actiontype int4 NULL,
	ordinal int4 NULL,
	errorlog text NULL,
	dbschemaid int4 NOT NULL,
	CONSTRAINT queue_id_key UNIQUE (id)
);


-- public.servicestatus definition

-- Drop table

-- DROP TABLE public.servicestatus;

CREATE TABLE public.servicestatus (
	id bigserial NOT NULL,
	servicename varchar(250) NULL,
	servicedescription varchar(450) NULL,
	status int4 NULL,
	lastrun timestamp NULL,
	CONSTRAINT servicestatus_id_key UNIQUE (id)
);


-- public.sourcefile definition

-- Drop table

-- DROP TABLE public.sourcefile;

CREATE TABLE public.sourcefile (
	id bigserial NOT NULL,
	workloadid int8 NOT NULL,
	filename varchar(250) NOT NULL,
	filepath varchar(450) NOT NULL,
	filehash varchar(250) NULL,
	processed int4 NOT NULL DEFAULT 0,
	tablename varchar(250) NOT NULL,
	code text NOT NULL,
	isfile int4 NOT NULL DEFAULT 0,
	CONSTRAINT sourcefile_id_key UNIQUE (id)
);


-- public.workload definition

-- Drop table

-- DROP TABLE public.workload;

CREATE TABLE public.workload (
	id bigserial NOT NULL,
	"name" varchar(250) NOT NULL,
	releasedate timestamp NULL,
	fileslocked bool NOT NULL DEFAULT false,
	sourceprocessed bool NOT NULL DEFAULT false,
	cdmloaded bool NOT NULL DEFAULT false,
	chunkssetup bool NOT NULL DEFAULT false,
	chunksloaded bool NOT NULL DEFAULT false,
	cdmprocessed bool NOT NULL DEFAULT false,
	chunksize int4 NULL DEFAULT 500,
	isrunning int4 NOT NULL DEFAULT 0,
	maxparallels int4 NOT NULL DEFAULT 3,
	testchunkcount int4 NULL,
	CONSTRAINT workload_id_key UNIQUE (id)
);


-- public.dbschema definition

-- Drop table

-- DROP TABLE public.dbschema;

CREATE TABLE public.dbschema (
	id bigserial NOT NULL,
	workloadid int8 NOT NULL,
	schematype varchar(250) NULL,
	"server" varchar(250) NULL,
	port int4 NULL,
	dbname varchar(250) NULL,
	schemaname varchar(250) NULL,
	username varchar(250) NULL,
	"password" varchar(250) NULL,
	testsuccess int4 NOT NULL DEFAULT 0,
	CONSTRAINT dbschema_id_key UNIQUE (id),
	CONSTRAINT dbschema_workloadid_fkey FOREIGN KEY (workloadid) REFERENCES public.workload(id)
);


-- public.workqueue definition

-- Drop table

-- DROP TABLE public.workqueue;

CREATE TABLE public.workqueue (
	id bigserial NOT NULL,
	workloadid int8 NOT NULL,
	"name" varchar(250) NULL,
	queuetype int4 NOT NULL,
	starttime timestamp NULL,
	endtime timestamp NULL,
	status int4 NULL,
	progresspercent int4 NULL,
	errorlog text NULL,
	CONSTRAINT workqueue_id_key UNIQUE (id),
	CONSTRAINT workqueue_workloadid_fkey FOREIGN KEY (workloadid) REFERENCES public.workload(id)
);
CREATE UNIQUE INDEX workqueue_unique ON public.workqueue USING btree (workloadid, queuetype);