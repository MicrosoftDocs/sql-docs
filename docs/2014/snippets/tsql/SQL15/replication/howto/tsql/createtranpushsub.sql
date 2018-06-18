--:setvar Login N'REDMOND\glenga'
--:setvar Password N'Rock&Roll'
--:setvar SubServer N'GLENGATEST2'

--------------------------------------------------------------------------
-- Create AdventureWorks2012Replica if not exists
--------------------------------------------------------------------------
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = N'AdventureWorks2012Replica')
BEGIN
	CREATE DATABASE [AdventureWorks2012Replica]
END

--------------------------------------------------------------------------
-- add a transactional push subscription
--------------------------------------------------------------------------
--<snippetsp_addtranpushsubscription_agent>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

DECLARE @publication AS sysname;
DECLARE @subscriber AS sysname;
DECLARE @subscriptionDB AS sysname;
SET @publication = N'AdvWorksProductTran';
SET @subscriber = $(SubServer);
SET @subscriptionDB = N'AdventureWorks2012Replica';

--Add a push subscription to a transactional publication.
USE [AdventureWorks2012]
EXEC sp_addsubscription 
  @publication = @publication, 
  @subscriber = @subscriber, 
  @destination_db = @subscriptionDB, 
  @subscription_type = N'push';

--Add an agent job to synchronize the push subscription.
EXEC sp_addpushsubscription_agent 
  @publication = @publication, 
  @subscriber = @subscriber, 
  @subscriber_db = @subscriptionDB, 
  @job_login = $(Login), 
  @job_password = $(Password);
GO
--</snippetsp_addtranpushsubscription_agent>