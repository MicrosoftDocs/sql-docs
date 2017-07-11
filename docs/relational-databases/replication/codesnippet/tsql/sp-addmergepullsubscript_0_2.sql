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