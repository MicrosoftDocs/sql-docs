DECLARE @publication AS sysname;
SET @publication = N'AdvWorksSalesOrdersMerge';

USE [AdventureWorks2022]
EXEC sp_helpmergearticle
  @publication = @publication;
GO