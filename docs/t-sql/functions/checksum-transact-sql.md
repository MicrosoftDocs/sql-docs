---
title: "CHECKSUM (Transact-SQL)"
description: "CHECKSUM (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CHECKSUM_TSQL"
  - "CHECKSUM"
helpviewer_keywords:
  - "hash indexes"
  - "CHECKSUM function"
  - "checksum values"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# CHECKSUM (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

The `CHECKSUM` function returns the checksum value computed over a table row, or over an expression list. Use `CHECKSUM` to build hash indexes.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
CHECKSUM ( * | expression [ ,...n ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]
> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

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

`CHECKSUM` ignores the nchar and nvarchar dash character (`N'-'` or `nchar(45)`).  Therefore, a hash collision is **guaranteed** for any two strings where the only differences are dashes.  Put another way, `Select checksum(nchar(45));` and `Select checksum(N'-');` both return a value of `0`, so they have no effect on the hash of any additional characters in the string or any additional data in the checksum list.
Practical problems:
1. Checksum ignores negative signature in numeric string
```
SELECT CHECKSUM(N'1'), CHECKSUM(N'-1');
```
2. A checksum comparison cannot detect that code was commented-out in stored proc definition
```
CREATE PROCEDURE Checksum_Test AS
BEGIN
  RAISERROR('Error Raised',18,1);
  RETURN 1;
END
GO

-- get checksum for original proc definition.
SELECT
  checksum(definition),
  definition
FROM sys.sql_modules
WHERE object_id = object_id('Checksum_Test');
GO

-- comment out a line of code in the proc.
ALTER PROCEDURE Checksum_Test AS
BEGIN
  --RAISERROR('Error Raised',18,1);
  RETURN 1;
END
GO

-- get checksum for altered proc definition. Note the definition text now includes the -- comment dashes.
SELECT
  checksum(definition),
  definition
FROM sys.sql_modules
WHERE object_id = object_id('Checksum_Test');

DROP PROCEDURE Checksum_Test
```

`CHECKSUM` trims trailing spaces from nchar and nvarchar strings.  The effect is the same as the problem of ignored dashes.
 
The expression order affects the computed `CHECKSUM` value. The order of columns used for `CHECKSUM(*)` is the order of columns specified in the table or view definition. This includes computed columns.
  
The `CHECKSUM` value depends on the collation. The same value stored with a different collation will return a different `CHECKSUM` value.
  
`CHECKSUM ()` does not guarantee unique results.

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
  
  
