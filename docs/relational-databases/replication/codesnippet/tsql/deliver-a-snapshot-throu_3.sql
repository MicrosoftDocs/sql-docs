-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

-- Execute this batch at the Subscriber.
DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
DECLARE @publisher AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS sysname;
DECLARE @subscriber AS sysname;
DECLARE @subscriptionDB AS sysname;
SET @publicationDB = N'AdventureWorks'; 
SET @publication = N'AdvWorksSalesOfferMergeFtp'; 
SET @publisher = $(PubServer);
SET @login = $(Login);
SET @password = $(Password);
SET @subscriber = $(SubServer);
SET @subscriptionDB = N'AdventureWorksReplica';

EXEC sp_addmergepullsubscription 
	@publisher = @publisher, 
	@publication = @publication, 
	@publisher_db = @publicationDB, 
	@subscriber_type = N'Local', 
	@subscription_priority = 0, 
	@sync_type = N'Automatic';

exec sp_addmergepullsubscription_agent 
	@publisher = @publisher, 
	@publisher_db = @publicationDB, 
	@publication = @publication, 
	@distributor = @publisher, 
	@distributor_security_mode = 1, 
	@use_ftp = N'true', 
	@job_login = @login, 
	@job_password = @password, 
	@publisher_security_mode = 1, 
	@use_web_sync = 0;
GO