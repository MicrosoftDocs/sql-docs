---
title: "BufferWithTolerance (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "BufferWithTolerance_TSQL"
  - "BufferWithTolerance"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "BefferWithTolerance method"
ms.assetid: f1783e6b-0f17-464f-b1c7-1c3f7d8aa042
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# BufferWithTolerance (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns a geometric object representing the union of all point values whose distance from a **geography** instance is less than or equal to a specified value, allowing for a specified tolerance.  
  
 This geography data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.  
  
## Syntax  
  
```  
  
.BufferWithTolerance ( distance, tolerance, relative )  
```  
  
## Arguments  
 *distance*  
 Is a **float** expression specifying the distance from the **geography** instance around which to calculate the buffer.  
  
 The maximum distance of the buffer cannot exceed 0.999 \* *π*  * minorAxis \* minorAxis / majorAxis (~0.999 \* 1/2 Earth's circumference) or the full globe.  
  
 *tolerance*  
 Is a **float** expression specifying the tolerance of the buffer distance.  
  
 The *tolerance* value refers to the maximum variation in the ideal buffer distance for the returned linear approximation.  
  
 For example, the ideal buffer distance of a point is a circle, but this must be approximated by a polygon. The smaller the tolerance, the more points the polygon will have, which increases the complexity of the result, but decreases the error.  
  
 The minimum limit is 0.1 percent of the distance, and any tolerance less than that will be rounded up to the minimum limit.  
  
 *relative*  
 Is a **bit** specifying whether the *tolerance* value is relative or absolute. If 'TRUE' or 1, tolerance is relative and is calculated as the product of the *tolerance* parameter and the angular extent \* equatorial radius of the ellipsoid. If 'FALSE' or 0, tolerance is absolute and the *tolerance* value is the absolute maximum variation in the ideal buffer distance for the returned linear approximation.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 This method will throw an **ArgumentException** if the *distance* is not a number (NAN), or if *distance* is positive or negative infinity.  This method will also throw an **ArgumentException** if *tolerance* is zero (0), not a number (NaN), negative, or positive or negative infinity.  
  
 `STBuffer()` will return a **FullGlobe** instance in certain cases; for example, `STBuffer()` returns a **FullGlobe** instance on two poles when the buffer distance is greater than the distance from the equator to the poles.  
  
 This method will throw an **ArgumentException** in **FullGlobe** instances where the distance of the buffer exceeds the following limitation:  
  
 0.999 \* *π* * minorAxis \* minorAxis / majorAxis (~0.999 \* 1/2 Earth's circumference)  
  
 The error between the theoretical and computed buffer is max(tolerance, extents \* 1.E-7) where tolerance is the value of the *tolerance* parameter. For more information on extents, see [geography Data Type Method Reference](https://msdn.microsoft.com/library/028e6137-7128-4c74-90a7-f7bdd2d79f5e).  
  
 This method is not precise.  
  
## Examples  
 The following example creates a `Point` instance and uses `BufferWithTolerance()` to obtain a rough buffer around it.  
  
```  
DECLARE @g geography;  
SET @g = geography::STGeomFromText('POINT(-122.34900 47.65100)', 4326);  
SELECT @g.BufferWithTolerance(1, .5, 0).ToString();  
```  
  
## See Also  
 [STBuffer &#40;geography Data Type&#41;](../../t-sql/spatial-geography/stbuffer-geography-data-type.md)   
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)  
  
  
