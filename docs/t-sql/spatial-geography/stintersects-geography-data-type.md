---
title: "STIntersects (geography Data Type)"
description: "STIntersects (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STIntersects (geography Data Type)"
  - "STIntersects_TSQL"
helpviewer_keywords:
  - "STIntersects method"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# STIntersects (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns 1 if a **geography** instance intersects another **geography** instance. Returns 0 if it does not.  
  
## Syntax  
  
```syntaxsql
.STIntersects ( other_geography )  
```  

## Arguments

*other_geography*  
Is another **geography** instance to compare to the instance on which `STIntersects()` is invoked.  
  
## Return Types

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 This method always returns **NULL** if the spatial reference IDs (SRIDs) of the **geography** instances do not match.  
  
## Examples  
 The following example uses `STIntersects()` to determine whether two `geography` instances intersect each other.  
  
```  
 DECLARE @g geography;  
 DECLARE @h geography;  
 SET @g = geography::STGeomFromText('POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))', 4326);  
 SET @h = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);  
```  
  
 ```
 SELECT CASE @g.STIntersects(@h) 
 WHEN 1 THEN '@g intersects @h'  
 ELSE '@g does not intersect @h'  
 END;
 ```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
