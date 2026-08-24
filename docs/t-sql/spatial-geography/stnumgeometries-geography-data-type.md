---
title: "STNumGeometries (geography Data Type)"
description: "STNumGeometries (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
f1_keywords:
  - "STNumGeometries (geography Data Type)"
  - "STNumGeometries_TSQL"
helpviewer_keywords:
  - "STNumGeometries method"
dev_langs:
  - "TSQL"
---
# STNumGeometries (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Returns the number of **geometries** that make up a **geography** instance.  
  
## Syntax  
  
```  
  
.STNumGeometries ( )  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **int**  
  
 CLR return type: **SqlInt32**  
  
## Remarks  
 This method returns 1 if the **geography** instance is not a **MultiPoint**, **MultiLineString**, **MultiPolygon**, or **GeometryCollection** instance, or 0 if the **geography** instance is empty.  
  
## Examples  
 The following example creates a `MultiPoint` instance and uses `STNumGeometries()` to find out how many **geometries** the instance contains.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('MULTIPOINT((-122.360 47.656), (-122.343 47.656))', 4326);  
SELECT @g.STNumGeometries();  
```  
  
## Related content

- [OGC methods on geography instances](ogc-methods-on-geography-instances.md)
