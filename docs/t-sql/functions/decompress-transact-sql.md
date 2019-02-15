---
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
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# DECOMPRESS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-asdw-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-asdw-xxx-md.md)]

This function will decompress an input expression value, using the GZIP algorithm. `DECOMPRESS` will return a byte array (VARBINARY(MAX) type).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
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
  
```  
SELECT _id, name, surname, datemodified,  
             CAST(DECOMPRESS(info) AS NVARCHAR(MAX)) AS info  
FROM player;  
```  
  
### B. Display Compressed Data Using Computed Column  
This example shows how to create a table for decompressed data storage:  
  
```  
CREATE TABLE example_table (  
    _id int primary key identity,  
    name nvarchar(max),  
    surname nvarchar(max),  
    info varbinary(max),  
    info_json as CAST(decompress(info) as nvarchar(max))  
);  
```  
  
## See Also  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)   
 [COMPRESS &#40;Transact-SQL&#41;](../../t-sql/functions/compress-transact-sql.md)  
  
  
