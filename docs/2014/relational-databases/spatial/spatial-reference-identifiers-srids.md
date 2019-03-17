---
title: "Spatial Reference Identifiers (SRIDs) | Microsoft Docs"
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "Spatial Reference Identifiers (SRIDs)"
  - "geodetic spatial data [SQL Server], identifiers"
  - "SRID"
ms.assetid: 0612658a-7d1b-4178-bdc2-42b914ea31a7
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Spatial Reference Identifiers (SRIDs)
  Each spatial instance has a spatial reference identifier (SRID). The SRID corresponds to a spatial reference system based on the specific ellipsoid used for either flat-earth mapping or round-earth mapping.  
  
> [!IMPORTANT]  
>  For a detailed description and examples of spatial features introduced in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], including a new SRID, download the white paper, [New Spatial Features in SQL Server 2012](https://go.microsoft.com/fwlink/?LinkId=226407).  
  
 A spatial column can contain objects with different SRIDs. However, only spatial instances with the same SRID can be used when performing operations with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] spatial data methods on your data. The result of any spatial method derived from two spatial data instances is valid only if those instances have the same SRID that is based on the same unit of measurement, datum, and projection used to determine the coordinates of the instances. The most common units of measurement of a SRID are meters or square meters.  
  
 If two spatial instances do not have the same SRID, the results from a `geometry` or `geography` Data Type method used on the instances will return NULL. For example, for the following predicate term to return a non-NULL result, the two `geometry` instances, `geometry1` and `geometry2`, must have the same SRID:  
  
 `geometry1.STIntersects(geometry2) = 1`  
  
> [!NOTE]  
>  The spatial reference identification system is defined by the [European Petroleum Survey Group (EPSG)](https://go.microsoft.com/fwlink/?LinkId=99349) standard, which is a set of standards developed for cartography, surveying, and geodetic data storage. This standard is owned by the Oil and Gas Producers (OGP) Surveying and Positioning Committee.  
  
## See Also  
 [Spatial Data Types Overview](spatial-data-types-overview.md)  
  
  
