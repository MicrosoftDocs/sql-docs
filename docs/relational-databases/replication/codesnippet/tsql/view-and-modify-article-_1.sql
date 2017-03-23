DECLARE @publication AS sysname;
SET @publication = N'AdvWorksProductTran';

USE [AdventureWorks2012]
EXEC sp_helparticle
  @publication = @publication;
GO