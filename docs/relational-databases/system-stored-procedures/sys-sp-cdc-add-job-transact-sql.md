---
title: "sys.sp_cdc_add_job (Transact-SQL)"
description: "Creates a change data capture cleanup or capture job in the current database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/13/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cdc_add_job_TSQL"
  - "sys.sp_cdc_add_job"
  - "sys.sp_cdc_add_job_TSQL"
  - "sp_cdc_add_job"
helpviewer_keywords:
  - "sp_cdc_add_job"
dev_langs:
  - "TSQL"
---
# sys.sp_cdc_add_job (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates a change data capture cleanup or capture job in the current database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_cdc_add_job [ @job_type = ] N'job_type'
    [ , [ @start_job = ] start_job ]
    [ , [ @maxtrans = ] max_trans ]
    [ , [ @maxscans = ] max_scans ]
    [ , [ @continuous = ] continuous ]
    [ , [ @pollinginterval = ] polling_interval ]
    [ , [ @retention ] = retention ]
    [ , [ @threshold ] = 'delete_threshold' ]
[ ; ]
```

## Arguments

#### [ @job_type = ] N'*job_type*'

Type of job to add. *@job_type* is **nvarchar(20)** and can't be NULL. Valid inputs are `capture` and `cleanup`.

#### [ @start_job = ] *start_job*

Flag indicating whether the job should be started immediately after it's added. *@start_job* is **bit** with a default of `1`.

#### [ @maxtrans ] = *max_trans*

Maximum number of transactions to process in each scan cycle. *@maxtrans* is **int** with a default of `500`. If specified, the value must be a positive integer.

*@maxtrans* is valid only for capture jobs.

#### [ @maxscans ] = *max_scans*

Maximum number of scan cycles to execute in order to extract all rows from the log. *@maxscans* is **int** with a default of `10`.

*@max_scan* is valid only for capture jobs.

#### [ @continuous ] = *continuous*

Indicates whether the capture job is to run continuously (`1`), or run only once (`0`). *@continuous* is **bit** with a default of `1`.

- When *@continuous* is `1`, the [sp_cdc_scan](sys-sp-cdc-scan-transact-sql.md) job scans the log and processes up to (`@maxtrans * @maxscans`) transactions. It then waits the number of seconds specified in *@pollinginterval* before beginning the next log scan.

- When *@continuous* is `0`, the `sp_cdc_scan` job executes up to *@maxscans* scans of the log, processing up to *@maxtrans* transaction during each scan, and then exits.

*@continuous* is valid only for capture jobs.

#### [ @pollinginterval ] = *polling_interval*

Number of seconds between log scan cycles. *@pollinginterval* is **bigint** with a default of `5`.

*@pollinginterval* is valid only for capture jobs when *@continuous* is set to `1`. If specified, the value must be greater than or equal to `0` and less than 24 hours (up to 86399 seconds). If a value of `0` is specified, there's no wait between log scans.

#### [ @retention ] = *retention*

Number of minutes that change data rows are to be retained in change tables. *@retention* is **bigint** with a default of `4320` (72 hours). The maximum value is `52494800` (100 years). If specified, the value must be a positive integer.

*@retention* is valid only for cleanup jobs.

#### [ @threshold = ] '*delete_threshold*'

Maximum number of delete entries that can be deleted by using a single statement on cleanup. *@threshold* is **bigint** with a default of `5000`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

A cleanup job is created using the default values when the first table in the database is enabled for change data capture. A capture job is created using the default values when the first table in the database is enabled for change data capture and no transactional publications exist for the database. When a transactional publication exists, the transactional log reader is used to drive the capture mechanism, and a separate capture job isn't required or allowed.

Because the cleanup and capture jobs are created by default, this stored procedure is necessary only when a job has been explicitly dropped and must be recreated.

The name of the job is `cdc.<database_name>_cleanup` or `cdc.<database_name>_capture`, where `<database_name>` is the name of the current database. If a job with the same name already exists, the name is appended with a period (`.`) followed by a unique identifier, for example: `cdc.AdventureWorks_capture.A1ACBDED-13FC-428C-8302-10100EF74F52`.

To view the current configuration of a cleanup or capture job, use [sp_cdc_help_jobs](sys-sp-cdc-help-jobs-transact-sql.md). To change the configuration of a job, use [sp_cdc_change_job](sys-sp-cdc-change-job-transact-sql.md).

## Permissions

Requires membership in the **db_owner** fixed database role.

## Examples

### A. Create a capture job

The following example creates a capture job. This example assumes that the existing cleanup job was explicitly dropped and must be recreated. The job is created using the default values.

```sql
USE AdventureWorks2022;
GO
EXEC sys.sp_cdc_add_job @job_type = N'capture';
GO
```

### B. Create a cleanup job

The following example creates a cleanup job in the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. The parameter *@start_job* is set to `0` and *@retention* is set to 5760 minutes (96 hours). This example assumes that the existing cleanup job was explicitly dropped and must be recreated.

```sql
USE AdventureWorks2022;
GO

EXEC sys.sp_cdc_add_job
    @job_type = N'cleanup',
    @start_job = 0,
    @retention = 5760;
```

## Related content

- [dbo.cdc_jobs (Transact-SQL)](../system-tables/dbo-cdc-jobs-transact-sql.md)
- [sys.sp_cdc_enable_table (Transact-SQL)](sys-sp-cdc-enable-table-transact-sql.md)
- [What is change data capture (CDC)?](../track-changes/about-change-data-capture-sql-server.md)
