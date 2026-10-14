---
title: geometry (Transact-SQL)
description: Spatial Types - geometry (Transact-SQL)
author: MladjoA
ms.author: mlandzic
ms.reviewer: randolphwest
ms.date: 10/13/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "geometry"
helpviewer_keywords:
  - "spatial data types [SQL Server]"
  - "geometry data type [SQL Server], Transact-SQL"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Spatial types - geometry (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The planar spatial data type, **geometry**, is implemented as a common language runtime (CLR) data type in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This type represents data in a Euclidean (flat) coordinate system.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports a set of methods for the **geometry** spatial data type. These methods include methods on **geometry** that are defined by the Open Geospatial Consortium (OGC) standard and a set of [!INCLUDE [msCoName](../../includes/msconame-md.md)] extensions to that standard.

The error tolerance for the geometry methods can be as large as 1.0e-7 * extents. The extents refer to the approximate maximal distance between points of the **geometry** object.

<a id="registering-the-geometry-type"></a>

## Register the geometry type

The **geometry** type is predefined and available in each database. You can create table columns of type **geometry** and operate on **geometry** data in the same manner as you would use other CLR types. The type can be used in persisted and non-persisted computed columns.

## Conversion

Conversion from **geometry** to **binary**, **varbinary**, **char**, **varchar**, **nchar**, and **nvarchar** must be explicit. Use `CAST` or `CONVERT`.

Conversion from **binary**, **varbinary**, **char**, **varchar**, **nchar**, and **nvarchar** to **geometry** can be implicit or explicit.

Conversion between **geometry** and all other data types isn't allowed. This includes conversion between **geometry** and **geography**. To move data between these two types, use the Well-Known Binary (WKB) or Well-Known Text (WKT) methods, for example `STAsBinary()` with `STGeomFromWKB()`.

Conversion from a string type to **geometry** uses the same rules as `geometry::Parse()`. The string must be Well-Known Text. The SRID of the result is 0.

Conversion between **geometry** and **binary** or **varbinary** uses the SQL Server native serialization format of **geometry**, not Well-Known Binary. A Well-Known Binary value causes an error. To convert to or from Well-Known Binary, use `STAsBinary()` and `STGeomFromWKB()`.

If the target string or binary type is too short for the **geometry** value, an error occurs. The value isn't truncated.

## Remarks

In [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], **geography** and **geometry** data types are supported but can't be [mirrored to the Fabric OneLake](/fabric/database/sql/mirroring-overview).

## Examples

<a id="a-showing-how-to-add-and-query-geometry-data"></a>

### A. Show how to add and query geometry data

The following two examples show how to add and query geometry data. The first example creates a table with an identity column and a `geometry` column, `GeomCol1`. A third column renders the `geometry` column into its Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation, and uses the `STAsText()` method. Two rows are then inserted: one row contains a `LineString` instance of `geometry`, and one row contains a `Polygon` instance.

```sql
IF OBJECT_ID('dbo.SpatialTable', 'U') IS NOT NULL
    DROP TABLE dbo.SpatialTable;
GO

CREATE TABLE SpatialTable
(
    id INT IDENTITY (1, 1),
    GeomCol1 geometry,
    GeomCol2 AS GeomCol1.STAsText()
);
GO

INSERT INTO SpatialTable
(
    GeomCol1
)
VALUES (geometry::STGeomFromText('LINESTRING (100 100, 20 180, 180 180)', 0));

INSERT INTO SpatialTable
(
    GeomCol1
)
VALUES (geometry::STGeomFromText('POLYGON ((0 0, 150 0, 150 150, 0 150, 0 0))', 0));
GO
```

<a id="b-returning-the-intersection-of-two-geometry-instances"></a>

### B. Return the intersection of two geometry instances

The second example uses the `STIntersection()` method to return the points where the two previously inserted `geometry` instances intersect.

```sql
DECLARE @geom1 AS geometry;
DECLARE @geom2 AS geometry;
DECLARE @result AS geometry;

SELECT @geom1 = GeomCol1
FROM SpatialTable
WHERE id = 1;

SELECT @geom2 = GeomCol1
FROM SpatialTable
WHERE id = 2;

SELECT @result = @geom1.STIntersection(@geom2);

SELECT @result.STAsText();
```

<a id="c-using-geometry-in-a-computed-column"></a>

### C. Use geometry in a computed column

The following example creates a table with a persisted computed column using a **geometry** type.

```sql
IF OBJECT_ID('dbo.SpatialTable', 'U') IS NOT NULL
    DROP TABLE dbo.SpatialTable;
GO

CREATE TABLE SpatialTable
(
    locationId INT IDENTITY (1, 1),
    location geometry,
    deliveryArea AS location.STBuffer(10) PERSISTED
);
```

## Related content

- [Spatial Data](../../relational-databases/spatial/spatial-data-sql-server.md)
