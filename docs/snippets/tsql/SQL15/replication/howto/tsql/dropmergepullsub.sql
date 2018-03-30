--:setvar PubServer N'GLENGATEST2'
--:setvar SubServer N'GLENGATEST2'

--------------------------------------------------------------------------
-- remove a merge pull subscription
--------------------------------------------------------------------------
--<snippetsp_dropmergepullsubscription>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

-- This batch is executed at the Subscriber to remove 
-- a merge pull subscription.
DECLARE @publication AS sysname;
DECLARE @publisher AS sysname;
DECLARE @publication_db AS sysname;
SET @publication = N'AdvWorksSalesOrdersMerge';
SET @publisher = $(PubServer);
SET @publication_db = N'AdventureWorks2012';

USE [AdventureWorks2012Replica]
EXEC sp_dropmergepullsubscription 
  @publisher = @publisher, 
  @publisher_db = @publication_db, 
  @publication = @publication;
GO
--</snippetsp_dropmergepullsubscription>

--<snippetsp_dropmergesubscription>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

-- This batch is executed at the Publisher to remove 
-- a pull or push subscription to a merge publication.
DECLARE @publication AS sysname;
DECLARE @subscriber AS sysname;
DECLARE @subscriptionDB AS sysname;
SET @publication = N'AdvWorksSalesOrdersMerge';
SET @subscriber = $(SubServer);
SET @subscriptionDB = N'AdventureWorks2012Replica';

USE [AdventureWorks2012]
EXEC sp_dropmergesubscription 
  @publication = @publication, 
  @subscriber = @subscriber, 
  @subscriber_db = @subscriptionDB;
GO
--</snippetsp_dropmergesubscription>
