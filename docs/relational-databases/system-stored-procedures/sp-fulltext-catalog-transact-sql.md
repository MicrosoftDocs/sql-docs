---
title: "sp_fulltext_catalog (Transact-SQL)"
description: Creates and drops a full-text catalog, and starts and stops the indexing action for a catalog.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/07/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_fulltext_catalog_TSQL"
  - "sp_fulltext_catalog"
helpviewer_keywords:
  - "sp_fulltext_catalog"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_fulltext_catalog (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Creates and drops a full-text catalog, and starts and stops the indexing action for a catalog. Multiple full-text catalogs can be created for each database.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE FULLTEXT CATALOG](../../t-sql/statements/create-fulltext-catalog-transact-sql.md), [ALTER FULLTEXT CATALOG](../../t-sql/statements/alter-fulltext-catalog-transact-sql.md), and [DROP FULLTEXT CATALOG](../../t-sql/statements/drop-fulltext-catalog-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_fulltext_catalog
    [ @ftcat = ] N'ftcat'
    , [ @action = ] 'action'
    [ , [ @path = ] N'path' ]
[ ; ]
```

## Arguments

#### [ @ftcat = ] N'*ftcat*'

The name of the full-text catalog. Catalog names must be unique for each database. *@ftcat* is **sysname**, with no default.

#### [ @action = ] '*action*'

The action to be performed. *@action* is **varchar(20)**, and can be one of these values.

> [!NOTE]  
> Full-text catalogs can be created, dropped, and modified as needed. However, avoid making schema changes on multiple catalogs at the same time. These actions can be performed using the `sp_fulltext_table` stored procedure, which is the recommended way.

| Value | Description |
| --- | --- |
| **create** | Creates an empty, new full-text catalog in the file system and adds an associated row in `sysfulltextcatalogs` with the *@ftcat* and *@path*, if present, values. *@ftcat* must be unique within the database. |
| **drop** | Drops *@ftcat* by removing it from the file system and deleting the associated row in `sysfulltextcatalogs`. This action fails if this catalog contains indexes for one or more tables. `sp_fulltext_table '<table_name>', 'drop'` should be executed to drop the tables from the catalog.<br /><br />An error is displayed if the catalog doesn't exist. |
| **start_incremental** | Starts an incremental population for *@ftcat*. An error is displayed if the catalog doesn't exist. If a full-text index population is already active, a warning is displayed but no population action occurs. With incremental population only changed rows are retrieved for full-text indexing, provided there's a **timestamp** column present in the table being full-text indexed. |
| **start_full** | Starts a full population for *@ftcat*. Every row of every table associated with this full-text catalog is retrieved for full-text indexing even if they have already been indexed. |
| **stop** | Stops an index population for *@ftcat*. An error is displayed if the catalog doesn't exist. No warning is displayed if population is already stopped. |
| **rebuild** | Rebuilds *@ftcat*. When a catalog is rebuilt, the existing catalog is deleted and a new catalog is created in its place. All the tables that have full-text indexing references are associated with the new catalog. Rebuilding resets the full-text metadata in the database system tables.<br /><br />If change tracking is OFF, rebuilding doesn't cause a repopulation of the newly created full-text catalog. In this case, to repopulate, execute `sp_fulltext_catalog` with the **start_full** or **start_incremental** action. |

#### [ @path = ] N'*path*'

The root directory (not the complete physical path) for a **create** action. *@path* is **nvarchar(100)**, with a default of `NULL`, which indicates the use of the default location specified at setup.

This is the `FTData` subdirectory in the `MSSQL` directory; for example, `C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\FTData`. The specified root directory must reside on a drive on the same computer, consist of more than just the drive letter, and can't be a relative path. Network drives, removable drives, floppy disks, and UNC paths aren't supported. Full-text catalogs must be created on a local hard drive associated with an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

*@path* is valid only when *@action* is **create**. For actions other than **create** (**stop**, **rebuild**, and so on), *@path* must be NULL or omitted.

If the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is a virtual server in a cluster, the catalog directory specified needs to be on a shared disk drive on which the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] resource depends. If *@path* isn't specified, the location of default catalog directory is on the shared disk drive, in the directory that was specified when the virtual server was installed.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

The **start_full** action is used to create a complete snapshot of the full-text data in *@ftcat*. The **start_incremental** action is used to reindex only the changed rows in the database. Incremental population can be applied only if the table has a column of the type **timestamp**. If a table in the full-text catalog doesn't contain a column of the type **timestamp**, the table undergoes a full population.

Full-text catalog and index data is stored in files created in a full-text catalog directory. The full-text catalog directory is created as a subdirectory of the directory specified in *@path* or in the server default full-text catalog directory if *@path* isn't specified. The name of the full-text catalog directory is built in a way that guarantees it's unique on the server. Therefore, all full-text catalog directories on a server can share the same path.

## Permissions

The caller is required to be member of the **db_owner** role. Depending on the action requested, the caller shouldn't be denied ALTER or CONTROL permissions (which **db_owner** has) on the target full-text catalog.

## Examples

### A. Create a full-text catalog

This example creates an empty full-text catalog, `Cat_Desc`, in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO
EXEC sp_fulltext_catalog 'Cat_Desc', 'create';
GO
```

### B. Rebuild a full-text catalog

This example rebuilds an existing full-text catalog, `Cat_Desc`, in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO
EXEC sp_fulltext_catalog 'Cat_Desc', 'rebuild';
GO
```

### C. Start the population of a full-text catalog

This example begins a full population of the `Cat_Desc` catalog.

```sql
USE AdventureWorks2022;
GO
EXEC sp_fulltext_catalog 'Cat_Desc', 'start_full';
GO
```

### D. Stop the population of a full-text catalog

This example stops the population of the `Cat_Desc` catalog.

```sql
USE AdventureWorks2022;
GO
EXEC sp_fulltext_catalog 'Cat_Desc', 'stop';
GO
```

### E. Remove a full-text catalog

This example removes the `Cat_Desc` catalog.

```sql
USE AdventureWorks2022;
GO
EXEC sp_fulltext_catalog 'Cat_Desc', 'drop';
GO
```

## Related content

- [FULLTEXTCATALOGPROPERTY (Transact-SQL)](../../t-sql/functions/fulltextcatalogproperty-transact-sql.md)
- [sp_fulltext_database (Transact-SQL)](sp-fulltext-database-transact-sql.md)
- [sp_help_fulltext_catalogs (Transact-SQL)](sp-help-fulltext-catalogs-transact-sql.md)
- [sp_help_fulltext_catalogs_cursor (Transact-SQL)](sp-help-fulltext-catalogs-cursor-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Full-Text Search](../search/full-text-search.md)
