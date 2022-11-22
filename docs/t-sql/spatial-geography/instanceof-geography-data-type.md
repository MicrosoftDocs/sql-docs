---
description: "InstanceOf (geography Data Type)"
title: "InstanceOf (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "InstanceOf"
  - "InstanceOf_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "InstanceOf method"
ms.assetid: 1eaed0e4-1c72-45a9-9926-5b513335cf33
author: MladjoA
ms.author: mlandzic 
---
# InstanceOf (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Tests if the **geography** instance is the same as the specified type.  
  
## Syntax  
  
```sql  
  
.InstanceOf ( 'geography_type')  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*geography_type*  
The **nvarchar(4000)** string specifying one of 16 types exposed in the **geography** type hierarchy.  
  
## Return Types  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
CLR return type: **SqlBoolean**  
  
## Remarks  
Returns 1 if the type of a **geography** instance is the same as the specified type, or if the specified type is an ancestor of the instance type; otherwise, returns 0.  
  
This **geography** data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.  
  
The input for the method must be one of these types: Geometry, Point, Curve, LineString, CircularString, Surface, Polygon, CurvePolygon, **GeometryCollection**, **MultiSurface**, **MultiPolygon, MultiCurve, MultiLineString**, **MultiPoint**, or **FullGlobe**.  
  
This method throws an `ArgumentException` if you use any other strings for the input.  
  
This method isn't precise.  
  
## Examples  
The following example creates a `MultiPoint` instance and uses `InstanceOf()` to see whether the instance is a `GeometryCollection`.  
  
```sql  
DECLARE @g geography;  
SET @g = geography::STGeomFromText('MULTIPOINT(-122.360 47.656, -122.343 47.656)', 4326);  
SELECT @g.InstanceOf('GEOMETRYCOLLECTION');  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)  
  
  
