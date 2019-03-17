---
title: "CurveToLineWithTolerance (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CurveToLineWithTolerance method (geometry)"
ms.assetid: 96871075-1998-4cd9-86b1-3fc55577aee4
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# CurveToLineWithTolerance (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

Returns a polygonal approximation of a **geometry** instance that contains circular arc segments.
  
## Syntax  
  
```  
  
.CurveToLineWithTolerance ( tolerance, relative )  
```  
  
## Arguments  
 *tolerance*  
 Is a **double** expression that defines the maximum error between the original circular arc segment and its linear approximation.  
  
 *relative*  
 Is a **bool** expression that indicates whether to use a relative maximum for the deviation. When relative is set to false (0), then an absolute maximum is set for the deviation that a linear approximate can have. When relative is set to true (1) then the tolerance is calculated as a product of the tolerance parameter and the diameter of the bounding box for the spatial object.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Exceptions  
 Setting tolerance <= 0 throws an `ArgumentOutOfRange` exception.  
  
## Remarks  
 This method can specify an error tolerance amount for the resultant **LineString**.  
  
 The following table shows the instance type returned by `CurveToLineWithTolerance()`for various types.  
  
|Invoking Instance Type|Spatial Type Returned|  
|----------------------------|---------------------------|  
|Empty geometry instance|Empty **GeometryCollection** instance|  
|**Point** and **MultiPoint**|**Point** instance|  
|**MultiPoint**|**Point** or **MultiPoint** instance|  
|**CircularString**, **CompoundCurve**, or **LineString**|**LineString** instance|  
|**MultiLineString**|**LineString** or **MultiLineString** instance|  
|**CurvePolygon** and **Polygon**|**Polygon** instance|  
|**MultiPolygon**|**Polygon** or **MultiPolygon** instance|  
|**GeometryCollection** with a single instance that does not contain a circular arc segment|The instance that is contained in the **GeometryCollection** determines the type of instance returned.|  
|**GeometryCollection** with a single one-dimensional circular arc segment instance (**CircularString**, **CompoundCurve**)|**LineString** instance|  
|**GeometryCollection** with a single two-dimensional circular arc segment instance (**CurvePolygon**)|**Polygon** instance|  
|**GeometryCollection** with multiple one dimensional instances|**MultiLineString** instance|  
|**GeometryCollection** with multiple two dimensional instances|**MultiPolygon** instance|  
|**GeometryCollection** with multiple instances of different dimensions|**GeometryCollection** instance|  
  
## Examples  
  
### A. Using different tolerance values on a CircularString instance  
 The following example shows how setting the tolerance affects the `LineString`instance returned from a `CircularString` instance:  
  
```
 DECLARE @g geometry; 
 SET @g = geometry::Parse('CIRCULARSTRING(0 0, 1 2.1082, 3 6.3246, 0 7, -3 6.3246, -1 2.1082, 0 0)'); 
 SELECT @g.CurveToLineWithTolerance(0.1,0).STNumPoints(), @g.CurveToLineWithTolerance(0.01, 0).STNumPoints();
 ```  
  
### B. Using the method on a MultiLineString instance containing one LineString  
 The following example shows what is returned from a `MultiLineString` instance that only contains one `LineString` instance:  
  
```
 DECLARE @g geometry; 
 SET @g = geometry::Parse('MULTILINESTRING((1 3, 4 8, 6 9))'); 
 SELECT @g.CurveToLineWithTolerance(0.1,0).ToString();
 ```  
  
### C. Using the method on a MultiLineString instance containing multiple LineStrings  
 The following example shows what is returned from a `MultiLineString` instance that contains more than one `LineString` instance:  
  
```
 DECLARE @g geometry; 
 SET @g = geometry::Parse('MULTILINESTRING((1 3, 4 8, 6 9),(4 4, 9 18))'); 
 SELECT @g.CurveToLineWithTolerance(0.1,0).ToString();
 ```  
  
### D. Setting relative to true for an invoking CurvePolygon instance  
 The following example uses a `CurvePolygon` instance to call `CurveToLineWithTolerance()` with *relative* set to true:  
  
```
 DECLARE @g geometry = 'CURVEPOLYGON(COMPOUNDCURVE(CIRCULARSTRING(0 4, 4 0, 8 4), (8 4, 0 4)))'; 
 SELECT @g.CurveToLineWithTolerance(.5,1).ToString();
 ```  
  
### E. Using the method on a GeometryCollection instance  
 The following example calls `CurveToLineWithTolerance()` on a `GeometryCollection` that contains a two-dimensional `CurvePolygon` instance and a one-dimensional `CircularString` instance. `CurveToLineWithTolerance()` converts both circular arc segment types to line segment types and returns them in a `GeometryCollection` type.  
  
```
 DECLARE @g geometry; 
 SET @g = geometry::Parse('GEOMETRYCOLLECTION(CURVEPOLYGON( COMPOUNDCURVE(CIRCULARSTRING(0 2, 2 0, 4 2), (4 2, 0 2))), CIRCULARSTRING(4 4, 8 6, 9 5))'); 
 SELECT @g.CurveToLineWithTolerance(0.1,0).STNumPoints(), @g.CurveToLineWithTolerance(0.1, 0).ToString();
 ```  
  
## See Also  
 [CurveToLineWithTolerance &#40;geography Data Type&#41;](../../t-sql/spatial-geography/curvetolinewithtolerance-geography-data-type.md)   
 [STCurveToLine &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stcurvetoline-geometry-data-type.md)  
  
  

