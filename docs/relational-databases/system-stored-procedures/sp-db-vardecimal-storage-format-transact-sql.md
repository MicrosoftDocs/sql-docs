---
title: "sp_db_vardecimal_storage_format (Transact-SQL)"
description: sp_db_vardecimal_storage_format returns the current vardecimal storage format state of a database, or enables a database for vardecimal storage format.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_db_vardecimal_storage_format"
  - "sp_db_vardecimal_storage_format_TSQL"
helpviewer_keywords:
  - "sp_db_vardecimal_storage_format"
  - "decimal data type, compressing"
  - "compressing decimal data"
  - "numeric data type, compressing"
  - "database compression [SQL Server]"
  - "table compression [SQL Server]"
dev_langs:
  - "TSQL"
---
# sp_db_vardecimal_storage_format (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the current **vardecimal** storage format state of a database or enables a database for **vardecimal** storage format. In [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions, user databases are always enabled. However, because [row-level compression](../data-compression/row-compression-implementation.md) achieves the same goals, the **vardecimal** storage format is deprecated. Enabling databases for the **vardecimal** storage format is only necessary in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)].

> [!IMPORTANT]  
> Changing the **vardecimal** storage format state of a database can affect backup and recovery, database mirroring, `sp_attach_db`, log shipping, and replication.

## Syntax

```syntaxsql
sp_db_vardecimal_storage_format
    [ [ @dbname = ] N'dbname' ]
    [ , [ @vardecimal_storage_format = ] 'vardecimal_storage_format' ]
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The name of the database for which the storage format is to be changed. *@dbname* is **sysname**, with a default of `NULL`. If the database name is omitted, the **vardecimal** storage format status of all the databases in the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] are returned.

#### [ @vardecimal_storage_format = ] '*vardecimal_storage_format*'

Specifies whether the **vardecimal** storage format is enabled. *@vardecimal_storage_format* is **varchar(3)**, with a default of `NULL`. *@vardecimal_storage_format* can be `ON` or `OFF`. If a database name is provided but *@vardecimal_storage_format* is omitted, the current setting of the specified database is returned.

This argument has no effect on [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions.

## Return code values

`0` (success) or `1` (failure).

## Result set

If the database storage format can't be changed, `sp_db_vardecimal_storage_format` returns an error. If the database is already in the specified state, the stored procedure has no effect.

If the *@vardecimal_storage_format* argument isn't provided, `sp_db_vardecimal_storage_format` returns the columns `Database Name` and the `Vardecimal State`.

## Remarks

`sp_db_vardecimal_storage_format` returns the **vardecimal** state, but can't change the **vardecimal** state.

`sp_db_vardecimal_storage_format` fails in the following circumstances:

- There are active users in the database.
- The database is enabled for mirroring.
- The edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support **vardecimal** storage format.

To change the **vardecimal** storage format state to `OFF`, a database must be set to the simple recovery model. When a database is set to simple recovery, the log chain is broken. Perform a full database backup after you set the **vardecimal** storage format state to `OFF`.

Changing the state to `OFF` fails if there are tables using **vardecimal** database compression. To change the storage format of a table, use [sp_tableoption](sp-tableoption-transact-sql.md). To determine which tables in a database are using **vardecimal** storage format, use the `OBJECTPROPERTY` function and search for the `TableHasVarDecimalStorageFormat` property, as shown in the following example.

```sql
USE AdventureWorks2022;
GO

SELECT name,
    object_id,
    type_desc
FROM sys.objects
WHERE OBJECTPROPERTY(object_id, N'TableHasVarDecimalStorageFormat') = 1;
GO
```

## Examples

The following code enables compression in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database, confirms the state, and then compresses decimal and numeric columns in the `Sales.SalesOrderDetail` table.

```sql
USE master;
GO

EXEC sp_db_vardecimal_storage_format 'AdventureWorks2022', 'ON';
GO

-- Check the vardecimal storage format state for
-- all databases in the instance.
EXEC sp_db_vardecimal_storage_format;
GO

USE AdventureWorks2022;
GO

EXEC sp_tableoption 'Sales.SalesOrderDetail',
    'vardecimal storage format',
    1;
GO
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
