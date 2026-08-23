---
title: IDENT_SEED (Transact-SQL)
description: IDENT_SEED returns the original seed value specified when creating an identity column.
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf
ms.date: 08/19/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "IDENT_SEED_TSQL"
  - "IDENT_SEED"
helpviewer_keywords:
  - "identity columns [SQL Server], IDENT_SEED function"
  - "seed values [SQL Server]"
  - "IDENT_SEED function"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# IDENT_SEED (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The `IDENT_SEED` system function returns the original seed value specified when creating an identity column in a table or a view. Changing the current value of an identity column by using `DBCC CHECKIDENT` doesn't change the value returned by this function.  

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
IDENT_SEED ( 'table_or_view' )
```  

## Arguments

#### *table_or_view*

An [expression](../language-elements/expressions-transact-sql.md) that specifies the table or view to check for an identity seed value. *table_or_view* can be a character string constant enclosed in quotation marks, a variable, a function, or a column name. *table_or_view* is **char**, **nchar**, **varchar**, or **nvarchar**.

## Return types

**numeric**([@@MAXPRECISION](max-precision-transact-sql.md),0))  

## Exceptions

 Returns `NULL` on error or if a caller doesn't have permission to view the object.  

 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a user can only view the metadata of securables that the user either owns or is granted permission on. This security means that metadata-emitting, built-in functions such as `IDENT_SEED` might return `NULL` if the user doesn't have any permission on the object. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  

## Remarks

The `IDENT_SEED` function is not available for [identity columns in Fabric Data Warehouse](/fabric/data-warehouse/identity).

## Examples

<a id="a-returning-the-seed-value-from-a-specified-table"></a>

### A. Return the seed value from a specified table

 The following example returns the seed value for the `Person.Address` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  

```sql  
USE AdventureWorks2022;  
GO  
SELECT IDENT_SEED('Person.Address') AS Identity_Seed;  
GO  
```  

<a id="b-returning-the-seed-value-from-multiple-tables"></a>

### B. Return the seed value from multiple tables

 The following example returns the tables in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database with an identity column with a seed value.  

```sql  
USE AdventureWorks2022;  
GO  
SELECT TABLE_SCHEMA, TABLE_NAME,   
   IDENT_SEED(TABLE_SCHEMA + '.' + TABLE_NAME) AS IDENT_SEED  
FROM INFORMATION_SCHEMA.TABLES  
WHERE IDENT_SEED(TABLE_SCHEMA + '.' + TABLE_NAME) IS NOT NULL;  
GO  
```  

 Here is a partial result set.  

 ```
 TABLE_SCHEMA       TABLE_NAME                   IDENT_SEED  
------------       ---------------------------  -----------  
Person             Address                                1  
Production         ProductReview                          1  
Production         TransactionHistory                100000  
Person             AddressType                            1  
Production         ProductSubcategory                     1  
Person             vAdditionalContactInfo                 1  
dbo                AWBuildVersion                         1
```  

## Related content

- [Expressions (Transact-SQL)](../language-elements/expressions-transact-sql.md)
- [System Functions by category for Transact-SQL](../../relational-databases/system-functions/system-functions-category-transact-sql.md)
- [IDENT_CURRENT (Transact-SQL)](ident-current-transact-sql.md)
- [IDENT_INCR (Transact-SQL)](ident-incr-transact-sql.md)
- [DBCC CHECKIDENT (Transact-SQL)](../database-console-commands/dbcc-checkident-transact-sql.md)
- [sys.identity_columns (Transact-SQL)](../../relational-databases/system-catalog-views/sys-identity-columns-transact-sql.md)
