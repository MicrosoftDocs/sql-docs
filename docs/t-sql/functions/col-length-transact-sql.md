---
title: "COL_LENGTH (Transact-SQL)"
description: "COL_LENGTH (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "COL_LENGTH"
  - "COL_LENGTH_TSQL"
helpviewer_keywords:
  - "lengths [SQL Server], columns"
  - "COL_LENGTH function"
  - "column properties [SQL Server]"
  - "column length [SQL Server]"
dev_langs:
  - "TSQL"
---
# COL_LENGTH (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This function returns the defined length of a column, in bytes.
  
:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
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
USE AdventureWorks2022;  
GO  
CREATE TABLE t1(c1 VARCHAR(40), c2 NVARCHAR(40) );  
GO  
SELECT COL_LENGTH('t1','c1')AS 'VarChar',  
      COL_LENGTH('t1','c2')AS 'NVarChar';  
GO  
DROP TABLE t1;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
VarChar     NVarChar  
40          80  
```  
  
## See also
[Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)  
[Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)  
[COL_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/col-name-transact-sql.md)  
[COLUMNPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/columnproperty-transact-sql.md)
  
  
