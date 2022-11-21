---
title: "COMPRESS (Transact-SQL)"
description: "COMPRESS (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "10/11/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "COMPRESS"
  - "COMPRESS_TSQL"
helpviewer_keywords:
  - "COMPRESS function"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqledge-current || = azure-sqldw-latest"
---
# COMPRESS (Transact-SQL)
[!INCLUDE [sqlserver2016-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

This function compresses the input expression, using the GZIP algorithm. The function returns a byte array of type **varbinary(max)**.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
COMPRESS ( expression )  
```  
  
## Arguments
*expression*  
A

* **binary(***n***)**
* **char(***n***)**
* **nchar(***n***)**
* **nvarchar(max)**
* **nvarchar(***n***)**
* **varbinary(max)**
* **varbinary(***n***)**
* **varchar(max)**

or

* **varchar(***n***)**

expression. See [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md) for more information.
  
## Return types
**varbinary(max)** representing the compressed content of the input.
  
## Remarks  
Compressed data cannot be indexed.
  
The `COMPRESS` function compresses the input expression data. You must invoke this function for each data section to compress. See [Data Compression](../../relational-databases/data-compression/data-compression.md) for more information about automatic data compression during storage at the row or page level.
  
## Examples  
  
### A. Compress Data During the Table Insert  
This example shows how to compress data inserted into a table:
  
```sql
INSERT INTO player (name, surname, info )  
VALUES (N'Ovidiu', N'Cracium',   
        COMPRESS(N'{"sport":"Tennis","age": 28,"rank":1,"points":15258, turn":17}'));  
  
INSERT INTO player (name, surname, info )  
VALUES (N'Michael', N'Raheem', compress(@info));  
```  
  
### B. Archive compressed version of deleted rows  
This statement first deletes old player records from the `player` table. To save space, it then stores the records in the `inactivePlayer` table, in a compressed format.
  
```sql
DELETE FROM player  
OUTPUT deleted.id, deleted.name, deleted.surname, deleted.datemodifier, COMPRESS(deleted.info)   
INTO dbo.inactivePlayers
WHERE datemodified < @startOfYear; 
```  
  
## See also
[String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
[DECOMPRESS &#40;Transact-SQL&#41;](../../t-sql/functions/decompress-transact-sql.md)
  
  
