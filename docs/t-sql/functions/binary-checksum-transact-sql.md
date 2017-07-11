---
title: "BINARY_CHECKSUM  (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/17/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "BINARY_CHECKSUM"
  - "BINARY_CHECKSUM_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "BINARY_CHECKSUM function"
  - "binary [SQL Server], checksum values"
ms.assetid: 07fece4d-58e3-446e-a3b5-92fe24d2d1fb
caps.latest.revision: 21
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# BINARY_CHECKSUM  (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-asdw-xxx-md.md)]

  Returns the binary checksum value computed over a row of a table or over a list of expressions. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
BINARY_CHECKSUM ( * | expression [ ,...n ] )   
```  
  
## Arguments  
 *\**  
 Specifies that the computation is over all the columns of the table. BINARY_CHECKSUM ignores columns of noncomparable data types in its computation. Noncomparable data types include **text**, **ntext**, **image**, **cursor**, **xml**, and noncomparable common language runtime (CLR) user-defined types.  
  
 *expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any type. BINARY_CHECKSUM ignores expressions of noncomparable data types in its computation.  
  
## Remarks  
 BINARY_CHECKSUM(*), computed on any row of a table, returns the same value as long the row is not subsequently modified. BINARY_CHECKSUM satisfies the properties of a hash function: BINARY_CHECKSUM applied over any two lists of expressions returns the same value if the corresponding elements of the two lists have the same type and are equal when compared using the equals (=) operator. For this definition, null values of a specified type are considered to compare as equal. If one of the values in the expression list changes, the checksum of the list also generally changes. However, there is a small chance that the checksum will not change. For this reason, we do not recommend using BINARY_CHECKSUM to detect whether values have changed, unless your application can tolerate occasionally missing a change. Consider using HashBytes instead. When an MD5 hash algorithm is specified, the probability of HashBytes returning the same result for two different inputs is much lower than that of BINARY_CHECKSUM.
  
 BINARY_CHECKSUM can be applied over a list of expressions, and returns the same value for a specified list. BINARY_CHECKSUM applied over any two lists of expressions returns the same value if the corresponding elements of the two lists have the same type and byte representation. For this definition, null values of a specified type are considered to have the same byte representation.  
  
 BINARY_CHECKSUM and CHECKSUM are similar functions: They can be used to compute a checksum value on a list of expressions, and the order of expressions affects the resultant value. The order of columns used for BINARY_CHECKSUM(*) is the order of columns specified in the table or view definition. These include computed columns.  
  
 CHECKSUM and BINARY_CHECKSUM return different values for the string data types, where locale can cause strings with different representation to compare equal. The string data types are **char**, **varchar**, **nchar**, **nvarchar**, or **sql_variant** (if the base type of **sql_variant** is a string data type). For example, the BINARY_CHECKSUM values for the strings "McCavity" and "Mccavity" are different. In contrast, in a case-insensitive server, CHECKSUM returns the same checksum values for those strings. CHECKSUM values should not be compared with BINARY_CHECKSUM values.
 
 BINARY_CHECKSUM supports up to 8,000 characters of type **varbinary(max)** and up to 255 characters of type **nvarchar(max)**.  
  
## Examples  
 The following example uses `BINARY_CHECKSUM` to detect changes in a row of a table.  
  
```  
USE AdventureWorks2012;  
GO  
CREATE TABLE myTable (column1 int, column2 varchar(256));  
GO  
INSERT INTO myTable VALUES (1, 'test');  
GO  
SELECT BINARY_CHECKSUM(*) from myTable;  
GO  
UPDATE myTable set column2 = 'TEST';  
GO  
SELECT BINARY_CHECKSUM(*) from myTable;  
GO  
```  
  
## See Also  
 [Aggregate Functions &#40;Transact-SQL&#41;](../../t-sql/functions/aggregate-functions-transact-sql.md)   
 [CHECKSUM &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-transact-sql.md)   
 [CHECKSUM_AGG &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-agg-transact-sql.md)  
  
  
