--------------------------------------------------------------------------
-- Drop articles from the publication.
--------------------------------------------------------------------------
DECLARE @myMergePub  AS sysname;
DECLARE @myMergeArt1 AS sysname;
DECLARE @myMergeArt2 AS sysname;
SET @myMergePub = N'AdvWorksSalesPersonMerge'; --merge publication name
SET @myMergeArt1 = N'Employee'; --first table article to remove
SET @myMergeArt2 = N'SalesPerson'; --first table article to remove

-- Remove articles from a merge publication.
USE [AdventureWorks2012];
EXEC sp_dropmergearticle 
  @publication = @myMergePub, 
  @article = 'all',
  @force_invalidate_snapshot = 1;
GO

--------------------------------------------------------------------------
-- Drop a merge publication.
--------------------------------------------------------------------------
DECLARE @myMergePub	AS sysname;
DECLARE @myPubDB	AS sysname;
SET @myMergePub = N'AdvWorksSalesPersonMerge'; --merge publication name
SET @myPubDB = N'AdventureWorks2012';

-- Remove a merge publication.
USE [AdventureWorks2012];
EXEC sp_dropmergepublication 
  @publication = @myMergePub;
/*
-- Remove replication objects from the database.
USE master
EXEC sp_replicationdboption 
  @dbname = @myPubDB, 
  @optname = N'merge publish', 
  @value = N'false';
*/
GO 
