DECLARE @publication AS sysname;
SET @publication = N'AdvWorksSalesOrdersMerge';

USE [AdventureWorks2012]
EXEC sp_helpmergepublication @publication = @publication;
GO