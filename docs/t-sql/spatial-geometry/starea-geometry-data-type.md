---
title: "STArea (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STArea (geometry Data Type)"
  - "STArea_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STArea (geometry Data Type)"
ms.assetid: a7dd6083-c649-4ac3-885d-1234e0db62f1
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STArea (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns the total surface area of a **geometry** instance.  
  
## Syntax  
  
```  
  
.STArea ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **float**  
  
 CLR return type: **SqlDouble**  
  
## Remarks  
 `STArea()` returns 0 if a **geometry** instance contains only 0- and 1-dimensional figures, or if it is empty. `STArea()` returns **NULL** if the **geometry** instance has not been initialized.  
  
## Examples  
  
### A. Computing the area of a Polygon instance  
 The following example creates a `Polygon``geometry` instance and computes the area of the polygon.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 3 0, 3 3, 0 3, 0 0),(2 2, 2 1, 1 1, 1 2, 2 2))', 0);  
SELECT @g.STArea();  
```  
  
### B. Computing the area of a CurvePolygon instance  
 The following example computes the area of a `CurvePolygon` instance.  
  
```
 DECLARE @g geometry;  
 SET @g = geometry::Parse('CURVEPOLYGON(CIRCULARSTRING(0 2, 2 0, 4 2, 4 2, 0 2))');  
 SELECT @g.STArea() AS Area;
 ```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  
