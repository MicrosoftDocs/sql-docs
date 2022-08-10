--- Add the article to the publication.
DECLARE @publication AS sysname;
DECLARE @newtable AS sysname;
SET @publication = N'AdvWorksProductTran';
SET @newtable = N'ProductTest';

USE AdventureWorks2012

EXEC sp_addarticle 
  @publication = @publication,
  @article = @newtable,
  @source_object = @newtable,
  @destination_table = @newtable,
  @force_invalidate_snapshot = 0;
GO
