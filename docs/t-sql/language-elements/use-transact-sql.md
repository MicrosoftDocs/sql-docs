---
title: "USE (Transact-SQL)"
description: Changes the database context to the specified database or database snapshot.
author: uc-msft
ms.author: umajay
ms.reviewer: wiassaf
ms.date: 10/28/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
f1_keywords:
  - "USE_TSQL"
  - "USE"
helpviewer_keywords:
  - "USE statement"
  - "database context [SQL Server]"
  - "context changes [SQL Server]"
  - "modifying database context"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# USE (Transact-SQL)

[!INCLUDE [sql-asdbmi-fabricdw](../../includes/applies-to-version/sql-asdbmi-fabricdw.md)]

Changes the database context to the specified database or database snapshot.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
USE { database_name }
[ ; ]
```

## Arguments

#### *database_name*

The name of the database or database snapshot to which the user context is switched. Database and database snapshot names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).

In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], the `USE` statement isn't available. Instead, connect directly to the desired database. In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the database parameter can only refer to the current database. If a database other than the current database is provided, the `USE` statement doesn't switch between databases, and error code 40508 is returned. 

## Remarks

When a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login connects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the login is automatically connected to its default database and acquires the security context of a database user. If no database user is created for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login, the login connects as guest. If the database user doesn't have CONNECT permission on the database, the `USE` statement fails. If no default database is assigned to the login, its default database is set to `master`.

`USE` is executed at both compile and execution time and takes effect immediately. Therefore, statements that appear in a batch after the `USE` statement are executed in the specified database.

## Permissions

Requires `CONNECT` permission on the target database.

## Examples

The following example changes the database context to the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO
```

## Related content

- [CREATE LOGIN (Transact-SQL)](../statements/create-login-transact-sql.md)
- [CREATE USER (Transact-SQL)](../statements/create-user-transact-sql.md)
- [Principals (Database Engine)](../../relational-databases/security/authentication-access/principals-database-engine.md)
- [CREATE DATABASE](../statements/create-database-transact-sql.md)
- [DROP DATABASE (Transact-SQL)](../statements/drop-database-transact-sql.md)
- [EXECUTE (Transact-SQL)](execute-transact-sql.md)
