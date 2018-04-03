IF NOT EXISTS (SELECT * FROM master..sysdatabases WHERE name='NorthwindReplica')
BEGIN
	CREATE DATABASE NorthwindReplica
END
GO
--<snippetsp_createtranpullsub_NWpreupgrade>
-- Execute at the Subscriber.
DECLARE @publication AS sysname
DECLARE @publisher AS sysname
DECLARE @publicationDB AS sysname
DECLARE @subscriber AS sysname
DECLARE @subscriptionDB AS sysname
SET @publication = N'NwdProductTran' 
SET @publisher = N'PUBSERVER' 
SET @publicationDB = N'Northwind' 
SET @subscriber = N'SUBSERVER' 
SET @subscriptionDB = N'NorthwindReplica' 

-- At the subscription database, create a pull subscription 
-- to a transactional publication.
USE [NorthwindReplica]
EXEC sp_addpullsubscription 
    @publisher = @publisher, 
    @publication = @publication, 
    @publisher_db = @publicationDB

-- Add an agent job to synchronize the pull subscription.
EXEC sp_addpullsubscription_agent 
    @publisher = @publisher, 
    @publisher_db = @publicationDB, 
    @publication = @publication, 
    @subscriber = @subscriber, 
    @subscriber_db = @subscriptionDB, 
    @distributor = @publisher
GO

-- Execute at the Publisher.
DECLARE @publication AS sysname
DECLARE @subscriber AS sysname
DECLARE @subscriptionDB AS sysname
SET @publication = N'NwdProductTran' 
SET @subscriber = N'MYSUBSERVER' 
SET @subscriptionDB = N'NorthwindReplica' 

-- Add a Subscriber, using the defaults.
USE [master]
EXEC sp_addsubscriber 
    @subscriber = @subscriber

-- Add a pull subscription to a transactional publication.
USE [Northwind]
EXEC sp_addsubscription 
    @publication = @publication, 
    @subscriber = @subscriber, 
    @destination_db = @subscriptionDB, 
    @subscription_type = N'pull'
GO
--</snippetsp_createtranpullsub_NWpreupgrade>