---
description: "STCentroid (geometry Data Type)"
title: "STCentroid (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STCentroid_TSQL"
  - "STCentroid (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STCentroid (geometry Data Type)"
ms.assetid: 4dc5a004-7a53-4cce-81dd-9f5e1dd0db78
author: MladjoA
ms.author: mlandzic 
---
# STCentroid (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the geometric center of a **geometry** instance that consists of one or more polygons.
  
## Syntax  
  
```  
  
.STCentroid ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
 Open Geospatial Consortium (OGC) type: **Point**  
  
## Remarks  
 `STCentroid()` returns null if the **geometry** instance is not a **Polygon, CurvePolygon**, or **MultiPolygon** type.  
  
## Examples  
  
### A. Computing the centroid of a Polygon instance  
 The following example uses `STCentroid()` to compute the centroid of a `polygon``geometry` instance:  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 3 0, 3 3, 0 3, 0 0),(2 2, 2 1, 1 1, 1 2, 2 2))', 0);  
SELECT @g.STCentroid().ToString();  
```  
  
### B. Computing the centroid of a CurvePolygon instance  
 The following example computes the centroid for a `CurvePolygon` instance:  
  
```
 DECLARE @g geometry = 'CURVEPOLYGON(CIRCULARSTRING(0 4, 4 0, 8 4, 4 8, 0 4), CIRCULARSTRING(2 4, 4 2, 6 4, 4 6, 2 4))';  
 SELECT @g.STCentroid().ToString() AS Centroid
 ```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

