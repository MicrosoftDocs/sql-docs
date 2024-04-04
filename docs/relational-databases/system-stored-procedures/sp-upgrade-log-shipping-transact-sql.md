---
title: "sp_upgrade_log_shipping (Transact-SQL)"
description: "sp_upgrade_log_shipping is invoked automatically for upgrading metadata that is specific to log shipping."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_upgrade_log_shipping"
  - "sp_upgrade_log_shipping_TSQL"
helpviewer_keywords:
  - "sp_upgrade_log_shipping"
dev_langs:
  - "TSQL"
---
# sp_upgrade_log_shipping (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The `sp_upgrade_log_shipping` stored procedure is invoked automatically for upgrading metadata that is specific to log shipping.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_upgrade_log_shipping
[ ; ]
```

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

This stored procedure is invoked automatically during [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] upgrade for upgrading metadata for log shipping. You don't need to execute this procedure explicitly, unless a problem occurs with the metadata during upgrade.

`sp_upgrade_log_shipping` must be run from the `master` database on the primary, secondary, or monitor server.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Related content

- [About Log Shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
