--:setvar Login N'REDMOND\glenga'
--:setvar Password N'Love&War'
USE [AdventureWorks]
GO

--<snippetsp_AddTranPub>
-- To avoid storing the login and password in the script file, the values 
-- are passed into SQLCMD as scripting variables. For information about 
-- how to use scripting variables on the command line and in SQL Server
-- Management Studio, see the "Executing Replication Scripts" section in
-- the topic "Programming Replication Using System Stored Procedures".

DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS sysname;
SET @publicationDB = N'AdventureWorks'; 
SET @publication = N'AdvWorksProductTran'; 
-- Windows account used to run the Log Reader and Snapshot Agents.
SET @login = $(Login); 
-- This should be passed at runtime.
SET @password = $(Password); 

-- Enable transactional or snapshot replication on the publication database.
EXEC sp_replicationdboption 
	@dbname=@publicationDB, 
	@optname=N'publish',
	@value = N'true';

-- Execute sp_addlogreader_agent to create the agent job. 
EXEC sp_addlogreader_agent 
	@job_login = @login, 
	@job_password = @password,
	-- Explicitly specify the use of Windows Integrated Authentication (default) 
	-- when connecting to the Publisher.
	@publisher_security_mode = 1;

-- Create a new transactional publication with the required properties. 
EXEC sp_addpublication 
	@publication = @publication, 
	@status = N'active',
	@allow_push = N'true',
	@allow_pull = N'true',
	@independent_agent = N'true';

-- Create a new snapshot job for the publication, using a default schedule.
EXEC sp_addpublication_snapshot 
	@publication = @publication, 
	@job_login = @login, 
	@job_password = @password,
	-- Explicitly specify the use of Windows Integrated Authentication (default) 
	-- when connecting to the Publisher.
	@publisher_security_mode = 1;
GO
--</snippetsp_AddTranPub>

--------------------------------------------------------------------------
-- add an article to the publication
--------------------------------------------------------------------------
USE [AdventureWorks]
GO
--<snippetsp_AddTranArticle>
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
--</snippetsp_AddTranArticle>