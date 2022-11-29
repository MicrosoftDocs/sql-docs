---
title: "@@TIMETICKS (Transact-SQL)"
description: "&#x40;&#x40;TIMETICKS (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "@@TIMETICKS_TSQL"
  - "@@TIMETICKS"
helpviewer_keywords:
  - "ticks [SQL Server]"
  - "@@TIMETICKS function"
  - "microseconds per tick [SQL Server]"
  - "time [SQL Server], ticks"
  - "number of microseconds per tick"
dev_langs:
  - "TSQL"
---
# &#x40;&#x40;TIMETICKS (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns the number of microseconds per tick.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
@@TIMETICKS  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **integer**  
  
## Remarks  
 The amount of time per tick is computer-dependent. Each tick on the operating system is 31.25 milliseconds, or one thirty-second of a second.  
  
## Examples  
  
```sql
SELECT @@TIMETICKS AS 'Time Ticks';  
```  
  
## See Also  
 [System Statistical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/system-statistical-functions-transact-sql.md)  
  
  
