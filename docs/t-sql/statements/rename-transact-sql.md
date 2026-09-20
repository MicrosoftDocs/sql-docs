---
title: "RENAME (Transact-SQL)"
description: "RENAME (Transact-SQL) renames a user-created table."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: "WilliamDAssafMSFT"
ms.date: 08/24/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest"
---
# RENAME (Transact-SQL)
[!INCLUDE[asa-md](../../includes/applies-to-version/asa.md)]

Renames a user-created table in [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)].

This article applies to [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] only:

 - To rename a database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use the stored procedure [sp_renamedb](../../relational-databases/system-stored-procedures/sp-renamedb-transact-sql.md).
 - To rename a database in Azure SQL Database, use the [ALTER DATABASE (Azure SQL Database)](alter-database-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true) statement. 
 - Renaming standalone dedicated SQL pools (formerly SQL DW) is supported. Renaming a dedicated SQL pool in Azure Synapse Analytics workspaces isn't currently supported. 
 - [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]


## Syntax

```syntaxsql
-- Syntax for Azure Synapse Analytics

-- Rename a table.
RENAME OBJECT [::] [ [ database_name . [schema_name ] ] . ] | [schema_name . ] ] table_name TO new_table_name
[;]
```

## Arguments

#### RENAME OBJECT [::] [ [*database_name* . [ *schema_name* ] . ] | [ *schema_name* . ] ] *table_name* TO *new_table_name*

**Applies to:** [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]

Change the name of a user-defined table. Specify the table to be renamed with a one-, two-, or three-part name. Specify the new table *new_table_name* as a one-part name.

## Permissions

To run this command, you need this permission:

- **ALTER** permission on the table

## Limitations and Restrictions

### Cannot rename an external table, indexes, or views

You can't rename an external table, indexes, or views. Instead of renaming, you can drop the external table, index, or view and then re-create it with the new name.

### Cannot rename a table in use

You can't rename a table or database while it is in use. Renaming a table requires an exclusive lock on the table. If the table is in use, you may need to terminate sessions that are using the table. To terminate a session, you can use the KILL command. Use KILL cautiously since when a session is terminated any uncommitted work will be rolled back. Sessions in [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] are prefixed by 'SID'. Include 'SID' and the session number when invoking the KILL command. This example views a list of active or idle sessions and then terminates session 'SID1234'.

### Rename column restrictions

You can't rename a column that is used for the table's distribution. You also can't rename any columns in an external table or a temp table. 

### Views are not updated

When renaming a database, all views that use the former database name will become invalid. This behavior applies to views both inside and outside the database. For example, if the Sales database is renamed, a view that contains `SELECT * FROM Sales.dbo.table1` will become invalid. To resolve this issue, you can either avoid using three-part names in views, or update the views to reference the new database name.

When renaming a table, views aren't updated to reference the new table name. Each view, inside or outside of the database, that references the former table name will become invalid. To resolve this issue, you can update each view to reference the new table name.

When renaming a column, views aren't updated to reference the new column name. Views will keep showing the old column name until an alter view is performed. In certain cases, views can become invalid needing a drop and recreate.

## Locking

Renaming a table takes a shared lock on the DATABASE object, a shared lock on the SCHEMA object, and an exclusive lock on the table.

## Examples

### A. Rename a table

**Applies to:** [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]

This example renames the Customer table to Customer1.

```sql
-- Rename the customer table
RENAME OBJECT Customer TO Customer1;

RENAME OBJECT mydb.dbo.Customer TO Customer1;
```

When renaming a table, all objects and properties associated with the table are updated to reference the new table name. For example, table definitions, indexes, constraints, and permissions are updated. Views aren't updated.

### B. Move a table to a different schema

**Applies to:** [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]

If your intent is to move the object to a different schema, use [ALTER SCHEMA](../../t-sql/statements/alter-schema-transact-sql.md). For example, the following statement moves the table item from the product schema to the dbo schema.

```sql
ALTER SCHEMA dbo TRANSFER OBJECT::product.item;
```

### C. Terminate sessions before renaming a table

**Applies to:** [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]

You can't rename a table while it is in use. A rename of a table requires an exclusive lock on the table. If the table is in use, you may need to terminate the session using the table. To terminate a session, you can use the KILL command. Use KILL cautiously since when a session is terminated any uncommitted work will be rolled back. Sessions in [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] are prefixed by 'SID'. You'll need to include 'SID' and the session number when invoking the KILL command. This example views a list of active or idle sessions and then terminates session 'SID1234'.

```sql
-- View a list of the current sessions
SELECT session_id, login_name, status
FROM sys.dm_pdw_exec_sessions
WHERE status='Active' OR status='Idle';

-- Terminate a session using the session_id.
KILL 'SID1234';
```

## Related content

- [sys.sp_renamedb (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-renamedb-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](alter-database-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
