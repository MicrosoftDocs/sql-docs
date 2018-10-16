--:setvar Login N'REDMOND\glenga'
--:setvar Password N'Fun@Work'
--:setvar WebServer N'GLENGATEST2'

--------------------------------------------------------------------------
-- add a merge publication
--------------------------------------------------------------------------

--<snippetsp_addmergepub_websync>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

--Declarations for adding a merge publication
DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
DECLARE @websyncurl AS nvarchar(256);
DECLARE @login AS sysname;
DECLARE @password AS sysname;
SET @publicationDB = N'AdventureWorks2012'; 
SET @publication = N'AdvWorksSalesOrdersMerge'; 
SET @websyncurl = 'https://' + $(WebServer) + '/WebSync';
SET @login = $(Login);
SET @password = $(Password);

-- Enable merge replication on the publication database, using defaults.
USE master
EXEC sp_replicationdboption 
  @dbname=@publicationDB, 
  @optname=N'merge publish',
  @value = N'true' 

-- Create a new merge publication, explicitly setting the defaults. 
EXEC sp_addmergepublication 
	@publication = @publication,
	-- optional parameters 
	@description = N'Merge publication of AdventureWorks2012 using Web synchronization.',
	@publication_compatibility_level  = N'120RTM',
	-- Enable Web synchronization.
	@allow_web_synchronization = N'true',
	-- Web synchronization URL hint used by SQL Server Management Studio.
	@web_synchronization_url = @websyncurl;

-- Create a new snapshot job for the publication.
EXEC sp_addpublication_snapshot 
	@publication = @publication, 
	@job_login = @login, 
	@job_password = @password;
GO
--</snippetsp_addmergepub_websync>