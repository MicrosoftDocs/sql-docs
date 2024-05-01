---
title: "sp_delete_log_shipping_secondary_primary (Transact-SQL)"
description: Removes the information about the specified primary server from the secondary server, and removes the copy job and restore job from the secondary.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_log_shipping_secondary_primary_TSQL"
  - "sp_delete_log_shipping_secondary_primary"
helpviewer_keywords:
  - "sp_delete_log_shipping_secondary_primary"
dev_langs:
  - "TSQL"
---
# sp_delete_log_shipping_secondary_primary (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

`sp_delete_log_shipping_secondary_primary` removes the information about the specified primary server from the secondary server, and removes the copy job and restore job from the secondary.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_log_shipping_secondary_primary
    [ @primary_server = ] N'primary_server'
    , [ @primary_database = ] N'primary_database'
[ ; ]
```

## Arguments

#### [ @primary_server = ] N'*primary_server*'

The name of the primary instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration. *@primary_server* is **sysname**, and can't be `NULL`.

#### [ @primary_database = ] N'*primary_database*'

The name of the database on the primary server. *@primary_database* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_delete_log_shipping_secondary_primary` must be run from the `master` database on the secondary server. This stored procedure does the following:

1. Deletes the copy and restore jobs for the secondary ID.
1. Deletes the entry in **log_shipping_secondary**.
1. Calls `sp_delete_log_shipping_alert_job` on the monitor server.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
