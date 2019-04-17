---
title: "STDifference (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STDifference_TSQL"
  - "STDifference (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STDifference (geometry Data Type)"
ms.assetid: 737f39bb-8750-4ffb-8594-23febc2f1075
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STDifference (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns an object that represents the point set from one **geometry** instance that does not lie within another **geometry** instance.
  
## Syntax  
  
```  
  
.STDifference ( other_geometry )  
```  
  
## Arguments  
 *other_geometry*  
 Is another **geometry** instance indicating which points to remove from the instance on which `STDifference()` is being invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 This method always returns null if the spatial reference IDs (SRIDs) of the **geometry** instances do not match.   The result may contain circular arc segments only if the input instances contain circular arc segments.  
  
## Examples  
  
### A. Computing the difference between two Polygon instances  
 The following example uses `STDifference()` to compute the difference between two polygons.  
  
```  
DECLARE @g geometry;  
DECLARE @h geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 0 2, 2 2, 2 0, 0 0))', 0);  
SET @h = geometry::STGeomFromText('POLYGON((1 1, 3 1, 3 3, 1 3, 1 1))', 0);  
SELECT @g.STDifference(@h).ToString();  
```  
  
### B. Invoking STDifference() on a CurvePolygon instance  
 The following example uses STDifference() on a CurvePolygon instance.  
  
```
 DECLARE @g geometry = 'CURVEPOLYGON (CIRCULARSTRING (0 -4, 4 0, 0 4, -4 0, 0 -4))';  
 DECLARE @h geometry = 'POLYGON ((1 -1, 5 -1, 5 3, 1 3, 1 -1))';  
 -- Note the different results returned by the two SELECT statements  
 SELECT @h.STDifference(@g).ToString(), @g.STDifference(@h).ToString();
 ```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

