---
title: "Spatial Reference Identifiers (SRIDs)"
description: "Spatial Reference Identifiers (SRIDs) correspond to a spatial reference system based on the specific ellipsoid used for either flat-earth mapping or round-earth mapping in SQL Database Engine spatial data."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mlandzic, jovanpop
ms.date: 11/04/2024
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "Spatial Reference Identifiers (SRIDs)"
  - "geodetic spatial data [SQL Server], identifiers"
  - "SRID"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Spatial Reference Identifiers (SRIDs)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabric SQL endpoint Fabric DW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]  

  Each spatial instance has a spatial reference identifier (SRID). The SRID corresponds to a spatial reference system based on the specific ellipsoid used for either flat-earth mapping or round-earth mapping in SQL Database Engine spatial data.
  
 A spatial column can contain objects with different SRIDs. However, only spatial instances with the same SRID can be used when performing operations with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] spatial data methods on your data. The result of any spatial method derived from two spatial data instances is valid only if those instances have the same SRID that is based on the same unit of measurement, datum, and projection used to determine the coordinates of the instances. The most common units of measurement of a SRID are meters or square meters.  
  
 If two spatial instances do not have the same SRID, the results from a **geometry** or **geography** Data Type method used on the instances will return `NULL`. For example, for the following predicate term to return a non-`NULL` result, the two **geometry** instances, `geometry1` and `geometry2`, must have the same SRID:  
  
 `geometry1.STIntersects(geometry2) = 1`  
  
> [!NOTE]  
>  The spatial reference identification system is defined by the [European Petroleum Survey Group (EPSG)](https://epsg.org/home.html) standard, which is a set of standards developed for cartography, surveying, and geodetic data storage. This standard is owned by the Oil and Gas Producers (OGP) Surveying and Positioning Committee.  
  
## Related content

- [Spatial Data Types Overview](spatial-data-types-overview.md)
