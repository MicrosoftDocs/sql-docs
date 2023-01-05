---
description: "InstanceOf (geometry Data Type)"
title: "InstanceOf (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
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
  - "InstanceOf (geometry Data Type)"
ms.assetid: fdea1248-29a4-4bab-a60d-a1b359b5e109
author: MladjoA
ms.author: mlandzic 
---
# InstanceOf (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

A method that tests if the **geometry** instance is the same as the specified type. Returns 1 if the type of a **geometry** instance is the same as the specified type. This method also returns 1 if the specified type is an ancestor of the instance type. Otherwise, this method returns 0.
  
## Syntax  
  
```  
  
.InstanceOf (geometry_type )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*geometry_type*  
**nvarchar(4000)** string specifying one of 15 types exposed in the **geometry** type hierarchy.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 The input for the method must be one of the following types: **Geometry**, **Point**, **Curve**, **LineString**, **CircularString**, **CompoundCurve**, **Surface**, **Polygon**, **CurvePolygon**, **GeometryCollection**, **MultiSurface**, **MultiPolygon**, **MultiCurve**, **MultiLineString**, and **MultiPoint**. This method throws an **ArgumentException** if any other strings are used for the input.  
  
## Examples  
 The following example creates a `MultiPoint` instance and uses `InstanceOf()` to see if the instance is a `GeometryCollection`.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('MULTIPOINT(0 0, 13.5 2, 7 19)', 0);  
SELECT @g.InstanceOf('GEOMETRYCOLLECTION');  
```  
  
## See Also  
 [Extended Methods on Geometry Instances](../../t-sql/spatial-geometry/extended-methods-on-geometry-instances.md)  
  
  

