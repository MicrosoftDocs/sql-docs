DECLARE @publication AS sysname;
SET @publication = N'AdvWorksSalesOrdersMerge'; 

-- Disable DDL replication for the publication.
USE [AdventureWorks2012]
EXEC sp_changemergepublication 
  @publication = @publication, 
  @property = N'replicate_ddl', 
  @value = 0,
  @force_invalidate_snapshot = 0, 
  @force_reinit_subscription = 0;
GO