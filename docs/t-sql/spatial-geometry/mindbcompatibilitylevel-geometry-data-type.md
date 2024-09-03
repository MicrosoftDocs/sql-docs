---
title: "MinDbCompatibilityLevel (geometry Data Type)"
description: "MinDbCompatibilityLevel (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "MinDbCompatibilityLevel method (geometry)"
dev_langs:
  - "TSQL"
---
# MinDbCompatibilityLevel (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the minimum database compatibility level that recognizes the **geometry** data type instance.
  
## Syntax  
  
```  
  
.MinDbCompatibilityLevel ( )  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **int**  
  
 CLR return type: **int**  
  
## Remarks  
 Use `MinDbCompatibilityLevel()` to test a spatial object for compatibility before changing the compatibility level on a database.  
  
## Examples  
  
### A. Testing CircularString type for compatibility with compatibility level 110  
 The following example tests a `CircularString` instance for compatibility with an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
```sql
 DECLARE @g geometry = 'CIRCULARSTRING(3 4, 8 9, 5 6)'; 
 IF @g.MinDbCompatibilityLevel() <= 110 
 BEGIN 
 SELECT @g.ToString(); 
 END
 ```  
  
### B. Testing LineString type for compatibility with compatibility level 100  
 The following example tests a `LineString` instance for compatibility with [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)]:  
  
```sql
 DECLARE @g geometry = 'LINESTRING(3 4, 8 9, 5 6)'; 
 IF @g.MinDbCompatibilityLevel() <= 100 
 BEGIN 
 SELECT @g.ToString(); 
 END
``` 
  
## See Also  
 [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)  
  
  

