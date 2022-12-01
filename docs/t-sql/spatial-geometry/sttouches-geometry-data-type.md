---
description: "STTouches (geometry Data Type)"
title: "STTouches (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STTouches (geometry Data Type)"
  - "STTouches_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STTouches (geometry Data Type)"
ms.assetid: af3650b4-26da-4600-9cc2-1be71dd76a14
author: MladjoA
ms.author: mlandzic 
---
# STTouches (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns 1 if a **geometry** instance spatially touches another **geometry** instance. Returns 0 if it does not.
  
## Syntax  
  
```  
  
.STTouches ( other_geometry )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *other_geometry*  
 Is another **geometry** instance to compare against the instance on which `STTouches()` is invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 Two **geometry** instances touch if their point sets intersect, but their interiors do not intersect.  
  
 This method always returns null if the spatial reference IDs (SRIDs) of the **geometry** instances do not match.  
  
## Examples  
 The following example uses `STTouches()` to test two `geometry` instances to see if they touch.  
  
```sql
DECLARE @g geometry;  
DECLARE @h geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 2, 2 0, 4 2)', 0);  
SET @h = geometry::STGeomFromText('POINT(1 1)', 0);  
SELECT @g.STTouches(@h);  
```  
  
## See Also  
 [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

