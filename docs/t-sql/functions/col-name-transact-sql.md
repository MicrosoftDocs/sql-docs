---
title: "COL_NAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "COL_NAME"
  - "COL_NAME_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "column properties [SQL Server]"
  - "COL_NAME function"
  - "column names [SQL Server]"
  - "names [SQL Server], columns"
ms.assetid: 214144ab-f2bc-4052-83cf-caf0a85c4cc6
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# COL_NAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

This function returns the name of a table column, based on the table identification number and column identification number values of that table column.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
COL_NAME ( table_id , column_id )  
```  
  
## Arguments  
*table_id*  
The identification number of the table containing that column. The *table_id* argument has an **int** data type.
  
*column_id*  
The identification number of the column. The *column_id* argument has an **int** data type.
  
## Return types
**sysname**
  
## Exceptions  
Returns NULL on error, or if a caller does not have the correct permission to view the object.
  
In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a user can only view the metadata of securables that the user owns, or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as `COL_NAME` might return NULL, if the user does not have correct permissions on the object. See [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md) for more information.
  
## Remarks  
The *table_id* and *column_id* parameters together produce a column name string.
  
See [OBJECT_ID &#40;Transact-SQL&#41;](../../t-sql/functions/object-id-transact-sql.md) for more information about obtaining table and column identification numbers.
  
## Examples  
This example returns the name of the first column in a sample `Employee` table.
  
```sql
-- Uses AdventureWorks  
  
SELECT COL_NAME(OBJECT_ID('dbo.FactResellerSales'), 1) AS FirstColumnName,  
COL_NAME(OBJECT_ID('dbo.FactResellerSales'), 2) AS SecondColumnName;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
ColumnName          
------------   
BusinessEntityID  
```  
  
## See also
[Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)  
[Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)  
[COLUMNPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/columnproperty-transact-sql.md)  
[COL_LENGTH &#40;Transact-SQL&#41;](../../t-sql/functions/col-length-transact-sql.md)
  
  

