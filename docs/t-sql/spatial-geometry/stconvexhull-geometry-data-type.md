---
title: "STConvexHull (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STConvexHull (geometry Data Type)"
  - "STConvexHull_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STConvexHull (geometry Data Type)"
ms.assetid: 60a520a6-1a7c-486b-8d91-34401edf6233
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STConvexHull (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns an object representing the convex hull of a **geometry** instance.
  
## Syntax  
  
```  
  
.STConvexHull ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 `STConvexHull()` returns the smallest convex polygon that contains the given **geometry** instance. **Points** or co-linear **LineString** instances will produce an instance of the same type as that of the input.  
  
## Examples  
 The following example uses `STConvexHull()` to find the convex hull of a non-convex `Polygon``geometry` instance.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 0 2, 1 1, 2 2, 2 0, 0 0))', 0);  
SELECT @g.STConvexHull().ToString();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

