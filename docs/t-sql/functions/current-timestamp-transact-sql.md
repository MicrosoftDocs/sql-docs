---
title: "CURRENT_TIMESTAMP (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
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
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CURRENT_TIMESTAMP (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

> [!div class="nextstepaction"]
> [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

This function returns the current database system timestamp as a **datetime** value, without the database time zone offset. `CURRENT_TIMESTAMP` derives this value from the operating system of the computer on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] runs.
  
> [!NOTE]  
>  `SYSDATETIME` and `SYSUTCDATE` have more precision, as measured by fractional seconds precision, than `GETDATE` and `GETUTCDATE`. The `SYSDATETIMEOFFSET` function includes the system time zone offset. You can assign `SYSDATETIME`, `SYSUTCDATE`, and `SYSDATETIMEOFFSET` to a variable of any of the date and time types.  
  
This function is the ANSI SQL equivalent to [GETDATE](../../t-sql/functions/getdate-transact-sql.md).
  
See [Date and Time Data Types and Functions](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md) for an overview of all the [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
CURRENT_TIMESTAMP  
```  
  
## Arguments  
This function takes no arguments.
  
## Return Type  
**datetime**
  
## Remarks  
[!INCLUDE[tsql](../../includes/tsql-md.md)] statements can refer to `CURRENT_TIMESTAMP`, anywhere they can refer to a **datetime** expression.
  
`CURRENT_TIMESTAMP` is a nondeterministic function. Views and expressions that reference this column cannot be indexed.
  
## Examples  
These examples use the six [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system functions that return current date and time values, to return the date, the time, or both. The examples return the values in series, so their fractional seconds might differ. Note that the actual values returned will reflect the actual day / time of execution.
  
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
*/
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
  
  

