---
title: DBCC UPDATEUSAGE (Transact-SQL)
description: DBCC UPDATEUSAGE reports and corrects pages and row count inaccuracies in the catalog views.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/05/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "UPDATEUSAGE"
  - "UPDATEUSAGE_TSQL"
  - "DBCC_UPDATEUSAGE_TSQL"
  - "DBCC UPDATEUSAGE"
helpviewer_keywords:
  - "inaccurate page or row counts [SQL Server]"
  - "space [SQL Server], usage reports"
  - "updating space usage information"
  - "updating row counts"
  - "disk space [SQL Server], inaccurate counts"
  - "counting pages"
  - "reporting count inaccuracies"
  - "updating page counts"
  - "synchronization [SQL Server], inaccurate counts"
  - "incorrect space usage reports [SQL Server]"
  - "DBCC UPDATEUSAGE statement"
  - "integrity [SQL Server], database objects"
  - "counting rows"
  - "row count accuracy [SQL Server]"
  - "page count accuracy [SQL Server]"
dev_langs:
  - "TSQL"
---

# DBCC UPDATEUSAGE (Transact-SQL)

[!INCLUDE [SQL Server SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Reports and corrects pages and row count inaccuracies in the catalog views. These inaccuracies may cause incorrect space usage reports returned by the `sp_spaceused` system stored procedure.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC UPDATEUSAGE
(   { database_name | database_id | 0 }
    [ , { table_name | table_id | view_name | view_id }
    [ , { index_name | index_id } ] ]
) [ WITH [ NO_INFOMSGS ] [ , ] [ COUNT_ROWS ] ]
```

## Arguments

#### *database_name* | *database_id* | 0

The name or ID of the database for which to report and correct space usage statistics. If 0 is specified, the current database is used. Database names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).

#### *table_name* | *table_id* | *view_name* | *view_id*

The name or ID of the table or indexed view for which to report and correct space usage statistics. Table and view names must comply with the rules for identifiers.

#### *index_id* | *index_name*

The ID or name of the index to use. If not specified, the statement processes all indexes for the specified table or view.

#### WITH

Allows options to be specified.

#### NO_INFOMSGS

Suppresses all informational messages.

#### COUNT_ROWS

Specifies that the row count column is updated with the current count of the number of rows in the table or view.

## Remarks

`DBCC UPDATEUSAGE` corrects the rows, used pages, reserved pages, leaf pages and data page counts for each partition in a table or index. If there are no inaccuracies in the system tables, `DBCC UPDATEUSAGE` returns no data. If inaccuracies are found and corrected and WITH NO_INFOMSGS isn't used, `DBCC UPDATEUSAGE` returns the rows and columns being updated in the system tables.

`DBCC CHECKDB` has been enhanced to detect when page or row counts become negative. When detected, the `DBCC CHECKDB` output contains a warning and a recommendation to run `DBCC UPDATEUSAGE` to address the issue.

## Best practices

We recommend the following:

- Don't run `DBCC UPDATEUSAGE` routinely, as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] maintains the metadata under most circumstances. `DBCC UPDATEUSAGE` should be run on an as-needed basis, for example, when you suspect incorrect values are being returned by `sp_spaceused`. `DBCC UPDATEUSAGE` can take some time to run on large tables or databases.
- Consider running `DBCC UPDATEUSAGE` routinely (for example, weekly) only if the database undergoes frequent Data Definition Language (DDL) modifications, such as CREATE, ALTER, or DROP statements.

## Result sets

`DBCC UPDATEUSAGE` returns (values may vary):

`DBCC execution completed. If DBCC printed error messages, contact your system administrator.`

## Permissions

Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.

## Examples

### A. Update page or row counts or both for all objects in the current database

The following example specifies `0` for the database name and `DBCC UPDATEUSAGE` reports updated page or row count information for the current database.

```sql
DBCC UPDATEUSAGE (0);
GO
```

### B. Update page or row counts or both for AdventureWorks, and suppressing informational messages

The following example specifies [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] as the database name and suppresses all informational messages.

```sql
DBCC UPDATEUSAGE (AdventureWorks2022) WITH NO_INFOMSGS;
GO
```

### C. Update page or row counts or both for the Employee table

The following example reports updated page or row count information for the `Employee` table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
DBCC UPDATEUSAGE (AdventureWorks2022, 'HumanResources.Employee');
GO
```

### D. Update page or row counts or both for a specific index in a table

 The following example specifies `IX_Employee_ManagerID` as the index name.

```sql
DBCC UPDATEUSAGE (AdventureWorks2022, 'HumanResources.Employee', IX_Employee_OrganizationLevel_OrganizationNode);
GO
```

## See also

- [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md)
- [sp_spaceused (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)
- [UPDATE STATISTICS (Transact-SQL)](../../t-sql/statements/update-statistics-transact-sql.md)
