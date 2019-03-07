---
title: "STOverlaps (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STOverlaps (geometry Data Type)"
  - "STOverlaps_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STOverlaps (geometry Data Type)"
ms.assetid: 1813cba1-5780-456a-9489-6b40a79569b3
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STOverlaps (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns 1 if a **geometry** instance overlaps another **geometry** instance. Returns 0 if it does not.
  
## Syntax  
  
```  
  
.STOverlaps ( other_geometry )  
```  
  
## Arguments  
 *other_geometry*  
 Is another **geometry** instance to compare against the instance on which `STOverlaps()` is invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 Two **geometry** instances overlap if the region representing their intersection has the same dimension as the instances do and the region does not equal either instance.  
  
 `STOverlaps()` always returns 0 if the points where the **geometry** instances intersect are not the same dimension.  
  
 This method always returns null if the spatial reference IDs (SRIDs) of the **geometry** instances do not match.  
  
## Examples  
 The following example uses `STOverlaps()` to test two **geometry** instances for overlap.  
  
```  
DECLARE @g geometry;  
DECLARE @h geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 2 0, 2 2, 0 2, 0 0))', 0);  
SET @h = geometry::STGeomFromText('POLYGON((1 1, 3 1, 3 3, 1 3, 1 1))', 0);  
SELECT @g.STOverlaps(@h);  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

