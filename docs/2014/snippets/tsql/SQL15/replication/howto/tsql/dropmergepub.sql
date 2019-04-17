--------------------------------------------------------------------------
-- Drop articles from the publication.
--------------------------------------------------------------------------
--<snippetsp_dropmergearticle>
DECLARE @publication AS sysname;
DECLARE @article1 AS sysname;
DECLARE @article2 AS sysname;
SET @publication = N'AdvWorksSalesOrdersMerge';
SET @article1 = N'SalesOrderDetail'; 
SET @article2 = N'SalesOrderHeader'; 

-- Remove articles from a merge publication.
USE [AdventureWorks]
EXEC sp_dropmergearticle 
  @publication = @publication, 
  @article = @article1,
  @force_invalidate_snapshot = 1;
EXEC sp_dropmergearticle 
  @publication = @publication, 
  @article = @article2,
  @force_invalidate_snapshot = 1;
GO
--</snippetsp_dropmergearticle>

--------------------------------------------------------------------------
-- Drop a merge publication.
--------------------------------------------------------------------------
--<snippetsp_dropmergepublication>
DECLARE @publication AS sysname
DECLARE @publicationDB    AS sysname
SET @publication = N'AdvWorksSalesOrdersMerge' 
SET @publicationDB = N'AdventureWorks'

-- Remove the merge publication.
USE [AdventureWorks]
EXEC sp_dropmergepublication @publication = @publication;

-- Remove replication objects from the database.
USE master
EXEC sp_replicationdboption 
  @dbname = @publicationDB, 
  @optname = N'merge publish', 
  @value = N'false'
GO
--</snippetsp_dropmergepublication>
