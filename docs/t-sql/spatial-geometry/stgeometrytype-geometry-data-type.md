---
description: "STGeometryType (geometry Data Type)"
title: "STGeometryType (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STGeometryType_TSQL"
  - "STGeometryType (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STGeometryType (geometry Data Type)"
ms.assetid: 224cdc83-aa83-4ad4-bb82-b7481031e910
author: MladjoA
ms.author: mlandzic 
---
# STGeometryType (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the Open Geospatial Consortium (OGC) type name represented by a **geometry** instance.
  
## Syntax  
  
```  
  
.STGeometryType ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **nvarchar(4000)**  
  
 CLR return type: **SqlString**  
  
## Remarks  
 The OGC type names that can be returned by `STGeometryType()` are **Point**, **LineString**, **CircularString**, **CompoundCurve**, **Polygon, CurvePolygon**, **GeometryCollection**, **MultiPoint**, **MultiLineString**, and **MultiPolygon**.  
  
## Examples  
 The following example creates a `Polygon` instance and uses `STGeometryType()` to confirm that it is a polygon.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 3 0, 3 3, 0 3, 0 0))', 0);  
SELECT @g.STGeometryType();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

