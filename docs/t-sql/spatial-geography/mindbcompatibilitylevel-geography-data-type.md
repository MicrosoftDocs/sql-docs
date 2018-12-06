---
title: "MinDbCompatibilityLevel (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "MinDbCompatibilityLevel"
  - "MinDbCompatibilityLevel_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MinDbCompatibilityLevel method (geography)"
ms.assetid: a9e44748-4a9e-4179-abc4-7631597be5a7
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# MinDbCompatibilityLevel (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns the minimum database compatibility that recognizes the **geography** data type.  
  
## Syntax  
  
```  
  
. MinDbCompatibilityLevel ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **int**  
  
 CLR return type: **int**  
  
## Remarks  
 Use `MinDbCompatibilityLevel()` to test a spatial object for compatibility before changing the compatibility level on a database. An invalid **geography** type returns 110.  
  
## Examples  
  
### A. Testing CircularString type for compatibility with compatibility level 110  
 The following example tests a `CircularString` instance for compatibility with an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
```  
DECLARE @g geometry = 'CIRCULARSTRING(-120.533 46.566, -118.283 46.1, -122.3 47.45)';  
IF @g.MinDbCompatibilityLevel() <= 110  
BEGIN  
    SELECT @g.ToString();  
END  
  
```  
  
### B. Testing LineString type for compatibility with compatibility level 100  
 The following example tests a `LineString` instance for compatibility with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]:  
  
```  
DECLARE @g geometry = 'LINESTRING(-120.533 46.566, -118.283 46.1, -122.3 47.45)';  
IF @g.MinDbCompatibilityLevel() <= 100  
BEGIN  
    SELECT @g.ToString();  
END  
  
```  
  
### C. Testing the value of a Geography instance for compatibility  
 The following example shows the compatibility levels for two `geography` instances. One is smaller than a hemisphere and the other is larger than a hemisphere:  
  
```  
DECLARE @g geography = geography::Parse('POLYGON((0 -10, 120 -10, 240 -10, 0 -10))');  
DECLARE @h geography = geography::Parse('POLYGON((0 10, 120 10, 240 10, 0 10))');  
IF (@g.EnvelopeAngle() >= 90)  
BEGIN  
SELECT @g.MinDbCompatibilityLevel();  
END     
IF (@h.EnvelopeAngle() < 90)  
BEGIN  
SELECT @h.MinDbCompatibilityLevel();  
END  
  
```  
  
 The first SELECT statement returns 110 and the second SELECT statement returns 100.  
  
## See Also  
 [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)   
 [SQL Server Database Engine Backward Compatibility](../../database-engine/sql-server-database-engine-backward-compatibility.md)  
  
  
