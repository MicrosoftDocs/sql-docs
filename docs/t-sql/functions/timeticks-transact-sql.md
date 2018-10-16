---
title: "@@TIMETICKS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@TIMETICKS_TSQL"
  - "@@TIMETICKS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ticks [SQL Server]"
  - "@@TIMETICKS function"
  - "microseconds per tick [SQL Server]"
  - "time [SQL Server], ticks"
  - "number of microseconds per tick"
ms.assetid: 9d036633-837f-4309-9c45-3d9600258018
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;TIMETICKS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the number of microseconds per tick.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
@@TIMETICKS  
```  
  
## Return Types  
 **integer**  
  
## Remarks  
 The amount of time per tick is computer-dependent. Each tick on the operating system is 31.25 milliseconds, or one thirty-second of a second.  
  
## Examples  
  
```  
SELECT @@TIMETICKS AS 'Time Ticks';  
```  
  
## See Also  
 [System Statistical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/system-statistical-functions-transact-sql.md)  
  
  
