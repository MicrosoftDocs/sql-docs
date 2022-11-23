---
description: "STCurveToLine (geometry Data Type)"
title: "STCurveToLine (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STCurveToLine method (geometry)"
ms.assetid: abc80b32-4152-4e10-b816-798b901e0ac5
author: MladjoA
ms.author: mlandzic 
---
# STCurveToLine (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a polygonal approximation of a **geometry** instance that contains circular arc segments.
  
## Syntax  
  
```  
  
.STCurveToLine ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 Returns an empty **GeometryCollection**instance for empty **geometry** instance variables, and returns **NULL** for uninitialized **geometry** variables.  
  
 The polygonal approximation that the method returns depends on the **geometry** instance that you use to call the method:  
  
-   Returns a **LineString** instance for a **CircularString** or **CompoundCurve** instance.  
  
-   Returns a **Polygon** instance for a **CurvePolygon** instance.  
  
-   Returns a copy of the **geometry** instance if that instance is not a **CircularString**, **CompoundCurve**, or **CurvePolygon** instance. For example, the `STCurveToLine` method returns a **Point** instance for a **geometry** instance that is a **Point** instance.  
  
 Unlike the SQL/MM specification, the `STCurveToLine` method does not use z-coordinate values to calculate the polygonal approximation. The method ignores any z-coordinate values present in the calling **geometry** instance.  
  
## Examples  
  
### A. Using an Uninitialized Geometry Variable and Empty Instance  
 In the following example, the first **SELECT** statement uses an uninitialized **geometry** instance to call the `STCurveToLine` method, and the second **SELECT** statement uses an empty **geometry** instance. Thus, the method returns **NULL** to the first statement and a **GeometryCollection** collection to the second statement.  
  
```sql
 DECLARE @g geometry; 
 SET @g = @g.STCurveToLine(); 
 SELECT @g.STGeometryType(); 
 SET @g = geometry::Parse('LINESTRING EMPTY'); 
 SELECT @g.STGeometryType();
 ```  
  
### B. Using a LineString Instance  
 The **SELECT** statement in the following example uses a **LineString** instance to call the STCurveToLine method. Thus, the method returns a **LineString** instance.  
  
```sql
 DECLARE @g geometry; 
 SET @g = geometry::Parse('LINESTRING(1 3, 5 5, 4 3, 1 3)'); 
 SET @g = @g.STCurveToLine(); 
 SELECT @g.STGeometryType();
 ```  
  
### C. Using a CircularString Instance  
 The first **SELECT** statement in the following example uses a **CircularString** instance to call the STCurveToLine method. Thus, the method returns a **LineString** instance. This **SELECT** statement also compares the lengths of the two instances, which are approximately the same.  Finally, the second **SELECT** statement returns the number of points for each instance.  It returns only 5 points for the **CircularString** instance, but 65 points for the **LineString**instance.  
  
```sql
 DECLARE @g1 geometry, @g2 geometry; 
 SET @g1 = geometry::Parse('CIRCULARSTRING(10 0, 0 10, -10 0, 0 -10, 10 0)'); 
 SET @g2 = @g1.STCurveToLine(); 
 SELECT @g1.STGeometryType() AS [G1 Type], @g2.STGeometryType() AS [G2 Type], @g1.STLength() AS [G1 Perimeter], @g2.STLength() AS [G2 Perimeter], @g2.ToString() AS [G2 Def]; 
 SELECT @g1.STNumPoints(), @g2.STNumPoints();
 ```  
  
### D. Using a CurvePolygon Instance  
 The **SELECT** statement in the following example uses a **CurvePolygon** instance to call the STCurveToLine method. Thus, the method returns a **Polygon** instance.  
  
```sql
 DECLARE @g1 geometry, @g2 geometry; 
 SET @g1 = geometry::Parse('CURVEPOLYGON(CIRCULARSTRING(10 0, 0 10, -10 0, 0 -10, 10 0))'); 
 SET @g2 = @g1.STCurveToLine(); 
 SELECT @g1.STGeometryType() AS [G1 Type], @g2.STGeometryType() AS [G2 Type];
 ```  
  
## See Also  
 [Spatial Data Types Overview](../../relational-databases/spatial/spatial-data-types-overview.md)   
 [STLength &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stlength-geometry-data-type.md)   
 [STNumPoints &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stnumpoints-geometry-data-type.md)   
 [STGeometryType &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stgeometrytype-geometry-data-type.md)  
  
  

