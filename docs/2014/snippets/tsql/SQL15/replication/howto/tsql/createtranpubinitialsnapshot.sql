:setvar Login N'REDMOND\glenga'
:setvar Password N'Love&War'
:setvar InstanceName @@servername

--<snippetsp_trangenerate_snapshot>
-- To avoid storing the login and password in the script file, the values 
-- are passed into SQLCMD as scripting variables. For information about 
-- how to use scripting variables on the command line and in SQL Server
-- Management Studio, see the "Executing Replication Scripts" section in
-- the topic "Programming Replication Using System Stored Procedures".

DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS sysname;
SET @publicationDB = N'AdventureWorks2012'; --publication database
SET @publication = N'AdvWorksCustomerTran'; -- transactional publication name
SET @login = $(Login);
SET @password = $(Password);

USE [AdventureWorks]

-- Enable transactional and snapshot replication on the publication database.
EXEC sp_replicationdboption 
  @dbname = @publicationDB, 
  @optname = N'publish',
  @value = N'true';

-- Execute sp_addlogreader_agent to create the agent job. 
EXEC sp_addlogreader_agent 
  @job_login = @login, 
  @job_password = @password,
  -- Explicitly specify the security mode used when connecting to the Publisher.
  @publisher_security_mode = 1;

-- Create new transactional publication, using the defaults. 
USE [AdventureWorks2012]
EXEC sp_addpublication 
  @publication = @publication, 
  @description = N'transactional publication';

-- Create a new snapshot job for the publication, using the defaults.
EXEC sp_addpublication_snapshot 
  @publication = @publication,
  @job_login = @login,
  @job_password = @password;

-- Start the Snapshot Agent job.
EXEC sp_startpublication_snapshot @publication = @publication;
GO
--</snippetsp_trangenerate_snapshot>