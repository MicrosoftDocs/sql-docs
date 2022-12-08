---
title: "CURRENT_TIMEZONE (Transact-SQL)"
description: "CURRENT_TIMEZONE (Transact-SQL)"
author: MladjoA
ms.author: mlandzic
ms.date: "05/28/2020"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CURRENT_TIMEZONE"
  - "CURRENT_TIMEZONE_TSQL"
helpviewer_keywords:
  - "current time zone [SQL Server]"
  - "current timezone [SQL Server]"
  - "system time zone [SQL Server]"
  - "system timezone [SQL Server]"
  - "functions [SQL Server], time zone"
  - "functions [SQL Server], timezone"
  - "timezone [SQL Server], functions"
  - "time zone [SQL Server], functions"
  - "CURRENT_TIMEZONE function [SQL Server]"
dev_langs:
  - "TSQL"
---
# CURRENT_TIMEZONE (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

This function returns the name of the time zone observed by a server or an instance. For SQL Managed Instance, return value is based on the time zone of the instance itself assigned during instance creation, not the time zone of the underlying operating system.
  
> [!NOTE]  
> For SQL Database, the time zone is always set to UTC and `CURRENT_TIMEZONE` returns the name of the UTC time zone.
  
## Syntax  
  
```syntaxsql
CURRENT_TIMEZONE ( )  
```
  
## Arguments

This function takes no arguments.
  
## Return Type  

**varchar**
  
## Remarks  

`CURRENT_TIMEZONE` is a non-deterministic function. Views and expressions that reference this column cannot be indexed.
  
## Example

Note that the value returned will reflect the actual time zone and language settings of the server or the instance.

```sql
SELECT CURRENT_TIMEZONE();  
/* Returned:  
(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna 
*/
```  
  
## See also

[SQL Managed Instance Time Zone](/azure/sql-database/sql-database-managed-instance-timezone)

[CURRENT_TIMEZONE_ID()](./current-timezone-id-transact-sql.md)
