---
title: "sp_add_log_shipping_alert_job (Transact-SQL)"
description: "This stored procedure checks to see if an alert job has been created on this server."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 06/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_log_shipping_alert_job_TSQL"
  - "sp_add_log_shipping_alert_job"
helpviewer_keywords:
  - "sp_add_log_shipping_alert_job"
dev_langs:
  - "TSQL"
---
# sp_add_log_shipping_alert_job (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This stored procedure checks to see if an alert job has been created on this server. If an alert job doesn't exist, `sp_add_log_shipping_alert_job` creates the alert job, and adds its job ID to the `log_shipping_monitor_alert` table. The alert job is enabled by default, and runs on a schedule of once every two minutes.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_add_log_shipping_alert_job
[ , [ @alert_job_id = ] alert_job_id OUTPUT ]
[ ; ]
```

## Arguments

#### [ @alert_job_id = ] *alert_job_id* OUTPUT

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job ID of the log shipping alert job.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_add_log_shipping_alert_job` must be run from the `master` database on the monitor server.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Examples

This example shows the execution of `sp_add_log_shipping_alert_job` to create an alert job ID.

```sql
USE master;
GO

EXEC sp_add_log_shipping_alert_job;
```

## Related content

- [About Log Shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
