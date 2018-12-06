---
title: "MultiPoint | Microsoft Docs"
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "MultiPoint geometry subtype [SQL Server]"
ms.assetid: 2aaab211-3aba-4dbd-90b7-095d997b1f62
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# MultiPoint
  A `MultiPoint` is a collection of zero or more points. The boundary of a `MultiPoint` instance is empty.  
  
## Examples  
 The following example creates a `geometry MultiPoint` instance with SRID 23 and two points: one point with the coordinates (2, 3), one point with the coordinates (7, 8), and a Z value of 9.5.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('MULTIPOINT((2 3), (7 8 9.5))', 23);  
```  
  
 This `MultiPoint` instance can also be expressed using `STMPointFromText()` as shown below.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STMPointFromText('MULTIPOINT((2 3), (7 8 9.5))', 23);  
```  
  
 The following example uses the method `STGeometryN()` to retrieve a description of the first point in the collection.  
  
```  
SELECT @g.STGeometryN(1).STAsText();  
```  
  
## See Also  
 [Point](point.md)   
 [Spatial Data &#40;SQL Server&#41;](spatial-data-sql-server.md)  
  
  
