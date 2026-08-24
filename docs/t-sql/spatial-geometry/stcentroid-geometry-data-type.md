---
title: "STCentroid (geometry Data Type)"
description: "STCentroid (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "STCentroid_TSQL"
  - "STCentroid (geometry Data Type)"
helpviewer_keywords:
  - "STCentroid (geometry Data Type)"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# STCentroid (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns the geometric center of a **geometry** instance that consists of one or more polygons.
  
## Syntax  
  
```  
  
.STCentroid ( )  
```  
  
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
  
## Related content

- [OGC methods on geometry instances](ogc-methods-on-geometry-instances.md)
