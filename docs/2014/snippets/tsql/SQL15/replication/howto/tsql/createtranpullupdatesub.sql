--:setvar Login N'REDMOND\glenga'
--:setvar Password N'Love&War'
--:setvar Instance N'GLENGATEST2'

--------------------------------------------------------------------------
-- add an updating transactional pull subscription
--------------------------------------------------------------------------
IF NOT EXISTS(SELECT [name] FROM sys.databases WHERE name = 'AdventureWorks2012Replica')
BEGIN
	CREATE DATABASE AdventureWorks2012Replica
END
GO

USE AdventureWorks2012Replica
GO

--<snippetsp_addtranpullsubscriptionagent_failover>
-- This script uses sqlcmd scripting variables. They are in the form
-- $(MyVariable). For information about how to use scripting variables  
-- on the command line and in SQL Server Management Studio, see the 
-- "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".

-- Execute this batch at the Subscriber.
DECLARE @publication AS sysname;
DECLARE @publicationDB AS sysname;
DECLARE @publisher AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS nvarchar(512);
SET @publication = N'AdvWorksProductTran';
SET @publicationDB = N'AdventureWorks2012';
SET @publisher = $(PubServer);
SET @login = $(Login);
SET @password = $(Password);

-- At the subscription database, create a pull subscription to a transactional 
-- publication using immediate updating with queued updating as a failover.
EXEC sp_addpullsubscription 
    @publisher = @publisher, 
    @publication = @publication, 
    @publisher_db = @publicationDB, 
    @update_mode = N'failover', 
	@subscription_type = N'pull';

-- Add an agent job to synchronize the pull subscription, 
-- which uses Windows Authentication when connecting to the Distributor.
EXEC sp_addpullsubscription_agent 
    @publisher = @publisher, 
    @publisher_db = @publicationDB, 
    @publication = @publication,
    @job_login = @login,
    @job_password = @password; 
 
-- Add a Windows Authentication-based linked server that enables the 
-- Subscriber-side triggers to make updates at the Publisher. 
EXEC sp_link_publication 
    @publisher = @publisher, 
    @publication = @publication,
    @publisher_db = @publicationDB, 
    @security_mode = 0,
    @login = @login,
    @password = @password;
GO

USE AdventureWorks2012
GO

-- Execute this batch at the Publisher.
DECLARE @publication AS sysname;
DECLARE @subscriptionDB AS sysname;
DECLARE @subscriber AS sysname;
SET @publication = N'AdvWorksProductTran'; 
SET @subscriptionDB = N'AdventureWorks2012Replica'; 
SET @subscriber = $(SubServer);

-- At the Publisher, register the subscription, using the defaults.
USE [AdventureWorks2012]
EXEC sp_addsubscription 
	@publication = @publication, 
	@subscriber = @subscriber, 
	@destination_db = @subscriptionDB, 
	@subscription_type = N'pull', 
	@update_mode = N'failover';
GO
--</snippetsp_addtranpullsubscriptionagent_failover>

/*-- Start the snapshot job
DECLARE @publication AS sysname;
SET @publication = N'AdvWorksProductTran';

EXEC sp_startpublication_snapshot @publication;
GO
*/

WAIT DELAY '00:00:10';
GO