---
title: "Spatial Data (SQL Server) | Microsoft Docs"
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
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
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Spatial Data (SQL Server)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  Spatial data represents information about the physical location and shape of geometric objects. These objects can be point locations or more complex objects such as countries, roads, or lakes.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports two spatial data types: the **geometry** data type and the **geography** data type.  
  
-   The **geometry** type represents data in a Euclidean (flat) coordinate system.  
  
-   The **geography** type represents data in a round-earth coordinate system.  
  
 Both data types are implemented as .NET common language runtime (CLR) data types in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  For a detailed description and examples of spatial features introduced in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], download the white paper, [New Spatial Features in SQL Server 2012](https://go.microsoft.com/fwlink/?LinkId=226407).  
  
##  <a name="reltasks"></a> Related Tasks  
 [Create, Construct, and Query geometry Instances](../../relational-databases/spatial/create-construct-and-query-geometry-instances.md)  
 Describes the methods that you can use with instances of the geometry data type.  
  
 [Create, Construct, and Query geography Instances](../../relational-databases/spatial/create-construct-and-query-geography-instances.md)  
 Describes the methods that you can use with instances of the geography data type.  
  
 [Query Spatial Data for Nearest Neighbor](../../relational-databases/spatial/query-spatial-data-for-nearest-neighbor.md)  
 Describes the common query pattern that is used to find the closest spatial objects to a specific spatial object.  
  
 [Create, Modify, and Drop Spatial Indexes](../../relational-databases/spatial/create-modify-and-drop-spatial-indexes.md)  
 Provides information about creating, altering, and dropping a spatial index.  
  
## Related Content  
 [Spatial Data Types Overview](../../relational-databases/spatial/spatial-data-types-overview.md)  
 Introduces the spatial data types.  
  
-   [Point](../../relational-databases/spatial/point.md)  
  
-   [LineString](../../relational-databases/spatial/linestring.md)  
  
-   [CircularString](../../relational-databases/spatial/circularstring.md)  
  
-   [CompoundCurve](../../relational-databases/spatial/compoundcurve.md)  
  
-   [Polygon](../../relational-databases/spatial/polygon.md)  
  
-   [CurvePolygon](../../relational-databases/spatial/curvepolygon.md)  
  
-   [MultiPoint](../../relational-databases/spatial/multipoint.md)  
  
-   [MultiLineString](../../relational-databases/spatial/multilinestring.md)  
  
-   [MultiPolygon](../../relational-databases/spatial/multipolygon.md)  
  
-   [GeometryCollection](../../relational-databases/spatial/geometrycollection.md)  
  
 [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md)  
 Introduces spatial indexes and describes tessellation and tessellation schemes.  
  
  
