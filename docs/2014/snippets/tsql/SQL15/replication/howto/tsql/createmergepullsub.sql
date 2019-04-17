--:setvar Login N'REDMOND\glenga'
--:setvar Password N'Rock&Roll'
--:setvar PubServer N'GLENGATEST2'
--:setvar SubServer N'GLENGATEST2'

--------------------------------------------------------------------------
-- Create AdventureWorks2012Replica if not exists
--------------------------------------------------------------------------
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = N'AdventureWorks2012Replica')
BEGIN
	CREATE DATABASE [AdventureWorks2012Replica]
END

--------------------------------------------------------------------------
-- add a merge pull subscription
--------------------------------------------------------------------------
--<snippetsp_addmergepullsubscriptionagent>

-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

-- Execute this batch at the Subscriber.
DECLARE @publication AS sysname;
DECLARE @publisher AS sysname;
DECLARE @publicationDB AS sysname;
DECLARE @hostname AS sysname;
SET @publication = N'AdvWorksSalesOrdersMerge';
SET @publisher = $(PubServer);
SET @publicationDB = N'AdventureWorks2012';
SET @hostname = N'adventure-works\david8';

-- At the subscription database, create a pull subscription 
-- to a merge publication.
USE [AdventureWorks2012Replica]
EXEC sp_addmergepullsubscription 
  @publisher = @publisher, 
  @publication = @publication, 
  @publisher_db = @publicationDB;

-- Add an agent job to synchronize the pull subscription. 
EXEC sp_addmergepullsubscription_agent 
  @publisher = @publisher, 
  @publisher_db = @publicationDB, 
  @publication = @publication, 
  @distributor = @publisher, 
  @job_login = $(Login), 
  @job_password = $(Password),
  @hostname = @hostname;
GO
--</snippetsp_addmergepullsubscriptionagent>

--<snippetsp_addmergepullsubscription>
-- Execute this batch at the Publisher.
DECLARE @myMergePub  AS sysname;
DECLARE @mySub       AS sysname;
DECLARE @mySubDB     AS sysname;

SET @myMergePub = N'AdvWorksSalesOrdersMerge';
SET @mySub = N'MYSUBSERVER';
SET @mySubDB = N'AdventureWorks2012Replica';

-- At the Publisher, register the subscription, using the defaults.
USE [AdventureWorks2012]
EXEC sp_addmergesubscription @publication = @myMergePub, 
@subscriber = @mySub, @subscriber_db = @mySubDB, 
@subscription_type = N'pull';
GO
--</snippetsp_addmergepullsubscription>
