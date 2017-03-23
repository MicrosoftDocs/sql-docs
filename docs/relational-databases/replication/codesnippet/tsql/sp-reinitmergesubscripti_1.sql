-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

DECLARE @subscriptionDB AS sysname;
DECLARE @publication AS sysname;
SET @subscriptionDB = N'AdventureWorks2012Replica';
SET @publication = N'AdvWorksSalesOrdersMerge';

USE [AdventureWorks2012Replica]

-- Execute at the Publisher to reinitialize the push subscription. 
-- Pending changes at the Subscrber are lost.
EXEC sp_reinitmergesubscription 
    @subscriber = $(SubServer),
    @subscriber_db = @subscriptionDB,
    @publication = @publication,
    @upload_first = N'false';
GO

-- Start the Merge Agent.
