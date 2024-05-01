---
title: "sp_delete_alert (Transact-SQL)"
description: sp_delete_alert removes an alert.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_alert_TSQL"
  - "sp_delete_alert"
helpviewer_keywords:
  - "sp_delete_alert"
dev_langs:
  - "TSQL"
---
# sp_delete_alert (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes an alert.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_alert [ @name = ] N'name'
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the alert. *@name* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

Removing an alert also removes any notifications associated with the alert.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example removes an alert named `Test Alert`.

```sql
USE msdb;
GO

EXEC dbo.sp_delete_alert
    @name = N'Test Alert';
GO
```

## Related content

- [sp_add_alert (Transact-SQL)](sp-add-alert-transact-sql.md)
- [sp_help_alert (Transact-SQL)](sp-help-alert-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
