---
title: "sys.sp_cdc_start_job (Transact-SQL)"
description: "Starts a change data capture cleanup or capture job for the current database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/13/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cdc_start_job"
  - "sp_cdc_start_job_TSQL"
  - "sys.sp_cdc_start_job_TSQL"
  - "sys.sp_cdc_start_job"
helpviewer_keywords:
  - "sp_cdc_start_job"
dev_langs:
  - "TSQL"
---
# sys.sp_cdc_start_job (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Starts a change data capture cleanup or capture job for the current database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_cdc_start_job
    [ [ @job_type = ] N'job_type' ]
[ ; ]
```

## Arguments

#### [ @job_type = ] N'*job_type*'

Type of job to add. *@job_type* is **nvarchar(20)** with a default of `capture`. Valid inputs are `capture` and `cleanup`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sys.sp_cdc_start_job` can be used by an administrator to explicitly start either the capture job or the cleanup job.

## Permissions

Requires membership in the **db_owner** fixed database role.

## Examples

### A. Start a capture job

The following example starts the capture job for the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database. Specifying a value for *@job_type* isn't required because the default job type is `capture`.

```sql
USE AdventureWorks2022;
GO

EXEC sys.sp_cdc_start_job;
GO
```

### B. Start a cleanup job

The following example starts a cleanup job for the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO

EXEC sys.sp_cdc_start_job
    @job_type = N'cleanup';
```

## Related content

- [dbo.cdc_jobs (Transact-SQL)](../system-tables/dbo-cdc-jobs-transact-sql.md)
- [sys.sp_cdc_stop_job (Transact-SQL)](sys-sp-cdc-stop-job-transact-sql.md)
