IF NOT EXISTS (SELECT * FROM master..sysdatabases WHERE name='NorthwindReplica')
BEGIN
	CREATE DATABASE NorthwindReplica
END
GO
--<snippetsp_createmergepushsub_NWpreupgrade>
DECLARE @publication AS sysname
DECLARE @subscriber AS sysname
DECLARE @subscriptionDB AS sysname
SET @publication = N'NwdCustomersMerge' 
SET @subscriber = N'SUBSERVER' 
SET @subscriptionDB = N'NorthwindReplica' 

-- Add a Subscriber, using the defaults.
USE [master]
EXEC sp_addsubscriber 
  @subscriber = @subscriber

-- Add a push subscription to a merge publication.
USE [Northwind]
EXEC sp_addmergesubscription 
  @publication = @publication, 
  @subscriber = @subscriber, 
  @subscriber_db = @subscriptionDB, 
  @subscription_type = N'push',
  @subscriber_type = N'local',
  @sync_type = N'automatic'
GO
--</snippetsp_createmergepushsub_NWpreupgrade>