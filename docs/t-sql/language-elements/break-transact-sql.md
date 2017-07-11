---
title: "BREAK (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "BREAK"
  - "BREAK_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "exiting innermost loop [SQL Server]"
  - "END keyword"
  - "ignored statements"
  - "BREAK keyword"
ms.assetid: 67c30b8d-3f15-41ad-b9a9-a4ced3b2af9f
caps.latest.revision: 34
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# BREAK (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Exits the innermost loop in a WHILE statement or an IF…ELSE statement inside a WHILE loop. Any statements appearing after the END keyword, marking the end of the loop, are executed. BREAK is frequently, but not always, started by an IF test.  
  
## Examples  
  
```  
-- Uses AdventureWorks  
  
WHILE ((SELECT AVG(ListPrice) FROM dbo.DimProduct) < $300)  
BEGIN  
    UPDATE DimProduct  
        SET ListPrice = ListPrice * 2;  
     IF ((SELECT MAX(ListPrice) FROM dbo.DimProduct) > $500)  
         BREAK;  
END  
```  
  
## See Also  
 [Control-of-Flow Language &#40;Transact-SQL&#41;](~/t-sql/language-elements/control-of-flow.md)   
 [WHILE &#40;Transact-SQL&#41;](../../t-sql/language-elements/while-transact-sql.md)   
 [IF...ELSE &#40;Transact-SQL&#41;](../../t-sql/language-elements/if-else-transact-sql.md)  
  
  


