---
title: "BINARY_CHECKSUM  (Transact-SQL)"
description: "BINARY_CHECKSUM  (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "BINARY_CHECKSUM"
  - "BINARY_CHECKSUM_TSQL"
helpviewer_keywords:
  - "BINARY_CHECKSUM function"
  - "binary [SQL Server], checksum values"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# BINARY_CHECKSUM  (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Returns the binary checksum value computed over a row of a table or over a list of expressions.
  
![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
BINARY_CHECKSUM ( * | expression [ ,...n ] )   
```  
  
> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*\**  
Specifies that the computation covers all the table columns. BINARY_CHECKSUM ignores columns of noncomparable data types in its computation. Noncomparable data types include  
* **cursor**  
* **image**  
* **ntext**  
* **text**  
* **xml**  

and noncomparable common language runtime (CLR) user-defined types.
  
*expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any type. BINARY_CHECKSUM ignores expressions of noncomparable data types in its computation.

## Return Types  
 **int**
  
## Remarks  
`BINARY_CHECKSUM(*)`, computed on any row of a table, returns the same value as long the row isn't modified later. `BINARY_CHECKSUM` satisfies the properties of a hash function: when applied over any two lists of expressions, returns the same value if the corresponding elements of the two lists have the same type and are equal when compared using the equals (=) operator. For this definition, we say that null values, of a specified type, compare as equal values. If at least one of the values in the expression list changes, the expression checksum can also change. However, this change isn't guaranteed, and so to detect whether values have changed, we recommend use of `BINARY_CHECKSUM` only if your application can tolerate an occasional missed change. Otherwise, consider using `HASHBYTES` instead. With a specified MD5 hash algorithm, the probability that `HASHBYTES` will return the same result, for two different inputs, is much lower than `BINARY_CHECKSUM`.
  
`BINARY_CHECKSUM` can operate over a list of expressions, and it returns the same value for a specified list. `BINARY_CHECKSUM` applied over any two lists of expressions returns the same value if the corresponding elements of the two lists have the same type and byte representation. For this definition, null values of a specified type are considered to have the same byte representation.
  
`BINARY_CHECKSUM` and `CHECKSUM` are similar functions. They can be used to compute a checksum value on a list of expressions, and the order of expressions affects the resultant value. The order of columns used for `BINARY_CHECKSUM(*)` is the order of columns specified in the table or view definition. This ordering includes computed columns.
  
`BINARY_CHECKSUM` and `CHECKSUM` return different values for the string data types, where locale can cause strings with different representation to compare as equal. The string data types are  

* **char**  
* **nchar**  
* **nvarchar**  
* **varchar**  

or  

* **sql_variant** (if the base type of **sql_variant** is a string data type).  
  
For example, the strings "McCavity" and "Mccavity" have different `BINARY_CHECKSUM` values. In contrast, for a case-insensitive server, `CHECKSUM` returns the same checksum values for those strings. You should avoid comparison of `CHECKSUM` values with `BINARY_CHECKSUM` values.
 
`BINARY_CHECKSUM` supports any length of type **varbinary(max)** and up to 255 characters of type **nvarchar(max)**.
  
## Examples  
This example uses `BINARY_CHECKSUM` to detect changes in a table row.
  
```sql
USE AdventureWorks2012;  
GO  
CREATE TABLE myTable (column1 INT, column2 VARCHAR(256));  
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
  
## See also
[Aggregate Functions &#40;Transact-SQL&#41;](../../t-sql/functions/aggregate-functions-transact-sql.md)  
[CHECKSUM_AGG &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-agg-transact-sql.md)  
[CHECKSUM &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-transact-sql.md)  
[HASHBYTES &#40;Transact-SQL&#41;](../../t-sql/functions/hashbytes-transact-sql.md)  
  
  
