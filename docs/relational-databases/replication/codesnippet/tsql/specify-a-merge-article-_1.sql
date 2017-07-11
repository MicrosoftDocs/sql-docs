DECLARE @publication AS sysname;
DECLARE @article AS sysname;
SET @publication = 'AdvWorksSalesOrdersMerge';
SET @article = 'Products';

EXEC sp_addmergearticle 
	@publication = @publication, 
	@article = @article, 
	@source_object = @article, 
	@article_resolver = 'Microsoft SQL Server Averaging Conflict Resolver', 
	@resolver_info = 'UnitPrice';
GO