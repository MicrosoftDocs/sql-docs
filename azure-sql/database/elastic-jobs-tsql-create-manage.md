---
title: Create and manage elastic jobs by using Transact-SQL (T-SQL)
description: Learn how to create an elastic job agent and run scripts across many databases with an elastic job agent, using Transact-SQL (T-SQL).
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: srinia, mathoma
ms.date: 04/03/2024
ms.service: azure-sql-database
ms.subservice: elastic-jobs
ms.topic: how-to
ms.custom: sqldbrb=1
dev_langs:
  - "TSQL"
---
# Create and manage elastic jobs by using T-SQL

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article provides a tutorial and examples to get started working with elastic jobs using T-SQL. [Elastic jobs](elastic-jobs-overview.md) enable the running of one or more Transact-SQL (T-SQL) scripts in parallel across many databases.

The examples in this article use the [stored procedures](#job-stored-procedures) and [views](#job-views) available in the [*job database*](elastic-jobs-overview.md#elastic-job-database).

In this end-to-end tutorial, you learn the steps required to run a query across multiple databases:

> [!div class="checklist"]
> * Create an elastic job agent
> * Create job credentials so that jobs can execute scripts on its targets
> * Define the targets (servers, elastic pools, databases) you want to run the job against
> * Create database-scoped credentials in the target databases so the agent connect and execute jobs
> * Create a job
> * Add job steps to a job
> * Start execution of a job
> * Monitor a job

## Create the elastic job agent

Transact-SQL (T-SQL) can be used to create, configure, execute, and manage jobs.

Creating the elastic job agent is not supported in T-SQL, so you must first [create an elastic job agent by using the Azure portal](elastic-jobs-tutorial.md#create-and-configure-the-elastic-job-agent), or [create an elastic job agent by using PowerShell](elastic-jobs-powershell-create.md#create-the-elastic-job-agent).

## Create the job authentication

The elastic job agent must be able to authenticate to each target server or database. As covered in [Create job agent authentication](elastic-jobs-tutorial.md#create-job-agent-authentication), the recommended approach is to use [Microsoft Entra authentication (formerly Azure Active Directory)](#use-microsoft-entra-authentication-with-a-umi-for-job-execution) with a user-assigned managed identity (UMI). Previously, [database-scoped credentials](#use-a-database-scoped-credential-for-job-execution) were the only option.

### Use Microsoft Entra authentication with a UMI for job execution

To use the recommended method of Microsoft Entra (formerly Azure Active Directory) authentication to a user-assigned managed identity (UMI), follow these steps. The elastic job agent connects to the desired target logical server(s)/databases(s) via Microsoft Entra authentication.

In addition to the login and database users, note the addition of the `GRANT` commands in the following script. These permissions are required for the script we chose for this example job. Your jobs might require different permissions. Because the example creates a new table in the targeted databases, the database user in each target database needs the proper permissions to successfully run.

In each of the target server(s)/database(s), create a contained user mapped to the UMI.

- If the elastic job has logical server or pool targets, you must create the contained user mapped to the UMI in the `master` database of the target logical server.
- For example, to create a contained database login in the `master` database, and a user in the user database, based on the user-assigned managed identity (UMI) named `job-agent-UMI`:

```sql
--Create a login on the master database mapped to a user-assigned managed identity (UMI)
CREATE LOGIN [job-agent-UMI] FROM EXTERNAL PROVIDER; 
```

```sql
--Create a user on a user database mapped to a login.
CREATE USER [job-agent-UMI] FROM LOGIN [job-agent-UMI];

-- Grant permissions as necessary to execute your jobs. For example, ALTER and CREATE TABLE:
GRANT ALTER ON SCHEMA::dbo TO jobuser;
GRANT CREATE TABLE TO jobuser;
```

- To create a contained database user if a login is not needed on the logical server:

```sql
--Create a contained database user on a user database mapped to a user-assigned managed identity (UMI)
CREATE USER [job-agent-UMI] FROM EXTERNAL PROVIDER; 

-- Grant permissions as necessary to execute your jobs. For example, ALTER and CREATE TABLE:
GRANT ALTER ON SCHEMA::dbo TO jobuser;
GRANT CREATE TABLE TO jobuser;
```

### Use a database-scoped credential for job execution

A database-scoped credential is used to connect to your target databases for script execution. The credential needs appropriate permissions, on the databases specified by the target group, to successfully execute the script. When using a [logical SQL server](logical-servers.md) and/or pool target group member, it's recommended to create a credential for use to refresh the credential prior to expansion of the server and/or pool at time of job execution. The database-scoped credential is created on the job agent database.

The same credential must be used to *Create a Login* and *Create a User from Login to grant the Login Database Permissions* on all target databases.

```sql
--Connect to the new job database specified when creating the elastic job agent

-- Create a database master key if one does not already exist, using your own password.  
CREATE MASTER KEY ENCRYPTION BY PASSWORD='<EnterStrongPasswordHere>';  

-- Create two database-scoped credentials.  
-- The credential to connect to the Azure SQL logical server, to execute jobs
CREATE DATABASE SCOPED CREDENTIAL job_credential WITH IDENTITY = 'job_credential',
    SECRET = '<EnterStrongPasswordHere>';
GO
-- The credential to connect to the Azure SQL logical server, to refresh the database metadata in server
CREATE DATABASE SCOPED CREDENTIAL refresh_credential WITH IDENTITY = 'refresh_credential',
    SECRET = '<EnterStrongPasswordHere>';
GO
```

Then, create logins on the target servers, or contained database users on target databases. 

> [!IMPORTANT]
> The login/user on each target server/database must have the same name as the identity of the database-scoped credential for the job user, and the same password as the database-scoped credential for the job user.

Create a login in the `master` database of the logical SQL server, and users in each user database.

```sql
--Create a login on the master database
CREATE LOGIN job_credential WITH PASSWORD='<Enter_same_StrongPassword_as_database_scoped_credential>';
```

```sql
--Create a user on a user database mapped to a login.
CREATE USER [job_credential] FROM LOGIN [job_credential];

-- Grant permissions as necessary to execute your jobs. For example, ALTER and CREATE TABLE:
GRANT ALTER ON SCHEMA::dbo TO job_credential;
GRANT CREATE TABLE TO job_credential;
```

Create a contained database user if a login is not needed on the logical server. Typically you would only do this if you have a single database to manage with this elastic job agent.

```sql
--Create a contained database user on a user database mapped to a Microsoft Entra account
CREATE USER [job_credential] WITH PASSWORD='<Enter_same_StrongPassword_as_database_scoped_credential>';

-- Grant permissions as necessary to execute your jobs. For example, ALTER and CREATE TABLE:
GRANT ALTER ON SCHEMA::dbo TO job_credential;
GRANT CREATE TABLE TO job_credential;
```

## Define target servers and databases

The following example shows how to execute a job against all databases in a server.  

Connect to the `job_database` and run the following command to add a target group and target member:

```sql
-- Connect to the job database specified when creating the job agent

-- Add a target group containing server(s)
EXEC jobs.sp_add_target_group 'ServerGroup1';

-- Add a server target member
EXEC jobs.sp_add_target_group_member
@target_group_name = 'ServerGroup1',
@target_type = 'SqlServer',
@server_name = 'server1.database.windows.net';

--View the recently created target group and target group members
SELECT * FROM jobs.target_groups WHERE target_group_name='ServerGroup1';
SELECT * FROM jobs.target_group_members WHERE target_group_name='ServerGroup1';
```

## Exclude an individual database

The following example shows how to execute a job against all databases in a server, except for the database named `MappingDB`.

When using Microsoft Entra authentication (formerly Azure Active Directory), omit the `@refresh_credential_name` parameter, which should only be provided when using database-scoped credentials. In the following examples, the `@refresh_credential_name` parameter is commented out.

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

-- Add a target group containing server(s)
EXEC [jobs].sp_add_target_group N'ServerGroup';
GO

-- Add a server target member
EXEC [jobs].sp_add_target_group_member
@target_group_name = N'ServerGroup',
@target_type = N'SqlServer',
--@refresh_credential_name = N'refresh_credential', --credential required to refresh the databases in a server
@server_name = N'London.database.windows.net';
GO

-- Add a server target member
EXEC [jobs].sp_add_target_group_member
@target_group_name = N'ServerGroup',
@target_type = N'SqlServer',
--@refresh_credential_name = N'refresh_credential', --credential required to refresh the databases in a server
@server_name = 'server2.database.windows.net';
GO

--Exclude a database target member from the server target group
EXEC [jobs].sp_add_target_group_member
@target_group_name = N'ServerGroup',
@membership_type = N'Exclude',
@target_type = N'SqlDatabase',
@server_name = N'server1.database.windows.net',
@database_name = N'MappingDB';
GO

--View the recently created target group and target group members
SELECT * FROM [jobs].target_groups WHERE target_group_name = N'ServerGroup';
SELECT * FROM [jobs].target_group_members WHERE target_group_name = N'ServerGroup';
```

## Create a target group (pools)

The following example shows how to target all the databases in one or more elastic pools.  

When using Microsoft Entra authentication (formerly Azure Active Directory), omit the `@refresh_credential_name` parameter, which should only be provided when using database-scoped credentials. In the following examples, the `@refresh_credential_name` parameter is commented out.

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

-- Add a target group containing pool(s)
EXEC jobs.sp_add_target_group 'PoolGroup';

-- Add an elastic pool(s) target member
EXEC jobs.sp_add_target_group_member
@target_group_name = 'PoolGroup',
@target_type = 'SqlElasticPool',
--@refresh_credential_name = 'refresh_credential', --credential required to refresh the databases in a server
@server_name = 'server1.database.windows.net',
@elastic_pool_name = 'ElasticPool-1';

-- View the recently created target group and target group members
SELECT * FROM jobs.target_groups WHERE target_group_name = N'PoolGroup';
SELECT * FROM jobs.target_group_members WHERE target_group_name = N'PoolGroup';
```

## Create a job and steps

With T-SQL, create jobs using system stored procedures in the jobs database: [jobs.sp_add_job](/sql/relational-databases/system-stored-procedures/sp-add-job-elastic-jobs-transact-sql?view=azuresql-db&preserve-view=true) and [jobs.sp_add_jobstep](/sql/relational-databases/system-stored-procedures/sp-add-jobstep-elastic-jobs-transact-sql?view=azuresql-db&preserve-view=true). The T-SQL commands are syntax are similar to the steps needed to create SQL Agent jobs and job steps in SQL Server.

You should not update internal catalog views in the *job database*. Manually changing these catalog views can corrupt the *job database* and cause failure. These views are for read-only querying only. You can use the stored procedures in the `jobs` schema on your *job database*.

- When using Microsoft Entra authentication for a Microsoft Entra ID or user-assigned managed identity to authenticate to target server(s)/database(s), the *@credential_name* argument shouldn't be provided for `sp_add_jobstep` or `sp_update_jobstep`. Similarly, omit the optional *@output_credential_name* and *@refresh_credential_name* arguments.
- When using database-scoped credentials to authenticate to target server(s)/database(s), the *@credential_name* parameter is required for `sp_add_jobstep` and `sp_update_jobstep`.
    - For example, `@credential_name = 'job_credential'`.

The following examples provide guides to create job and job steps using T-SQL, to accomplish common tasks with elastic jobs.

## Samples

### Deploy new schema to many databases

The following example shows how to deploy new schema to all databases.  

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

--Add job for create table
EXEC jobs.sp_add_job @job_name = 'CreateTableTest', @description = 'Create Table Test';

-- Add job step for create table
EXEC jobs.sp_add_jobstep @job_name = 'CreateTableTest',
@command = N'IF NOT EXISTS (SELECT * FROM sys.tables WHERE object_id = object_id(''Test''))
CREATE TABLE [dbo].[Test]([TestId] [int] NOT NULL);',
@target_group_name = 'PoolGroup';
```

### Data collection using built-in parameters

In many data collection scenarios, it can be useful to include some of these scripting variables to help post-process the results of the job.

- `$(job_name)`
- `$(job_id)`
- `$(job_version)`
- `$(step_id)`
- `$(step_name)`
- `$(job_execution_id)`
- `$(job_execution_create_time)`
- `$(target_group_name)`

For example, to group all results from the same job execution together, use `$(job_execution_id)` as shown in the following command:

```sql
@command= N' SELECT DB_NAME() DatabaseName, $(job_execution_id) AS job_execution_id, * FROM sys.dm_db_resource_stats WHERE end_time > DATEADD(mi, -20, GETDATE());'
```

> [!NOTE]
> All times in elastic jobs are in the UTC time zone.

### Monitor database performance

The following example creates a new job to collect performance data from multiple databases.

By default, the job agent will create the output table to store returned results. Therefore, the database principal associated with the output credential must at a minimum have the following permissions: `CREATE TABLE` on the database, `ALTER`, `SELECT`, `INSERT`, `DELETE` on the output table or its schema, and `SELECT` on the [sys.indexes](/sql/relational-databases/system-catalog-views/sys-indexes-transact-sql) catalog view.

If you want to manually create the table ahead of time, then it needs to have the following properties:

1. Columns with the correct name and data types for the result set.
1. Additional column for `internal_execution_id` with the data type of uniqueidentifier.
1. A nonclustered index named `IX_<TableName>_Internal_Execution_ID` on the `internal_execution_id` column.
1. All previously listed permissions except for `CREATE TABLE` permission on the database.

Connect to the *job database* and run the following commands:

```sql
--Connect to the job database specified when creating the job agent

-- Add a job to collect perf results
EXEC jobs.sp_add_job @job_name ='ResultsJob', @description='Collection Performance data from all customers'

-- Add a job step w/ schedule to collect results
EXEC jobs.sp_add_jobstep
@job_name = 'ResultsJob',
@command = N' SELECT DB_NAME() DatabaseName, $(job_execution_id) AS job_execution_id, * FROM sys.dm_db_resource_stats WHERE end_time > DATEADD(mi, -20, GETDATE());',
@target_group_name = 'PoolGroup',
@output_type = 'SqlDatabase',
@output_server_name = 'server1.database.windows.net',
@output_database_name = '<resultsdb>',
@output_table_name = '<output_table_name>';

--Create a job to monitor pool performance

--Connect to the job database specified when creating the job agent

-- Add a target group containing elastic job database
EXEC jobs.sp_add_target_group 'ElasticJobGroup';

-- Add a server target member
EXEC jobs.sp_add_target_group_member
@target_group_name = 'ElasticJobGroup',
@target_type = 'SqlDatabase',
@server_name = 'server1.database.windows.net',
@database_name = 'master';

-- Add a job to collect perf results
EXEC jobs.sp_add_job
@job_name = 'ResultsPoolsJob',
@description = 'Demo: Collection Performance data from all pools',
@schedule_interval_type = 'Minutes',
@schedule_interval_count = 15;

-- Add a job step w/ schedule to collect results
EXEC jobs.sp_add_jobstep
@job_name='ResultsPoolsJob',
@command=N'declare @now datetime
DECLARE @startTime datetime
DECLARE @endTime datetime
DECLARE @poolLagMinutes datetime
DECLARE @poolStartTime datetime
DECLARE @poolEndTime datetime
SELECT @now = getutcdate ()
SELECT @startTime = dateadd(minute, -15, @now)
SELECT @endTime = @now
SELECT @poolStartTime = dateadd(minute, -30, @startTime)
SELECT @poolEndTime = dateadd(minute, -30, @endTime)

SELECT elastic_pool_name , end_time, elastic_pool_dtu_limit, avg_cpu_percent, avg_data_io_percent, avg_log_write_percent, max_worker_percent, max_session_percent,
        avg_storage_percent, elastic_pool_storage_limit_mb FROM sys.elastic_pool_resource_stats
        WHERE end_time > @poolStartTime and end_time <= @poolEndTime;
',
@target_group_name = 'ElasticJobGroup',
@output_type = 'SqlDatabase',
@output_server_name = 'server1.database.windows.net',
@output_database_name = 'resultsdb',
@output_table_name = '<output_table_name>';
```

## Run the job

The following example shows how to start a job immediately as a manual, unplanned action.

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

-- Execute the latest version of a job
EXEC jobs.sp_start_job 'CreateTableTest';

-- Execute the latest version of a job and receive the execution ID
declare @je uniqueidentifier;
exec jobs.sp_start_job 'CreateTableTest', @job_execution_id = @je output;
select @je;

-- Monitor progress
SELECT * FROM jobs.job_executions WHERE job_execution_id = @je;

```

## Schedule execution of a job

The following example shows how to schedule a job for future execution on a recurring basis every 15 minutes.

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

EXEC jobs.sp_update_job
@job_name = 'ResultsJob',
@enabled=1,
@schedule_interval_type = 'Minutes',
@schedule_interval_count = 15;
```

## View job definitions

The following example shows how to view current job definitions.  

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

-- View all jobs
SELECT * FROM jobs.jobs;

-- View the steps of the current version of all jobs
SELECT js.* FROM jobs.jobsteps js
JOIN jobs.jobs j
  ON j.job_id = js.job_id AND j.job_version = js.job_version;

-- View the steps of all versions of all jobs
SELECT * FROM jobs.jobsteps;
```

## Monitor job execution status

The following example shows how to view execution status details for all jobs.  

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

--View top-level execution status for the job named 'ResultsPoolJob'
SELECT * FROM jobs.job_executions
WHERE job_name = 'ResultsPoolsJob' and step_id IS NULL
ORDER BY start_time DESC;

--View all top-level execution status for all jobs
SELECT * FROM jobs.job_executions WHERE step_id IS NULL
ORDER BY start_time DESC;

--View all execution statuses for job named 'ResultsPoolsJob'
SELECT * FROM jobs.job_executions
WHERE job_name = 'ResultsPoolsJob'
ORDER BY start_time DESC;

-- View all active executions
SELECT * FROM jobs.job_executions
WHERE is_active = 1
ORDER BY start_time DESC;
```

## Cancel a job

The following example shows how to retrieve a job execution ID and then cancel a job execution.  

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

-- View all active executions to determine job execution ID
SELECT * FROM jobs.job_executions
WHERE is_active = 1 AND job_name = 'ResultPoolsJob'
ORDER BY start_time DESC;
GO

-- Cancel job execution with the specified job execution ID
EXEC jobs.sp_stop_job '01234567-89ab-cdef-0123-456789abcdef';
```

## Delete old job history

The following example shows how to delete job history prior to a specific date.  

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

-- Delete history of a specific job's executions older than the specified date
EXEC jobs.sp_purge_jobhistory @job_name='ResultPoolsJob', @oldest_date='2016-07-01 00:00:00';

--Note: job history is automatically deleted if it is >45 days old
```

## Delete a job and all its job history

The following example shows how to delete a job and all related job history.  

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

EXEC jobs.sp_delete_job @job_name='ResultsPoolsJob';
EXEC jobs.sp_purge_jobhistory @job_name='ResultsPoolsJob';

--Note: job history is automatically deleted if it is >45 days old
```

## Job stored procedures

The following stored procedures are in the [jobs database](elastic-jobs-overview.md#elastic-job-database). They are similarly named but distinctly different from the system stored procedures used for the SQL Server Agent service.

|Stored procedure  |Description  |
|---------|---------|
|[sp_add_job](/sql/relational-databases/system-stored-procedures/sp-add-job-elastic-jobs-transact-sql)     |     Adds a new job.    |
|[sp_update_job](/sql/relational-databases/system-stored-procedures/sp-update-job-elastic-jobs-transact-sql)    |      Updates an existing job.   |
|[sp_delete_job](/sql/relational-databases/system-stored-procedures/sp-delete-job-elastic-jobs-transact-sql)     |      Deletes an existing job.   |
|[sp_add_jobstep](/sql/relational-databases/system-stored-procedures/sp-add-jobstep-elastic-jobs-transact-sql)    |    Adds a step to a job.     |
|[sp_update_jobstep](/sql/relational-databases/system-stored-procedures/sp-update-jobstep-elastic-jobs-transact-sql)     |     Updates a job step.    |
|[sp_delete_jobstep](/sql/relational-databases/system-stored-procedures/sp-delete-jobstep-elastic-jobs-transact-sql)     |     Deletes a job step.    |
|[sp_start_job](/sql/relational-databases/system-stored-procedures/sp-start-job-elastic-jobs-transact-sql)    |  Starts executing a job.       |
|[sp_stop_job](/sql/relational-databases/system-stored-procedures/sp-stop-job-elastic-jobs-transact-sql)     |     Stops a job execution.   |
|[sp_add_target_group](/sql/relational-databases/system-stored-procedures/sp-add-target-group-elastic-jobs-transact-sql)    |     Adds a target group.    |
|[sp_delete_target_group](/sql/relational-databases/system-stored-procedures/sp-delete-target-group-elastic-jobs-transact-sql)     |    Deletes a target group.     |
|[sp_add_target_group_member](/sql/relational-databases/system-stored-procedures/sp-add-target-group-member-elastic-jobs-transact-sql)     |    Adds a database or group of databases to a target group.     |
|[sp_delete_target_group_member](/sql/relational-databases/system-stored-procedures/sp-delete-target-group-member-elastic-jobs-transact-sql)     |     Removes a target group member from a target group.    |
|[sp_purge_jobhistory](/sql/relational-databases/system-stored-procedures/sp-purge-jobhistory-elastic-jobs-transact-sql)    |    Removes the history records for a job.     |

## Job views

The following views are available in the [jobs database](elastic-jobs-overview.md#elastic-job-database).

|View  |Description  |
|---------|---------|
|[job_executions](/sql/relational-databases/system-catalog-views/jobs-job-executions-elastic-jobs-transact-sql)     |  Shows job execution history.      |
|[jobs](/sql/relational-databases/system-catalog-views/jobs-jobs-elastic-jobs-transact-sql)     |   Shows all jobs.      |
|[job_versions](/sql/relational-databases/system-catalog-views/jobs-job-versions-elastic-jobs-transact-sql)     |   Shows all job versions.      |
|[jobsteps](/sql/relational-databases/system-catalog-views/jobs-jobsteps-elastic-jobs-transact-sql)      |     Shows all steps in the current version of each job.    |
|[jobstep_versions](/sql/relational-databases/system-catalog-views/jobs-jobstep-versions-elastic-jobs-transact-sql)      |     Shows all steps in all versions of each job.    |
|[target_groups](/sql/relational-databases/system-catalog-views/jobs-target-groups-elastic-jobs-transact-sql)     |      Shows all target groups.   |
|[target_group_members](/sql/relational-databases/system-catalog-views/jobs-target-group-members-elastic-jobs-transact-sql)      |   Shows all members of all target groups.      |

## Next step

> [!div class="nextstepaction"]
> [Create and manage elastic jobs by using PowerShell](elastic-jobs-powershell-create.md)
