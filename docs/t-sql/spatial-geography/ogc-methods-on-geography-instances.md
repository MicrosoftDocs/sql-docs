---
title: "OGC methods on geography instances"
description: "Reference for Open Geospatial Consortium (OGC) standard methods available on geography instances in SQL Server, with descriptions and categories."
author: MladjoA
ms.author: mlandzic
ms.date: "02/05/2026"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
ai-usage: ai-assisted
---
# OGC methods on geography instances
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

SQL Server's **geography** data type implements methods defined by the Open Geospatial Consortium (OGC) Simple Features for SQL Specification version 1.1.0. These standardized methods ensure that spatial calculations conform to industry standards and work consistently with other geospatial applications.

The **geography** data type stores ellipsoidal (round-earth) data, such as GPS latitude and longitude coordinates. Use OGC methods when you need standards-compliant spatial operations for real-world geographic data, such as calculating distances between cities, areas of regions, or determining if locations intersect.

## OGC standards compliance

The OGC Simple Features specification defines a common architecture for geographic information and provides SQL implementation options for spatial data. SQL Server's geography type conforms to these specifications, making it interoperable with other geospatial systems.

For more information on OGC specifications, see:

- [OGC Specifications, Simple Feature Access Part 1 - Common Architecture](https://go.microsoft.com/fwlink/?LinkId=93627)
- [OGC Specifications, Simple Feature Access Part 2 - SQL Options](https://go.microsoft.com/fwlink/?LinkId=93628)

## Geometry vs. geography

The primary difference between geometry and geography types is the coordinate system:

- **Geometry** uses a planar (flat-earth) Euclidean coordinate system, suitable for localized data or when earth curvature isn't significant.
- **Geography** uses an ellipsoidal (round-earth) coordinate system and accounts for earth curvature, making it appropriate for GPS coordinates, worldwide mapping, and accurate long-distance calculations.

When using geography methods, distances are measured in meters, and areas in square meters, based on the spatial reference identifier (SRID) of the instance.

## Shape properties

These methods return measurements and properties that describe the geographic shape.

| Method | Description |
| --- | --- |
| [STArea](../../t-sql/spatial-geography/starea-geography-data-type.md) | Returns the total surface area of a geography instance in square meters. |
| [STLength](../../t-sql/spatial-geography/stlength-geography-data-type.md) | Returns the total length of elements in a geography instance in meters. |

## Geography representation

These methods convert geography instances between different representation formats.

| Method | Description |
| --- | --- |
| [STAsBinary](../../t-sql/spatial-geography/stasbinary-geography-data-type.md) | Returns the OGC Well-Known Binary (WKB) representation of a geography instance. |
| [STAsText](../../t-sql/spatial-geography/stastext-geography-data-type.md) | Returns the OGC Well-Known Text (WKT) representation of a geography instance. |

## Geography type information

These methods return information about the geography type and characteristics.

| Method | Description |
| --- | --- |
| [STGeometryType](../../t-sql/spatial-geography/stgeometrytype-geography-data-type.md) | Returns the OGC type name for a geography instance (Point, LineString, Polygon, MultiPoint, MultiLineString, MultiPolygon, or GeometryCollection). |
| [STDimension](../../t-sql/spatial-geography/stdimension-geography-data-type.md) | Returns the maximum dimension of a geography instance: 0 for points, 1 for curves, or 2 for surfaces. |
| [STSrid](../../t-sql/spatial-geography/stsrid-geography-data-type.md) | Returns the spatial reference identifier (SRID) of a geography instance. |

## Point and curve access

These methods access specific points and curves within a geography instance.

| Method | Description |
| --- | --- |
| [STStartPoint](../../t-sql/spatial-geography/ststartpoint-geography-data-type.md) | Returns the start point of a geography instance (for LineString types). |
| [STEndpoint](../../t-sql/spatial-geography/stendpoint-geography-data-type.md) | Returns the end point of a geography instance (for LineString types). |
| [STPointN](../../t-sql/spatial-geography/stpointn-geography-data-type.md) | Returns a specified point from a geography instance. |
| [STCurveN &#40;geography Data Type&#41;](../../t-sql/spatial-geography/stcurven-geography-data-type.md) | Returns the specified curve from a geography instance that's a LineString, CircularString, or CompoundCurve. |
| [STCurveToLine &#40;geography Data Type&#41;](../../t-sql/spatial-geography/stcurvetoline-geography-data-type.md) | Returns a polygonal approximation of a geography instance containing circular arc segments. |

## Collection access

These methods work with geography collections and return information about their elements.

| Method | Description |
| --- | --- |
| [STGeometryN](../../t-sql/spatial-geography/stgeometryn-geography-data-type.md) | Returns a specified geography from a geography collection. |
| [STNumGeometries](../../t-sql/spatial-geography/stnumgeometries-geography-data-type.md) | Returns the number of geographies in a geography collection. |
| [STNumPoints](../../t-sql/spatial-geography/stnumpoints-geography-data-type.md) | Returns the total number of points in each figure of a geography instance. |
| [STNumCurves &#40;geography Data Type&#41;](../../t-sql/spatial-geography/stnumcurves-geography-data-type.md) | Returns the number of curves in a one-dimensional geography instance. |

## Spatial relationship tests

These methods test spatial relationships between geography instances, returning 1 (true) or 0 (false).

| Method | Description |
| --- | --- |
| [STDisjoint](../../t-sql/spatial-geography/stdisjoint-geography-data-type.md) | Returns 1 if a geography instance is spatially disjoint from another instance (doesn't intersect). |
| [STEquals](../../t-sql/spatial-geography/stequals-geography-data-type.md) | Returns 1 if a geography instance represents the same point set as another instance. |
| [STIntersects](../../t-sql/spatial-geography/stintersects-geography-data-type.md) | Returns 1 if a geography instance intersects another instance. |
| [STDistance](../../t-sql/spatial-geography/stdistance-geography-data-type.md) | Returns the shortest distance in meters between a point in a geography instance and a point in another instance. |

## Spatial operations

These methods create new geography instances by performing spatial operations.

| Method | Description |
| --- | --- |
| [STBuffer](../../t-sql/spatial-geography/stbuffer-geography-data-type.md) | Returns a geography object representing all points within a specified distance (in meters) from a geography instance. |
| [STDifference](../../t-sql/spatial-geography/stdifference-geography-data-type.md) | Returns a geography representing the point set from one instance that doesn't lie within another instance. |
| [STIntersection](../../t-sql/spatial-geography/stintersection-geography-data-type.md) | Returns a geography representing the points where two geography instances intersect. |
| [STSymDifference](../../t-sql/spatial-geography/stsymdifference-geography-data-type.md) | Returns a geography representing the points in either of two instances but not in both (symmetric difference). |
| [STUnion](../../t-sql/spatial-geography/stunion-geography-data-type.md) | Returns a geography representing the union (all points) of two geography instances. |

## Validity tests

These methods test the validity and properties of geography instances.

| Method | Description |
| --- | --- |
| [STIsClosed](../../t-sql/spatial-geography/stisclosed-geography-data-type.md) | Returns 1 if the start and end points of a geography instance are the same. |
| [STIsEmpty](../../t-sql/spatial-geography/stisempty-geography-data-type.md) | Returns 1 if a geography instance is empty. |
| [STIsValid](../../t-sql/spatial-geography/stisvalid-geography-data-type.md) | Returns 1 if a geography instance is well-formed according to its OGC type and correctly labeled for an ellipsoidal earth. |

## Related content

- [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)
- [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)
- [Extended Static Geography Methods](../../t-sql/spatial-geography/extended-static-geography-methods.md)
- [Spatial Data Types Overview](../../relational-databases/spatial/spatial-data-types-overview.md)
- [Create, Construct, and Query geography Instances](../../relational-databases/spatial/create-construct-and-query-geography-instances.md)  
  
  
