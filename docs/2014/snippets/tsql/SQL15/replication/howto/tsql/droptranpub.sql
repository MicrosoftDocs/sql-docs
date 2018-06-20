--------------------------------------------------------------------------
-- Drop transactional article
--------------------------------------------------------------------------
--Declarations for removing an article.
--<snippetsp_droparticle>
DECLARE @publication AS sysname;
DECLARE @article AS sysname;
SET @publication = N'AdvWorksProductTran'; 
SET @article = N'Product'; 

-- Drop the transactional article.
USE [AdventureWorks2012]
EXEC sp_droparticle 
  @publication = @publication, 
  @article = @article,
  @force_invalidate_snapshot = 1;
GO
--</snippetsp_droparticle>

--------------------------------------------------------------------------
-- Drop transactional publication
--------------------------------------------------------------------------
--<snippetsp_droppublication>
DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
SET @publicationDB = N'AdventureWorks'; 
SET @publication = N'AdvWorksProductTran'; 

-- Remove a transactional publication.
USE [AdventureWorks2012]
EXEC sp_droppublication @publication = @publication;

-- Remove replication objects from the database.
USE [master]
EXEC sp_replicationdboption 
  @dbname = @publicationDB, 
  @optname = N'publish', 
  @value = N'false';
GO
--</snippetsp_droppublication>