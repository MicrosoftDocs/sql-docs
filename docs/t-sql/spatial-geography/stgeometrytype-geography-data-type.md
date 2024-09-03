---
title: "STGeometryType (geography Data Type)"
description: "STGeometryType (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STGeometryType (geography Data Type)"
  - "STGeometryType_TSQL"
helpviewer_keywords:
  - "STGeometryType method"
dev_langs:
  - "TSQL"
---
# STGeometryType (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the Open Geospatial Consortium (OGC) type name represented by a **geography** instance.  
  
## Syntax  
  
```  
  
.STGeometryType ( )  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **nvarchar(4000)**  
  
 CLR return type: **SqlString**  
  
## Remarks  
 The OGC type names that can be returned by `STGeometryType()` are **Point**, **LineString**, **CircularString**, **CompoundCurve**, **Polygon**, **CurvePolygon**, **GeometryCollection**, **MultiPoint**, **MultiLineString**, **MultiPolygon**, and **FullGlobe**.  
  
## Examples  
 The following example creates a `Polygon` instance and uses `STGeometryType()` to confirm that it is a polygon.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))', 4326);  
SELECT @g.STGeometryType();  
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
