-- To avoid storing the login and password in the script file, the values 
-- are passed into SQLCMD as scripting variables. For information about 
-- how to use scripting variables on the command line and in SQL Server
-- Management Studio, see the "Executing Replication Scripts" section in
-- the topic "Programming Replication Using System Stored Procedures".

DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS sysname;
SET @publicationDB = N'AdventureWorks'; 
SET @publication = N'AdvWorksProductTran'; 
-- Windows account used to run the Log Reader and Snapshot Agents.
SET @login = $(Login); 
-- This should be passed at runtime.
SET @password = $(Password); 

-- Enable transactional or snapshot replication on the publication database.
EXEC sp_replicationdboption 
	@dbname=@publicationDB, 
	@optname=N'publish',
	@value = N'true';

-- Execute sp_addlogreader_agent to create the agent job. 
EXEC sp_addlogreader_agent 
	@job_login = @login, 
	@job_password = @password,
	-- Explicitly specify the use of Windows Integrated Authentication (default) 
	-- when connecting to the Publisher.
	@publisher_security_mode = 1;

-- Create a new transactional publication with the required properties. 
EXEC sp_addpublication 
	@publication = @publication, 
	@status = N'active',
	@allow_push = N'true',
	@allow_pull = N'true',
	@independent_agent = N'true';

-- Create a new snapshot job for the publication, using a default schedule.
EXEC sp_addpublication_snapshot 
	@publication = @publication, 
	@job_login = @login, 
	@job_password = @password,
	-- Explicitly specify the use of Windows Integrated Authentication (default) 
	-- when connecting to the Publisher.
	@publisher_security_mode = 1;
GO