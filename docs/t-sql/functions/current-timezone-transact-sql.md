---
title: "CURRENT_TIMEZONE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CURRENT_TIMEZONE"
  - "CURRENT_TIMEZONE_TSQL"
dev_langs: 
  - "TSQL"
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
author: MladjoA
ms.author: mlandzic
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2019||=sqlallproducts-allversions||>=sql-server-linux-2019||=azuresqldb-mi-current"
---
# CURRENT_TIMEZONE (Transact-SQL)

[!INCLUDE[tsql-appliesto-ssver15-asdb-xxxx-xxx](../../includes/tsql-appliesto-ssver15-asdb-xxxx-xxx.md)]

This function returns the name of the time zone observed by a server or an instance. For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]`CURRENT_TIMEZONE` derives the return value from the operating system of the computer on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] runs. For SQL Database Managed Instance return value is based on the time zone of the instance itself assigned during instance creation, not the time zone of the underlying operating system.
  
> [!NOTE]  
> For single and pooled SQL Databases time zone is always set to UTC and `CURRENT_TIMEZONE` returns the name of the UTC time zone.
  
## Syntax  
  
```sql
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

[SQL Database Managed Instance Time Zone](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-managed-instance-timezone)
