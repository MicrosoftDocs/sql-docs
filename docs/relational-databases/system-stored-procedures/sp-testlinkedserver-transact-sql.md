---
title: "sp_testlinkedserver (Transact-SQL)"
description: sp_testlinkedserver tests the connection to a linked server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/23/2025
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
sp_testlinkedserver [ @servername = ] servername
[ ; ]
```

## Arguments

[!INCLUDE [extended-stored-procedures](includes/extended-stored-procedures.md)]

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

EXECUTE sp_addlinkedserver 'SEATTLESales', N'SQL Server';
GO

EXECUTE sp_testlinkedserver SEATTLESales;
GO
```

## Related content

- [sys.sp_addlinkedserver (Transact-SQL)](sp-addlinkedserver-transact-sql.md)
- [sys.sp_addlinkedsrvlogin (Transact-SQL)](sp-addlinkedsrvlogin-transact-sql.md)
