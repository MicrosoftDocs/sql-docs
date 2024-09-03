---
title: "STGeomFromText (geography data type)"
description: Returns a **geography** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation augmented with any Z (elevation) and M (measure) values carried by the instance.
author: MladjoA
ms.author: mlandzic
ms.reviewer: randolphwest
ms.date: 05/10/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STGeomFromText (geography data type)"
  - "STGeomFromText_TSQL"
helpviewer_keywords:
  - "full globe"
  - "STGeomFromText method"
dev_langs:
  - "TSQL"
---
# STGeomFromText (geography data type)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geography** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation augmented with any Z (elevation) and M (measure) values carried by the instance.

This **geography** data type method supports `FullGlobe` instances or spatial instances that are larger than a hemisphere.

## Syntax

```syntaxsql
STGeomFromText ( 'geography_tagged_text' , SRID )
```

## Arguments

#### *geography_tagged_text*

The WKT representation of the **geography** instance to return. *geography_tagged_text* is **nvarchar(max)**.

#### *SRID*

An **int** expression representing the spatial reference ID (SRID) of the **geography** instance to return.

## Return types

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**
- CLR return type: `SqlGeography`

## Remarks

The OGC type of the **geography** instance returned by `STGeomFromText()` is set to the corresponding WKT input.

This method throws an `ArgumentException` if the input contains an antipodal edge.

> [!NOTE]  
> The order in which the points are listed matters for geography polygons. It determines if the polygon area is to the inside or outside of the given ring. For more information, see [Polygon](../../relational-databases/spatial/polygon.md#orientation-of-spatial-data).

## Examples

The following example uses `STGeomFromText()` to create a **geography** instance.

```sql
DECLARE @g geography;
-- Starting point: Lat. 47.656, Lon. -122.360
-- Ending point: Lat. 47.656, Lon. -122.343
SET @g = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);
SELECT @g.ToString();
```

## Related content

- [OGC Static Geography Methods](ogc-static-geography-methods.md)
