-- To avoid storing the login and password in the script file, the value 
-- is passed into SQLCMD as a scripting variable. For information about 
-- how to use scripting variables on the command line and in SQL Server
-- Management Studio, see the "Executing Replication Scripts" section in
-- the topic "Programming Replication Using System Stored Procedures".

--Declarations for adding a merge publication
DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS sysname;
SET @publicationDB = N'AdventureWorks2012'; 
SET @publication = N'AdvWorksSalesOrdersMerge'; 
SET @login = $(Login);
SET @password = $(Password);

-- Enable merge replication on the publication database, using defaults.
USE master
EXEC sp_replicationdboption 
  @dbname=@publicationDB, 
  @optname=N'merge publish',
  @value = N'true' 

-- Create a new merge publication, explicitly setting the defaults. 
USE [AdventureWorks2012]
EXEC sp_addmergepublication 
-- These parameters are optional.
  @publication = @publication,
  -- optional parameters 
  @description = N'Merge publication of AdventureWorks2012.',
  @publication_compatibility_level  = N'120RTM';

-- Create a new snapshot job for the publication.
EXEC sp_addpublication_snapshot 
  @publication = @publication, 
  @job_login = @login, 
  @job_password = @password;
GO