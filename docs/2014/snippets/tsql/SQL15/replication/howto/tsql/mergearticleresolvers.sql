--<snippetsp_addmerge_resolver>
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
--</snippetsp_addmerge_resolver>

--<snippetsp_changemerge_resolver>
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
--</snippetsp_changemerge_resolver>
