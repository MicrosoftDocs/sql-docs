DECLARE @publication AS sysname;
DECLARE @article AS sysname;
SET @publication = 'AdvWorksSalesOrdersMerge';
SET @article = 'Products';

EXEC sp_changemergearticle 
	@publication = @publication, 
	@article = @article, 
	@property='article_resolver', 
	@value='Microsoft SQL Server Additive Conflict Resolver';

EXEC sp_changemergearticle 
	@publication = @publication, 
	@article = @article, 
	@property='resolver_info', 
	@value='UnitsOnOrder';
GO