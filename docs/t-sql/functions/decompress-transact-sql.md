---
description: "DECOMPRESS (Transact-SQL)"
title: "DECOMPRESS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/11/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DECOMPRESS"
  - "DECOMPRESS_TSQL"
helpviewer_keywords: 
  - "DECOMPRESS function"
ms.assetid: 738d56be-3870-4774-b112-3dce27becc11
author: markingmyname
ms.author: maghan
---
# DECOMPRESS (Transact-SQL)
[!INCLUDE [sqlserver2016-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

This function will decompress an input expression value, using the GZIP algorithm. `DECOMPRESS` will return a byte array (VARBINARY(MAX) type).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
DECOMPRESS ( expression )  
```  
  
## Arguments
 *expression*  
A **varbinary(**_n_**)**, **varbinary(max)**, or **binary(**_n_**)** value. See [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md) for more information.  
  
## Return Types  
A value of data type **varbinary(max)**. `DECOMPRESS` will use the ZIP algorithm to decompress the input argument. The user should explicitly cast result to a target type if necessary.  
  
## Remarks  
  
## Examples  
  
### A. Decompress Data at Query Time  
This example shows how to return compressed table data:  
  
```sql  
SELECT _id, name, surname, datemodified,  
             CAST(DECOMPRESS(info) AS NVARCHAR(MAX)) AS info  
FROM player;  
```  
  
### B. Display Compressed Data Using Computed Column  
This example shows how to create a table for decompressed data storage:  
  
```sql  
CREATE TABLE example_table (  
    _id INT PRIMARY KEY IDENTITY,  
    name NVARCHAR(max),  
    surname NVARCHAR(max),  
    info VARBINARY(max),  
    info_json as CAST(DECOMPRESS(info) as NVARCHAR(max))  
);  
```  
  
## See Also  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)   
 [COMPRESS &#40;Transact-SQL&#41;](../../t-sql/functions/compress-transact-sql.md)  
  
  
