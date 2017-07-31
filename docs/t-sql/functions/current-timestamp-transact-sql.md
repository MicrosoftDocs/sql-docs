---
title: "CURRENT_TIMESTAMP (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "CURRENT_TIMESTAMP"
  - "CURRENT_TIMESTAMP_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dates [SQL Server], functions"
  - "niladic functions"
  - "current date and time [SQL Server]"
  - "time [SQL Server], current"
  - "date and time [SQL Server], CURRENT_TIMESTAMP"
  - "functions [SQL Server], time"
  - "system date and time [SQL Server]"
  - "system time [SQL Server]"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
  - "dates [SQL Server], current date and time"
  - "dates [SQL Server], system date and time"
  - "CURRENT_TIMESTAMP function [SQL Server]"
  - "time [SQL Server], system"
ms.assetid: c724d9cc-7b1f-4c71-bdf5-08bc52b33afc
caps.latest.revision: 48
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CURRENT_TIMESTAMP (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns the current database system timestamp as a **datetime** value without the database time zone offset. This value is derived from the operating system of the computer on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running.
  
> [!NOTE]  
>  SYSDATETIME and SYSUTCDATE have more fractional seconds precision than GETDATE and GETUTCDATE. SYSDATETIMEOFFSET includes the system time zone offset. SYSDATETIME, SYSUTCDATE, and SYSDATETIMEOFFSET can be assigned to a variable of any of the date and time types.  
  
This function is the ANSI SQL equivalent to [GETDATE](../../t-sql/functions/getdate-transact-sql.md).
  
For an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and Time Data Types and Functions](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
CURRENT_TIMESTAMP  
```  
  
## Arguments  
Takes no arguments.
  
## Return Type  
**datetime**
  
## Remarks  
[!INCLUDE[tsql](../../includes/tsql-md.md)] statements can refer to CURRENT_TIMESTAMP anywhere they can refer to a **datetime** expression.
  
CURRENT_TIMESTAMP is a nondeterministic function. Views and expressions that reference this column cannot be indexed.
  
## Examples  
The following examples use the six [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system functions that return current date and time to return the date, the time, or both. The values are returned in series so their fractional seconds might differ.
  
### A. Get the Current System Date and Time  
  
```sql
SELECT SYSDATETIME()  
    ,SYSDATETIMEOFFSET()  
    ,SYSUTCDATETIME()  
    ,CURRENT_TIMESTAMP  
    ,GETDATE()  
    ,GETUTCDATE();  
/* Returned:  
SYSDATETIME()      2007-04-30 13:10:02.0474381  
SYSDATETIMEOFFSET()2007-04-30 13:10:02.0474381 -07:00  
SYSUTCDATETIME()   2007-04-30 20:10:02.0474381  
CURRENT_TIMESTAMP  2007-04-30 13:10:02.047  
GETDATE()          2007-04-30 13:10:02.047  
GETUTCDATE()       2007-04-30 20:10:02.047  
```  
  
### B. Get the Current System Date  
  
```sql
SELECT CONVERT (date, SYSDATETIME())  
    ,CONVERT (date, SYSDATETIMEOFFSET())  
    ,CONVERT (date, SYSUTCDATETIME())  
    ,CONVERT (date, CURRENT_TIMESTAMP)  
    ,CONVERT (date, GETDATE())  
    ,CONVERT (date, GETUTCDATE());  
  
/* Returned   
SYSDATETIME()      2007-05-03  
SYSDATETIMEOFFSET()2007-05-03  
SYSUTCDATETIME()   2007-05-04  
CURRENT_TIMESTAMP  2007-05-03  
GETDATE()          2007-05-03  
GETUTCDATE()       2007-05-04  
*/  
```  
  
### C. Get the Current System Time  
  
```sql
SELECT CONVERT (time, SYSDATETIME())  
    ,CONVERT (time, SYSDATETIMEOFFSET())  
    ,CONVERT (time, SYSUTCDATETIME())  
    ,CONVERT (time, CURRENT_TIMESTAMP)  
    ,CONVERT (time, GETDATE())  
    ,CONVERT (time, GETUTCDATE());  
  
/* Returned  
SYSDATETIME()      13:18:45.3490361  
SYSDATETIMEOFFSET()13:18:45.3490361  
SYSUTCDATETIME()   20:18:45.3490361  
CURRENT_TIMESTAMP  13:18:45.3470000  
GETDATE()          13:18:45.3470000  
GETUTCDATE()       20:18:45.3470000  
*/  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
```sql
SELECT CURRENT_TIMESTAMP;  
```  
  
## See also
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
  
  

