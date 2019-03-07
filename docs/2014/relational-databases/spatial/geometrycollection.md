---
title: "GeometryCollection | Microsoft Docs"
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "GeomCollection geometry subtype [SQL Server]"
  - "geometry subtypes [SQL Server]"
ms.assetid: 4445c0d9-a66b-4d7c-88e4-a66fa6f7d9fd
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# GeometryCollection
  A `GeometryCollection` is a collection of zero or more `geometry` or `geography` instances. A `GeometryCollection` can be empty.  
  
## GeometryCollection Instances  
  
### Accepted Instances  
 For a `GeometryCollection` instance to be accepted, it must either be an empty `GeometryCollection` instance or all the instances comprising the `GeometryCollection` instance must be accepted instances. The following example shows accepted instances.  
  
```  
DECLARE @g1 geometry = 'GEOMETRYCOLLECTION EMPTY';  
DECLARE @g2 geometry = 'GEOMETRYCOLLECTION(LINESTRING EMPTY,POLYGON((-1 -1, -1 -5, -5 -5, -5 -1, -1 -1)))';  
DECLARE @g3 geometry = 'GEOMETRYCOLLECTION(LINESTRING(1 1, 3 5),POLYGON((-1 -1, -1 -5, -5 -5, -5 -1, -1 -1)))';  
```  
  
 The following example throws a `System.FormatException` because the `LinesString` instance in the `GeometryCollection` instance is not accepted.  
  
```  
DECLARE @g geometry = 'GEOMETRYCOLLECTION(LINESTRING(1 1), POLYGON((-1 -1, -1 -5, -5 -5, -5 -1, -1 -1)))';  
```  
  
### Valid Instances  
 A `GeometryCollection` instance is valid when all instances that comprise the `GeometryCollection` instance are valid. The following shows three valid `GeometryCollection` instances and one instance that is not valid.  
  
```  
DECLARE @g1 geometry = 'GEOMETRYCOLLECTION EMPTY';  
DECLARE @g2 geometry = 'GEOMETRYCOLLECTION(LINESTRING EMPTY,POLYGON((-1 -1, -1 -5, -5 -5, -5 -1, -1 -1)))';  
DECLARE @g3 geometry = 'GEOMETRYCOLLECTION(LINESTRING(1 1, 3 5),POLYGON((-1 -1, -1 -5, -5 -5, -5 -1, -1 -1)))';  
DECLARE @g4 geometry = 'GEOMETRYCOLLECTION(LINESTRING(1 1, 3 5),POLYGON((-1 -1, 1 -5, -5 5, -5 -1, -1 -1)))';  
SELECT @g1.STIsValid(), @g2.STIsValid(), @g3.STIsValid(), @g4.STIsValid();  
```  
  
 `@g4` is not valid because the `Polygon` instance in the `GeometryCollection` instance is not valid.  
  
 For more information on accepted and valid instances, see [Point](point.md), [MultiPoint](multipoint.md), [LineString](linestring.md), [MultiLineString](multilinestring.md), [Polygon](polygon.md), and [MultiPolygon](multipolygon.md).  
  
## Examples  
 The following example instantiates a `geometry``GeometryCollection` with Z values in SRID 1 containing a `Point` instance and a `Polygon` instance.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomCollFromText('GEOMETRYCOLLECTION(POINT(3 3 1), POLYGON((0 0 2, 1 10 3, 1 0 4, 0 0 2)))', 1);  
```  
  
## See Also  
 [Spatial Data &#40;SQL Server&#41;](spatial-data-sql-server.md)  
  
  
