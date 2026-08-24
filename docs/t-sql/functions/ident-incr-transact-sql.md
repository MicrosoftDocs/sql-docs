---
title: IDENT_INCR (Transact-SQL)
description: IDENT_INCR returns the increment value specified when creating a table or view's identity column.
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
  - "IDENT_INCR"
  - "IDENT_INCR_TSQL"
helpviewer_keywords:
  - "incremental values [SQL Server]"
  - "IDENT_INCR function"
  - "identity columns [SQL Server], IDENT_INCR function"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# IDENT_INCR (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The `IDENT_INCR` system function returns the increment value specified when creating a table or view's identity column.  

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
IDENT_INCR ( 'table_or_view' )
```  

## Arguments

#### *table_or_view*

An [expression](../language-elements/expressions-transact-sql.md) specifying the table or view to check for a valid identity increment value. *table_or_view* can be a character string constant enclosed in quotation marks. It can also be a variable, a function, or a column name. *table_or_view* is **char**, **nchar**, **varchar**, or **nvarchar**.

## Return types

**numeric**([@@MAXPRECISION](max-precision-transact-sql.md),0))  

## Exceptions

Returns `NULL` on error or if a caller doesn't have object view permission.  

In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a user can only view the metadata of securables they own or have permissions for. Without user object permission, a metadata-emitting, built-in function, such as `IDENT_INCR`, might return `NULL`. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  

## Remarks

The `IDENT_INCR` function is not available for [identity columns in Fabric Data Warehouse](/fabric/data-warehouse/identity).

## Examples

<a id="a-returning-the-increment-value-for-a-specified-table"></a>

### A. Return the increment value for a specified table

 The following example returns the increment value for the `Person.Address` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  

```sql  
USE AdventureWorks2022;  
GO  
SELECT IDENT_INCR('Person.Address') AS Identity_Increment;  
GO  
```  

<a id="b-returning-the-increment-value-from-multiple-tables"></a>

### B. Return the increment value from multiple tables

 The following example returns the tables in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database that includes an identity column with an increment value.  

```sql  
USE AdventureWorks2022;  
GO  
SELECT TABLE_SCHEMA, TABLE_NAME,   
   IDENT_INCR(TABLE_SCHEMA + '.' + TABLE_NAME) AS IDENT_INCR  
FROM INFORMATION_SCHEMA.TABLES  
WHERE IDENT_INCR(TABLE_SCHEMA + '.' + TABLE_NAME) IS NOT NULL;  
```  

 Here is a partial result set.  

 ```
 TABLE_SCHEMA        TABLE_NAME                IDENT_INCR  
------------        ------------------------  ----------  
Person              Address                            1  
Production          ProductReview                      1  
Production          TransactionHistory                 1  
Person              AddressType                        1  
Production          ProductSubcategory                 1  
Person              vAdditionalContactInfo             1  
dbo                 AWBuildVersion                     1  
Production          BillOfMaterials                    1
```  

## Related content

- [Expressions (Transact-SQL)](../language-elements/expressions-transact-sql.md)
- [System Functions by category for Transact-SQL](../../relational-databases/system-functions/system-functions-category-transact-sql.md)
- [IDENT_CURRENT (Transact-SQL)](ident-current-transact-sql.md)
- [IDENT_SEED (Transact-SQL)](ident-seed-transact-sql.md)
- [DBCC CHECKIDENT (Transact-SQL)](../database-console-commands/dbcc-checkident-transact-sql.md)
- [sys.identity_columns (Transact-SQL)](../../relational-databases/system-catalog-views/sys-identity-columns-transact-sql.md)
