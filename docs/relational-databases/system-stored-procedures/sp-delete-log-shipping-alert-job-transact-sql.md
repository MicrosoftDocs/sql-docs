---
title: "sp_delete_log_shipping_alert_job (Transact-SQL)"
description: Removes an alert job from the log shipping monitor server if the job exists.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_log_shipping_alert_job"
  - "sp_delete_log_shipping_alert_job_TSQL"
helpviewer_keywords:
  - "sp_delete_log_shipping_alert_job"
dev_langs:
  - "TSQL"
---
# sp_delete_log_shipping_alert_job (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes an alert job from the log shipping monitor server if the job exists and there are no more primary or secondary databases to be monitored.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_log_shipping_alert_job
[ ; ]
```

## Arguments

None.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_delete_log_shipping_alert_job` must be run from the `master` database on the monitor server.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Examples

This example shows the execution of `sp_delete_log_shipping_alert_job` to delete an alert job.

```sql
USE master;
GO
EXEC sp_delete_log_shipping_alert_job;
GO
```

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
