---
title: "STConvexHull (geometry Data Type)"
description: "STConvexHull (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "STConvexHull (geometry Data Type)"
  - "STConvexHull_TSQL"
helpviewer_keywords:
  - "STConvexHull (geometry Data Type)"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# STConvexHull (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

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
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 0 2, 1 1, 2 2, 2 0, 0 0))', 0);  
SELECT @g.STConvexHull().ToString();  
```  
  
## Related content

- [OGC methods on geometry instances](ogc-methods-on-geometry-instances.md)
