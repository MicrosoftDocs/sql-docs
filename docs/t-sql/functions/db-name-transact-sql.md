---
title: "DB_NAME (Transact-SQL)"
description: The DB_NAME function returns the name of a specified database.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 04/04/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "DB_NAME"
  - "DB_NAME_TSQL"
helpviewer_keywords:
  - "database names [SQL Server], DB_NAME"
  - "names [SQL Server], databases"
  - "viewing database names"
  - "displaying database names"
  - "DB_NAME function"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# DB_NAME (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

This function returns the name of a specified database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DB_NAME ( [ database_id ] )
```

## Arguments

#### *database_id*

The identification number (ID) of the database whose name `DB_NAME` returns. If the call to `DB_NAME` omits *database_id*, or the *database_id* is `0`, `DB_NAME` returns the name of the current database.

## Return types

**nvarchar(128)**

## Permissions

If the caller of `DB_NAME` doesn't own a specific non-`master` or non-`tempdb` database, `ALTER ANY DATABASE` or `VIEW ANY DATABASE` server-level permissions are required at minimum to see the corresponding `DB_ID` row.

For the `master` database, `DB_ID` needs `CREATE DATABASE` permission at minimum.

The database to which the caller connects always appears in `sys.databases`.

> [!IMPORTANT]  
> By default, the public role has the `VIEW ANY DATABASE` permission, which allows all logins to see database information. To prevent a login from detecting a database, `REVOKE` the `VIEW ANY DATABASE` permission from public, or `DENY` the `VIEW ANY DATABASE` permission for individual logins.

## Examples

### A. Return the current database name

This example returns the name of the current database.

```sql
SELECT DB_NAME() AS [Current Database];
GO
```

### B. Return the database name of a specified database ID

This example returns the database name for database ID `3`.

```sql
USE master;
GO

SELECT DB_NAME(3) AS [Database Name];
GO
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

### C. Return the current database name

This example returns the current database name.

```sql
SELECT DB_NAME() AS [Current Database];
```

### D. Return the name of a database by using the database ID

This example returns the database name and `database_id` for each database.

```sql
SELECT DB_NAME(database_id) AS [Database],
       database_id
FROM sys.databases;
```

## Related content

- [DB_ID (Transact-SQL)](db-id-transact-sql.md)
- [Metadata Functions (Transact-SQL)](metadata-functions-transact-sql.md)
- [sys.databases (Transact-SQL)](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
