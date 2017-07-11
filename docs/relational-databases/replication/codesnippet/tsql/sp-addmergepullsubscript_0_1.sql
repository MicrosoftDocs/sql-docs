
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