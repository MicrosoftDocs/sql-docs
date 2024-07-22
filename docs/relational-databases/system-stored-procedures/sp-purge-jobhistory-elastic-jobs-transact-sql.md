---
title: "jobs.sp_purge_jobhistory (Azure Elastic Jobs) (Transact-SQL)"
description: "jobs.sp_purge_jobhistory removes the history records for a job created in the Azure Elastic Jobs service for Azure SQL Database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 10/30/2023
ms.service: sql-database
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.sp_purge_jobhistory (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Removes the history records for a job in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

This stored procedure shares the name of `sp_purge_jobhistory` with a similar object in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service. For information about the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent version, see [sp_purge_jobhistory (Transact-SQL)](sp-purge-jobhistory-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[jobs].sp_purge_jobhistory [ @job_name = ] 'job_name'
      [ , [ @job_id = ] job_id ]
      [ , [ @oldest_date = ] oldest_date []
```

## Arguments

#### @job_name

The name of the job for which to delete the history records. *job_name* is nvarchar(128), with a default of `NULL`. Either *job_id* or *job_name* must be specified, but both cannot be specified.

#### @job_id

 The job identification number of the job for the records to be deleted. *job_id* is uniqueidentifier, with a default of `NULL`. Either *job_id* or *job_name* must be specified, but both cannot be specified.

#### @oldest_date

 The oldest record to retain in the history. *oldest_date* is DATETIME2, with a default of `NULL`. When *oldest_date* is specified, `sp_purge_jobhistory` only removes records that are older than the value specified.

 All times in elastic jobs are in the UTC time zone.

## Return Code Values

0 (success) or 1 (failure)

## Remarks

Elastic job history is automatically deleted if more than 45 days old.

## Permissions

By default, members of the sysadmin fixed server role can execute this stored procedure.  Only members of sysadmin can use this stored procedure to edit the attributes of jobs that are owned by other users.

## Examples

### Delete old job history

The following example shows how to delete job history prior to a specific date.  

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

-- Delete history of a specific job's executions older than the specified date
EXEC jobs.sp_purge_jobhistory 
@job_name='ResultPoolsJob'
, @oldest_date='2016-07-01 00:00:00';
GO
```

### Delete a job and all its job history

The following example shows how to delete a job using [jobs.sp_delete_job](sp-delete-job-elastic-jobs-transact-sql.md) and all related job history.  

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

--Delete a job and all its history
EXEC jobs.sp_delete_job @job_name='ResultsPoolsJob';
```

## Related content

- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
