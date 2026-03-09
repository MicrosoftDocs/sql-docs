---
title: "Extended methods on geometry instances"
description: "Reference for SQL Server-specific extended methods on geometry instances, providing additional functionality beyond OGC standard methods."
author: MladjoA
ms.author: mlandzic
ms.date: "02/05/2026"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
helpviewer_keywords:
  - "Extended Methods on Geometry Instances [SQLServer]"
dev_langs:
  - "TSQL"
ai-usage: ai-assisted
---
# Extended methods on geometry instances
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

SQL Server supports extended methods on **geometry** instances that go beyond the Open Geospatial Consortium (OGC) standard methods. These extended methods provide additional functionality for working with planar geometric data, including Z (elevation) and M (measure) values, precise buffer operations, simplified representations, and enhanced WKT/WKB formats.

## Extended methods vs. OGC methods

While OGC methods provide standardized spatial operations defined by the OpenGIS specification, extended methods offer SQL Server-specific enhancements:

- **OGC methods**: Standardized operations for interoperability with other geospatial systems. Use when standards compliance is required.
- **Extended methods**: SQL Server-specific operations that provide additional functionality, performance optimizations, or support for features not covered by OGC standards (such as circular arcs, Z/M values, and advanced buffer control).

For most spatial tasks, OGC methods provide the necessary functionality. Use extended methods when you need the extra capabilities they provide.

## Enhanced representation formats

These methods provide alternative formats for representing geometry data, including support for Z (elevation) and M (measure) values.

| Method | Description |
| --- | --- |
| [AsBinaryZM &#40;geometry DataType&#41;](../../t-sql/spatial-geometry/asbinaryzm-geometry-datatype.md) | Returns the OGC Well-Known Binary (WKB) representation augmented with Z (elevation) and M (measure) values. |
| [AsTextZM &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/astextzm-geometry-data-type.md) | Returns the OGC Well-Known Text (WKT) representation augmented with Z (elevation) and M (measure) values. |
| [AsGml &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/asgml-geometry-data-type.md) | Returns the Geography Markup Language (GML) representation of a geometry instance. |
| [ToString &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/tostring-geometry-data-type.md) | Returns the string representation of a geometry instance augmented with Z and M values. |

## Z and M coordinate access

These methods access the Z (elevation) and M (measure) values of geometry instances, supporting 3D and 4D spatial data.

| Method | Description |
| --- | --- |
| [Z &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/z-geometry-data-type.md) | Returns the Z (elevation) value of a geometry instance. Null if not defined. |
| [M &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/m-geometry-data-type.md) | Returns the M (measure) value of a geometry instance. Null if not defined. |
| [HasZ &#40;geometry DataType&#41;](../../t-sql/spatial-geometry/hasz-geometry-datatype.md) | Returns 1 if a geometry instance contains at least one point with a Z value. |
| [HasM &#40;geometry DataType&#41;](../../t-sql/spatial-geometry/hasm-geometry-datatype.md) | Returns 1 if a geometry instance contains at least one point with an M value. |

## Advanced buffer operations

These methods provide more control over buffer calculations than the standard OGC STBuffer method.

| Method | Description |
| --- | --- |
| [BufferWithTolerance &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/bufferwithtolerance-geometry-data-type.md) | Returns a geometric object representing all points within a specified distance from a geometry instance, with explicit tolerance control for precision. |
| [BufferWithCurves &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/bufferwithcurves-geometry-data-type.md) | Returns a geometry instance representing all points within a specified distance, preserving circular arc segments in the result. |

## Geometry simplification

These methods create simplified versions of geometry instances, useful for performance optimization and visualization at different scales.

| Method | Description |
| --- | --- |
| [Reduce &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/reduce-geometry-data-type.md) | Returns a simplified approximation of a geometry instance produced by running the Douglas-Peucker algorithm with the specified tolerance. |
| [CurveToLineWithTolerance &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/curvetolinewithtolerance-geometry-data-type.md) | Returns a polygonal approximation of a geometry instance containing circular arc segments, with explicit tolerance control. |

## Spatial relationship queries

These methods perform advanced spatial relationship queries.

| Method | Description |
| --- | --- |
| [ShortestLineTo &#40;geography Data Type&#41;](../../t-sql/spatial-geography/shortestlineto-geography-data-type.md) | Returns a LineString instance with two points representing the shortest distance between the two geometry instances. |

## Validity and type checking

These methods provide detailed validity checking and type information.

| Method | Description |
| --- | --- |
| [IsValidDetailed &#40;geometry DataType&#41;](../../t-sql/spatial-geometry/isvaliddetailed-geometry-datatype.md) | Returns a message that helps identify problems with an invalid geometry instance. |
| [MakeValid &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/makevalid-geometry-data-type.md) | Converts an invalid geometry instance into a valid instance with a valid OGC type. |
| [InstanceOf &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/instanceof-geometry-data-type.md) | Returns 1 if a geometry instance is of the specified type. |
| [IsNull &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/isnull-geometry-data-type.md) | Returns 1 if a geometry instance is null. |

## Spatial operations

These methods perform spatial operations with enhanced capabilities.

| Method | Description |
| --- | --- |
| [Filter &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/filter-geometry-data-type.md) | Offers a fast, index-only intersection method to determine if a geometry instance intersects another instance. |

## Version compatibility

These methods provide information about SQL Server version compatibility.

| Method | Description |
| --- | --- |
| [MinDbCompatibilityLevel &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/mindbcompatibilitylevel-geometry-data-type.md) | Returns the minimum database compatibility level that recognizes the geometry data type. |

## Related content

- [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)
- [OGC Static Geometry Methods](../../t-sql/spatial-geometry/ogc-static-geometry-methods.md)
- [Extended Static Geometry Methods](../../t-sql/spatial-geometry/extended-static-geometry-methods.md)
- [Spatial Data Types Overview](../../relational-databases/spatial/spatial-data-types-overview.md)
- [Create, Construct, and Query geometry Instances](../../relational-databases/spatial/create-construct-and-query-geometry-instances.md)  
  
  
