---
description: "STNumPoints (geography Data Type)"
title: "STNumPoints (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STNumPoints (geography Data Type)"
  - "STNumPoints_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STNumPoints method"
ms.assetid: 25ff7ad1-ba5f-4cfb-816a-59255ac1591d
author: MladjoA
ms.author: mlandzic 
---
# STNumPoints (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the total number of points in each of the figures in a **geography** instance.  
  
## Syntax  
  
```  
  
.STNumPoints ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **int**  
  
 CLR return type: **SqlInt32**  
  
## Remarks  
 This method counts the points in the description of a **geography** instance. Duplicate points are counted; however, connecting points between segments are counted only once. If this instance is a collection, this method returns the total number of points in the collection.  
  
## Examples  
  
### A. Retrieving the total number of points in a LineString  
 The following example creates a `LineString` instance and uses `STNumPoints()` to determine how many points were used in the description of the instance.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);  
SELECT @g.STNumPoints();  
```  
  
### B. Retrieving the total number of points in a GeometryCollection  
 The following example returns a sum of the points for all elements in the `GeometryCollection`.  
  
```sql
DECLARE @g geography = 'GEOMETRYCOLLECTION(CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)  
    ,CURVEPOLYGON(CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)))';  
SELECT @g.STNumPoints();  
```  
  
### C. Returning the number of points in a CompoundCurve  
 The following example returns the number of points in a CompoundCurve instance. The query returns 5 instead of 6 because STNumPoints() only counts the connecting point between the segments once.  
  
```sql
 DECLARE @g geography = 'COMPOUNDCURVE(CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658),( -122.348 47.658, -121.56 48.12, -122.358 47.653))'  
 SELECT @g.STNumPoints();
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
