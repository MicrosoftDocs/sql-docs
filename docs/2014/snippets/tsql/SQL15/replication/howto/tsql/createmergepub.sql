--:setvar Login N'REDMOND\glenga'
--:setvar Password N'Rock&Roll'

--------------------------------------------------------------------------
-- add a merge publication
--------------------------------------------------------------------------
--<snippetsp_AddMergePub>
-- To avoid storing the login and password in the script file, the value 
-- is passed into SQLCMD as a scripting variable. For information about 
-- how to use scripting variables on the command line and in SQL Server
-- Management Studio, see the "Executing Replication Scripts" section in
-- the topic "Programming Replication Using System Stored Procedures".

--Declarations for adding a merge publication
DECLARE @publicationDB AS sysname;
DECLARE @publication AS sysname;
DECLARE @login AS sysname;
DECLARE @password AS sysname;
SET @publicationDB = N'AdventureWorks2012'; 
SET @publication = N'AdvWorksSalesOrdersMerge'; 
SET @login = $(Login);
SET @password = $(Password);

-- Enable merge replication on the publication database, using defaults.
USE master
EXEC sp_replicationdboption 
  @dbname=@publicationDB, 
  @optname=N'merge publish',
  @value = N'true' 

-- Create a new merge publication, explicitly setting the defaults. 
USE [AdventureWorks2012]
EXEC sp_addmergepublication 
-- These parameters are optional.
  @publication = @publication,
  -- optional parameters 
  @description = N'Merge publication of AdventureWorks2012.',
  @publication_compatibility_level  = N'120RTM';

-- Create a new snapshot job for the publication.
EXEC sp_addpublication_snapshot 
  @publication = @publication, 
  @job_login = @login, 
  @job_password = @password;
GO
--</snippetsp_AddMergePub>

--------------------------------------------------------------------------
-- add an article to the publication
--------------------------------------------------------------------------
USE [AdventureWorks2012]
GO

--<snippetsp_AddMergeArticle>
DECLARE @publication AS sysname;
DECLARE @table1 AS sysname;
DECLARE @table2 AS sysname;
DECLARE @table3 AS sysname;
DECLARE @salesschema AS sysname;
DECLARE @hrschema AS sysname;
DECLARE @filterclause AS nvarchar(1000);
SET @publication = N'AdvWorksSalesOrdersMerge'; 
SET @table1 = N'Employee'; 
SET @table2 = N'SalesOrderHeader'; 
SET @table3 = N'SalesOrderDetail'; 
SET @salesschema = N'Sales';
SET @hrschema = N'HumanResources';
SET @filterclause = N'Employee.LoginID = HOST_NAME()';

-- Add a filtered article for the Employee table.
EXEC sp_addmergearticle 
  @publication = @publication, 
  @article = @table1, 
  @source_object = @table1, 
  @type = N'table', 
  @source_owner = @hrschema,
  @schema_option = 0x0004CF1,
  @description = N'article for the Employee table',
  @subset_filterclause = @filterclause;

-- Add an article for the SalesOrderHeader table that is filtered
-- based on Employee and horizontally filtered.
EXEC sp_addmergearticle 
  @publication = @publication, 
  @article = @table2, 
  @source_object = @table2, 
  @type = N'table', 
  @source_owner = @salesschema, 
  @vertical_partition = N'true',
  @schema_option = 0x0034EF1,
  @description = N'article for the SalesOrderDetail table';

-- Add an article for the SalesOrderDetail table that is filtered
-- based on SaledOrderHeader.
EXEC sp_addmergearticle 
  @publication = @publication, 
  @article = @table3, 
  @source_object = @table3, 
  @source_owner = @salesschema,
  @description = 'article for the SalesOrderHeader table', 
  @identityrangemanagementoption = N'auto', 
  @pub_identity_range = 100000, 
  @identity_range = 100, 
  @threshold = 80,
  @schema_option = 0x0004EF1;

-- Add all columns to the SalesOrderHeader article.
EXEC sp_mergearticlecolumn 
  @publication = @publication, 
  @article = @table2, 
  @force_invalidate_snapshot = 1, 
  @force_reinit_subscription = 1;

-- Remove the credit card Approval Code column.
EXEC sp_mergearticlecolumn 
  @publication = @publication, 
  @article = @table2, 
  @column = N'CreditCardApprovalCode', 
  @operation = N'drop', 
  @force_invalidate_snapshot = 1, 
  @force_reinit_subscription = 1;

-- Add a merge join filter between Employee and SalesOrderHeader.
EXEC sp_addmergefilter 
  @publication = @publication, 
  @article = @table2, 
  @filtername = N'SalesOrderHeader_Employee', 
  @join_articlename = @table1, 
  @join_filterclause = N'Employee.BusinessEntityID = SalesOrderHeader.SalesPersonID', 
  @join_unique_key = 1, 
  @filter_type = 1, 
  @force_invalidate_snapshot = 1, 
  @force_reinit_subscription = 1;

-- Add a merge join filter between SalesOrderHeader and SalesOrderDetail.
EXEC sp_addmergefilter 
  @publication = @publication, 
  @article = @table3, 
  @filtername = N'SalesOrderDetail_SalesOrderHeader', 
  @join_articlename = @table2, 
  @join_filterclause = N'SalesOrderHeader.SalesOrderID = SalesOrderDetail.SalesOrderID', 
  @join_unique_key = 1, 
  @filter_type = 1, 
  @force_invalidate_snapshot = 1, 
  @force_reinit_subscription = 1;
GO
--</snippetsp_AddMergeArticle>
