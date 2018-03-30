--------------------------------------------------------------------------
-- change a transactional publication
--------------------------------------------------------------------------
--Declarations for changing setting for the AdvWorksProductTran publication.
--<snippetsp_changepublication>
DECLARE @publication AS sysname
SET @publication = N'AdvWorksProductTran' 

-- Turn off DDL replication for the transactional publication.
USE [AdventureWorks2012]
EXEC sp_changepublication 
  @publication = @publication, 
  @property = N'replicate_ddl', 
  @value = 0
GO
--</snippetsp_changepublication>

--------------------------------------------------------------------------
-- view the current settings for a transactional publication
--------------------------------------------------------------------------
-- To return the current publication properties for the 
-- AdvWorksProductTran publication.
--<snippetsp_helppublication>
DECLARE @myTranPub AS sysname
SET @myTranPub = N'AdvWorksProductTran' 

USE [AdventureWorks2012]
EXEC sp_helppublication @publication = @myTranPub
GO
--</snippetsp_helppublication>