---
title: "sys.sp_cdc_scan (Transact-SQL)"
description: "Executes the change data capture log scan operation."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/13/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_cdc_scan_TSQL"
  - "sp_cdc_scan"
  - "sys.sp_cdc_scan"
  - "sp_cdc_scan_TSQL"
helpviewer_keywords:
  - "sp_cdc_scan"
dev_langs:
  - "TSQL"
---
# sys.sp_cdc_scan (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Executes the change data capture log scan operation.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_cdc_scan [ [ @maxtrans = ] max_trans ]
    [ , [ @maxscans = ] max_scans ]
    [ , [ @continuous = ] continuous ]
    [ , [ @pollinginterval = ] polling_interval ]
[ ; ]
```

## Arguments

#### [ @maxtrans = ] *max_trans*

Maximum number of transactions to process in each scan cycle. *@maxtrans* is **int** with a default of `500`.

#### [ @maxscans = ] *max_scans*

Maximum number of scan cycles to execute in order to extract all rows from the log. *@maxscans* is **int** with a default of `10`.

#### [ @continuous = ] *continuous*

Indicates whether the stored procedure should end after executing a single scan cycle (`0`) or run continuously, pausing for the time specified by *@pollinginterval* before re-executing the scan cycle (`1`). *@continuous* is **tinyint** with a default of `0`.

#### [ @pollinginterval = ] *polling_interval*

Number of seconds between log scan cycles. *@pollinginterval* is **bigint** with a default of `0`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sys.sp_cdc_scan` is called internally by `sys.sp_MScdc_capture_job` if the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent capture job is being used by change data capture. The procedure can't be executed explicitly when a change data capture log scan operation is already active, or when the database is enabled for transactional replication. This stored procedure should be used by administrators who want to customize the behavior of the capture job that is automatically configured.

## Permissions

Requires membership in the **db_owner** fixed database role.

## Related content

- [dbo.cdc_jobs (Transact-SQL)](../system-tables/dbo-cdc-jobs-transact-sql.md)
