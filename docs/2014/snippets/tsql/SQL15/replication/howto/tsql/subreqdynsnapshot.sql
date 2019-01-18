--Add a new merge publication.
DECLARE @myPubDB AS sysname;
DECLARE @myMergePub AS sysname;
DECLARE @myMergeTable1 AS sysname;
DECLARE @myMergeTable2 AS sysname;
DECLARE @myMergeFilter AS sysname;
DECLARE @schemaOwner1 AS sysname;
DECLARE @schemaOwner2 AS sysname;

SET @myPubDB = N'AdventureWorks2012'; --publication database
SET @myMergePub = N'AdvWorksSalesPersonMerge'; -- merge publication name
SET @myMergeTable1 = N'Employee'; --first table article to add
SET @myMergeTable2 = N'SalesPerson'; --second table article to add
SET @myMergeFilter = N'SalesPerson_Employee'; -- join filter name
SET @schemaOwner1 = N'HumanResources'; -- schema to which the HR tables belong
SET @schemaOwner2 = N'Sales'; -- schema to which Sales tables belong

-- Create new merge publication with Subscriber requested snapshot
-- and using the default agent schedule. 
USE [AdventureWorks2012];

IF (DATABASEPROPERTYEX(@myPubDB,N'IsMergePublished') = 0)
BEGIN 
    EXEC sp_replicationdboption @dbname=@myPubDB, @optname=N'merge publish',
    @value = N'true'
END
EXEC sp_addmergepublication @publication = @myMergePub, 
@description = N'Merge publication of AdventureWorks2012 from Publisher MYDISTPUB.', 
@allow_subscriber_initiated_snapshot = N'true', @max_concurrent_dynamic_snapshots = 10, 
@dynamic_snapshot_ready_timeout = 30, @add_publication_snapshot = N'true';

-- Add an article for the Employee table, which is horizontally partitioned.
EXEC sp_addmergearticle @publication = @myMergePub, @article = @myMergeTable1, 
@source_owner = @schemaOwner1, @source_object = @myMergeTable1, @type = N'table', 
@description = 'contains employee information', @subset_filterclause = N'[LoginID] = HOST_NAME()'

-- Add an article for the SalesPerson table, which is partitioned based on a join filter.
EXEC sp_addmergearticle @publication = @myMergePub, @article = @myMergeTable2, 
@source_owner = @schemaOwner2, @source_object = @myMergeTable2, @type = N'table', 
@description = 'contains customer information'

-- Add a join filter to horizontally partition SalesPerson based on Employee, 
-- which is filtered by HOST_NAME().
EXEC sp_addmergefilter @publication = @myMergePub, @article = @myMergeTable1, 
@filtername = @myMergeFilter, @join_articlename = @myMergeTable2, 
@join_filterclause = N'[Employee].[EmployeeID] = [SalesPerson].[SalesPersonID]', 
@join_unique_key = 1, @filter_type = 1, @force_invalidate_snapshot = 1, 
@force_reinit_subscription = 1;

GO

-- Start the snapshot agent by running the agent job
DECLARE @myPubSvr AS sysname;
DECLARE @myPubDB AS sysname;
DECLARE @myMergePub AS sysname;
DECLARE @jobName AS sysname;
SET @myPubSvr = CAST(SERVERPROPERTY('ServerName') AS sysname);
SET @myPubDB = N'AdventureWorks2012';
SET @myMergePub = N'AdvWorksSalesPersonMerge';

-- Get the snapshot job information needed to start the job.
SET @jobName = (SELECT TOP 1 name FROM msdb..sysjobs WHERE name LIKE 
@myPubSvr + '-' + @myPubDB + '-' + @myMergePub + '-%')

EXEC msdb..sp_start_job @job_name = @jobName, @server_name = @myPubSvr;
GO