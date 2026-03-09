---
title: "Extended methods on geography instances"
description: "Reference for SQL Server-specific extended methods on geography instances, providing additional functionality beyond OGC standard methods."
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
# Extended methods on geography instances
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

SQL Server supports extended methods on **geography** instances that go beyond the Open Geospatial Consortium (OGC) standard methods. These extended methods provide additional functionality for working with geographic data, including Z (elevation) and M (measure) values, precise buffer operations, simplified representations, and enhanced WKT/WKB formats.

## Extended methods vs. OGC methods

While OGC methods provide standardized spatial operations defined by the OpenGIS specification, extended methods offer SQL Server-specific enhancements:

- **OGC methods**: Standardized operations for interoperability with other geospatial systems. Use when standards compliance is required.
- **Extended methods**: SQL Server-specific operations that provide additional functionality, performance optimizations, or support for features not covered by OGC standards (such as circular arcs, Z/M values, and advanced buffer control).

For most spatial tasks, OGC methods provide the necessary functionality. Use extended methods when you need the extra capabilities they provide.

## Enhanced representation formats

These methods provide alternative formats for representing geography data, including support for Z (elevation) and M (measure) values.

| Method | Description |
| --- | --- |
| [AsBinaryZM &#40;geography Data Type&#41;](../../t-sql/spatial-geography/asbinaryzm-geography-data-type.md) | Returns the OGC Well-Known Binary (WKB) representation augmented with Z (elevation) and M (measure) values. |
| [AsTextZM](../../t-sql/spatial-geography/astextzm-geography-data-type.md) | Returns the OGC Well-Known Text (WKT) representation augmented with Z (elevation) and M (measure) values. |
| [AsGml](../../t-sql/spatial-geography/asgml-geography-data-type.md) | Returns the Geography Markup Language (GML) representation of a geography instance. |
| [ToString](../../t-sql/spatial-geography/tostring-geography-data-type.md) | Returns the string representation of a geography instance augmented with Z and M values. |

## Z and M coordinate access

These methods access the Z (elevation) and M (measure) values of geography instances, supporting 3D and 4D spatial data.

| Method | Description |
| --- | --- |
| [Lat](../../t-sql/spatial-geography/lat-geography-data-type.md) | Returns the latitude property of a Point geography instance. |
| [Long](../../t-sql/spatial-geography/long-geography-data-type.md) | Returns the longitude property of a Point geography instance. |
| [Z](../../t-sql/spatial-geography/z-geography-data-type.md) | Returns the Z (elevation) value of a geography instance. Null if not defined. |
| [M](../../t-sql/spatial-geography/m-geography-data-type.md) | Returns the M (measure) value of a geography instance. Null if not defined. |
| [HasZ &#40;geography Data Type&#41;](../../t-sql/spatial-geography/hasz-geography-data-type.md) | Returns 1 if a geography instance contains at least one point with a Z value. |
| [HasM &#40;geography Data Type&#41;](../../t-sql/spatial-geography/hasm-geography-data-type.md) | Returns 1 if a geography instance contains at least one point with an M value. |

## Advanced buffer operations

These methods provide more control over buffer calculations than the standard OGC STBuffer method.

| Method | Description |
| --- | --- |
| [BufferWithTolerance](../../t-sql/spatial-geography/bufferwithtolerance-geography-data-type.md) | Returns a geometric object representing all points within a specified distance from a geography instance, with explicit tolerance control for precision. |
| [BufferWithCurves &#40;geography Data Type&#41;](../../t-sql/spatial-geography/bufferwithcurves-geography-data-type.md) | Returns a geography instance representing all points within a specified distance, preserving circular arc segments in the result. |

## Geometry simplification

These methods create simplified versions of geography instances, useful for performance optimization and visualization at different scales.

| Method | Description |
| --- | --- |
| [Reduce](../../t-sql/spatial-geography/reduce-geography-data-type.md) | Returns a simplified approximation of a geography instance produced by running the Douglas-Peucker algorithm with the specified tolerance. |
| [CurveToLineWithTolerance &#40;geography Data Type&#41;](../../t-sql/spatial-geography/curvetolinewithtolerance-geography-data-type.md) | Returns a polygonal approximation of a geography instance containing circular arc segments, with explicit tolerance control. |

## Envelope and bounding information

These methods provide information about the bounding region of a geography instance.

| Method | Description |
| --- | --- |
| [EnvelopeCenter](../../t-sql/spatial-geography/envelopecenter-geography-data-type.md) | Returns the center point of the bounding circle for a geography instance. |
| [EnvelopeAngle](../../t-sql/spatial-geography/envelopeangle-geography-data-type.md) | Returns the maximum angle between the center point and a point in the geography instance, measured in degrees. |

## Polygon ring access

These methods provide access to rings in a geography Polygon instance.

| Method | Description |
| --- | --- |
| [NumRing](../../t-sql/spatial-geography/numrings-geography-data-type.md) | Returns the total number of rings in a Polygon geography instance. |
| [RingN](../../t-sql/spatial-geography/ringn-geography-data-type.md) | Returns the specified ring of a Polygon geography instance. |

## Spatial relationship queries

These methods perform advanced spatial relationship queries.

| Method | Description |
| --- | --- |
| [ShortestLineTo &#40;geography Data Type&#41;](../../t-sql/spatial-geography/shortestlineto-geography-data-type.md) | Returns a LineString instance with two points representing the shortest distance between the two geography instances. |

## Validity and type checking

These methods provide detailed validity checking and type information.

| Method | Description |
| --- | --- |
| [IsValidDetailed &#40;geography Data Type&#41;](../../t-sql/spatial-geography/isvaliddetailed-geography-data-type.md) | Returns a message that helps identify problems with an invalid geography instance. |
| [MakeValid](../../t-sql/spatial-geography/makevalid-geography-data-type.md) | Converts an invalid geography instance into a valid instance with a valid OGC type. |
| [InstanceOf](../../t-sql/spatial-geography/instanceof-geography-data-type.md) | Returns 1 if a geography instance is of the specified type. |
| [IsNull](../../t-sql/spatial-geography/isnull-geography-data-type.md) | Returns 1 if a geography instance is null. |

## Spatial operations

These methods perform spatial operations with enhanced capabilities.

| Method | Description |
| --- | --- |
| [Filter](../../t-sql/spatial-geography/filter-geography-data-type.md) | Offers a fast, index-only intersection method to determine if a geography instance intersects another instance. |
| [ReorientObject](../../t-sql/spatial-geography/reorientobject-geography-data-type.md) | Returns a geography instance with interchanged interior and exterior regions (reverses ring orientation). |

## Version compatibility

These methods provide information about SQL Server version compatibility.

| Method | Description |
| --- | --- |
| [MinDbCompatibilityLevel &#40;geography Data Type&#41;](../../t-sql/spatial-geography/mindbcompatibilitylevel-geography-data-type.md) | Returns the minimum database compatibility level that recognizes the geography data type. |

## Related content

- [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)
- [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)
- [Extended Static Geography Methods](../../t-sql/spatial-geography/extended-static-geography-methods.md)
- [Spatial Data Types Overview](../../relational-databases/spatial/spatial-data-types-overview.md)
- [Create, Construct, and Query geography Instances](../../relational-databases/spatial/create-construct-and-query-geography-instances.md)  
  
  
