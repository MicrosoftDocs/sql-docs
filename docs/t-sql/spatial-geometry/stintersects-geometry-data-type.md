---
description: "STIntersects (geometry Data Type)"
title: "STIntersects (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STIntersects (geometry Data Type)"
  - "STIntersects_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STIntersects (geometry Data Type)"
ms.assetid: 7c18f5be-5a29-422e-8ca7-d8a5f38e03f5
author: MladjoA
ms.author: mlandzic 
---
# STIntersects (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns 1 if a **geometry** instance intersects another **geometry** instance. Returns 0 if it does not.
  
## Syntax  
  
```  
  
.STIntersects ( other_geometry )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *other_geometry*  
 Is another **geometry** instance to compare against the instance on which `STIntersects()` is invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 This method always returns null if the spatial reference IDs (SRIDs) of the **geometry** instances do not match.  
  
## Examples  
 The following example uses `STIntersects()` to determine if two `geometry` instances intersect each other.  
  
```  
DECLARE @g geometry;  
DECLARE @h geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 2, 2 0, 4 2)', 0);  
SET @h = geometry::STGeomFromText('POINT(1 1)', 0);  
SELECT @g.STIntersects(@h);  
```  
  
## See Also  
 [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

