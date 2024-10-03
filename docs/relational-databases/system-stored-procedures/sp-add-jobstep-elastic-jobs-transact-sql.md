---
title: "jobs.sp_add_jobstep (Azure Elastic Jobs) (Transact-SQL)"
description: "jobs.sp_add_jobstep adds a step to an existing job in the Azure Elastic Jobs service for Azure SQL Database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 08/21/2024
ms.service: azure-sql-database
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.sp_add_jobstep (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Adds a step to an existing job in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true). Use [jobs.sp_update_jobstep](sp-update-jobstep-elastic-jobs-transact-sql.md) to modify existing elastic job steps.

This stored procedure shares the name of `sp_add_jobstep` with a similar object in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service. For information about the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent version, see [sp_add_jobstep](sp-add-jobstep-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[jobs].sp_add_jobstep [ @job_name = ] 'job_name'
     [ , [ @step_id = ] step_id ]
     [ , [ @step_name = ] step_name ]
     [ , [ @command_type = ] 'command_type' ]
     [ , [ @command_source = ] 'command_source' ]
     , [ @command = ] 'command'
     [ , [ @credential_name = ] 'credential_name' ]
     , [ @target_group_name = ] 'target_group_name'
     [ , [ @initial_retry_interval_seconds = ] initial_retry_interval_seconds ]
     [ , [ @maximum_retry_interval_seconds = ] maximum_retry_interval_seconds ]
     [ , [ @retry_interval_backoff_multiplier = ] retry_interval_backoff_multiplier ]
     [ , [ @retry_attempts = ] retry_attempts ]
     [ , [ @step_timeout_seconds = ] step_timeout_seconds ]
     [ , [ @output_type = ] 'output_type' ]
     [ , [ @output_credential_name = ] 'output_credential_name' ]
     [ , [ @output_subscription_id = ] 'output_subscription_id' ]
     [ , [ @output_resource_group_name = ] 'output_resource_group_name' ]
     [ , [ @output_server_name = ] 'output_server_name' ]
     [ , [ @output_database_name = ] 'output_database_name' ]
     [ , [ @output_schema_name = ] 'output_schema_name' ]
     [ , [ @output_table_name = ] 'output_table_name' ]
     [ , [ @job_version = ] job_version OUTPUT ]
     [ , [ @max_parallelism = ] max_parallelism ]
```

## Arguments

#### @job_name

The name of the job to which to add the step. *job_name* is nvarchar(128).

#### @step_id

The sequence identification number for the job step. Step identification numbers start at 1 and increment without gaps. If an existing step already has this ID, then that step and all following steps will have their IDs incremented so that this new step can be inserted into the sequence. If not specified, the *step_id* will be automatically assigned to the last in the sequence of steps. *step_id* is an int.

#### @step_name

The name of the step. Must be specified, except for the first step of a job that (for convenience) has a default name of `JobStep`. *step_name* is nvarchar(128).

#### @command_type

The type of command that is executed by this job step. *command_type* is nvarchar(50), with a default value of `TSql`, meaning that the value of the *@command_type* parameter is a T-SQL script.

If specified, the value must be `TSql`.

#### @command_source

The type of location where the command is stored. *command_source* is nvarchar(50), with a default value of `Inline`, meaning that the value of the *@command* parameter is the literal text of the command.

If specified, the value must be `Inline`.

#### @command

The valid T-SQL script that is to be executed by this job step. *command* is nvarchar(max), with a default of `NULL`.

#### @credential_name

The name of the database-scoped credential stored in this job control database that is used to connect to each of the target databases within the target group when this step is executed. *credential_name* is nvarchar(128).

When using Microsoft Entra authentication (formerly Azure Active Directory), omit the *@credential_name* parameter, which should only be provided when using database-scoped credentials.

#### @target_group_name

The name of the target group that contains the target databases that the job step will be executed on. *target_group_name* is nvarchar(128).

#### @initial_retry_interval_seconds

The delay before the first retry attempt, if the job step fails on the initial execution attempt. *initial_retry_interval_seconds* is int, with default value of 1.

#### @maximum_retry_interval_seconds

The maximum delay between retry attempts. If the delay between retries would grow larger than this value, it's capped to this value instead. *maximum_retry_interval_seconds* is int, with default value of 120.

#### @retry_interval_backoff_multiplier

The multiplier to apply to the retry delay if multiple job step execution attempts fail. For example, if the first retry had a delay of 5 second and the backoff multiplier is 2.0, then the second retry will have a delay of 10 seconds and the third retry will have a delay of 20 seconds. *retry_interval_backoff_multiplier* is the **real** data type, with default value of 2.0.

#### @retry_attempts

The number of times to retry execution if the initial attempt fails. For example, if the *retry_attempts* value is 10, then there will be 1 initial attempt and 10 retry attempts, giving a total of 11 attempts. If the final retry attempt fails, then the job execution will terminate with a `lifecycle` of `Failed` recorded in [jobs.job_executions](../system-catalog-views/jobs-job-executions-elastic-jobs-transact-sql.md). *retry_attempts* is int, with default value of 10.

#### @step_timeout_seconds

The maximum amount of time allowed for the step to execute. If this time is exceeded, then the job execution will terminate with a `lifecycle` of `TimedOut` recorded in [jobs.job_executions](../system-catalog-views/jobs-job-executions-elastic-jobs-transact-sql.md). *step_timeout_seconds* is int, with default value of 43,200 seconds (12 hours).

#### @output_type

If not `NULL`, the type of destination that the *command*'s first result set is written to. *output_type* is nvarchar(50), with a default of `NULL`.

If specified, the value must be `SqlDatabase`.

#### @output_credential_name

If not null, the name of the database-scoped credential that is used to connect to the output destination database. Must be specified if *output_type* equals `SqlDatabase`. *output_credential_name* is nvarchar(128), with a default value of `NULL`.

When using Microsoft Entra authentication (formerly Azure Active Directory), omit the *@output_credential_name* parameter, which should only be provided when using database-scoped credentials.

#### @output_subscription_id

Azure subscription ID to use for the output. Defaults to the job agent's subscription. *output_subscription_id* is a *uniqueidentifier*.

#### @output_resource_group_name

Name of the resource group in which the output database resides. Defaults to job agent's resource group. *output_resource_group_name* is nvarchar(128).

#### @output_server_name

If not `NULL`, the fully qualified DNS name of the server that contains the output destination database, for example: `@output_server_name = 'server1.database.windows.net'`. Must be specified if *output_type* equals `SqlDatabase`. *output_server_name* is nvarchar(256), with a default of `NULL`.

#### @output_database_name

If not `NULL`, the name of the database that contains the output destination table. Must be specified if *output_type* equals `SqlDatabase`. *output_database_name* is nvarchar(128), with a default of `NULL`.

#### @output_schema_name

If not `NULL`, the name of the SQL schema that contains the output destination table. If *output_type* equals `SqlDatabase`, the default value is `dbo`. *output_schema_name* is nvarchar(128).

#### @output_table_name

If not `NULL`, the name of the table that the *command*'s first result set will be written to. If the table doesn't already exist, it will be created based on the schema of the returning result set. Must be specified if *output_type* equals `SqlDatabase`. *output_table_name* is nvarchar(128), with a default value of `NULL`.

If specifying an *output_table_name*, the Job Agent UMI or database-scoped credential should be granted needed permissions to CREATE TABLE and INSERT data into the table.

#### @job_version OUTPUT

Output parameter that will be assigned the new job version number. *job_version* is int.

#### @max_parallelism OUTPUT

The maximum level of parallelism per elastic pool.

If set, then the job step will be restricted to only run on a maximum of that many databases per elastic pool. This applies to each elastic pool that is either directly included in the target group or to elastic pools inside a server that is included in the target group. *max_parallelism* is int.

## Return code values

`0` (success) or `1` (failure).

## Remarks

When `sp_add_jobstep` succeeds, the job's current version number is incremented. The next time the job is executed, the new version will be used. If the job is currently executing, that execution will not contain the new step.

- When using Microsoft Entra authentication to authenticate to target server(s)/database(s), the *@credential_name* and *@output_credential_name* arguments shouldn't be provided for `sp_add_jobstep` or `sp_update_jobstep`.
- When using database-scoped credentials to authenticate to target server(s)/database(s), the *@credential_name* parameter is required for `sp_add_jobstep` and `sp_update_jobstep`. For example, `@credential_name = 'job_credential'`.

## Permissions

By default, members of the **sysadmin** fixed server role can execute this stored procedure. Only members of sysadmin can use this stored procedure to edit the attributes of jobs that are owned by other users.

## Examples

### Create a job step to execute a T-SQL statement

The following example shows how to create an elastic job to execute a T-SQL statement in an elastic job. The following example uses `jobs.sp_add_jobstep` to create a job step in the job named `CreateTableTest`, to be executed on the target group `PoolGroup`.

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

### Create a job step to execute a T-SQL statement and collect results

The following example shows how to create an elastic job to execute a T-SQL statement in an elastic job, and collect the results in an Azure SQL Database. The following example uses `jobs.sp_add_jobstep` to create a job step in the job named `ResultsJob`, to be executed on the target group `PoolGroup`. The results are recorded in a table named `dbo.results_table` in the database named `Results` in the server `server1.database.windows.net`.

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
@output_database_name = 'Results',
@output_schema_name = 'dbo',
@output_table_name = 'results_table';
```

## Related content

- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
