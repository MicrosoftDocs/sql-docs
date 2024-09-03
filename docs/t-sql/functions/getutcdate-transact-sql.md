---
title: "GETUTCDATE (Transact-SQL)"
description: "GETUTCDATE (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: 10/30/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "GETUTCDATE"
  - "GETUTCDATE_TSQL"
helpviewer_keywords:
  - "time [SQL Server], UTC time"
  - "dates [SQL Server], functions"
  - "UTC time [SQL Server]"
  - "date and time [SQL Server], GETUTCDATE"
  - "Universal Time Coordinate [SQL Server]"
  - "GETUTCDATE function [SQL Server]"
  - "current date and time [SQL Server]"
  - "time [SQL Server], current"
  - "Greenwich Mean Time [SQL Server]"
  - "functions [SQL Server], time"
  - "system date and time [SQL Server]"
  - "current UTC date and time [SQL Server]"
  - "system time [SQL Server]"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
  - "dates [SQL Server], current date and time"
  - "dates [SQL Server], system date and time"
  - "time [SQL Server], system"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=fabric"
---
# GETUTCDATE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

  Returns the current database system timestamp as a **datetime** value. The database time zone offset is not included. This value represents the current UTC time (Coordinated Universal Time). This value is derived from the operating system of the computer on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running.  
  
> [!NOTE]  
>  SYSDATETIME and SYSUTCDATETIME have more fractional seconds precision than GETDATE and GETUTCDATE. SYSDATETIMEOFFSET includes the system time zone offset. SYSDATETIME, SYSUTCDATETIME, and SYSDATETIMEOFFSET can be assigned to a variable of any of the date and time types.  
  
 For an overview of all [!INCLUDE [tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and Time Data Types and Functions (Transact-SQL)](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax

```syntaxsql
GETUTCDATE()
```  


## Return Types
 **datetime**  
  
## Remarks
 [!INCLUDE [tsql](../../includes/tsql-md.md)] statements can refer to GETUTCDATE anywhere they can refer to a **datetime** expression.  
  
 GETUTCDATE is a nondeterministic function. Views and expressions that reference this function in a column cannot be indexed.  
  
## Examples
 The following examples use the six [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system functions that return current date and time to return the date, time or both. The values are returned in series; therefore, their fractional seconds might be different.  
  
### A. Get the current system date and time
  
```sql  
SELECT 'SYSDATETIME()      ', SYSDATETIME();  
SELECT 'SYSDATETIMEOFFSET()', SYSDATETIMEOFFSET();  
SELECT 'SYSUTCDATETIME()   ', SYSUTCDATETIME();  
SELECT 'CURRENT_TIMESTAMP  ', CURRENT_TIMESTAMP;  
SELECT 'GETDATE()          ', GETDATE();  
SELECT 'GETUTCDATE()       ', GETUTCDATE();  
```

Result set:  

```output
 SYSDATETIME()         2007-05-03 18:34:11.9351421   
 SYSDATETIMEOFFSET()   2007-05-03 18:34:11.9351421 -07:00   
 SYSUTCDATETIME()      2007-05-04 01:34:11.9351421   
 CURRENT_TIMESTAMP     2007-05-03 18:34:11.933   
 GETDATE()             2007-05-03 18:34:11.933   
 GETUTCDATE()          2007-05-04 01:34:11.933  
```

### B. Get the current system date
  
```sql
SELECT 'SYSDATETIME()      ', CONVERT (date, SYSDATETIME());  
SELECT 'SYSDATETIMEOFFSET()', CONVERT (date, SYSDATETIMEOFFSET());  
SELECT 'SYSUTCDATETIME()   ', CONVERT (date, SYSUTCDATETIME());  
SELECT 'CURRENT_TIMESTAMP  ', CONVERT (date, CURRENT_TIMESTAMP);  
SELECT 'GETDATE()          ', CONVERT (date, GETDATE());  
SELECT 'GETUTCDATE()       ', CONVERT (date, GETUTCDATE());  
```

Result set:

```output
 SYSDATETIME()        2007-05-03  
 SYSDATETIMEOFFSET()  2007-05-03  
 SYSUTCDATETIME()     2007-05-04  
 CURRENT_TIMESTAMP    2007-05-03  
 GETDATE()            2007-05-03  
 GETUTCDATE()         2007-05-04  
```
  
### C. Get the current system time
  
```sql  
SELECT 'SYSDATETIME()      ', CONVERT (time, SYSDATETIME());  
SELECT 'SYSDATETIMEOFFSET()', CONVERT (time, SYSDATETIMEOFFSET());  
SELECT 'SYSUTCDATETIME()   ', CONVERT (time, SYSUTCDATETIME());  
SELECT 'CURRENT_TIMESTAMP  ', CONVERT (time, CURRENT_TIMESTAMP);  
SELECT 'GETDATE()          ', CONVERT (time, GETDATE());  
SELECT 'GETUTCDATE()       ', CONVERT (time, GETUTCDATE());  
```

Result set:  

```output
 SYSDATETIME()        18:25:01.6958841
 SYSDATETIMEOFFSET()  18:25:01.6958841
 SYSUTCDATETIME()     01:25:01.6958841
 CURRENT_TIMESTAMP    18:25:01.6930000
 GETDATE()            18:25:01.6930000
 GETUTCDATE()         01:25:01.6930000
```
  
## Related content

- [CAST and CONVERT (Transact-SQL)](../../t-sql/functions/cast-and-convert-transact-sql.md)
- [AT TIME ZONE (Transact-SQL)](../../t-sql/queries/at-time-zone-transact-sql.md)
