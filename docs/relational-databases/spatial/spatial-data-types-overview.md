---
title: "Spatial Data Types Overview"
description: "Spatial Data Types represent information about the physical location and shape of geometric objects in the SQL Database Engine."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mlandzic, jovanpop
ms.date: 11/04/2024
ms.service: sql
ms.topic: language-reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "geometry data type [SQL Server], understanding"
  - "geography data type [SQL Server], spatial data"
  - "planar spatial data [SQL Server], geometry data type"
  - "spatial data types [SQL Server]"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Spatial Data Types Overview

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabric SQL endpoint Fabric DW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]

There are two types of spatial data. The **geometry** data type supports planar, or Euclidean (flat-earth), data. The **geometry** data type both conforms to the *Open Geospatial Consortium (OGC) Simple Features for SQL Specification* version 1.1.0 and is compliant with SQL MM (ISO standard).

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] also supports the **geography** data type, which stores ellipsoidal (round-earth) data, such as GPS latitude and longitude coordinates.

> [!TIP]
> [!INCLUDE [Spatial tools project information](../../includes/spatial-tools.md)]

## Spatial data objects

The **geometry** and **geography** data types support 16 types of spatial data objects, or instance types. However, only 11 of these instance types are *instantiable*; you can create and work with these instances (or instantiate them) in a database. These instances derive certain properties from their parent data types.

The following figure shows the geometry hierarchy upon which the **geometry** and **geography** data types are based. The instantiable types of **geometry** and **geography** are indicated in blue.  

:::image type="content" source="media/spatial-data-types-overview/geom-hierarchy.png" alt-text="Diagram of the hierarchy of Geometry types." lightbox="media/spatial-data-types-overview/geom-hierarchy.png":::

There's an additional instantiable type for the **geography** data type: **FullGlobe**. The **geometry** and **geography** types can recognize a specific instance as long as it's a well-formed instance, even if the instance isn't defined explicitly. For example, if you define a **Point** instance explicitly using the `STPointFromText()` method, **geometry** and **geography** recognize the instance as a **Point**, as long as the method input is well formed. If you define the same instance using the `STGeomFromText()` method, both the **geometry** and **geography** data types recognize the instance as a **Point**.  

The subtypes for geometry and geography types are divided into simple and collection types.  Some methods like `STNumCurves()` work only with simple types.  

Simple types are:

- [Point](point.md)  
- [LineString](linestring.md)  
- [CircularString](circularstring.md)  
- [CompoundCurve](compoundcurve.md)  
- [Polygon](polygon.md)  
- [CurvePolygon](curvepolygon.md)  

Collection types are:

- [MultiPoint](multipoint.md)  
- [MultiLineString](multilinestring.md)  
- [MultiPolygon](multipolygon.md)  
- [GeometryCollection](geometrycollection.md)  

## Geometry and geography data type differences

The two types of spatial data often behave similarly. There are some key differences in how the data is stored and manipulated.  

### How connecting edges are defined

The defining data for **LineString** and **Polygon** types are vertices only. The connecting edge between two vertices in a geometry type is a straight line. However, the connecting edge between two vertices in a geography type is a short great elliptic arc between the two vertices. A great ellipse is the intersection of the ellipsoid with a plane through its center. A great elliptic arc is an arc segment on the great ellipse.  

### How circular arc segments are defined

Circular arc segments for geometry types are defined on the XY Cartesian coordinate plane (Z values are ignored). Circular arc segments for geography types are defined by curve segments on a reference sphere. Any parallel on the reference sphere can be defined by two complementary circular arcs where the points for both arcs have a constant latitude angle.  

### Measurements in spatial data types

In the planar (flat-earth) system, measurements of distances and areas are given in the same unit of measurement as coordinates. Using the **geometry** data type, the distance between (2, 2) and (5, 6) is five units, regardless of the units used.  

In an ellipsoidal, or round-earth system, coordinates are given in degrees of latitude and longitude. However, lengths and areas are typically measured in meters and square meters, though the measurement might depend on the [spatial reference identifier](spatial-reference-identifiers-srids.md) of the **geography** instance. The most common unit of measurement for the **geography** data type is meters.  

### Orientation of spatial data

The ring orientation of a polygon is not an important factor in the planar system. The *OGC Simple Features for SQL Specification* doesn't dictate a ring ordering, and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't enforce ring ordering.  

