---
title: "CURRENT_TIMEZONE_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/18/2020"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CURRENT_TIMEZONE)ID"
  - "CURRENT_TIMEZONE_ID_TSQL"
dev_langs: 
  - "TSQL"
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
author: MladjoA
ms.author: mlandzic

---
# CURRENT_TIMEZONE_ID (Transact-SQL)

[!INCLUDE[Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

This function returns the ID of the time zone observed by a server or an instance. For Azure SQL Managed Instance, return value is based on the time zone of the instance itself assigned during instance creation, not the time zone of the underlying operating system.
  
> [!NOTE]  
> For single and pooled SQL Databases time zone is always set to UTC and `CURRENT_TIMEZONE_ID` returns the id of the UTC time zone.
  
## Syntax  
  
```sql
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

[SQL Database Managed Instance Time Zone](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-timezone)

[CURRENT_TIMEZONE()](https://docs.microsoft.com/sql/t-sql/functions/current-timezone-transact-sql)
