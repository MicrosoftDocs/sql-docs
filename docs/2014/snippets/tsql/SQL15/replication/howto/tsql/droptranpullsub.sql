--:setvar PubServer N'GLENGATEST2'
--:setvar SubServer N'GLENGATEST2'
USE AdventureWorks2012Replica
GO
--------------------------------------------------------------------------
-- remove a transactional pull subscription
--------------------------------------------------------------------------
--<snippetsp_droptranpullsubscription>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

-- This is the batch executed at the Subscriber to drop 
-- a pull subscription to a transactional publication.
DECLARE @publication AS sysname;
DECLARE @publisher AS sysname;
DECLARE @publicationDB     AS sysname;
SET @publication = N'AdvWorksProductTran';
SET @publisher = $(PubServer);
SET @publicationDB = N'AdventureWorks2012';

USE [AdventureWorks2012Replica]
EXEC sp_droppullsubscription 
  @publisher = @publisher, 
  @publisher_db = @publicationDB, 
  @publication = @publication;
GO
--</snippetsp_droptranpullsubscription>
USE AdventureWorks2012
GO
--<snippetsp_droptransubscription>
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
--</snippetsp_droptransubscription>

