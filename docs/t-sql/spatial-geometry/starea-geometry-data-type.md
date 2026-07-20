---
title: "STArea (geometry Data Type)"
description: "STArea (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.reviewer: maghan
ms.date: 12/27/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "STArea (geometry Data Type)"
  - "STArea_TSQL"
helpviewer_keywords:
  - "STArea (geometry Data Type)"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# STArea (geometry data type)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The `STArea` function returns the area of a **geometry** instance in square units, based on the spatial reference identifier (SRID) of the geometry.

## Syntax

```syntaxsql
.STArea ( )
```

## Return Types

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] return type: **float**

CLR return type: **SqlDouble**

## Remarks

`STArea()` returns `0` if a **geometry** instance contains only 0-dimensional and 1-dimensional figures, or if it's empty. `STArea()` returns `NULL` if the **geometry** instance hasn't been initialized.

## Examples

### A. Computing the area of a Polygon instance

The following example creates a `Polygon``geometry` instance and computes the area of the polygon.

```sql
DECLARE @g geometry;
SET @g = geometry::STGeomFromText('POLYGON((0 0, 3 0, 3 3, 0 3, 0 0),(2 2, 2 1, 1 1, 1 2, 2 2))', 0);
SELECT @g.STArea();
```

### B. Computing the area of a CurvePolygon instance

The following example computes the area of a `CurvePolygon` instance.

```sql
 DECLARE @g geometry;
 SET @g = geometry::Parse('CURVEPOLYGON(CIRCULARSTRING(0 2, 2 0, 4 2, 4 2, 0 2))');
 SELECT @g.STArea() AS Area;
```

## Related content

- [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)
