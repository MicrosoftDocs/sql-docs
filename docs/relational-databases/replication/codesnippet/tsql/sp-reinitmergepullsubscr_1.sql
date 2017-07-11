-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
SET @publicationDB = N'AdventureWorks2012';
SET @publication = N'AdvWorksSalesOrdersMerge';

USE [AdventureWorks2012Replica]

-- Execute at the Subscriber to reinitialize the pull subscription. 
-- Pending changes at the Subscrber are lost.
EXEC sp_reinitmergepullsubscription 
    @publisher = $(PubServer),
    @publisher_db = @publicationDB,
    @publication = @publication,
    @upload_first = N'false';
GO

-- Start the Merge Agent.
