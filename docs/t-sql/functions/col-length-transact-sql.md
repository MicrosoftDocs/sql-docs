---
title: "COL_LENGTH (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "COL_LENGTH"
  - "COL_LENGTH_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "lengths [SQL Server], columns"
  - "COL_LENGTH function"
  - "column properties [SQL Server]"
  - "column length [SQL Server]"
ms.assetid: cf891206-c49f-40eb-858e-eefd2b638a33
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# COL_LENGTH (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This function returns the defined length of a column, in bytes.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
COL_LENGTH ( 'table' , 'column' )   
```  
  
## Arguments  
**'** *table* **'**  
The name of the table whose column length information we want to determine. *table* is an expression of type **nvarchar**.
  
**'** *column* **'**  
The column name whose length we want to determine. *column* is an expression of type **nvarchar**.
  
## Return type
**smallint**
  
## Exceptions  
Returns NULL on error, or if a caller does not have the correct permission to view the object.
  
In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a user can only view the metadata of securables that the user owns, or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as COL_LENGTH might return NULL, if the user does not have correct permission on the object. See [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md) for more information.
  
## Remarks  
For **varchar** columns declared with the **max** specifier (**varchar(max)**), COL_LENGTH returns the value -1.
  
## Examples  
This example shows the return values for a column of type `varchar(40)` and a column of type `nvarchar(40)`:
  
```sql
USE AdventureWorks2012;  
GO  
CREATE TABLE t1(c1 varchar(40), c2 nvarchar(40) );  
GO  
SELECT COL_LENGTH('t1','c1')AS 'VarChar',  
      COL_LENGTH('t1','c2')AS 'NVarChar';  
GO  
DROP TABLE t1;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
VarChar     NVarChar  
40          80  
```  
  
## See also
[Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)  
[Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)  
[COL_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/col-name-transact-sql.md)  
[COLUMNPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/columnproperty-transact-sql.md)
  
  
