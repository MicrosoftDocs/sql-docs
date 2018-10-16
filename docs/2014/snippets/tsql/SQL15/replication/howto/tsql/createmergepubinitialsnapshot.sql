:setvar Login N'REDMOND\glenga'
:setvar Password N'Love&War'
:setvar InstanceName @@servername

--<snippetsp_mergegenerate_snapshot>
-- To avoid storing the login and password in the script file, the value 
-- is passed into SQLCMD as a scripting variable. For information about 
-- how to use scripting variables on the command line and in SQL Server
-- Management Studio, see the "Executing Replication Scripts" section in
-- the topic "Programming Replication Using System Stored Procedures".

DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS sysname;
SET @publicationDB = N'AdventureWorks2012'; 
SET @publication = N'AdvWorksSalesOrdersMerge'; 
SET @login = $(Login);
SET @password = $(Password);

-- Enable merge replication on the publication database.
USE master
EXEC sp_replicationdboption 
  @dbname = @publicationDB, 
  @optname=N'merge publish',
  @value = N'true';

-- Create new merge publication, using the defaults. 
USE [AdventureWorks]
EXEC sp_addmergepublication 
  @publication = @publication, 
  @description = N'Merge publication.';

-- Create a new snapshot job for the publication, using the defaults.
EXEC sp_addpublication_snapshot 
  @publication = @publication,
  @job_login = @login,
  @job_password = @password;

-- Start the Snapshot Agent job.
EXEC sp_startpublication_snapshot @publication = @publication;
GO
--</snippetsp_mergegenerate_snapshot>