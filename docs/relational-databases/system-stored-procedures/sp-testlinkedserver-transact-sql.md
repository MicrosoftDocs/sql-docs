---
title: "sp_testlinkedserver (Transact-SQL)"
description: sp_testlinkedserver tests the connection to a linked server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_testlinkedserver"
  - "sp_testlinkedserver_TSQL"
helpviewer_keywords:
  - "sp_testlinkedserver"
dev_langs:
  - "TSQL"
---
# sp_testlinkedserver (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Tests the connection to a linked server. If the test is unsuccessful, the procedure raises an exception with the reason of the failure.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_testlinkedserver [ @servername ] = servername
[ ; ]
```

## Arguments

#### [ @servername = ] N'*servername*'

The name of the linked server. *@servername* is **sysname**, with no default.

## Result set

None.

## Permissions

No permissions are checked. However, the caller must have the appropriate login mapping.

## Examples

The following example creates a linked server named `SEATTLESales`, and then tests the connection.

```sql
USE master;
GO

EXEC sp_addlinkedserver 'SEATTLESales', N'SQL Server';
GO

EXEC sp_testlinkedserver SEATTLESales;
GO
```

## Related content

- [sp_addlinkedserver (Transact-SQL)](sp-addlinkedserver-transact-sql.md)
- [sp_addlinkedsrvlogin (Transact-SQL)](sp-addlinkedsrvlogin-transact-sql.md)
