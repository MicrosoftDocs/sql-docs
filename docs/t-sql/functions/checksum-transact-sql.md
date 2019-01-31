---
title: "CHECKSUM (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
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
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"

---
# CHECKSUM (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-asdw-xxx-md.md)]

The `CHECKSUM` function returns the checksum value computed over a table row, or over an expression list. Use `CHECKSUM` to build hash indexes.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
CHECKSUM ( * | expression [ ,...n ] )  
```  
  
## Arguments  
\*  
This argument specifies that the checksum computation covers all table columns. `CHECKSUM` returns an error if any column has a noncomparable data type. Noncomparable data types include:

- **cursor**
- **image**
- **ntext**
- **text**
- **XML**

Another noncomparable data type is **sql_variant** with any one of the preceding data types as its base type.
  
*expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any type, except a noncomparable data type.
  
## Return types
 **int**  
  
## Remarks  
`CHECKSUM` computes a hash value, called the checksum, over its argument list. Use this hash value to build hash indexes. A hash index will result if the `CHECKSUM` function has column arguments, and an index is built over the computed `CHECKSUM` value. This can be used for equality searches over the columns.
  
The `CHECKSUM` function satisfies hash function properties: `CHECKSUM` applied over any two lists of expressions will return the same value, if the corresponding elements of the two lists have the same data type, and if those corresponding elements have equality when compared using the equals (=) operator. Null values of a specified type are defined to compare as equal for `CHECKSUM` function purposes. If at least one of the values in the expression list changes, the list checksum will probably change. However, this is not guaranteed. 
Therefore, to detect whether values have changed, we recommend use of `CHECKSUM` only if your application can tolerate an occasional missed change. Otherwise, consider using `HASHBYTES` instead. With a specified MD5 hash algorithm, the probability that `HASHBYTES` will return the same result, for two different inputs, is much lower compared to `CHECKSUM`.
  
The expression order affects the computed `CHECKSUM` value. The order of columns used for `CHECKSUM(*)` is the order of columns specified in the table or view definition. This includes computed columns.
  
The `CHECKSUM` value depends on the collation. The same value stored with a different collation will return a different `CHECKSUM` value.
  
## Examples  
These examples show the use of `CHECKSUM` to build hash indexes.
  
To build the hash index, the first example adds a computed checksum column to the table we want to index. It then builds an index on the checksum column. 
  
```sql
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
  
This example shows the use of a checksum index as a hash index. This can help improve indexing speed when the column to index is a long character column. The checksum index can be used for equality searches.
  
```sql
/*Use the index in a SELECT query. Add a second search   
condition to catch stray cases where checksums match,   
but the values are not the same.*/  

SELECT *   
FROM Production.Product  
WHERE CHECKSUM(N'Bearing Ball') = cs_Pname  
AND Name = N'Bearing Ball';  
GO  
```  
  
Index creation on the computed column materializes the checksum column, and any changes to the `ProductName` value will propagate to the checksum column. Alternatively, we could build an index directly on the column we want to index. However, for long key values, a regular index will probably not perform as well as a checksum index.
  
## See also
[CHECKSUM_AGG &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-agg-transact-sql.md)  
[HASHBYTES &#40;Transact-SQL&#41;](../../t-sql/functions/hashbytes-transact-sql.md)  
[BINARY_CHECKSUM  &#40;Transact-SQL&#41;](../../t-sql/functions/binary-checksum-transact-sql.md)
  
  
