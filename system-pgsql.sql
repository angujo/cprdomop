-- CDMTimer definition

CREATE TABLE IF NOT EXISTS CDMTimer (
	Id bigserial NOT NULL,
	Name varchar(250),
	ChunkId INTEGER DEFAULT 0,
	"Query" TEXT,
	StartTime timestamp,
	EndTime timestamp
, WorkLoadId INT8 NOT NULL, 
Status INTEGER, 
ErrorLog TEXT);

CREATE UNIQUE INDEX CDMTimer_Unique ON CDMTimer (Name,ChunkId,WorkLoadId);


-- ChunkTimer definition

CREATE TABLE IF NOT EXISTS ChunkTimer (
	Id bigserial NOT NULL,
	ChunkId INTEGER,
	StartTime timestamp,
	EndTime timestamp,
	Touched INTEGER DEFAULT 0
, WorkLoadId INTEGER, Status INTEGER, ErrorLog TEXT);

CREATE UNIQUE INDEX ChunkTimer_Unique ON ChunkTimer (ChunkId,WorkLoadId);


-- Queue definition

CREATE TABLE IF NOT EXISTS "Queue" (
	"Id"	bigserial NOT NULL ,
	"WorkQueueId"	INTEGER,
	"TaskIndex"	INTEGER,
	"ParallelIndex"	INTEGER,
	"FilePath"	TEXT,
	"FileContent"	TEXT,
	"StartTime"	timestamp,
	"EndTime"	timestamp,
	"Status"	INTEGER,
	"ActionType"	INTEGER,
	"Ordinal"	INTEGER,
	"ErrorLog"	TEXT, DBSchemaId INTEGER NOT NULL
);


-- ServiceStatus definition

CREATE TABLE IF NOT EXISTS ServiceStatus (
	Id bigserial NOT NULL,
	ServiceName varchar(250),
	ServiceDescription varchar(450),
	Status INTEGER,
	LastRun timestamp
);


-- SourceFile definition

CREATE TABLE IF NOT EXISTS "SourceFile" (
	"Id"	bigserial NOT NULL,
	"WorkLoadId"	int8 NOT NULL,
	"FileName"	varchar(250) NOT NULL,
	"FilePath"	varchar(450) NOT NULL,
	"FileHash"	varchar(250),
	"Processed"	INTEGER NOT NULL DEFAULT 0,
	"TableName"	varchar(250) NOT NULL,
	"Code"	TEXT NOT NULL,
	"IsFile"	INTEGER NOT NULL DEFAULT 0
);


-- WorkLoad definition

CREATE TABLE IF NOT EXISTS "WorkLoad" (
	"Id"	bigserial NOT NULL,
	"Name"	varchar(250) NOT NULL,
	"ReleaseDate"	timestamp,
	"FilesLocked"	INTEGER NOT NULL DEFAULT 0,
	"SourceProcessed"	INTEGER NOT NULL DEFAULT 0, CdmLoaded INTEGER DEFAULT 0 NOT NULL, ChunksSetup INTEGER DEFAULT 0 NOT NULL, 
	ChunksLoaded INTEGER NOT NULL, CdmProcessed INTEGER DEFAULT 0 NOT NULL, ChunkSize INTEGER DEFAULT 500, IsRunning INTEGER DEFAULT 0 NOT NULL, 
	MaxParallels INTEGER DEFAULT 3 NOT NULL, TestChunkCount INTEGER
);


-- DBSchema definition

CREATE TABLE IF NOT EXISTS "DBSchema" (
	"Id"	bigserial NOT NULL,
	"WorkLoadId"	int8 NOT NULL,
	"SchemaType"	varchar(250),
	"Server"	varchar(250),
	"Port"	INTEGER,
	"DBName"	varchar(250),
	"SchemaName"	varchar(250),
	"Username"	varchar(250),
	"Password"	varchar(250),
	"TestSuccess"	INTEGER NOT NULL DEFAULT 0,
	FOREIGN KEY("WorkLoadId") REFERENCES "WorkLoad"("Id")
);


-- WorkQueue definition

CREATE TABLE IF NOT EXISTS "WorkQueue" (
	"Id"	bigserial NOT NULL ,
	"WorkLoadId"	int8 NOT NULL,
	"Name"	varchar(250),
	"QueueType"	INTEGER NOT NULL,
	"StartTime"	timestamp,
	"EndTime"	timestamp,
	"Status"	INTEGER,
	"ProgressPercent"	INTEGER, ErrorLog TEXT,
	FOREIGN KEY("WorkLoadId") REFERENCES "WorkLoad"("Id")
);

CREATE UNIQUE INDEX WorkQueue_Unique ON WorkQueue (WorkLoadId,QueueType);