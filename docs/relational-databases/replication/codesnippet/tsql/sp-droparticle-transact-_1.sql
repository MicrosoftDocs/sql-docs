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