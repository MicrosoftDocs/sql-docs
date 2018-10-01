---
title: "Filter (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "Filter"
  - "Filter_TSQL"
  - "Filter (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Filter method"
ms.assetid: 3d629a39-157e-4159-a3ca-a3c2e0ed4160
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Filter (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

A method that offers a fast, index-only intersection method to determine if a **geometry** instance intersects another **geometry** instance, assuming an index is available.
  
Returns 1 if a **geometry** instance potentially intersects another **geometry** instance. This method may produce a false positive return, and the exact result may be plan-dependent. Returns an accurate 0 value (true negative return) if there is no intersection of **geometry** instances found.
  
In cases where an index is not available, or is not used, the method will return the same values as **STIntersects()** when called with the same parameters.
  
## Syntax  
  
```  
  
.Filter ( other_geometry )  
```  
  
## Arguments  
 *other_geometry*  
 Is another **geometry** instance to compare against the instance on which Filter() is invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 This method is not deterministic and is not precise.  
  
## Examples  
 The following example uses `Filter()` to determine if two `geometry` instances intersect each other.  
  
```  
CREATE TABLE sample (id int primary key, g geometry);  
GO  
INSERT INTO sample VALUES  
   (0, geometry::Point(0, 0, 0)),  
   (1, geometry::Point(0, 1, 0)),  
   (2, geometry::Point(0, 2, 0)),  
   (3, geometry::Point(0, 3, 0)),  
   (4, geometry::Point(0, 4, 0));  
  
CREATE SPATIAL INDEX sample_idx ON sample(g)  
WITH (bounding_box = (-8000, -8000, 8000, 8000));  
GO  
SELECT id  
FROM sample   
WHERE g.Filter(geometry::Parse('POLYGON((-1 -1, 1 -1, 1 1, -1 1, -1 -1))')) = 1;  
```  
  
## See Also  
 [Extended Methods on Geometry Instances](../../t-sql/spatial-geometry/extended-methods-on-geometry-instances.md)   
 [STIntersects &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stintersects-geometry-data-type.md)  
  
  

