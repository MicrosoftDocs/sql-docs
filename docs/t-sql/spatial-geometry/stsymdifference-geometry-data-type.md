---
title: "STSymDifference (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STSymDifference_TSQL"
  - "STSymDifference (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STSymDifference (geometry Data Type)"
ms.assetid: 1d4cf35a-ca89-4aa4-ae30-e61a0ff18b53
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STSymDifference (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns an object that represents all points that are either in one **geometry** instance or another **geometry** instance, but not those points that lie in both instances.  
  
## Syntax  
  
```  
  
.STSymDifference ( other_geometry )  
```  
  
## Arguments  
 *other_geometry*  
 Is another **geometry** instance in addition to the instance on which `STSymDistance()` is being invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 This method always returns null if the spatial reference IDs (SRIDs) of the **geometry** instances do not match. The result may contain circular arc segments only if the input instances contain circular arc segments.  
  
## Examples  
  
### A. Computing the symmetric difference of two Polygon instances  
 The following example uses `STSymDifference()` to compute the symmetric difference of two `Polygon` instances.  
  
```  
DECLARE @g geometry;  
DECLARE @h geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 0 2, 2 2, 2 0, 0 0))', 0);  
SET @h = geometry::STGeomFromText('POLYGON((1 1, 3 1, 3 3, 1 3, 1 1))', 0);  
SELECT @g.STSymDifference(@h).ToString();  
```  
  
### B. Computing the symmetric difference between a CurvePolygon and a Polygon instance  
 The following example returns a `GeometryCollection` that represents the symmetric difference between a `CurvePolygon` and a `Polygon`.  
  
```
 DECLARE @g geometry = 'CURVEPOLYGON (CIRCULARSTRING (0 -4, 4 0, 0 4, -4 0, 0 -4))';  
 DECLARE @h geometry = 'POLYGON ((1 -1, 5 -1, 5 3, 1 3, 1 -1))';  
 SELECT @h.STSymDifference(@g).ToString();
 ```  
  
## C. Using STSymDifference() on CurvePolygon instance with an inscribed Polygon instance  
 The following example returns a `CurvePolygon` instance with an interior `Polygon` ring that represents the symmetric difference between the two instances compared.  
  
```
 DECLARE @g geometry = 'CURVEPOLYGON (CIRCULARSTRING (0 -4, 4 0, 0 4, -4 0, 0 -4))';  
 DECLARE @h geometry = 'POLYGON ((1 -1, 2 -1, 2 1, 1 1, 1 -1))';  
 SELECT @h.STSymDifference(@g).ToString();
 ```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  
