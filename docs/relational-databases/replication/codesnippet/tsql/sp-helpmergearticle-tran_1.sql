DECLARE @publication AS sysname;
SET @publication = N'AdvWorksSalesOrdersMerge';

USE [AdventureWorks2012]
EXEC sp_helpmergearticle
  @publication = @publication;
GO