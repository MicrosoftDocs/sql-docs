---
title: "IDENTITY (Function) (Transact-SQL)"
description: The IDENTITY (Function) is used only in a SELECT statement with an INTO table clause to insert an identity column into a new table.
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf, randolphwest, procha
ms.date: 08/18/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "IDENTITY_TSQL"
  - "IDENTITY"
helpviewer_keywords:
  - "IDENTITY function"
  - "SELECT statement [SQL Server], IDENTITY function"
  - "inserting identity columns"
  - "columns [SQL Server], creating"
  - "identity columns [SQL Server], IDENTITY function"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =azure-sqldw-latest || =fabric || =fabric-sqldb"
---
# IDENTITY (Function) (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse FabricDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricdw-fabricsqldb.md)]

Use `IDENTITY` only in a `SELECT` statement with an `INTO table` clause to add an identity column to a new table. Although similar, the `IDENTITY` function isn't the same as the `IDENTITY` property that you use with `CREATE TABLE` and `ALTER TABLE`.

> [!NOTE]  
> To create an automatically incrementing number that you can use in multiple tables or call from applications without referencing any table, see [Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md).  

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server, Azure SQL Database, Azure SQL Managed Instance, SQL database in Fabric:

```syntaxsql
IDENTITY ( data_type [ , seed , increment ] ) AS column_name  
```

Syntax for Fabric Data Warehouse:

```syntaxsql
IDENTITY ( data_type ) AS column_name  
```

## Arguments

#### *data_type*

The data type of the identity column. Valid data types for an identity column are any data types in the **integer** data type category, except for the **bit** data type, or the **decimal** data type.

#### *seed*

The integer value to assign to the first row in the table. Each subsequent row gets the next identity value, which is the last `IDENTITY` value plus the *increment* value. If you don't specify *seed* or *increment*, both default to 1.

#### *increment*

The integer value to add to the *seed* value for successive rows in the table.

#### *column_name*

The name of the column to insert into the new table.

## Return types

 Returns the same as *data_type*.  

## Remarks

 Because this function creates a column in a table, you must specify a name for the column in the select list in one of the following ways:  

```sql  
--(1)  
SELECT IDENTITY(int, 1,1) AS ID_Num  
INTO NewTable  
FROM OldTable;  

--(2)  
SELECT ID_Num = IDENTITY(int, 1, 1)  
INTO NewTable  
FROM OldTable;  
```  

### Support in Microsoft Fabric Data Warehouse

In Fabric Data Warehouse, you can't specify `seed` or `increment` values because the system automatically manages these values to provide unique integers. For a column definition in a `CREATE TABLE` statement, you only need to use `BIGINT IDENTITY`. For more information, see [CREATE TABLE (Transact-SQL) IDENTITY (Property)](../statements/create-table-transact-sql-identity-property.md?view=fabric&preserve-view=true) and [IDENTITY in Fabric Data Warehouse](/fabric/data-warehouse/identity).

## Examples

The following example inserts all rows from the `Contact` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database into a new table named `NewContact`. The `IDENTITY` function sets the identification numbers to start at 100 instead of 1 in the `NewContact` table.  

```sql  
USE AdventureWorks2022;  
GO  
IF OBJECT_ID (N'Person.NewContact', N'U') IS NOT NULL  
    DROP TABLE Person.NewContact;  
GO  
ALTER DATABASE AdventureWorks2022 SET RECOVERY BULK_LOGGED;  
GO  
SELECT  IDENTITY(smallint, 100, 1) AS ContactNum,  
        FirstName AS First,  
        LastName AS Last  
INTO Person.NewContact  
FROM Person.Person;  
GO  
ALTER DATABASE AdventureWorks2022 SET RECOVERY FULL;  
GO  
SELECT ContactNum, First, Last FROM Person.NewContact;  
GO  
```  

## Related content

- [CREATE TABLE (Transact-SQL)](../statements/create-table-transact-sql.md)
- [@@IDENTITY (Transact-SQL)](identity-transact-sql.md)
- [CREATE TABLE (Transact-SQL) IDENTITY (Property)](../statements/create-table-transact-sql-identity-property.md)
- [SELECT @local_variable (Transact-SQL)](../language-elements/select-local-variable-transact-sql.md)
- [DBCC CHECKIDENT (Transact-SQL)](../database-console-commands/dbcc-checkident-transact-sql.md)
- [sys.identity_columns (Transact-SQL)](../../relational-databases/system-catalog-views/sys-identity-columns-transact-sql.md)
