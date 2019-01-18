--:setvar Login N'REDMOND\glenga'
--:setvar Password N'Rock&Roll'
--:setvar SubServer N'GLENGATEST2'

--------------------------------------------------------------------------
-- Create AdventureWorksReplica if not exists
--------------------------------------------------------------------------
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = N'AdventureWorksReplica')
BEGIN
	CREATE DATABASE [AdventureWorksReplica]
END
GO

--------------------------------------------------------------------------
-- add a merge push subscription
--------------------------------------------------------------------------
--<snippetsp_addmergepushsubscriptionagent>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

DECLARE @publication AS sysname;
DECLARE @subscriber AS sysname;
DECLARE @subscriptionDB AS sysname;
DECLARE @hostname AS sysname;
SET @publication = N'AdvWorksSalesOrdersMerge';
SET @subscriber = $(SubServer);
SET @subscriptionDB = N'AdventureWorksReplica'; 
SET @hostname = N'adventure-works\david8'

-- Add a push subscription to a merge publication.
USE [AdventureWorks2012];
EXEC sp_addmergesubscription 
  @publication = @publication, 
  @subscriber = @subscriber, 
  @subscriber_db = @subscriptionDB, 
  @subscription_type = N'push',
  @hostname = @hostname;

--Add an agent job to synchronize the push subscription.
EXEC sp_addmergepushsubscription_agent 
  @publication = @publication, 
  @subscriber = @subscriber, 
  @subscriber_db = @subscriptionDB, 
  @job_login = $(Login), 
  @job_password = $(Password);
GO
--</snippetsp_addmergepushsubscriptionagent>
