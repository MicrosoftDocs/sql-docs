---
title: "jobs.sp_update_jobstep (Azure Elastic Jobs) (Transact-SQL)"
description: "jobs.sp_update_jobstep modifies a job step in an existing job in the Azure Elastic Jobs service for Azure SQL Database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/04/2023
ms.service: azure-sql-database
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.sp_update_jobstep (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Modifies a job step in an existing job in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true). Use [jobs.sp_add_jobstep](sp-add-jobstep-elastic-jobs-transact-sql.md) to add job steps to a job.

This stored procedure shares the name of `sp_update_jobstep` with a similar object in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service. For information about the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent version, see [sp_update_jobstep (Transact-SQL)](sp-update-jobstep-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[jobs].sp_update_jobstep [ @job_name = ] 'job_name'
     [ , [ @step_id = ] step_id ]
     [ , [ @step_name = ] 'step_name' ]
     [ , [ @new_id = ] new_id ]
     [ , [ @new_name = ] 'new_name' ]
     [ , [ @command_type = ] 'command_type' ]
     [ , [ @command_source = ] 'command_source' ]  
     , [ @command = ] 'command'
     [, [ @credential_name = ] 'credential_name' ]
     , [ @target_group_name = ] 'target_group_name'
     [ , [ @initial_retry_interval_seconds = ] initial_retry_interval_seconds ]
     [ , [ @maximum_retry_interval_seconds = ] maximum_retry_interval_seconds ]
     [ , [ @retry_interval_backoff_multiplier = ]retry_interval_backoff_multiplier ]
     [ , [ @retry_attempts = ] retry_attempts ]
     [ , [ @step_timeout_seconds = ] step_timeout_seconds ]
     [ , [ @output_type = ] 'output_type' ]
     [ , [ @output_credential_name = ] 'output_credential_name' ]
     [ , [ @output_server_name = ] 'output_server_name' ]
     [ , [ @output_database_name = ] 'output_database_name' ]
     [ , [ @output_schema_name = ] 'output_schema_name' ]
     [ , [ @output_table_name = ] 'output_table_name' ]
     [ , [ @job_version = ] job_version OUTPUT ]
     [ , [ @max_parallelism = ] max_parallelism ]
```

## Arguments

#### @job_name

The name of the job to which the step belongs. *job_name* is nvarchar(128).

#### @step_id

The identification number for the job step to be modified. Either *step_id* or *step_name* must be specified. *step_id* is an int.

#### @step_name

The name of the step to be modified. Either *step_id* or *step_name* must be specified. *step_name* is nvarchar(128).

#### @new_id

The new sequence identification number for the job step. Step identification numbers start at 1 and increment without gaps. If a step is reordered, then other steps will be automatically renumbered.

#### @new_name

The new name of the step. *new_name* is nvarchar(128).

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

The maximum delay between retry attempts. If the delay between retries would grow larger than this value, it is capped to this value instead. *maximum_retry_interval_seconds* is int, with default value of 120.

#### @retry_interval_backoff_multiplier

The multiplier to apply to the retry delay if multiple job step execution attempts fail. For example, if the first retry had a delay of 5 second and the backoff multiplier is 2.0, then the second retry will have a delay of 10 seconds and the third retry will have a delay of 20 seconds. *retry_interval_backoff_multiplier* is the **real** data type, with default value of 2.0.

#### @retry_attempts

The number of times to retry execution if the initial attempt fails. For example, if the *retry_attempts* value is 10, then there will be 1 initial attempt and 10 retry attempts, giving a total of 11 attempts. If the final retry attempt fails, then the job execution will terminate with a `lifecycle` of `Failed` recorded in [jobs.job_executions](../system-catalog-views/jobs-job-executions-elastic-jobs-transact-sql.md). *retry_attempts* is int, with default value of 10.

#### @step_timeout_seconds

The maximum amount of time allowed for the step to execute. If this time is exceeded, then the job execution will terminate with a `lifecycle` of `TimedOut` recorded in [jobs.job_executions](../system-catalog-views/jobs-job-executions-elastic-jobs-transact-sql.md). *step_timeout_seconds* is int, with default value of 43,200 seconds (12 hours).

#### @output_type

If not `NULL`, the type of destination that the *command*'s first result set is written to. To reset the value of *output_type* back to `NULL`, set this parameter's value to '' (empty string). *output_type* is nvarchar(50), with a default of `NULL`.

If specified, the value must be `SqlDatabase`.

#### @output_credential_name

If not `NULL`, the name of the database-scoped credential that is used to connect to the output destination database. Must be specified if *output_type* equals `SqlDatabase`. To reset the value of *output_credential_name* back to NULL, set this parameter's value to '' (empty string). *output_credential_name* is nvarchar(128), with a default value of `NULL`.

When using Microsoft Entra authentication (formerly Azure Active Directory), omit the *@output_credential_name* parameter, which should only be provided when using database-scoped credentials.

#### @output_server_name

If not `NULL`, the fully qualified DNS name of the server that contains the output destination database, for example, `@output_server_name = 'server1.database.windows.net'`. Must be specified if *output_type* equals `SqlDatabase`. To reset the value of output_server_name back to `NULL`, set this parameter's value to '' (empty string). *output_server_name* is nvarchar(256), with a default of `NULL`.

#### @output_database_name

If not `NULL`, the name of the database that contains the output destination table. Must be specified if *output_type* equals `SqlDatabase`. To reset the value of *output_database_name* back to `NULL`, set this parameter's value to '' (empty string). *output_database_name* is nvarchar(128), with a default of `NULL`.

#### @output_schema_name

If not `NULL`, the name of the SQL schema that contains the output destination table. If *output_type* equals `SqlDatabase`, the default value is `dbo`. To reset the value of *output_schema_name* back to `NULL`, set this parameter's value to '' (empty string). *output_schema_name* is nvarchar(128).

#### @output_table_name
  
If not `NULL`, the name of the table that the *command*'s first result set will be written to. If the table doesn't already exist, it will be created based on the schema of the returning result-set. Must be specified if *output_type* equals `SqlDatabase`. To reset the value of *output_server_name* back to `NULL`, set this parameter's value to '' (empty string). *output_table_name* is nvarchar(128), with a default value of `NULL`.

If specifying an *output_table_name*, the Job Agent UMI or database-scoped credential should be granted needed permissions to CREATE TABLE and INSERT data into the table.

#### @job_version  OUTPUT  

Output parameter that will be assigned the new job version number. *job_version* is int.

#### @max_parallelism   OUTPUT  

The maximum level of parallelism per elastic pool. If set, then the job step will be restricted to only run on a maximum of that many databases per elastic pool. This applies to each elastic pool that is either directly included in the target group or to elastic pools inside a server that is included in the target group. To reset the value of *max_parallelism* back to `NULL`, set this parameter's value to `-1`. *max_parallelism* is int.

## Return Code Values

0 (success) or 1 (failure)

## Remarks

Any in-progress executions of the job will not be affected. When `jobs.sp_update_jobstep` succeeds, the job's version number is incremented. The next time the job is executed, the new version will be used.

## Permissions

By default, members of the sysadmin fixed server role can execute this stored procedure.  Only members of sysadmin can use this stored procedure to edit the attributes of jobs that are owned by other users

## Examples

### Update command of an elastic job step

This sample updates the T-SQL command of an existing elastic job step. The T-SQL script adds a job step to create a table if it does not exist.

```sql
--Connect to the job database specified when creating the elastic job agent

-- Add job step to create a table if it does not exist
EXEC jobs.sp_update_jobstep @job_name = 'CreateTableTest',
@command = N'IF NOT EXISTS (SELECT * FROM sys.tables WHERE object_id = object_id(''Test''))
CREATE TABLE [dbo].[Test]([TestId] [int] NOT NULL);',
@target_group_name = 'PoolGroup';
```

## Related content

- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
