---
title: "sp_delete_operator (Transact-SQL)"
description: sp_delete_operator removes an operator.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_operator"
  - "sp_delete_operator_TSQL"
helpviewer_keywords:
  - "sp_delete_operator"
dev_langs:
  - "TSQL"
---
# sp_delete_operator (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes an operator.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_operator
    [ @name = ] N'name'
    [ , [ @reassign_to_operator = ] N'reassign_to_operator' ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the operator to delete. *@name* is **sysname**, with no default.

#### [ @reassign_to_operator = ] N'*reassign_to_operator*'

The name of an operator to whom the specified operator's alerts can be reassigned. *@reassign_to_operator* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

When an operator is removed, all the notifications associated with the operator are also removed.

## Permissions

Members of the **sysadmin** fixed server role can execute `sp_delete_operator`.

## Examples

The following example deletes operator `François Ajenstat`.

```sql
USE msdb;
GO

EXEC sp_delete_operator @name = 'François Ajenstat';
GO
```

## Related content

- [sp_add_operator (Transact-SQL)](sp-add-operator-transact-sql.md)
- [sp_help_operator (Transact-SQL)](sp-help-operator-transact-sql.md)
- [sp_update_operator (Transact-SQL)](sp-update-operator-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
