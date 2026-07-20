---
title: "Spatial Data (SQL Server)"
description: "Spatial data in the SQL Database Engine represents information about the physical location and shape of geometric objects."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mlandzic, jovanpop
ms.date: 11/04/2024
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "geography data type [SQL Server], spatial storage design"
  - "planar spatial data [SQL Server], designing"
  - "spatial data types [SQL Server]"
  - "geodetic spatial data [SQL Server]"
  - "geometry data type [SQL Server], spatial storage design"
  - "spatial storage [SQL Server]"
  - "geodetic spatial data [SQL Server], designing"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Spatial Data
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabric SQL endpoint Fabric DW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]  

  Spatial data represents information about the physical location and shape of geometric objects. These objects can be point locations or more complex objects such as countries/regions, roads, or lakes.  
  
 [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports two spatial data types: the **geometry** data type and the **geography** data type.  
  
-   The **geometry** type represents data in a Euclidean (flat) coordinate system.  
  
-   The **geography** type represents data in a round-earth coordinate system.  
  
 Both data types are implemented as .NET common language runtime (CLR) data types in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Limitations

In [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], **geography** and **geometry** data types are supported but cannot be [mirrored to the Fabric OneLake](/fabric/database/sql/mirroring-overview).

<a id="reltasks"></a>

## Related Tasks
 [Create, Construct, and Query geometry Instances](create-construct-and-query-geometry-instances.md)  
 Describes the methods that you can use with instances of the **geometry** data type.  
  
 [Create, Construct, and Query geography Instances](create-construct-and-query-geography-instances.md)  
 Describes the methods that you can use with instances of the **geography** data type.  
  
 [Query Spatial Data for Nearest Neighbor](query-spatial-data-for-nearest-neighbor.md)  
 Describes the common query pattern that is used to find the closest spatial objects to a specific spatial object.  
  
 [Create, Modify, and Drop Spatial Indexes](create-modify-and-drop-spatial-indexes.md)  
 Provides information about creating, altering, and dropping a spatial index.  

## Spatial tools open-source project

[!INCLUDE [Spatial tools project information](../../includes/spatial-tools.md)]

## Related content

- [Spatial Data Types Overview](spatial-data-types-overview.md)
- [Point](point.md)
- [LineString](linestring.md)
- [CircularString](circularstring.md)
- [CompoundCurve](compoundcurve.md)
- [Polygon](polygon.md)
- [CurvePolygon](curvepolygon.md)
- [MultiPoint](multipoint.md)
- [MultiLineString](multilinestring.md)
- [MultiPolygon](multipolygon.md)
- [GeometryCollection](geometrycollection.md)
- [Spatial Indexes Overview](spatial-indexes-overview.md)
