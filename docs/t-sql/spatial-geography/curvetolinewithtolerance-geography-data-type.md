---
title: "CurveToLineWithTolerance (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CurveToLineWithTolerance_TSQL"
  - "CurveToLineWithTolerance"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CurveToLineWithTolerance method (geography)"
ms.assetid: 74369c76-2cf6-42ae-b9cc-e7a051db2767
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# CurveToLineWithTolerance (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns a polygonal approximation of a **geography** instance that contains circular arc segments.  
  
## Syntax  
  
```  
  
.CurveToLineWithTolerance( tolerance, relative )  
```  
  
## Arguments  
 *tolerance*  
 Is a **double** expression that defines the maximum error between the original circular arc segment and its linear approximation.  
  
 *relative*  
 Is a **bool** expression indicating whether to use a relative maximum for the deviation. When relative is set to false (0), then an absolute maximum is set for the deviation that a linear approximate can have.  When relative is set to true (1) then the tolerance is calculated as a product of the tolerance parameter and the diameter of the bounding box for the spatial object.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Exceptions  
 Setting tolerance <= 0 throws an **ArgumentOutOfRange** exception.  
  
## Remarks  
 This method allows for an error tolerance amount to be specified for the resultant **LineString**.  
  
 **CurveToLineWithTolerance** method will return a **LineString** instance for a **CircularString** or **CompoundCurve** instance and **Polygon** instance for a **CurvePolygon** instance.  
  
## Examples  
  
### A. Using different tolerance values on a CircularString instance  
 The following example shows how setting the tolerance affects the `LineString`instance returned from a `CircularString` instance:  
  
 ```
 DECLARE @g geography;  
 SET @g = geography::Parse('CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)');  
 SELECT @g.CurveToLineWithTolerance(0.1,0).STNumPoints(), @g.CurveToLineWithTolerance(0.01, 0).STNumPoints();
```  
  
### B. Using the method on a MultiLineString instance containing one LineString  
 The following example shows what is returned from a `MultiLineString` instance that only contains one `LineString` instance:  
  
 ```
 DECLARE @g geography;  
 SET @g = geography::Parse('MULTILINESTRING((-122.358 47.653, -122.348 47.649))');  
 SELECT @g.CurveToLineWithTolerance(0.1,0).ToString();
 ```  
  
### C. Using the method on a MultiLineString instance containing multiple LineStrings  
 The following example shows what is returned from a `MultiLineString` instance that contains more than one `LineString` instance:  
  
 ```
 DECLARE @g geography;  
 SET @g = geography::Parse('MULTILINESTRING((-122.358 47.653, -122.348 47.649),(-123.358 47.653, -123.348 47.649))');  
 SELECT @g.CurveToLineWithTolerance(0.1,0).ToString();
 ```  
  
### D. Setting relative to true for an invoking CurvePolygon instance  
 The following example uses a `CurvePolygon` instance to call `CurveToLineWithTolerance()` with *relative* set to true:  
  
 ```
 DECLARE @g geography = 'CURVEPOLYGON(COMPOUNDCURVE(CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658), (-122.348 47.658, -122.358 47.658, -122.358 47.653)))';  
 SELECT @g.CurveToLineWithTolerance(.5,1).ToString();
 ```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)  
  
  
