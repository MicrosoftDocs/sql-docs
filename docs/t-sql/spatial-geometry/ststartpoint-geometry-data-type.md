---
description: "STStartPoint (geometry Data Type)"
title: "STStartPoint (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STStartPoint_TSQL"
  - "STStartPoint (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STStartPoint (geometry Data Type)"
ms.assetid: 049917db-3f76-4053-8cd2-bc54158e89bc
author: MladjoA
ms.author: mlandzic 
---
# STStartPoint (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the start point of a **geometry** instance.
  
## Syntax  
  
```  
  
.STStartPoint ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
 Open Geospatial Consortium (OGC) type: **Point**  
  
## Remarks  
 `STStartPoint()` is the equivalent of [STPointN](../../t-sql/spatial-geometry/stpointn-geometry-data-type.md) (1).  
  
## Examples  
 The following example creates a `LineString` instance and uses `STStartPoint()` to retrieve the start point of the instance.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 2, 1 0)', 0);  
SELECT @g.STStartPoint().ToString();  
```  
  
## See Also  
 [STPointN &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stpointn-geometry-data-type.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

