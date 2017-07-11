---
title: "COMPRESS (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "12/01/2015"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "reference"
f1_keywords: 
  - "COMPRESS"
  - "COMPRESS_TSQL"
helpviewer_keywords: 
  - "COMPRESS function"
ms.assetid: c2bfe9b8-57a4-48b4-b028-e1a3ed5ece88
caps.latest.revision: 9
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# COMPRESS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Compresses the input expression using the GZIP algorithm. The result of the compression is byte array of type **varbinary(max)**.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
COMPRESS ( expression )  
```  
  
## Arguments  
 *expression*  
 Is a **nvarchar(***n***)**, **nvarchar(max)**, **varchar(***n***)**, **varchar(max)**, **varbinary(***n***)**, **varbinary(max)**, **char(***n***)**, **nchar(***n***)**, or **binary(***n***)** expression. For more information, see [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md).  
  
## Return Types  
 Returns the data type of **varbinary(max)** that represents the compressed content of input.  
  
## Remarks  
 Compressed data cannot be indexed.  
  
 The COMPRESS function compresses the data provided as the input expression and must be invoked for each section of data to be compressed. For automatic compression at the row or page level during storage, see [Data Compression](../../relational-databases/data-compression/data-compression.md).  
  
## Examples  
  
### A. Compress Data During the Table Insert  
 The following example shows how to compress data inserted into table:  
  
```  
INSERT INTO player (name, surname, info )  
VALUES (N'Ovidiu', N'Cracium',   
        COMPRESS(N'{"sport":"Tennis","age": 28,"rank":1,"points":15258, turn":17}'));  
  
INSERT INTO player (name, surname, info )  
VALUES (N'Michael', N'Raheem', compress(@info));  
```  
  
### B. Archive compressed version of deleted rows  
 The following statement deletes old player records from the `player` table and stores the records in the `inactivePlayer` table in a compressed format to save space.  
  
```  
DELETE player  
WHERE datemodified < @startOfYear  
OUTPUT id, name, surname datemodifier, COMPRESS(info)   
INTO dbo.inactivePlayers ;  
```  
  
## See Also  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)   
 [DECOMPRESS &#40;Transact-SQL&#41;](../../t-sql/functions/decompress-transact-sql.md)  
  
  
