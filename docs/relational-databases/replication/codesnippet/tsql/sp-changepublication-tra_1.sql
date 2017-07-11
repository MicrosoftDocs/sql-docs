DECLARE @publication AS sysname
SET @publication = N'AdvWorksProductTran' 

-- Turn off DDL replication for the transactional publication.
USE [AdventureWorks2012]
EXEC sp_changepublication 
  @publication = @publication, 
  @property = N'replicate_ddl', 
  @value = 0
GO