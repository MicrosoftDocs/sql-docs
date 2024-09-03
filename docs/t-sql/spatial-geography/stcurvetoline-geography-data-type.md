---
title: "STCurveToLine (geography Data Type)"
description: "STCurveToLine (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STCurveToLine_TSQL"
  - "STCurveToLine"
helpviewer_keywords:
  - "STCurveToLine method (geography)"
dev_langs:
  - "TSQL"
---
# STCurveToLine (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a polygonal approximation of a **geography** instance that contains circular arc segments.  
  
## Syntax  
  
```  
  
.STCurveToLine()  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 Returns a **LineString** instance for a **CircularString** or **CompoundCurve** instance.  
  
 Returns a **Polygon** instance for a **CurvePolygon** instance.  
  
 Return a copy of **geography** instances that do not contain **CircularString**, **CompoundCurve**, or **CurvePolygon** instances.  
  
 Unlike the SQL MM specification, this method does not use z-coordinate values in calculating the polygonal approximation. Any z-coordinate values present in the calling **geography** instance are ignored.  
  
## Examples  
 The following example returns a `LineString` instance that is a polygonal approximation of a `CircularString` instance:  
  
```sql
 DECLARE @g1 geography = 'CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)';  
 DECLARE @g2 geography;  
 SET @g2 = @g1.STCurveToLine();  
 SELECT @g1.STNumPoints() AS G1, @g2.STNumPoints() AS G2;
```  
  
## See Also  
 [STLength &#40;geography Data Type&#41;](../../t-sql/spatial-geography/stlength-geography-data-type.md)   
 [STNumPoints &#40;geography Data Type&#41;](../../t-sql/spatial-geography/stnumpoints-geography-data-type.md)   
 [Spatial Data Types Overview](../../relational-databases/spatial/spatial-data-types-overview.md)  
  
  
