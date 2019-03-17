---
title: "InstanceOf (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "InstanceOf"
  - "InstanceOf_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "InstanceOf method"
ms.assetid: 1eaed0e4-1c72-45a9-9926-5b513335cf33
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# InstanceOf (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Tests if the **geography** instance is the same as the specified type.  
  
## Syntax  
  
```sql  
  
.InstanceOf ( 'geography_type')  
```  
  
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
  
  
