:setvar Login N'REDMOND\glenga'
:setvar Password N'Love&War'
:setvar Publisher N'glengatest2'
:setvar Subscriber N'glengatest2'
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'NorthwindReplica')
BEGIN
	CREATE DATABASE NorthwindReplica;
END
GO

--<snippetsp_createtranpushsub_NWpostupgrade>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

DECLARE @publication AS sysname;
DECLARE @subscriber AS sysname;
DECLARE @subscriptionDB AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS sysname;
SET @publication = N'NwdProductTran'; 
SET @subscriber = $(Subscriber); 
SET @subscriptionDB = N'NorthwindReplica'; 
-- Specify the Windows account to run the Distribution Agent.
SET @login = $(Login); 
-- Supply the password at runtime.
SET @password = $(Password); 

-- Add a push subscription to a transactional publication.
USE [Northwind]
EXEC sp_addsubscription 
	@publication = @publication, 
	@subscriber = @subscriber, 
	@destination_db = @subscriptionDB, 
	@subscription_type = N'push';

-- Add an agent job to synchronize the push subscription.
EXEC sp_addpushsubscription_agent 
	@publication = @publication, 
	@subscriber = @subscriber, 
	@subscriber_db = @subscriptionDB, 
	@job_login = @login, 
	@job_password = @password;
GO
--</snippetsp_createtranpushsub_NWpostupgrade>


--<snippetsp_createmergepushsub_NWpostupgrade>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

DECLARE @publication AS sysname;
DECLARE @subscriber AS sysname;
DECLARE @subscriptionDB AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS sysname;
SET @publication = N'NwdCustomersMerge'; 
SET @subscriber = $(Subscriber); 
SET @subscriptionDB = N'NorthwindReplica'; 
-- Specify the Windows account to run the Merge Agent.
SET @login = $(Login); 
-- Supply the password at runtime.
SET @password = $(Password); 

-- Add a push subscription to a merge publication.
USE [Northwind]
EXEC sp_addmergesubscription 
	@publication = @publication, 
	@subscriber = @subscriber, 
	@subscriber_db = @subscriptionDB, 
	@subscription_type = N'push';

-- Add an agent job to synchronize the push subscription.
EXEC sp_addmergepushsubscription_agent 
	@publication = @publication, 
	@subscriber = @subscriber, 
	@subscriber_db = @subscriptionDB, 
	@job_login = @login, 
	@job_password = @password;
GO
--</snippetsp_createmergepushsub_NWpostupgrade>
