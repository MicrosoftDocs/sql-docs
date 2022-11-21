---
description: "STGeometryType (geography Data Type)"
title: "STGeometryType (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STGeometryType (geography Data Type)"
  - "STGeometryType_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STGeometryType method"
ms.assetid: 3e169ead-a98e-44af-8d33-fd59a955cae4
author: MladjoA
ms.author: mlandzic 
---
# STGeometryType (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the Open Geospatial Consortium (OGC) type name represented by a **geography** instance.  
  
## Syntax  
  
```  
  
.STGeometryType ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
  
