DECLARE @publication AS sysname;
SET @publication = N'AdvWorksProductTran';

USE [AdventureWorks2022]
EXEC sp_helparticle
  @publication = @publication;
GO