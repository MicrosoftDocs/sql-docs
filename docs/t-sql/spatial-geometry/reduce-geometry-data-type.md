---
title: "Reduce (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "Reduce_TSQL"
  - "Reduce"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Reduce method"
ms.assetid: 132184bf-c4d2-4a27-900d-8373445dce2a
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Reduce (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns an approximation of the given **geometry** instance produced by running an extension of the Douglas-Peucker algorithm on the instance with the given tolerance.
  
## Syntax  
  
```  
  
.Reduce ( tolerance )  
```  
  
## Arguments  
 *tolerance*  
 Is a value of type **float**. *tolerance* is the tolerance to input for the approximation algorithm.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 For collection types, this algorithm operates independently on each **geometry** contained in the instance.  
  
 This algorithm does not modify **Point** instances.  
  
 On **LineString**, **CircularString**, and **CompoundCurve** instances, the approximation algorithm retains the original start and end points of the instance, and iteratively adds back the point from the original instance that most deviates from the result until no point deviates more than the given tolerance.  
  
 `Reduce()` returns a **LineString**, **CircularString**, or **CompoundCurve** instance for **CircularString** instances.  `Reduce()` returns either a **CompoundCurve** or **LineString** instance for **CompoundCurve** instances.  
  
 On **Polygon** instances, the approximation algorithm is applied independently to each ring. The method will produce a `FormatException` if the returned **Polygon** instance is not valid; for example, a non-valid **MultiPolygon** instance is created if `Reduce()` is applied to simplify each ring in the instance and the resulting rings overlap.  On **CurvePolygon** instances with a exterior ring and no interior rings, `Reduce()` returns a **CurvePolygon**, **LineString**, or **Point** instance.  If the **CurvePolygon** has interior rings then a **CurvePolygon** or **MultiPoint** instance is returned.  
  
 When a circular arc segment is encountered the approximation algorithm checks whether the arc can be approximated by its chord within half the given tolerance.  If the chord meets this criteria, then the circular arc is replaced in the calculations by the chord. If it does not meet this criteria, then the circular arc is retained and the approximation algorithm is applied to the remaining segments.  
  
## Examples  
  
### A. Using Reduce() to simplify a LineString  
 The following example creates a `LineString` instance and uses `Reduce()` to simplify the instance.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 0 1, 1 0, 2 1, 3 0, 4 1)', 0);  
SELECT @g.Reduce(.75).ToString();  
```  
  
### B. Using Reduce() with varying tolerance levels on a CircularString  
 The following example uses `Reduce()` with three tolerance levels on a **CircularString** instance:  
  
```
 DECLARE @g geometry = 'CIRCULARSTRING(0 0, 8 8, 16 0, 20 -4, 24 0)'; 
 SELECT @g.Reduce(5).ToString(); 
 SELECT @g.Reduce(15).ToString(); 
 SELECT @g.Reduce(16).ToString();
 ```  
  
 This example produces the following output:  
  
 ```
 CIRCULARSTRING (0 0, 8 8, 16 0, 20 -4, 24 0) 
 COMPOUNDCURVE (CIRCULARSTRING (0 0, 8 8, 16 0), (16 0, 24 0)) 
 LINESTRING (0 0, 24 0)
 ```  
  
 Each of the instances returned contain the endpoints (0 0) and (24 0).  
  
### C. Using Reduce() with varying tolerance levels on a CompoundCurve  
 The following example uses `Reduce()` with two tolerance levels on a **CompoundCurve** instance:  
  
```
 DECLARE @g geometry = 'COMPOUNDCURVE(CIRCULARSTRING(0 0, 8 8, 16 0, 20 -4, 24 0),(24 0, 20 4, 16 0))';  
 SELECT @g.Reduce(15).ToString();  
 SELECT @g.Reduce(16).ToString();
 ```  
  
 In this example notice that the second **SELECT** statement returns the **LineString** instance: `LineString(0 0, 16 0)`.  
  
### Showing an example where the original start and end points are lost  
 The following example shows how the original start and endpoints may not be retained by the resulting intstance. This occurs because retaining the original start and end points would result in an invalid **LineString** instance.  
  
```  
DECLARE @g geometry = 'LINESTRING(0 0, 4 0, 2 .01, 1 0)';  
DECLARE @h geometry = @g.Reduce(1);  
SELECT @g.STIsValid() AS Valid  
SELECT @g.ToString() AS Original, @h.ToString() AS Reduced;  
```  
  
## See Also  
 [Extended Static Geometry Methods](../../t-sql/spatial-geometry/extended-static-geometry-methods.md)  
  
