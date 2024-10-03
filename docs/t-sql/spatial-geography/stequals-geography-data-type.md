---
title: "STEquals (geography Data Type)"
description: "STEquals (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STEquals_TSQL"
  - "STEquals (geography Data Type)"
helpviewer_keywords:
  - "STEquals method"
dev_langs:
  - "TSQL"
---
# STEquals (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns 1 if a **geography** instance represents the same point set as another **geography** instance. Returns 0 if it does not.  
  
## Syntax  
  
```  
  
.STEquals ( other_geography )  
```  
  
## Arguments
 *other_geography*  
 Is another **geography** instance to compare against the instance on which `STEquals()` is invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 This method always returns null if the spatial reference IDs (SRIDs) of the **geography** instances do not match.  
  
## Examples  
 The following example creates two `geography` instances with `STGeomFromText()` that are equal but not trivially equal, and uses `STEquals()` to test their equality. The instances are equal because `LINESTRING` and `POINT` are contained within the `POLYGON`.  
  
```  
DECLARE @g geography;  
DECLARE @h geography;  
SET @g = geography::STGeomFromText('GEOMETRYCOLLECTION(POLYGON((-122.368 47.658, -122.338 47.649, -122.338 47.658, -122.368 47.658, -122.368 47.658)), LINESTRING(-122.360 47.656, -122.343 47.656), POINT (-122.35 47.656))', 4326);  
SET @h = geography::STGeomFromText('POLYGON((-122.368 47.658, -122.338 47.649, -122.338 47.658, -122.368 47.658, -122.368 47.658))', 4326);  
SELECT @g.STEquals(@h);  
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
