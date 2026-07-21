---
title: "DROP TABLE (Transact-SQL)"
description: DROP TABLE (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 02/14/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
f1_keywords:
  - "DROP_TABLE_TSQL"
  - "DROP TABLE"
helpviewer_keywords:
  - "removing indexes"
  - "table removal [SQL Server]"
  - "deleting indexes"
  - "dropping data"
  - "deleting tables"
  - "dropping indexes"
  - "removing constraints"
  - "removing permissions"
  - "DROP TABLE statement"
  - "removing tables"
  - "deleting triggers"
  - "removing data"
  - "dropping tables"
  - "deleting permissions"
  - "dropping triggers"
  - "deleting constraints"
  - "removing triggers"
  - "deleting data"
  - "dropping constraints"
  - "dropping permissions"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---

# DROP TABLE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw.md)]

Removes one or more table definitions and all data, indexes, triggers, constraints, and permission specifications for those tables. Any view or stored procedure that references the dropped table must be explicitly dropped by using [DROP VIEW](../../t-sql/statements/drop-view-transact-sql.md) or [DROP PROCEDURE](../../t-sql/statements/drop-procedure-transact-sql.md). To report the dependencies on a table, use [sys.dm_sql_referencing_entities](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referencing-entities-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
-- Syntax for SQL Server, Azure SQL Database, Warehouse in Microsoft Fabric

DROP TABLE [ IF EXISTS ] { database_name.schema_name.table_name | schema_name.table_name | table_name } [ ,...n ]
[ ; ]
```

```syntaxsql
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse

DROP TABLE { database_name.schema_name.table_name | schema_name.table_name | table_name }
[;]
```

## Arguments

#### *database_name*

Is the name of the database in which the table was created.

Azure SQL Database supports the three-part name format `database_name.schema_name.object_name` when `database_name` is the current database or `database_name` is `tempdb` and `object_name` starts with `#` or `##`. Azure SQL Database doesn't support four-part names.

#### *IF EXISTS*

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).

Conditionally drops the table only if it already exists.

#### *schema_name*

Is the name of the schema to which the table belongs.

#### *table_name*

Is the name of the table to be removed.

## Remarks

`DROP TABLE` can't be used to drop a table that is referenced by a `FOREIGN KEY` constraint. The referencing `FOREIGN KEY` constraint or the referencing table must be dropped first. 

Multiple tables can be dropped in the same `DROP TABLE` statement. If both the referencing table in a `FOREIGN KEY` constraint and the table with the referenced primary or unique key are being dropped in the same `DROP TABLE` statement, the referencing table must be listed first.

When a table is dropped, rules or defaults on the table lose their binding, and any constraints or triggers associated with the table are automatically dropped. If you re-create a table, you must rebind the appropriate rules and defaults, re-create any triggers, and add all required constraints.

If you delete all rows in a table by using the `DELETE` statement or use the `TRUNCATE TABLE` statement, the table definition exists until it's dropped using `DROP TABLE`.

If you drop a table that contains a `varbinary(max)` column with the `FILESTREAM` attribute, any data stored in the file system isn't removed.

When a ledger table is dropped, its dependent objects (the history table and the ledger view) are also dropped. A history table or a ledger view can't be dropped directly. The system enforces a *soft-delete* semantics when dropping ledger tables and its dependent objects – they aren't actually dropped, but instead they're marked as dropped in system catalog views and renamed. For more information, see [Ledger considerations and limitations](../../relational-databases/security/ledger/ledger-limits.md).

> [!IMPORTANT]
> `DROP TABLE` and `CREATE TABLE` shouldn't be executed on the same table in the same batch. Otherwise an unexpected error may occur.

In Fabric SQL database, dropping a table drops it both from the database and from Fabric OneLake. All mirrored data for the dropped table is removed.

### Deferred deallocation

When a table is dropped, and the table or its indexes have 128 extents or more, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] defers the actual page deallocations, and their associated locks, until after the transaction commits. The table and indexes are dropped in two separate phases: logical and physical. In the logical phase, the existing allocation units are marked for deallocation and locked until the transaction commits. In the physical phase, a background process removes the pages marked for deallocation. This means that the space released by `DROP TABLE` might not be available for new allocations immediately.

If [accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md) is enabled, the separate logical and physical phases are used regardless of the number of extents.

## Permissions

Requires the `ALTER` permission on the schema to which the table belongs, `CONTROL` permission on the table, or membership in the `db_ddladmin` fixed database role.

If the statement drops a ledger table, the `ALTER LEDGER` permission is required.

## Examples

### A. Dropping a table in the current database

The following example removes the `ProductVendor1` table and its data and indexes from the current database.

```sql
DROP TABLE ProductVendor1;
```

### B. Dropping a table in another database

The following example drops the `SalesPerson2` table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. The example can be executed from any database on the server instance.

```sql
DROP TABLE AdventureWorks2022.dbo.SalesPerson2 ;
```

### C. Dropping a temporary table

The following example creates a temporary table, tests for its existence, drops it, and tests again for its existence by attempting to execute a `SELECT` statement, which fails. This example doesn't use the `IF EXISTS` syntax which is available beginning with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].

```sql
CREATE TABLE #temptable (col1 int);

INSERT INTO #temptable
VALUES (10);

SELECT col1 FROM #temptable;

IF OBJECT_ID(N'tempdb..#temptable', N'U') IS NOT NULL
  DROP TABLE #temptable;

SELECT col1 FROM #temptable;
```

### D. Dropping a table using IF EXISTS

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).

The following example creates a table named `T1`. Then the second statement drops the table. The third statement performs no action because the table is already dropped, however it doesn't cause an error.

```sql
CREATE TABLE T1 (Col1 int);

DROP TABLE T1;

DROP TABLE IF EXISTS T1;
```

## Related content

- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)   
- [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md)   
- [DELETE (Transact-SQL)](../../t-sql/statements/delete-transact-sql.md)   
- [sp_help (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)   
- [sp_spaceused (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)   
- [TRUNCATE TABLE (Transact-SQL)](../../t-sql/statements/truncate-table-transact-sql.md)   
- [DROP VIEW (Transact-SQL)](../../t-sql/statements/drop-view-transact-sql.md)   
- [DROP PROCEDURE (Transact-SQL)](../../t-sql/statements/drop-procedure-transact-sql.md)   
- [EVENTDATA (Transact-SQL)](../../t-sql/functions/eventdata-transact-sql.md)   
- [sys.sql_expression_dependencies (Transact-SQL)](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)
