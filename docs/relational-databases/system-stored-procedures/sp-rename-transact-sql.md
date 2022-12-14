---
title: "sp_rename (Transact-SQL)"
description: "Changes the name of a user-created object in the current database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest, maghan
ms.date: 12/01/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_rename_TSQL"
  - "sp_rename"
helpviewer_keywords:
  - "renaming indexes"
  - "renaming columns"
  - "sp_rename"
  - "renaming tables"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=azure-sqldw-latest"
---

# sp_rename (Transact-SQL)

[!INCLUDE [sql-asdb-asa](../../includes/applies-to-version/sql-asdb-asa.md)]

Changes the name of a user-created object in the current database. This object can be a table, index, column, alias data type, or [!INCLUDE[msCoName](../../includes/msconame-md.md)]

[!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] common language runtime (CLR) user-defined type.

> [!NOTE]  
> In [!INCLUDE[ssazuresynapse](../../includes/ssazuresynapse_md.md)], `sp_rename` is in **Preview** for dedicated SQL pools and can only be used to rename a COLUMN in a user object.

[!INCLUDE[synapse-analytics-severless-sql-pools-tsql](Includes/synapse-analytics-severless-sql-pools-tsql.md)]

> [!CAUTION]  
> Changing any part of an object name can break scripts and stored procedures. We recommend you do not use this statement to rename stored procedures, triggers, user-defined functions, or views; instead, drop the object and re-create it with the new name.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for `sp_rename` in SQL Server and Azure SQL Database:

```syntaxsql
sp_rename [ @objname = ] 'object_name' , [ @newname = ] 'new_name'
    [ , [ @objtype = ] 'object_type' ]
```

Syntax for `sp_rename` (preview) in Azure Synapse Analytics:

```syntaxsql
sp_rename [ @objname = ] 'object_name' , [ @newname = ] 'new_name'
    , [ @objtype = ] 'COLUMN'
```

## Arguments

#### [ @objname = ] '*object_name*'

The current qualified or nonqualified name of the user object or data type. If the object to be renamed is a column in a table, *object_name* must be in the form *table.column* or *schema.table.column*. If the object to be renamed is an index, *object_name* must be in the form *table.index* or *schema.table.index*. If the object to be renamed is a constraint, *object_name* must be in the form *schema.constraint*.

Quotation marks are only necessary if a qualified object is specified. If a fully qualified name, including a database name, is provided, the database name must be the name of the current database. *object_name* is **nvarchar(776)**, with no default.

#### [ @newname = ] '*new_name*'

The new name for the specified object. *new_name* must be a one-part name and must follow the rules for identifiers. *newname* is **sysname**, with no default.

> [!NOTE]  
> Trigger names cannot start with # or ##.

#### [ @objtype = ] '*object_type*'

The type of object being renamed. *object_type* is **varchar(13)**, with a default of NULL, and can be one of these values.

| Value | Description |
| --- | --- |
| COLUMN | A column to be renamed. |
| DATABASE | A user-defined database. This object type is required when renaming a database. |
| INDEX | A user-defined index. Renaming an index with statistics, also automatically renames the statistics. |
| OBJECT | An item of a type tracked in [sys.objects](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md). For example, OBJECT could be used to rename objects including constraints (CHECK, FOREIGN KEY, PRIMARY/UNIQUE KEY), user tables, and rules. |
| STATISTICS | **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].<br /><br />Statistics created explicitly by a user or created implicitly with an index. Renaming the statistics of an index automatically renames the index as well. |
| USERDATATYPE | A [CLR user-defined type](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md) added by executing [CREATE TYPE](../../t-sql/statements/create-type-transact-sql.md) or [sp_addtype](../../relational-databases/system-stored-procedures/sp-addtype-transact-sql.md). |

#### [ @objtype = ] '*COLUMN*'

**Applies to**: Azure Synapse Analytics

In `sp_rename` (preview) for [!INCLUDE[ssazuresynapse](../../includes/ssazuresynapse_md.md)], *COLUMN* is a mandatory parameter specifying that the object type to be renamed is a column. It is a **varchar(13)** with no default value and must always be included in the `sp_rename` (preview) statement. A column can only be renamed if it is a non-distribution column.

## Return code values

0 (success) or a nonzero number (failure)

## Remarks

**Applies to** SQL Server (all supported versions) and Azure SQL Database:

- `sp_rename` automatically renames the associated index whenever a PRIMARY KEY or UNIQUE constraint is renamed. If a renamed index is tied to a PRIMARY KEY constraint, the PRIMARY KEY constraint is also automatically renamed by `sp_rename`.

- `sp_rename` can be used to rename primary and secondary XML indexes.

- Renaming a stored procedure, function, view, or trigger won't change the name of the corresponding object either in the definition column of the [sys.sql_modules](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md) catalog view or obtained using the [OBJECT_DEFINITION](../../t-sql/functions/object-definition-transact-sql.md) built-in function. Therefore, we recommend that `sp_rename` not be used to rename these object types. Instead, drop and re-create the object with its new name.

**Applies to** SQL Server (all supported versions), Azure SQL Database, and Azure Synapse Analytics:

- Renaming an object such as a table or column won't automatically rename references to that object. You must modify any objects that reference the renamed object manually. For example, if you rename a table column and that column is referenced in a trigger, you must modify the trigger to reflect the new column name. Use [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) to list dependencies on the object before renaming it.

- Renaming a column doesn't automatically update the metadata for any objects which SELECT all columns (using the `*`) from that table. For example, if you rename a table column and that column is referenced by a non-schema-bound view or function that SELECTs all columns (using the `*`), the metadata for the view or function continues to reflect the original column name. Refresh the metadata using [sp_refreshsqlmodule](../../relational-databases/system-stored-procedures/sp-refreshsqlmodule-transact-sql.md) or [sp_refreshview](../../relational-databases/system-stored-procedures/sp-refreshview-transact-sql.md).

