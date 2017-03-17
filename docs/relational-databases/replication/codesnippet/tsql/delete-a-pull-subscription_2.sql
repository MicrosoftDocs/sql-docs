-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

-- This batch is executed at the Publisher to remove 
-- a pull or push subscription to a transactional publication.
DECLARE @publication AS sysname;
DECLARE @subscriber AS sysname;
SET @publication = N'AdvWorksProductTran';
SET @subscriber = $(SubServer);

USE [AdventureWorks2012]
EXEC sp_dropsubscription 
  @publication = @publication, 
  @article = N'all',
  @subscriber = @subscriber;
GO