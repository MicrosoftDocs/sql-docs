---
title: "sp_help_log_shipping_alert_job (Transact-SQL)"
description: Returns the job ID of the alert job from the log shipping monitor.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_log_shipping_alert_job_TSQL"
  - "sp_help_log_shipping_alert_job"
helpviewer_keywords:
  - "sp_help_log_shipping_alert_job"
dev_langs:
  - "TSQL"
---
# sp_help_log_shipping_alert_job (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This stored procedure returns the job ID of the alert job from the log shipping monitor.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_log_shipping_alert_job
[ ; ]
```

## Arguments

None.

## Return code values

`0` (success) or `1` (failure).

## Result set

This stored procedure returns the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job ID of the log shipping alert job. If no log shipping alert job exists, it returns an empty result set.

## Remarks

`sp_help_log_shipping_alert_job` must be run from the `master` database on the monitor server.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
