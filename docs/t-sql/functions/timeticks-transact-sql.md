---
title: "@@TIMETICKS (Transact-SQL)"
description: "@@TIMETICKS (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
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
# @@TIMETICKS (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns the number of microseconds per tick.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
@@TIMETICKS  
```  
  
## Return Types
 **integer**  
  
## Remarks  
 The amount of time per tick is computer-dependent. Each tick on the operating system is 31.25 milliseconds, or one thirty-second of a second.  
  
## Examples  
  
```sql
SELECT @@TIMETICKS AS 'Time Ticks';  
```  
  
## Related content

- [System Statistical Functions (Transact-SQL)](system-statistical-functions-transact-sql.md)
