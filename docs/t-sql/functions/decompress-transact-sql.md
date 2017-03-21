---
title: "DECOMPRESS (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "11/30/2015"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DECOMPRESS"
  - "DECOMPRESS_TSQL"
helpviewer_keywords: 
  - "DECOMPRESS function"
ms.assetid: 738d56be-3870-4774-b112-3dce27becc11
caps.latest.revision: 8
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DECOMPRESS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Decompress input expression using GZIP algorithm. Result of the compression is byte array (VARBINARY(MAX) type).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
DECOMPRESS ( expression )  
```  
  
## Arguments  
 *expression*  
 Is a **varbinary(***n***)**, **varbinary(max)**, or **binary(***n***)**. For more information, see [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md).  
  
## Return Types  
 Returns the data type of **varbinary(max)** type. The input argument is decompressed using the ZIP algorithm. The user should explicitly cast result to a target type if needed.  
  
## Remarks  
  
## Examples  
  
### A. Decompress Data at Query Time  
 The following example shows how to show compress data from a table:  
  
```  
SELECT _id, name, surname, datemodified,  
             CAST(DECOMPRESS(info) AS NVARCHAR(MAX)) AS info  
FROM player;  
```  
  
### B. Display Compressed Data Using Computed Column  
 The following example shows how to create a table to store decompressed data:  
  
```  
CREATE TABLE (  
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
  
  
