---
title: "STIntersection (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STIntersection_TSQL"
  - "STIntersection (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STIntersection (geometry Data Type)"
ms.assetid: 354843f5-cc14-478c-974a-04f363f9530f
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STIntersection (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns an object that represents the points where a **geometry** instance intersects another **geometry** instance.
  
## Syntax  
  
```  
  
.STIntersection ( other_geometry )  
```  
  
## Arguments  
 *other_geometry*  
 Is another **geometry** instance to compare with the instance on which `STIntersection()` is being invoked, to determine where they intersect.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 `STIntersection()` always returns null if the spatial reference IDs (SRIDs) of the **geometry** instances do not match. The result may contain circular arc segments only if the input instances contain them.  
  
## Examples  
  
### A. Using STIntersection() on Polygon instances  
 The following example uses `STIntersection()` to compute the intersection of two polygons.  
  
```  
DECLARE @g geometry;  
DECLARE @h geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 0 2, 2 2, 2 0, 0 0))', 0);  
SET @h = geometry::STGeomFromText('POLYGON((1 1, 3 1, 3 3, 1 3, 1 1))', 0);  
SELECT @g.STIntersection(@h).ToString();  
```  
  
### B. Using STIntersection() with CurvePolygon instance  
 The following example returns an instance that contains a circular arc segment.  
  
```
 DECLARE @g geometry = 'CURVEPOLYGON (CIRCULARSTRING (0 -4, 4 0, 0 4, -4 0, 0 -4))';  
 DECLARE @h geometry = 'POLYGON ((1 -1, 5 -1, 5 3, 1 3, 1 -1))';  
 SELECT @h.STIntersection(@g).ToString();
 ```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

