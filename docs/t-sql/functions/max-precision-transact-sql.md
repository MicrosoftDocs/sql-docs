---
title: "@@MAX_PRECISION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@MAX_PRECISION_TSQL"
  - "@@MAX_PRECISION"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "precision [SQL Server], @@MAX_PRECISION"
  - "numeric data type, precision level"
  - "decimal data type, precision level"
  - "@@MAX_PRECISION function"
  - "data types [SQL Server], precision"
ms.assetid: 9e7158a1-e503-422a-b326-3c9b06e181b2
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;MAX_PRECISION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the precision level used by **decimal** and **numeric** data types as currently set in the server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
@@MAX_PRECISION  
```  
  
## Return Types  
 **tinyint**  
  
## Remarks  
 By default, the maximum precision returns 38.  
  
## Examples  
  
```  
SELECT @@MAX_PRECISION AS 'Max Precision'  
```  
  
## See Also  
 [Configuration Functions &#40;Transact-SQL&#41;](../../t-sql/functions/configuration-functions-transact-sql.md)   
 [decimal and numeric &#40;Transact-SQL&#41;](../../t-sql/data-types/decimal-and-numeric-transact-sql.md)   
 [Precision, Scale, and Length &#40;Transact-SQL&#41;](../../t-sql/data-types/precision-scale-and-length-transact-sql.md)  
  
  
