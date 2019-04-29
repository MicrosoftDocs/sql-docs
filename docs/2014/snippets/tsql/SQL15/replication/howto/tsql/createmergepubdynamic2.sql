:setvar Login N'REDMOND\glenga'
:setvar Password N'Love&War'

--<snippetsp_MergeDynamicPubPlusPartition>
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
  @job_password = $(password);

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

-- Start the snapshot agent job.
DECLARE @publication AS sysname;
SET @publication = N'AdvWorksSalesPersonMerge';

EXEC sp_startpublication_snapshot 
  @publication = @publication;
GO

PRINT '*** Waiting for the initial snapshot.';
GO

-- Create a temporary table to store the filtered data snapshot 
-- job information.
CREATE TABLE #temp (id int,
	job_name sysname,
	job_id uniqueidentifier,
	dynamic_filter_login sysname NULL,
	dynamic_filter_hostname sysname NULL,
	dynamic_snapshot_location nvarchar(255),
	frequency_type int, 
	frequency_interval int, 
	frequency_subday_type int,
	frequency_subday_interval int, 
	frequency_relative_interval int, 
	frequency_recurrence_factor int, 
	active_start_date int, 
	active_end_date int, 
	active_start_time int, 
	active_end_time int
)

-- Create each snapshot for a partition 
-- The initial snapshot must already be generated.
DECLARE @publication AS sysname;
DECLARE @jobname AS sysname
DECLARE @hostname AS sysname
SET @publication = N'AdvWorksSalesPersonMerge';
SET @hostname = N'adventure-works\Fernando';

WHILE NOT EXISTS(SELECT * FROM sysmergepublications 
    WHERE [name] = @publication 
    AND snapshot_ready = 1)
BEGIN
    WAITFOR DELAY '00:00:05'
END

-- Create a data partition by overriding HOST_NAME().
EXEC sp_addmergepartition 
  @publication = @publication,
  @host_name = @hostname;

-- Create the filtered data snapshot job, and use the returned 
-- information to start the job.
EXEC sp_adddynamicsnapshot_job 
  @publication = @publication,
  @host_name = @hostname;

INSERT INTO #temp (id, job_name, job_id, dynamic_filter_login,
	dynamic_filter_hostname, dynamic_snapshot_location,
	frequency_type,	frequency_interval, frequency_subday_type,
	frequency_subday_interval, frequency_relative_interval, 
	frequency_recurrence_factor, active_start_date,	active_end_date, 
	active_start_time,active_end_time)
EXEC sp_helpdynamicsnapshot_job;

SELECT @jobname = (SELECT DISTINCT job_name FROM #temp WHERE dynamic_filter_hostname = @hostname);

EXEC msdb..sp_start_job @job_name = @jobname;
DROP TABLE #temp;
GO
--</snippetsp_MergeDynamicPubPlusPartition>