In an ellipsoidal system, a polygon without an orientation has no meaning, or is ambiguous. For example, does a ring around the equator describe the northern or southern hemisphere? If we use the **geography** data type to store the spatial instance, we must specify the orientation of the ring and accurately describe the location of the instance.

The interior of the polygon in an ellipsoidal system is defined by the "left-hand rule": if you imagine yourself walking along the ring of a geography Polygon, following the points in the order in which they are listed, the area on the left is being treated as the interior of the Polygon, and the area on the right as the exterior of the Polygon.

When the compatibility level is 100 or below in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] then the **geography** data type has the following restrictions:

- Each **geography** instance must fit inside a single hemisphere. No spatial objects larger than a hemisphere can be stored.

- Any **geography** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) or Well-Known Binary (WKB) representation that produces an object larger than a hemisphere throws an **ArgumentException**.  

- The **geography** data type methods that require the input of two **geography** instances, such as `STIntersection()`, `STUnion()`, `STDifference()`, and `STSymDifference()`, will return null if the results from the methods do not fit inside a single hemisphere. `STBuffer()` will also return null if the output exceeds a single hemisphere.  

In [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], **FullGlobe** is a special type of Polygon that covers the entire globe. It has an area, but no borders or vertices.  

### Outer and inner rings in `geography` data type

The *OGC Simple Features for SQL Specification* discusses outer rings and inner rings, but this distinction makes little sense for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] **geography** data type; any ring of a polygon can be taken to be the outer ring.  

For more information on OGC specifications, see the following documents:

