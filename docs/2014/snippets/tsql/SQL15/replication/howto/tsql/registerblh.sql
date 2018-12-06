--<snippetsp_registerBLH>
DECLARE @publication AS sysname;
DECLARE @article AS sysname;
DECLARE @friendlyname AS sysname;
DECLARE @assembly AS nvarchar(500);
DECLARE @class AS sysname;
SET @publication = N'AdvWorksCustomers';
SET @article = N'Customers';
SET @friendlyname = N'OrderEntryLogic';
SET @assembly = N'C:\Program Files\Microsoft SQL Server\90\COM\CustomLogic.dll';
SET @class = N'Microsoft.Samples.SqlServer.BusinessLogicHandler.OrderEntryBusinessLogicHandler';

-- Register the business logic handler at the Distributor.
EXEC sys.sp_registercustomresolver 
	@article_resolver = @friendlyname,
	@resolver_clsid = NULL,
	@is_dotnet_assembly = N'true',
	@dotnet_assembly_name = @assembly,
	@dotnet_class_name = @class;

-- Add an article that uses the business logic handler
-- at the Publisher.
EXEC sp_changemergearticle 
	@publication = @publication, 
	@article = @article,
	@property = N'article_resolver', 
	@value = @friendlyname,
	@force_invalidate_snapshot = 0,
	@force_reinit_subscription = 0;
GO
--</snippetsp_registerBLH>