---
title: "Spatial Data (SQL Server) | Microsoft Docs"
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "geography data type [SQL Server], spatial storage design"
  - "planar spatial data [SQL Server], designing"
  - "spatial data types [SQL Server]"
  - "geodetic spatial data [SQL Server]"
  - "geometry data type [SQL Server], spatial storage design"
  - "spatial storage [SQL Server]"
  - "geodetic spatial data [SQL Server], designing"
ms.assetid: 41a132a1-09e2-4426-b9df-225270cb8e15
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Spatial Data (SQL Server)
  Spatial data represents information about the physical location and shape of geometric objects. These objects can be point locations or more complex objects such as countries, roads, or lakes.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports two spatial data types: the `geometry` data type and the `geography` data type.  
  
-   The `geometry` type represents data in a Euclidean (flat) coordinate system.  
  
-   The `geography` type represents data in a round-earth coordinate system.  
  
 Both data types are implemented as .NET common language runtime (CLR) data types in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  For a detailed description and examples of spatial features introduced in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], download the white paper, [New Spatial Features in SQL Server 2012](https://go.microsoft.com/fwlink/?LinkId=226407).  
  
##  <a name="reltasks"></a> Related Tasks  
 [Create, Construct, and Query geometry Instances](create-construct-and-query-geometry-instances.md)  
 Describes the methods that you can use with instances of the geometry data type.  
  
 [Create, Construct, and Query geography Instances](create-construct-and-query-geography-instances.md)  
 Describes the methods that you can use with instances of the geography data type.  
  
 [Query Spatial Data for Nearest Neighbor](query-spatial-data-for-nearest-neighbor.md)  
 Describes the common query pattern that is used to find the closest spatial objects to a specific spatial object.  
  
 [Create, Modify, and Drop Spatial Indexes](create-modify-and-drop-spatial-indexes.md)  
 Provides information about creating, altering, and dropping a spatial index.  
  
## Related Content  
 [Spatial Data Types Overview](spatial-data-types-overview.md)  
 Introduces the spatial data types.  
  
-   [Point](point.md)  
  
-   [LineString](linestring.md)  
  
-   [CircularString](circularstring.md)  
  
-   [CompoundCurve](compoundcurve.md)  
  
-   [Polygon](polygon.md)  
  
-   [CurvePolygon](curvepolygon.md)  
  
-   [MultiPoint](multipoint.md)  
  
-   [MultiLineString](multilinestring.md)  
  
-   [MultiPolygon](multipolygon.md)  
  
-   [GeometryCollection](geometrycollection.md)  
  
 [Spatial Indexes Overview](spatial-indexes-overview.md)  
 Introduces spatial indexes and describes tessellation and tessellation schemes.  
  
  
