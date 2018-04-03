:setvar PubServer N'GLENGATEST2'
:setvar SubServer N'GLENGATEST2'
:setvar WebServer N'GLENGATEST2'
:setvar Login N'REDMOND\glenga'
:setvar Password N'Fun@Work'
--------------------------------------------------------------------------
-- Create AdventureWorks2012Replica if not exists
--------------------------------------------------------------------------
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = N'AdventureWorks2012Replica')
BEGIN
	CREATE DATABASE [AdventureWorks2012Replica]
END
GO

USE [AdventureWorks2012]
GO

DECLARE @publication AS sysname;
DECLARE @property AS sysname;
SET @publication = 'AdvWorksSalesOrdersMergeWebSync';
SET @property = 'allow_anonymous';

IF EXISTS (SELECT * FROM sysmergepublications WHERE [name] = @publication AND allow_anonymous = 0)
BEGIN
	EXEC sp_changepublication 
  @publication = @publication, 
  @property = @property, 
  @value = N'true';
END
GO
 
USE [AdventureWorks2012Replica]
GO

--------------------------------------------------------------------------
-- add a merge pull subscription
--------------------------------------------------------------------------
--<snippetsp_addmergepullsub_websync_anon>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

-- Publication must support anonymous Subscribers.
-- Execute this batch at the Subscriber.
DECLARE @publication AS sysname;
DECLARE @publisher AS sysname;
DECLARE @publicationDB AS sysname;
DECLARE @websyncurl AS sysname;
DECLARE @security_mode AS int;
DECLARE @login AS sysname;
DECLARE @password AS nvarchar(512);
SET @publication = N'AdvWorksSalesOrdersMergeWebSync';
SET @publisher = $(PubServer);
SET @publicationDB = N'AdventureWorks2012';
SET @websyncurl = 'https://' + $(WebServer) + '/WebSync';
SET @security_mode = 0; -- Basic Authentication for IIS
SET @login = $(Login);
SET @password = $(Password);

-- At the subscription database, create a pull subscription 
-- to a merge publication.
USE [AdventureWorks2012Replica]
EXEC sp_addmergepullsubscription 
	@publisher = @publisher, 
	@publication = @publication, 
	@publisher_db = @publicationDB,
	@subscriber_type = N'anonymous';

-- Add an agent job to synchronize the pull subscription. 
EXEC sp_addmergepullsubscription_agent 
	@publisher = @publisher, 
	@publisher_db = @publicationDB, 
	@publication = @publication, 
	@distributor = @publisher, 
	@job_login = @login, 
	@job_password = @password,
	@use_web_sync = 1,
	@internet_security_mode = @security_mode,
	@internet_url = @websyncurl,
	@internet_login = @login,
	@internet_password = @password;
GO
--</snippetsp_addmergepullsub_websync_anon>