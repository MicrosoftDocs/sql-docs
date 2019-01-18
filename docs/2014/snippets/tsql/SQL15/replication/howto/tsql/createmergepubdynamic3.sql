:setvar Login N'REDMOND\glenga'
:setvar Password N'Rock&Roll'

--<snippetsp_MergeDynamicPubPartitionManual>
-- To avoid storing the login and password in the script file, the value 
-- is passed into SQLCMD as a scripting variable. For information about 
-- how to use scripting variables on the command line and in SQL Server
-- Management Studio, see the "Executing Replication Scripts" section in
-- the topic "Programming Replication Using System Stored Procedures".

--Add a new merge publication.
DECLARE @publicationdb AS sysname;
DECLARE @publication AS sysname;
DECLARE @table1 AS sysname;
DECLARE @table2 AS sysname;
DECLARE @filter AS sysname;
DECLARE @schema_hr AS sysname;
DECLARE @schema_sales AS sysname;

SET @publicationdb = N'AdventureWorks2012';
SET @publication = N'AdvWorksSalesPersonMerge';
SET @table1 = N'Employee';
SET @table2 = N'SalesPerson';
SET @filter = N'SalesPerson_Employee';
SET @schema_hr = N'HumanResources';
SET @schema_sales = N'Sales';

USE [AdventureWorks2012];

-- Enable AdventureWorks2012 for merge replication.
EXEC sp_replicationdboption
  @dbname = @publicationdb,
  @optname = N'merge publish',
  @value = N'true';  

-- Create new merge publication.  
EXEC sp_addmergepublication 
  @publication = @publication, 
  @description = N'Merge publication of AdventureWorks2012.', 
  @allow_subscriber_initiated_snapshot = N'false';

-- Create a new snapshot job for the publication, using the 
-- default schedule. Pass credentials at runtime using 
-- sqlcmd scripting variables.
EXEC sp_addpublication_snapshot 
  @publication = @publication, 
  @job_login = $(Login), 
  @job_password = $(Password);

-- Add an article for the Employee table, 
-- which is horizontally partitioned using 
-- a parameterized row filter.
EXEC sp_addmergearticle 
  @publication = @publication, 
  @article = @table1, 
  @source_owner = @schema_hr, 
  @source_object = @table1, 
  @type = N'table', 
  @description = 'contains employee information', 
  @subset_filterclause = N'[LoginID] = HOST_NAME()';

-- Add an article for the SalesPerson table, 
-- which is partitioned based on a join filter.
EXEC sp_addmergearticle 
  @publication = @publication, 
  @article = @table2, 
  @source_owner = @schema_sales, 
  @source_object = @table2, 
  @type = N'table', 
  @description = 'contains customer information';

-- Add a join filter between the two articles.
EXEC sp_addmergefilter 
  @publication = @publication, 
  @article = @table1, 
  @filtername = @filter, 
  @join_articlename = @table2, 
  @join_filterclause = N'[Employee].[BusinessEntityID] = [SalesPerson].[SalesPersonID]', 
  @join_unique_key = 1, 
  @filter_type = 1;
GO
--</snippetsp_MergeDynamicPubPartitionManual>
