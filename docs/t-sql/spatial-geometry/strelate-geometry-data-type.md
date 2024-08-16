---
title: "STRelate (geometry Data Type)"
description: "STRelate (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STRelate (geometry Data Type)"
  - "STRelate_TSQL"
helpviewer_keywords:
  - "STRelate (geometry Data Type)"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# STRelate (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns 1 if a **geometry** instance is related to another **geometry** instance, where the relationship is defined by a Dimensionally Extended 9 Intersection Model (DE-9IM) pattern matrix value; otherwise, returns 0.  
  
## Syntax  
  
```  
  
.STRelate ( other_geometry, intersection_pattern_matrix )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *other_geometry*  
 Is another **geometry** instance to compare against the instance on which `STRelate()` is invoked.  
  
 *intersection_pattern_matrix*  
 Is a string of type **nchar(9)** encoding acceptable values for the DE-9IM pattern matrix device between the two **geometry** instances.  
  
## Remarks  
 This method always returns null if the spatial reference IDs (SRIDs) of the **geometry** instances do not match. This method will throw an **ArgumentException** if the matrix isn't well formed.
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Examples  
 The following example uses `STRelate()` to test two **geometry** instances for spatial disjoint using an explicit DE-9IM pattern.  
  
```sql
DECLARE @g geometry;  
DECLARE @h geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 2, 2 0, 4 2)', 0);  
SET @h = geometry::STGeomFromText('POINT(5 5)', 0);  
SELECT @g.STRelate(@h, 'FF*FF****');  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  
