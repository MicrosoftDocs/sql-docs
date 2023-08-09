DECLARE @publication AS sysname;
SET @publication = N'AdvWorksSalesOrdersMerge';

USE [AdventureWorks2022]
EXEC sp_helpmergepublication @publication = @publication;
GO