- You can change the name of an object or data type in the current database only. The names of most system data types and system objects can't be changed.

## Permissions

To rename objects, columns, and indexes, requires ALTER permission on the object. To rename user types, requires CONTROL permission on the type. To rename a database, requires membership in the sysadmin or dbcreator fixed server roles. To rename a ledger table, ALTER LEDGER permission is required.

## Examples

### A. Rename a table

The following example renames the `SalesTerritory` table to `SalesTerr` in the `Sales` schema.

```sql
USE AdventureWorks2012;
GO
EXEC sp_rename 'Sales.SalesTerritory', 'SalesTerr';
GO
```

### B. Rename a column

The following example renames the `TerritoryID` column in the `SalesTerritory` table to `TerrID`.

```sql
USE AdventureWorks2012;
GO
EXEC sp_rename 'Sales.SalesTerritory.TerritoryID', 'TerrID', 'COLUMN';
GO
```

### C. Rename an index

The following example renames the `IX_ProductVendor_VendorID` index to `IX_VendorID`.

```sql
USE AdventureWorks2012;
GO
EXEC sp_rename N'Purchasing.ProductVendor.IX_ProductVendor_VendorID', N'IX_VendorID', N'INDEX';
GO
```

### D. Rename an alias data type

The following example renames the `Phone` alias data type to `Telephone`.

```sql
USE AdventureWorks2012;
GO
EXEC sp_rename N'Phone', N'Telephone', N'USERDATATYPE';
GO
```

### E. Rename constraints

The following examples rename a PRIMARY KEY constraint, a CHECK constraint and a FOREIGN KEY constraint. When renaming a constraint, the schema to which the constraint belongs must be specified.

```sql
USE AdventureWorks2012;
GO
-- Return the current Primary Key, Foreign Key and Check constraints for the Employee table.
SELECT name, SCHEMA_NAME(schema_id) AS schema_name, type_desc
FROM sys.objects
WHERE parent_object_id = (OBJECT_ID('HumanResources.Employee'))
AND type IN ('C','F', 'PK');
GO

-- Rename the primary key constraint.
EXEC sp_rename 'HumanResources.PK_Employee_BusinessEntityID', 'PK_EmployeeID';
GO

-- Rename a check constraint.
EXEC sp_rename 'HumanResources.CK_Employee_BirthDate', 'CK_BirthDate';
GO

-- Rename a foreign key constraint.
EXEC sp_rename 'HumanResources.FK_Employee_Person_BusinessEntityID', 'FK_EmployeeID';

-- Return the current Primary Key, Foreign Key and Check constraints for the Employee table.
SELECT name, SCHEMA_NAME(schema_id) AS schema_name, type_desc
FROM sys.objects
WHERE parent_object_id = (OBJECT_ID('HumanResources.Employee'))
AND type IN ('C','F', 'PK');
GO
```

```output
name                                  schema_name        type_desc
------------------------------------- ------------------ ----------------------
FK_Employee_Person_BusinessEntityID   HumanResources     FOREIGN_KEY_CONSTRAINT
PK_Employee_BusinessEntityID          HumanResources     PRIMARY_KEY_CONSTRAINT
CK_Employee_BirthDate                 HumanResources     CHECK_CONSTRAINT
CK_Employee_MaritalStatus             HumanResources     CHECK_CONSTRAINT
CK_Employee_HireDate                  HumanResources     CHECK_CONSTRAINT
CK_Employee_Gender                    HumanResources     CHECK_CONSTRAINT
CK_Employee_VacationHours             HumanResources     CHECK_CONSTRAINT
CK_Employee_SickLeaveHours            HumanResources     CHECK_CONSTRAINT

(7 row(s) affected)

name                                  schema_name        type_desc
------------------------------------- ------------------ ----------------------
FK_Employee_ID                        HumanResources     FOREIGN_KEY_CONSTRAINT
PK_Employee_ID                        HumanResources     PRIMARY_KEY_CONSTRAINT
CK_BirthDate                          HumanResources     CHECK_CONSTRAINT
CK_Employee_MaritalStatus             HumanResources     CHECK_CONSTRAINT
CK_Employee_HireDate                  HumanResources     CHECK_CONSTRAINT
CK_Employee_Gender                    HumanResources     CHECK_CONSTRAINT
CK_Employee_VacationHours             HumanResources     CHECK_CONSTRAINT
CK_Employee_SickLeaveHours            HumanResources     CHECK_CONSTRAINT

(7 row(s) affected)
```

### F. Rename statistics

The following example creates a statistics object named contactMail1 and then renames the statistic to NewContact by using `sp_rename`. When renaming statistics, the object must be specified in the format schema.table.statistics_name.

```sql
CREATE STATISTICS ContactMail1
    ON Person.Person (BusinessEntityID, EmailPromotion)
    WITH SAMPLE 5 PERCENT;

EXEC sp_rename 'Person.Person.ContactMail1', 'NewContact','Statistics';
```

## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)]

### G. Rename a column

The following example renames the `c1` column in the `table1` table to `col1`.

> [!NOTE]  
> This [!INCLUDE[ssazuresynapse](../../includes/ssazuresynapse_md.md)] feature is still in preview for dedicated SQL pools and is currently available only for objects in the **dbo** schema.

```sql
CREATE TABLE table1 (c1 INT, c2 INT);
EXEC sp_rename 'table1.c1', 'col1', 'COLUMN';
GO
```

## See also

- [sys.sql_expression_dependencies (Transact-SQL)](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)
- [sys.sql_modules (Transact-SQL)](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)
- [System Stored Procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)
- [Database Engine Stored Procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)
