---
title: "STDistance (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STDistance_TSQL"
  - "STDistance (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STDistance (geometry Data Type)"
ms.assetid: ac815bc7-5342-4cc4-af40-c80a1c4c8b68
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STDistance (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the shortest distance between a point in a **geometry** instance and a point in another **geometry** instance.  
  
## Syntax  
  
```  
  
.STDistance ( other_geometry )  
```  
  
## Arguments  
 *other_geometry*  
 Is another **geometry** instance from which to measure the distance between the instance on which `STDistance()` is invoked. If *other_geometry* is an empty set, `STDistance()` returns null.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **float**  
  
 CLR return type: **SqlDouble**  
  
## Remarks  
 `STDistance()` always returns null if the spatial reference IDs (SRIDs) of the **geometry** instances do not match.  
  
## Examples  
  
```  
DECLARE @g geometry;  
DECLARE @h geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 2 0, 2 2, 0 2, 0 0))', 0);  
SET @h = geometry::STGeomFromText('POINT(10 10)', 0);  
SELECT @g.STDistance(@h);  
```  
  
## See Also  
 [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  
