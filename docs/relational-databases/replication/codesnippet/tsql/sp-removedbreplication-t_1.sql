-- Remove replication objects from the subscription database on MYSUB.
DECLARE @subscriptionDB AS sysname
SET @subscriptionDB = N'AdventureWorks2012Replica'

-- Remove replication objects from a subscription database (if necessary).
USE master
EXEC sp_removedbreplication @subscriptionDB
GO