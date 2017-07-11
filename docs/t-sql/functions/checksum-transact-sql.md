---
title: "CHECKSUM (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "CHECKSUM_TSQL"
  - "CHECKSUM"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "hash indexes"
  - "CHECKSUM function"
  - "checksum values"
ms.assetid: e26d3339-845c-49c2-9d89-243376874c13
caps.latest.revision: 44
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CHECKSUM (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-asdw-xxx-md.md)]

  Returns the checksum value computed over a row of a table, or over a list of expressions. CHECKSUM is intended for use in building hash indexes.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CHECKSUM ( * | expression [ ,...n ] )  
```  
  
## Arguments  
 \*  
 Specifies that computation is over all the columns of the table. CHECKSUM returns an error if any column is of noncomparable data type. Noncomparable data types are **text**, **ntext**, **image**, XML, and **cursor**, and also **sql_variant** with any one of the preceding types as its base type.  
  
 *expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any type except a noncomparable data type.  
  
## Return Types  
 **int**  
  
## Remarks  
 CHECKSUM computes a hash value, called the checksum, over its list of arguments. The hash value is intended for use in building hash indexes. If the arguments to CHECKSUM are columns, and an index is built over the computed CHECKSUM value, the result is a hash index. This can be used for equality searches over the columns.  
  
 CHECKSUM satisfies the properties of a hash function: CHECKSUM applied over any two lists of expressions returns the same value if the corresponding elements of the two lists have the same type and are equal when compared using the equals (=) operator. For this definition, null values of a specified type are considered to compare as equal. If one of the values in the expression list changes, the checksum of the list also generally changes. However, there is a small chance that the checksum will not change. For this reason, we do not recommend using CHECKSUM to detect whether values have changed, unless your application can tolerate occasionally missing a change. Consider using [HashBytes](../../t-sql/functions/hashbytes-transact-sql.md) instead. When an MD5 hash algorithm is specified, the probability of HashBytes returning the same result for two different inputs is much lower than that of CHECKSUM.  
  
 The order of expressions affects the resultant value of CHECKSUM. The order of columns used with CHECKSUM(*) is the order of columns specified in the table or view definition. This includes computed columns.  
  
 The CHECKSUM value is dependent upon the collation. The same value stored with a different collation will return a different CHECKSUM value.  
  
## Examples  
 The following examples show using `CHECKSUM` to build hash indexes. The hash index is built by adding a computed checksum column to the table being indexed, and then building an index on the checksum column.  
  
```  
-- Create a checksum index.  
SET ARITHABORT ON;  
USE AdventureWorks2012;   
GO  
ALTER TABLE Production.Product  
ADD cs_Pname AS CHECKSUM(Name);  
GO  
CREATE INDEX Pname_index ON Production.Product (cs_Pname);  
GO  
```  
  
 The checksum index can be used as a hash index, particularly to improve indexing speed when the column to be indexed is a long character column. The checksum index can be used for equality searches.  
  
```  
/*Use the index in a SELECT query. Add a second search   
condition to catch stray cases where checksums match,   
but the values are not the same.*/  
SELECT *   
FROM Production.Product  
WHERE CHECKSUM(N'Bearing Ball') = cs_Pname  
AND Name = N'Bearing Ball';  
GO  
```  
  
 Creating the index on the computed column materializes the checksum column, and any changes to the `ProductName` value will be propagated to the checksum column. Alternatively, an index could be built directly on the column indexed. However, if the key values are long, a regular index is not likely to perform as well as a checksum index.  
  
## See Also  
 [CHECKSUM_AGG &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-agg-transact-sql.md)   
 [HASHBYTES &#40;Transact-SQL&#41;](../../t-sql/functions/hashbytes-transact-sql.md)   
 [BINARY_CHECKSUM  &#40;Transact-SQL&#41;](../../t-sql/functions/binary-checksum-transact-sql.md)  
  
  