---
title: "SYSDATETIME (Transact-SQL)"
description: "SYSDATETIME (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "SYSDATETIME_TSQL"
  - "SYSDATETIME"
helpviewer_keywords:
  - "dates [SQL Server], functions"
  - "date and time [SQL Server], SYSDATETIME"
  - "current date and time [SQL Server]"
  - "functions [SQL Server], time"
  - "system date and time [SQL Server]"
  - "system time [SQL Server]"
  - "SYSDATETIME function [SQL Server]"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
  - "dates [SQL Server], current date and time"
  - "dates [SQL Server], system date and time"
  - "time [SQL Server], system"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# SYSDATETIME (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a **datetime2(7)** value that contains the date and time of the computer on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running.  
  
> [!NOTE]  
>  SYSDATETIME and SYSUTCDATETIME have more fractional seconds precision than GETDATE and GETUTCDATE. SYSDATETIMEOFFSET includes the system time zone offset. SYSDATETIME, SYSUTCDATETIME, and SYSDATETIMEOFFSET can be assigned to a variable of any of the date and time types.  
  
Azure SQL Database (with the exception of Azure SQL Managed Instance) and Azure Synapse Analytics follow UTC. Use [AT TIME ZONE](../../t-sql/queries/at-time-zone-transact-sql.md) in Azure SQL Database or Azure Synapse Analytics if you need to interpret date and time information in a non-UTC time zone.

 For an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
SYSDATETIME ( )  
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Type  
 **datetime2(7)**  
  
## Remarks  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] statements can refer to SYSDATETIME anywhere they can refer to a **datetime2(7)** expression.  
  
 SYSDATETIME is a nondeterministic function. Views and expressions that reference this function in a column cannot be indexed.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] obtains the date and time values by using the GetSystemTimeAsFileTime() Windows API. The accuracy depends on the computer hardware and version of Windows on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running. The precision of this API is fixed at 100 nanoseconds. The accuracy can be determined by using the GetSystemTimeAdjustment() Windows API.  
  
## Examples  
 The following examples use the six [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system functions that return current date and time to return the date, time or both. The values are returned in series; therefore, their fractional seconds might be different.  
  
### A. Getting the current system date and time  
  
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
*/
```    
  
### B. Getting the current system date  
  
```sql
SELECT CONVERT (date, SYSDATETIME())  
    ,CONVERT (date, SYSDATETIMEOFFSET())  
    ,CONVERT (date, SYSUTCDATETIME())  
    ,CONVERT (date, CURRENT_TIMESTAMP)  
    ,CONVERT (date, GETDATE())  
    ,CONVERT (date, GETUTCDATE());  
  
/* All returned 2007-04-30 */  
```  
  
### C. Getting the current system time  
  
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
  
### D: Getting the current system date and time  
  
```sql
SELECT SYSDATETIME();  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
--------------------------  
7/20/2013 2:49:59 PM
```  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)   
 [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md)  
  
  

