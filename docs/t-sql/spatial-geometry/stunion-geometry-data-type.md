---
description: "STUnion (geometry Data Type)"
title: "STUnion (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STUnion (geometry Data Type)"
  - "STUnion_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STUnion (geometry Data Type)"
ms.assetid: 5b168118-137d-402f-9173-fee3f365a89c
author: MladjoA
ms.author: mlandzic 
---
# STUnion (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns an object that represents the union of a **geometry** instance with another **geometry** instance.
  
## Syntax  
  
```  
  
.STUnion ( other_geometry )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *other_geometry*  
 Is another **geometry** instance to form a union with the instance on which `STUnion()` is being invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 This method always returns null if the spatial reference IDs (SRIDs) of the **geometry** instances do not match. The result may contain circular arc segments only if the input instances contain circular arc segments.  
  
## Examples  
  
### A. Computing the union of two Polygon instances  
 The following example uses `STUnion()` to compute the union of two `Polygon` instances.  
  
```sql
DECLARE @g geometry;  
DECLARE @h geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 0 2, 2 2, 2 0, 0 0))', 0);  
SET @h = geometry::STGeomFromText('POLYGON((1 1, 3 1, 3 3, 1 3, 1 1))', 0);  
SELECT @g.STUnion(@h).ToString();  
```  
  
### B. Computing the union of a Polygon instance with a CurvePolygon instance  
 The following example returns a `GeometryCollection` instance that contains a circular arc segment.  
  
```sql
 DECLARE @g geometry = 'CURVEPOLYGON(CIRCULARSTRING(0 -4, 4 0, 0 4, -4 0, 0 -4))';  
 DECLARE @h geometry = 'POLYGON((5 -1, 5 -3, 7 -3, 7 -1, 5 -1))';  
 SELECT @g.STUnion(@h).ToString();
 ```  
  
 `STUnion()` returns a result that contains a circular arc segment because the instance that invoked `STUnion()` contains a circular arc segment.  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

