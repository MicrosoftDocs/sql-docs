---
title: "sp_helplinkedsrvlogin (Transact-SQL)"
description: Provides information about login mappings defined against a specific linked server used for distributed queries and remote stored procedures.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helplinkedsrvlogin_TSQL"
  - "sp_helplinkedsrvlogin"
helpviewer_keywords:
  - "sp_helplinkedsrvlogin"
dev_langs:
  - "TSQL"
---
# sp_helplinkedsrvlogin (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Provides information about login mappings defined against a specific linked server used for distributed queries and remote stored procedures.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helplinkedsrvlogin
    [ [ @rmtsrvname = ] N'rmtsrvname' ]
    [ , [ @locallogin = ] N'locallogin' ]
[ ; ]
```

## Arguments

#### [ @rmtsrvname = ] N'*rmtsrvname*'

The name of the linked server that the login mapping applies to. *@rmtsrvname* is **sysname**, with a default of `NULL`. If `NULL`, all login mappings defined against all the linked servers defined in the local computer running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] are returned.

#### [ @locallogin = ] N'*locallogin*'

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login on the local server that's a mapping to the linked server *@rmtsrvname*. *@locallogin* is **sysname**, with a default of `NULL`. `NULL` specifies that all login mappings defined on *@rmtsrvname* are returned. If not `NULL`, a mapping for *@locallogin* to *@rmtsrvname* must already exist. *@locallogin* can be a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login or a Windows user. The Windows user must be granted access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] either directly or through its membership in a Windows group that was granted access.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `Linked Server` | **sysname** | Linked server name. |
| `Local Login` | **sysname** | Local login for which the mapping applies. |
| `Is Self Mapping` | **smallint** | `0` = `Local Login` is mapped to `Remote Login` when connecting to `Linked Server`.<br /><br />`1` = `Local Login` is mapped to the same login and password when connecting to `Linked Server`. |
| `Remote Login` | **sysname** | Login name on `Linked Server` that is mapped to `Local Login` when `Is Self Mapping` is `0`. If `Is Self Mapping` is `1`, `Remote Login` is `NULL`. |

## Remarks

Before you delete login mappings, use `sp_helplinkedsrvlogin` to determine the linked servers that are involved.

## Permissions

No permissions are checked.

## Examples

### A. Display all login mappings for all linked servers

The following example displays all login mappings for all linked servers defined on the local computer running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

```sql
EXEC sp_helplinkedsrvlogin;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Linked Server    Local Login   Is Self Mapping Remote Login
---------------- ------------- --------------- --------------
Accounts         NULL          1               NULL
Sales            NULL          1               NULL
Sales            Mary          0               sa
Marketing        NULL          1               NULL
```

### B. Display all login mappings for a linked server

The following example displays all locally defined login mappings for the `Sales` linked server.

```sql
EXEC sp_helplinkedsrvlogin 'Sales';
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Linked Server    Local Login   Is Self Mapping Remote Login
---------------- ------------- --------------- --------------
Sales            NULL          1               NULL
Sales            Mary          0               sa
```

### C. Display all login mappings for a local login

The following example displays all locally defined login mappings for the login `Mary`.

```sql
EXEC sp_helplinkedsrvlogin NULL, 'Mary';
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Linked Server    Local Login   Is Self Mapping Remote Login
---------------- ------------- --------------- --------------
Sales            NULL          1               NULL
Sales            Mary          0               sa
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sp_addlinkedserver (Transact-SQL)](sp-addlinkedserver-transact-sql.md)
- [sp_droplinkedsrvlogin (Transact-SQL)](sp-droplinkedsrvlogin-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
