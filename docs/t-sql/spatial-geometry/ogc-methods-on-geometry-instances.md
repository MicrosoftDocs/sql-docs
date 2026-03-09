---
title: "OGC methods on geometry instances"
description: "Reference for Open Geospatial Consortium (OGC) standard methods available on geometry instances in SQL Server, with descriptions and categories."
author: MladjoA
ms.author: mlandzic
ms.date: "02/05/2026"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "OGC Methods on Geometry Instances [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
ai-usage: ai-assisted
---
# OGC methods on geometry instances
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

SQL Server's **geometry** data type implements methods defined by the Open Geospatial Consortium (OGC) Simple Features for SQL Specification version 1.1.0. These standardized methods ensure that spatial calculations conform to industry standards and work consistently with other geospatial applications.

The **geometry** data type represents data in a Euclidean (flat) coordinate system. Use OGC methods when you need standards-compliant spatial operations for planar data, such as calculating areas, distances, spatial relationships, and performing geometric transformations.

## OGC standards compliance

The OGC Simple Features specification defines a common architecture for geographic information and provides SQL implementation options for spatial data. SQL Server's geometry type conforms to these specifications, making it interoperable with other geospatial systems.

For more information on OGC specifications, see:

- [OGC Specifications, Simple Feature Access Part 1 - Common Architecture](https://go.microsoft.com/fwlink/?LinkId=93627)
- [OGC Specifications, Simple Feature Access Part 2 - SQL Options](https://go.microsoft.com/fwlink/?LinkId=93628)

## Shape properties

These methods return measurements and properties that describe the geometric shape.

| Method | Description |
| --- | --- |
| [STArea](../../t-sql/spatial-geometry/starea-geometry-data-type.md) | Returns the total surface area of a geometry instance in square units based on its spatial reference identifier (SRID). |
| [STLength](../../t-sql/spatial-geometry/stlength-geometry-data-type.md) | Returns the total length of elements in a geometry instance or all elements in a geometry collection. |
| [STBoundary](../../t-sql/spatial-geometry/stboundary-geometry-data-type.md) | Returns the boundary of a geometry instance as a lower-dimensional geometry. |
| [STCentroid](../../t-sql/spatial-geometry/stcentroid-geometry-data-type.md) | Returns the geometric center (centroid) of a geometry instance consisting of one or more polygons. |
| [STEnvelope](../../t-sql/spatial-geometry/stenvelope-geometry-data-type.md) | Returns the minimum axis-aligned bounding rectangle (envelope) of a geometry instance. |
| [STConvexHull](../../t-sql/spatial-geometry/stconvexhull-geometry-data-type.md) | Returns the convex hull of a geometry instance, representing the smallest convex polygon that contains all points. |

## Geometry representation

These methods convert geometry instances between different representation formats.

| Method | Description |
| --- | --- |
| [STAsBinary](../../t-sql/spatial-geometry/stasbinary-geometry-data-type.md) | Returns the OGC Well-Known Binary (WKB) representation of a geometry instance. |
| [STAsText](../../t-sql/spatial-geometry/stastext-geometry-data-type.md) | Returns the OGC Well-Known Text (WKT) representation of a geometry instance. |

## Geometry type information

These methods return information about the geometry type and characteristics.

| Method | Description |
| --- | --- |
| [STGeometryType](../../t-sql/spatial-geometry/stgeometrytype-geometry-data-type.md) | Returns the OGC type name for a geometry instance (Point, LineString, Polygon, MultiPoint, MultiLineString, MultiPolygon, or GeometryCollection). |
| [STDimension](../../t-sql/spatial-geometry/stdimension-geometry-data-type.md) | Returns the maximum dimension of a geometry instance: 0 for points, 1 for curves, or 2 for surfaces. |
| [STSrid](../../t-sql/spatial-geometry/stsrid-geometry-data-type.md) | Returns the spatial reference identifier (SRID) of a geometry instance. |

## Point and curve access

These methods access specific points and curves within a geometry instance.

| Method | Description |
| --- | --- |
| [STStartPoint](../../t-sql/spatial-geometry/ststartpoint-geometry-data-type.md) | Returns the start point of a geometry instance (for LineString types). |
| [STEndpoint](../../t-sql/spatial-geometry/stendpoint-geometry-data-type.md) | Returns the end point of a geometry instance (for LineString types). |
| [STPointN](../../t-sql/spatial-geometry/stpointn-geometry-data-type.md) | Returns a specified point from a geometry instance. |
| [STPointOnSurface](../../t-sql/spatial-geometry/stpointonsurface-geometry-data-type.md) | Returns an arbitrary point guaranteed to be within a geometry instance. |
| [STCurveN &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stcurven-geometry-data-type.md) | Returns the specified curve from a geometry instance that's a LineString, CircularString, or CompoundCurve. |
| [STCurveToLine &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stcurvetoline-geometry-data-type.md) | Returns a polygonal approximation of a geometry instance containing circular arc segments. |
| [STX](../../t-sql/spatial-geometry/stx-geometry-data-type.md) | Returns the X-coordinate of a Point instance. |
| [STY](../../t-sql/spatial-geometry/sty-geometry-data-type.md) | Returns the Y-coordinate of a Point instance. |

## Polygon ring access

These methods access rings within polygon geometry instances.

| Method | Description |
| --- | --- |
| [STExteriorRing](../../t-sql/spatial-geometry/stexteriorring-geometry-data-type.md) | Returns the exterior ring of a Polygon instance as a LineString. |
| [STInteriorRingN](../../t-sql/spatial-geometry/stinteriorringn-geometry-data-type.md) | Returns the specified interior ring of a Polygon instance as a LineString. |
| [STNumInteriorRing](../../t-sql/spatial-geometry/stnuminteriorring-geometry-data-type.md) | Returns the number of interior rings in a Polygon instance. |

## Collection access

These methods work with geometry collections and return information about their elements.

| Method | Description |
| --- | --- |
| [STGeometryN](../../t-sql/spatial-geometry/stgeometryn-geometry-data-type.md) | Returns a specified geometry from a geometry collection. |
| [STNumGeometries](../../t-sql/spatial-geometry/stnumgeometries-geometry-data-type.md) | Returns the number of geometries in a geometry collection. |
| [STNumPoints](../../t-sql/spatial-geometry/stnumpoints-geometry-data-type.md) | Returns the total number of points in each figure of a geometry instance. |
| [STNumCurves &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stnumcurves-geometry-data-type.md) | Returns the number of curves in a one-dimensional geometry instance. |

## Spatial relationship tests

These methods test spatial relationships between geometry instances, returning 1 (true) or 0 (false).

| Method | Description |
| --- | --- |
| [STContains](../../t-sql/spatial-geometry/stcontains-geometry-data-type.md) | Returns 1 if a geometry instance completely contains another instance. |
| [STCrosses](../../t-sql/spatial-geometry/stcrosses-geometry-data-type.md) | Returns 1 if a geometry instance crosses another instance. |
| [STDisjoint](../../t-sql/spatial-geometry/stdisjoint-geometry-data-type.md) | Returns 1 if a geometry instance is spatially disjoint from another instance (doesn't intersect). |
| [STEquals](../../t-sql/spatial-geometry/stequals-geometry-data-type.md) | Returns 1 if a geometry instance represents the same point set as another instance. |
| [STIntersects](../../t-sql/spatial-geometry/stintersects-geometry-data-type.md) | Returns 1 if a geometry instance intersects another instance. |
| [STOverlaps](../../t-sql/spatial-geometry/stoverlaps-geometry-data-type.md) | Returns 1 if a geometry instance overlaps another instance (they intersect and neither contains the other). |
| [STRelate](../../t-sql/spatial-geometry/strelate-geometry-data-type.md) | Returns 1 if a geometry instance is related to another instance based on a Dimensionally Extended 9 Intersection Model (DE-9IM) pattern. |
| [STTouches](../../t-sql/spatial-geometry/sttouches-geometry-data-type.md) | Returns 1 if a geometry instance touches (shares boundary points but not interior points with) another instance. |
| [STWithin](../../t-sql/spatial-geometry/stwithin-geometry-data-type.md) | Returns 1 if a geometry instance is completely within another instance. |
| [STDistance](../../t-sql/spatial-geometry/stdistance-geometry-data-type.md) | Returns the shortest distance between a point in a geometry instance and a point in another instance. |

## Spatial operations

These methods create new geometry instances by performing spatial operations.

| Method | Description |
| --- | --- |
| [STBuffer](../../t-sql/spatial-geometry/stbuffer-geometry-data-type.md) | Returns a geometry object representing all points within a specified distance from a geometry instance. |
| [STDifference](../../t-sql/spatial-geometry/stdifference-geometry-data-type.md) | Returns a geometry representing the point set from one instance that doesn't lie within another instance. |
| [STIntersection](../../t-sql/spatial-geometry/stintersection-geometry-data-type.md) | Returns a geometry representing the points where two geometry instances intersect. |
| [STSymDifference](../../t-sql/spatial-geometry/stsymdifference-geometry-data-type.md) | Returns a geometry representing the points in either of two instances but not in both (symmetric difference). |
| [STUnion](../../t-sql/spatial-geometry/stunion-geometry-data-type.md) | Returns a geometry representing the union (all points) of two geometry instances. |

## Validity tests

These methods test the validity and properties of geometry instances.

| Method | Description |
| --- | --- |
| [STIsClosed](../../t-sql/spatial-geometry/stisclosed-geometry-data-type.md) | Returns 1 if the start and end points of a geometry instance are the same. |
| [STIsEmpty](../../t-sql/spatial-geometry/stisempty-geometry-data-type.md) | Returns 1 if a geometry instance is empty. |
| [STIsRing](../../t-sql/spatial-geometry/stisring-geometry-data-type.md) | Returns 1 if a geometry instance is closed and simple (doesn't intersect itself). |
| [STIsSimple](../../t-sql/spatial-geometry/stissimple-geometry-data-type.md) | Returns 1 if a geometry instance is simple (doesn't intersect itself). |
| [STIsValid](../../t-sql/spatial-geometry/stisvalid-geometry-data-type.md) | Returns 1 if a geometry instance is well-formed according to its OGC type. |

## Related content

- [Extended Methods on Geometry Instances](../../t-sql/spatial-geometry/extended-methods-on-geometry-instances.md)
- [OGC Static Geometry Methods](../../t-sql/spatial-geometry/ogc-static-geometry-methods.md)
- [Extended Static Geometry Methods](../../t-sql/spatial-geometry/extended-static-geometry-methods.md)
- [Spatial Data Types Overview](../../relational-databases/spatial/spatial-data-types-overview.md)
- [Create, Construct, and Query geometry Instances](../../relational-databases/spatial/create-construct-and-query-geometry-instances.md)  
  
  