- [OGC Specifications, Simple Feature Access Part 1 - Common Architecture](https://go.microsoft.com/fwlink/?LinkId=93627)  
- [OGC Specifications, Simple Feature Access Part 2 - SQL Options](https://go.microsoft.com/fwlink/?LinkId=93628)  

## Circular arc segments

Three instantiable types can take circular arc segments: **CircularString**, **CompoundCurve**, and **CurvePolygon**.  A circular arc segment is defined by three points in a two-dimensional plane and the third point cannot be the same as the first point. Few examples of circular arc segments:

:::image type="content" source="media/spatial-data-types-overview/spatial-types.gif" alt-text="Diagram of circular arc segments that can be represented in SQL Database Engine spatial types.":::

First two examples show typical circular arc segments. Note how each of the three points lies on the perimeter of a circle.  

Other two examples show how a line segment can be defined as a circular arc segment. Three points are still needed to define the circular arc segment unlike a regular line segment, which can be defined by just two points.

Methods operating on circular arc segment types use straight-line segments to approximate the circular arc. The number of line segments used to approximate the arc will depend on the length and curvature of the arc. Z values can be stored for each of the circular arc segment types, but will not be used in the calculations.  

> [!NOTE]  
> If Z values are given for circular arc segments then they must be the same for all points in the circular arc segment for it to be accepted for input. For example: `CIRCULARSTRING(0 0 1, 2 2 1, 4 0 1)` is accepted, but `CIRCULARSTRING(0 0 1, 2 2 2, 4 0 1)` is not accepted.  

<a id="linestring-and-circularstring-comparison"></a>

### LineStr and CircularString comparison

This example shows how to store identical isosceles triangles using both a **LineString** instance and **CircularString** instance:  

```sql
DECLARE @g1 geometry;
DECLARE @g2 geometry;
SET @g1 = geometry::STGeomFromText('LINESTRING(1 1, 5 1, 3 5, 1 1)', 0);
SET @g2 = geometry::STGeomFromText('CIRCULARSTRING(1 1, 3 1, 5 1, 4 3, 3 5, 2 3, 1 1)', 0);
IF @g1.STIsValid() = 1 AND @g2.STIsValid() = 1
  BEGIN
      SELECT @g1.ToString(), @g2.ToString()
      SELECT @g1.STLength() AS [LS Length], @g2.STLength() AS [CS Length]
  END
```  

A **CircularString** instance requires seven points to define the triangle. **LineString** instance requires only four points to define the triangle. The reason for this is that a **CircularString** instance stores circular arc segments and not line segments. The sides of the triangle stored in the **CircularString** instance are ABC, CDE, and EFA. The sides of the triangle stored in the **LineString** instance are AC, CE, and EA.  

Consider the following example:  

```sql
SET @g1 = geometry::STGeomFromText('LINESTRING(0 0, 2 2, 4 0)', 0);
SET @g2 = geometry::STGeomFromText('CIRCULARSTRING(0 0, 2 2, 4 0)', 0);
SELECT @g1.STLength() AS [LS Length], @g2.STLength() AS [CS Length];
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```sql
LS Length    CS Length
5.65685...   6.28318...
```

**CircularString** instances use fewer points to store curve boundaries with greater precision than **LineString** instances. **CircularString** instances are useful for storing circular boundaries like a 20-mile search radius from a specific point. **LineString** instances are good for storing boundaries that are linear like a square city block.  

<a id="linestring-and-compoundcurve-comparison"></a>

### LineStr and CompoundCurve comparison

The following code examples show how to store the same figure using **LineString** and **CompoundCurve** instances:

```sql
SET @g = geometry::Parse('LINESTRING(2 2, 4 2, 4 4, 2 4, 2 2)');
SET @g = geometry::Parse('COMPOUNDCURVE((2 2, 4 2), (4 2, 4 4), (4 4, 2 4), (2 4, 2 2))');
SET @g = geometry::Parse('COMPOUNDCURVE((2 2, 4 2, 4 4, 2 4, 2 2))');
```

In the previous examples, either a **LineString** instance or a **CompoundCurve** instance could store the figure. This next example uses a **CompoundCurve** to store a pie slice:  

```sql
SET @g = geometry::Parse('COMPOUNDCURVE(CIRCULARSTRING(2 2, 1 3, 0 2),(0 2, 1 0, 2 2))');  
```  

A **CompoundCurve** instance can store the circular arc segment (2 2, 1 3, 0 2) directly, but a **LineString** instance would have to convert the curve into several smaller line segments.  

<a id="circularstring-and-compoundcurve-comparison"></a>

### CircularStr and CompoundCurve comparison

The following code example shows how the pie slice can be stored in a **CircularString** instance:  

```sql
DECLARE @g geometry;
SET @g = geometry::Parse('CIRCULARSTRING( 0 0, 1 2.1082, 3 6.3246, 0 7, -3 6.3246, -1 2.1082, 0 0)');
SELECT @g.ToString(), @g.STLength();
```

Storing the pie slice using a **CircularString** instance requires that three points be used for each line segment. If an intermediate point isn't known, it either has to be calculated, or the endpoint of the line segment has to be doubled as the following snippet shows:  

```sql
SET @g = geometry::Parse('CIRCULARSTRING( 0 0, 3 6.3246, 3 6.3246, 0 7, -3 6.3246, 0 0, 0 0)');
```

**CompoundCurve** instances allow both **LineString** and **CircularString** components so that only two points to the line segments of the pie slice need to be known. This code example shows how to use a **CompoundCurve** to store the same figure:

```sql
DECLARE @g geometry;
SET @g = geometry::Parse('COMPOUNDCURVE(CIRCULARSTRING( 3 6.3246, 0 7, -3 6.3246), (-3 6.3246, 0 0, 3 6.3246))');
SELECT @g.ToString(), @g.STLength();
```

### Polygon and CurvePolygon comparison

**CurvePolygon** instances can use **CircularString** and **CompoundCurve** instances when defining their exterior and interior rings. **Polygon** instances can't.

## Limitations

In [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], **geography** and **geometry** data types are supported but cannot be [mirrored to the Fabric OneLake](/fabric/database/sql/mirroring-overview).

## Related content

- [Spatial Data](spatial-data-sql-server.md)
- [geometry Data Type Method Reference](../../t-sql/spatial-geometry/spatial-types-geometry-transact-sql.md)
- [geography Data Type Method Reference](../../t-sql/spatial-geography/spatial-types-geography.md)
- [STNumCurves (geometry Data Type)](../../t-sql/spatial-geometry/stnumcurves-geometry-data-type.md)
- [STNumCurves (geography Data Type)](../../t-sql/spatial-geography/stnumcurves-geography-data-type.md)
- [STGeomFromText (geometry Data Type)](../../t-sql/spatial-geometry/stgeomfromtext-geometry-data-type.md)
- [STGeomFromText (geography data type)](../../t-sql/spatial-geography/stgeomfromtext-geography-data-type.md)
