:setvar Login N'REDMOND\glenga';
:setvar Password N'Love&War';
:setvar Server N'glengatest2'

--<snippetsp_createtranpub_NWpostupgrade>
-- To avoid storing the login and password in the script file, the value 
-- is passed into SQLCMD as a scripting variable. For information about 
-- how to use scripting variables on the command line and in SQL Server
-- Management Studio, see the "Executing Replication Scripts" section in
-- the topic "Programming Replication Using System Stored Procedures".

-- Execute at the Distributor.
USE [distribution]

DECLARE @login AS sysname;
DECLARE @password AS sysname;
-- Specify the Windows account to run the Queue Reader Agent.
SET @login = $(Login); 
-- Pass the password at runtime.
SET @password = $(Password); 

-- Execute sp_addqreader_agent to create the Queue Reader Agent job. 
EXEC sp_addqreader_agent 
	@job_login = @login, 
	@job_password = @password;
GO

-- Execute at the Publisher.
USE [Northwind]
GO

DECLARE @publication AS sysname;
DECLARE @publicationDB AS sysname;
DECLARE @article AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS sysname;
SET @publication = N'NwdProductTran';
SET @publicationDB = N'Northwind';
SET @article = N'Products';
-- Specify the Windows account to run the Log Reader and Snapshot Agents.
SET @login = $(Login); 
-- Pass the password at runtime.
SET @password = $(Password); 

-- Enable the replication database.
EXEC sp_replicationdboption 
	@dbname = @publicationDB, 
	@optname = N'publish', 
	@value = N'true';

-- Execute sp_addlogreader_agent to create the agent job. 
EXEC sp_addlogreader_agent 
	@job_login = @login, 
	@job_password = @password, 
	-- Explicitly specify the use of Windows Integrated Authentication (default) 
	-- when connecting to the Publisher.
	@publisher_security_mode = 1;

-- Add the transactional publication.
EXEC sp_addpublication 
	@publication = @publication, 
	@sync_method = N'native', 
	@repl_freq = N'continuous', 
	@status = N'active',
	@description = N'Transactional publication of Northwind.', 
	@allow_push = N'true', 
	@allow_pull = N'true', 
	@allow_sync_tran = N'true', 
	@autogen_sync_procs = N'true', 
	@allow_queued_tran = N'true',
	@replicate_ddl = 1,
	@enabled_for_p2p = N'false';

-- Create a new snapshot job for the publication, using a default schedule.
EXEC sp_addpublication_snapshot 
	@publication = @publication, 
	@job_login = @login, 
	@job_password = @password,
	-- Explicitly specify the use of Windows Integrated Authentication (default) 
	-- when connecting to the Publisher.
	@publisher_security_mode = 1;

-- Add a transactional article.
EXEC sp_addarticle 
  @publication = @publication, 
  @article = @article, 
  @source_owner = N'dbo', 
  @source_object = @article, 
  @destination_table = @article, 
  @type = N'logbased', 
  @schema_option = 0x00000000000080F3, 
  @ins_cmd = N'CALL sp_MSins_Products', 
  @del_cmd = N'XCALL sp_MSdel_Products', 
  @upd_cmd = N'XCALL sp_MSupd_Products', 
  @auto_identity_range = N'false',
  @identityrangemanagementoption = N'manual',
  @fire_triggers_on_snapshot = N'false';
GO
--</snippetsp_createtranpub_NWpostupgrade>