---
description: "STPointOnSurface (geometry Data Type)"
title: "STPointOnSurface (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STPointOnSurface (geometry Data Type)"
  - "STPointOnSurface_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STPointOnSurface (geometry Data Type)"
ms.assetid: 23b2b8eb-4176-49fb-ace0-92398928d60e
author: MladjoA
ms.author: mlandzic 
---
# STPointOnSurface (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns an arbitrary point located within the interior of a **geometry** instance.
  
## Syntax  
  
```  
  
.STPointOnSurface ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
 Open Geospatial Consortium (OGC) type: **Point**  
  
## Remarks  
 This method returns null if the instance is empty.  
  
## Examples  
 The following example creates a `Polygon` instance and uses `STPointOnSurface()` to find a point on the instance.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 3 0, 3 3, 0 3, 0 0),(2 2, 2 1, 1 1, 1 2, 2 2))', 0);  
SELECT @g.STPointOnSurface().ToString();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

