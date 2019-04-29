---
title: "STPointN (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STPointN_TSQL"
  - "STPointN (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STPointN method"
ms.assetid: 47670feb-b9e0-4b4b-af83-b9bba7da66ac
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STPointN (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the specified point in a **geography** instance.  
  
## Syntax  
  
```  
  
.STPointN ( expression )  
```  
  
## Arguments  
 *expression*  
 Is an **int** expression between 1 and the number of points in the **geography** instance.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
 Open Geospatial Consortium (OGC) type: **Point**  
  
## Remarks  
 If a **geography** instance is user-created, STPointN() returns the point specified by *expression* by ordering the points in the order in which they were originally input.  
  
 If a **geography** instance is constructed by the system, STPointN() returns the point specified by *expression* by ordering all the points in the same order they would be output: first by **geography** instance, then by ring within the instance (if appropriate), and then by point within the ring. This order is deterministic.  
  
 If this method is called with a value less than 1, it throws an **ArgumentOutOfRangeException**.  
  
 If this method is called with a value greater than the number of points in the instance, it returns null.  
  
## Examples  
 The following example creates a `LineString` instance and uses `STPointN()` to retrieve the second point in the description of the instance.  
  
```  
DECLARE @g geography;  
SET @g = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);  
SELECT @g.STPointN(2).ToString();  
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
