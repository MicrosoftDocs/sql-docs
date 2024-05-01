---
title: "sp_droptype (Transact-SQL)"
description: sp_droptype deletes an alias data type from systypes.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_droptype_TSQL"
  - "sp_droptype"
helpviewer_keywords:
  - "sp_droptype"
dev_langs:
  - "TSQL"
---
# sp_droptype (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes an alias data type from `systypes`.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_droptype [ @typename = ] N'typename'
[ ; ]
```

## Arguments

#### [ @typename = ] N'*typename*'

The name of an alias data type that you own. *@typename* is **sysname**, with no default.

## Return code type

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

The **type** alias data type can't be dropped if tables or other database objects reference it.

> [!NOTE]  
> An alias data type can't be dropped if the alias data type is used within a table definition or if a rule or default is bound to it.

## Permissions

Requires membership in the **db_owner** fixed database role or the **db_ddladmin** fixed database role.

## Examples

The following example drops the alias data type `birthday`. This alias data type must already exist, or this example returns an error message.

```sql
USE master;
GO
EXEC sp_droptype 'birthday';
GO
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [sp_addtype (Transact-SQL)](sp-addtype-transact-sql.md)
- [sp_rename (Transact-SQL)](sp-rename-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
