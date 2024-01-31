---
title: "sys.sp_cdc_help_jobs (Transact-SQL)"
description: "Reports information about all change data capture cleanup or capture jobs in the current database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/13/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cdc_help_jobs"
  - "sys.sp_cdc_help_jobs_TSQL"
  - "sp_cdc_help_jobs_TSQL"
  - "sys.sp_cdc_help_jobs"
helpviewer_keywords:
  - "sp_cdc_help_jobs"
dev_langs:
  - "TSQL"
---
# sys.sp_cdc_help_jobs (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reports information about all change data capture cleanup or capture jobs in the current database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_cdc_help_jobs
[ ; ]
```

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `job_id` | **uniqueidentifier** | The ID of the job. |
| `job_type` | **nvarchar(20)** | The type of job. |
| `maxtrans` | **int** | The maximum number of transactions to process in each scan cycle.<br /><br />`maxtrans` is valid only for capture jobs. |
| `maxscans` | **int** | The maximum number of scan cycles to execute in order to extract all rows from the log.<br /><br />`maxscans` is valid only for capture jobs. |
| `continuous` | **bit** | A flag indicating whether the capture job is to run continuously (`1`), or run in one-time mode (`0`). For more information, see [sys.sp_cdc_add_job (Transact-SQL)](sys-sp-cdc-add-job-transact-sql.md).<br /><br />`continuous` is valid only for capture jobs. |
| `pollinginterval` | **bigint** | The number of seconds between log scan cycles.<br /><br />`pollinginterval` is valid only for capture jobs. |
| `retention` | **bigint** | The number of minutes that change rows are to be retained in change tables.<br /><br />`retention` is valid only for cleanup jobs. |
| `threshold` | **bigint** | The maximum number of delete entries that can be deleted using a single statement on cleanup. |

## Permissions

Requires membership in the **db_owner** fixed database role.

## Examples

The following example returns information about the defined capture and cleanup jobs for the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO

EXEC sys.sp_cdc_help_jobs;
GO
```

## Related content

- [dbo.cdc_jobs (Transact-SQL)](../system-tables/dbo-cdc-jobs-transact-sql.md)
- [sys.sp_cdc_add_job (Transact-SQL)](sys-sp-cdc-add-job-transact-sql.md)
