---
description: "STIsSimple (geometry Data Type)"
title: "STIsSimple (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STIsSimple_TSQL"
  - "STIsSimple (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STIsSimple (geometry Data Type)"
ms.assetid: da8f45d4-4f9c-405d-b883-760eb5344a71
author: MladjoA
ms.author: mlandzic 
---
# STIsSimple (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns 1 if a **geometry** instance is simple, as defined by the Open Geospatial Consortium (OGC). Returns 0 if a **geometry** instance is not simple.
  
## Syntax  
  
```  
  
.STIsSimple ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 To be simple, a **geometry** instance must meet all of the following requirements:  
  
-   Each figure of the instance must not intersect itself, except at its endpoints.  
  
-   No two figures of the instance can intersect each other at a point that is not in both of their boundaries.  
  
## Examples  
 The following example creates a nonsimple `LineString` instance that intersects itself and uses `STIsSimple()` to test whether the `LineString` is simple.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 2, 0 2, 2 0)', 0);  
SELECT @g.STIsSimple();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

