---
title: "COL_LENGTH (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 20
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# COL_LENGTH (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the defined length, in bytes, of a column.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
COL_LENGTH ( 'table' , 'column' )   
```  
  
## Arguments  
 **'** *table* **'**  
 Is the name of the table for which to determine column length information. *table* is an expression of type **nvarchar**.  
  
 **'** *column* **'**  
 Is the name of the column for which to determine length. *column* is an expression of type **nvarchar**.  
  
## Return Type  
 **smallint**  
  
## Exceptions  
 Returns NULL on error or if a caller does not have permission to view the object.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as COL_LENGTH may return NULL if the user does not have any permission on the object. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Remarks  
 For columns of type **varchar** declared with the **max** specifier (**varchar(max)**), COL_LENGTH returns the value –1.  
  
## Examples  
 The following example shows the return values for a column of type `varchar(40)` and a column of type `nvarchar(40)`.  
  
```  
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
  
```  
VarChar     NVarChar  
40          80  
```  
  
## See Also  
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [COL_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/col-name-transact-sql.md)   
 [COLUMNPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/columnproperty-transact-sql.md)  
  
  