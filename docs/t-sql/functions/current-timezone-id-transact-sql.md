---
title: "CURRENT_TIMEZONE_ID (Transact-SQL)"
description: "CURRENT_TIMEZONE_ID (Transact-SQL)"
author: MladjoA
ms.author: mlandzic
ms.date: "05/18/2020"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CURRENT_TIMEZONE)ID"
  - "CURRENT_TIMEZONE_ID_TSQL"
helpviewer_keywords:
  - "current time zone id [SQL Server]"
  - "current timezoneid [SQL Server]"
  - "system time zone id[SQL Server]"
  - "system timezone id[SQL Server]"
  - "functions [SQL Server], time zone id"
  - "functions [SQL Server], timezoneid"
  - "timezoneid [SQL Server], functions"
  - "time zone id [SQL Server], functions"
  - "CURRENT_TIMEZONE_ID function [SQL Server]"
dev_langs:
  - "TSQL"
---
# CURRENT_TIMEZONE_ID (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

This function returns the ID of the time zone observed by a server or an instance. For Azure SQL Managed Instance, return value is based on the time zone of the instance itself assigned during instance creation, not the time zone of the underlying operating system.
  
> [!NOTE]  
> For SQL Database time zone is always set to UTC and `CURRENT_TIMEZONE_ID` returns the id of the UTC time zone.
  
## Syntax  
  
```syntaxsql
CURRENT_TIMEZONE_ID ( )  
```
  
## Arguments

This function takes no arguments.
  
## Return Type  

**varchar**
  
## Remarks  

`CURRENT_TIMEZONE_ID` is a non-deterministic function. Views and expressions that reference this column cannot be indexed.
  
## Example

The value returned will reflect the actual time zone and language settings of the server or the instance.

```sql
SELECT CURRENT_TIMEZONE_ID();  
/* Returned:  
W. Europe Standard Time
*/
```  
  
## See also

[SQL Managed Instance Time Zone](/azure/sql-database/sql-database-managed-instance-timezone)

[CURRENT_TIMEZONE()](./current-timezone-transact-sql.md)
