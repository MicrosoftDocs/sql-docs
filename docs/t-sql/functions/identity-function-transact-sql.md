---
title: "IDENTITY (Function) (Transact-SQL)"
description: The IDENTITY (Function) is used only in a SELECT statement with an INTO table clause to insert an identity column into a new table.
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf, randolphwest, procha
ms.date: 10/29/2025
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
---
# IDENTITY (Function) (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricdw-fabricsqldb.md)]

 `IDENTITY` is used only in a `SELECT` statement with an INTO *table* clause to insert an identity column into a new table. Although similar, the `IDENTITY` function is not the `IDENTITY` property that is used with `CREATE TABLE` and `ALTER TABLE`.

> [!NOTE]  
>  To create an automatically incrementing number that can be used in multiple tables or that can be called from applications without referencing any table, see [Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md).  

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
IDENTITY (data_type [ , seed , increment ] ) AS column_name  
```  

## Arguments

#### *data_type*

The data type of the identity column. Valid data types for an identity column are any data types of the integer data type category, except for the **bit** data type, or **decimal** data type.

#### *seed*

The integer value to be assigned to the first row in the table. Each subsequent row is assigned the next identity value, which is equal to the last IDENTITY value plus the *increment* value. If neither *seed* nor *increment* is specified, both default to 1.

#### *increment*

The integer value to add to the *seed* value for successive rows in the table.

#### *column_name*

The name of the column that is to be inserted into the new table.

## Return types

 Returns the same as *data_type*.  

## Remarks

 Because this function creates a column in a table, a name for the column must be specified in the select list in one of the following ways:  

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

In Fabric Data Warehouse, you cannot specify `seed` or `increment`, as these values are automatically managed to provide unique integers. `BIGINT IDENTITY` is all that is required for a column definition in a `CREATE TABLE` statement. For more information, see [CREATE TABLE (Transact-SQL) IDENTITY (Property)](../statements/create-table-transact-sql-identity-property.md) and [IDENTITY in Fabric Data Warehouse](/fabric/data-warehouse/identity).

## Examples

 The following example inserts all rows from the `Contact` table from the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database into a new table called `NewContact`. The `IDENTITY` function is used to start identification numbers at 100 instead of 1 in the `NewContact` table.  

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
