---
description: "Spatial Reference Identifiers (SRIDs)"
title: "Spatial Reference Identifiers (SRIDs) | Microsoft Docs"
ms.date: "03/29/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "Spatial Reference Identifiers (SRIDs)"
  - "geodetic spatial data [SQL Server], identifiers"
  - "SRID"
ms.assetid: 0612658a-7d1b-4178-bdc2-42b914ea31a7
author: MladjoA
ms.author: mlandzic
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Spatial Reference Identifiers (SRIDs)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  Each spatial instance has a spatial reference identifier (SRID). The SRID corresponds to a spatial reference system based on the specific ellipsoid used for either flat-earth mapping or round-earth mapping.  
  
 A spatial column can contain objects with different SRIDs. However, only spatial instances with the same SRID can be used when performing operations with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] spatial data methods on your data. The result of any spatial method derived from two spatial data instances is valid only if those instances have the same SRID that is based on the same unit of measurement, datum, and projection used to determine the coordinates of the instances. The most common units of measurement of a SRID are meters or square meters.  
  
 If two spatial instances do not have the same SRID, the results from a **geometry** or **geography** Data Type method used on the instances will return NULL. For example, for the following predicate term to return a non-NULL result, the two **geometry** instances, `geometry1` and `geometry2`, must have the same SRID:  
  
 `geometry1.STIntersects(geometry2) = 1`  
  
> [!NOTE]  
>  The spatial reference identification system is defined by the [European Petroleum Survey Group (EPSG)](https://epsg.org/home.html) standard, which is a set of standards developed for cartography, surveying, and geodetic data storage. This standard is owned by the Oil and Gas Producers (OGP) Surveying and Positioning Committee.  
  
## See Also  
 [Spatial Data Types Overview](../../relational-databases/spatial/spatial-data-types-overview.md)  
  
  
