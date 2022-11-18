---
description: "STDistance (geography Data Type)"
title: "STDistance (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "11/19/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STDistance_TSQL"
  - "STDistance (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STDistance method"
ms.assetid: 063d8722-e019-4d3d-8fcf-dbf5325823e7
author: MladjoA
ms.author: mlandzic 
---
# STDistance (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the shortest distance between a point in a **geography** instance and a point in another **geography** instance.  
  
> [!NOTE]  
>  `STDistance()` returns the shortest **LineString** between two geography types. This is a close approximate to the geodesic distance. The deviation of `STDistance()` on common earth models from the exact geodesic distance is no more than .25%. This avoids confusion over the subtle differences between length and distance in geodesic types.  
  
## Syntax  
  
```  
  
.STDistance ( other_geography )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *other_geography*  
 Is another **geography** instance from which to measure the distance between the instance on which STDistance() is invoked. If *other_geography* is an empty set, STDistance() returns null.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **float**  
  
 CLR return type: **SqlDouble**  
  
## Remarks  
 The result is expressed in the unit of measure defined by the [Spatial Reference Identifier &#40;SRID&#41;](../../relational-databases/spatial/spatial-reference-identifiers-srids.md) of spatial data.
STDistance() always returns *null* if the spatial reference IDs (SRIDs) of the **geography** instances do not match.  
  
> [!NOTE]  
>  Methods on the **geography** data type that calculate an area or distance will return different results based on the SRID of the instance used in the method. For more information about SRIDs, see [Spatial Reference Identifiers &#40;SRIDs&#41;](../../relational-databases/spatial/spatial-reference-identifiers-srids.md).  
  
## Examples  
 The following example finds the distance between two **geography** instances.  
  
```sql
DECLARE @g geography;  
DECLARE @h geography;  
SET @g = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);  
SET @h = geography::STGeomFromText('POINT(-122.34900 47.65100)', 4326);  
SELECT @g.STDistance(@h);  
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
