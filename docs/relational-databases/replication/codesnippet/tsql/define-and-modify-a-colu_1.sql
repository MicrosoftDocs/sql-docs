DECLARE @publication    AS sysname;
DECLARE @table AS sysname;
DECLARE @filterclause AS nvarchar(500);
DECLARE @filtername AS nvarchar(386);
DECLARE @schemaowner AS sysname;
SET @publication = N'AdvWorksProductTran'; 
SET @table = N'Product';
SET @filterclause = N'[DiscontinuedDate] IS NULL'; 
SET @filtername = N'filter_out_discontinued';
SET @schemaowner = N'Production';

-- Add a horizontally and vertically filtered article for the Product table.
-- Manually set @schema_option to ensure that the Production schema 
-- is generated at the Subscriber (0x8000000).
EXEC sp_addarticle 
	@publication = @publication, 
	@article = @table, 
	@source_object = @table,
	@source_owner = @schemaowner, 
	@schema_option = 0x80030F3,
	@vertical_partition = N'true', 
	@type = N'logbased',
	@filter_clause = @filterclause;

-- (Optional) Manually call the stored procedure to create the 
-- horizontal filtering stored procedure. Since the type is 
-- 'logbased', this stored procedures is executed automatically.
EXEC sp_articlefilter 
	@publication = @publication, 
	@article = @table, 
	@filter_clause = @filterclause, 
	@filter_name = @filtername;

-- Add all columns to the article.
EXEC sp_articlecolumn 
	@publication = @publication, 
	@article = @table;

-- Remove the DaysToManufacture column from the article
EXEC sp_articlecolumn 
	@publication = @publication, 
	@article = @table, 
	@column = N'DaysToManufacture', 
	@operation = N'drop';

-- (Optional) Manually call the stored procedure to create the 
-- vertical filtering view. Since the type is 'logbased', 
-- this stored procedures is executed automatically.
EXEC sp_articleview 
	@publication = @publication, 
	@article = @table,
	@filter_clause = @filterclause;
GO