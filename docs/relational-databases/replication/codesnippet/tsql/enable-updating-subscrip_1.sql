-- To avoid storing the login and password in the script file, the values 
-- are passed into SQLCMD as scripting variables. For information about 
-- how to use scripting variables on the command line and in SQL Server
-- Management Studio, see the "Executing Replication Scripts" section in
-- the topic "Programming Replication Using System Stored Procedures".

--Declarations for adding a transactional publication
DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS sysname;
SET @publicationDB = N'AdventureWorks2012'; 
SET @publication = N'AdvWorksProductTran'; 
SET @login = $(Login); 
SET @password = $(Password); 

USE [AdventureWorks2012]
-- Enable transactional replication on the publication database.
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

-- Create a transactional publication that supports immediate updating, 
-- queued updating, and pull subscriptions. 
EXEC sp_addpublication 
	@publication = @publication, 
	@status = N'active',
	@allow_sync_tran = N'true', 
	@allow_queued_tran = N'true',
	@allow_pull = N'true',
	@independent_agent = N'true',
  -- Explicitly declare the related default properties 
	@conflict_policy = N'pub wins';

-- Create a new snapshot job for the publication, using a default schedule.
EXEC sp_addpublication_snapshot 
	@publication = @publication, 
	@job_login = @login, 
	@job_password = @password,
	-- Explicitly specify the use of Windows Integrated Authentication (default) 
	-- when connecting to the Publisher.
	@publisher_security_mode = 1;
GO

--Declarations for adding an article.
DECLARE @publication AS sysname;
DECLARE @article AS sysname;
DECLARE @owner AS sysname;
SET @publication = N'AdvWorksProductTran';
SET @article = N'Product'; 
SET @owner = N'Production'; 

-- Add a horizontally and vertically filtered article for the Product table.
USE [AdventureWorks2012]
EXEC sp_addarticle 
	@publication = @publication, 
	@article = @article, 
	@source_table = @article, 
	@vertical_partition = N'false', 
	@type = N'logbased',
	@source_owner = @owner, 
	@destination_owner = @owner;
GO
