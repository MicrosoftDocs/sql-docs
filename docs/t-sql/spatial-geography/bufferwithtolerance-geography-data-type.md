---
title: "BufferWithTolerance (geography Data Type)"
description: "BufferWithTolerance (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
f1_keywords:
  - "BufferWithTolerance_TSQL"
  - "BufferWithTolerance"
helpviewer_keywords:
  - "BufferWithTolerance method"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# BufferWithTolerance (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns a geometric object representing the union of all point values whose distance from a **geography** instance is less than or equal to a specified value, allowing for a specified tolerance.  
  
This geography data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.  
  
## Syntax  
  
```  
  
.BufferWithTolerance ( distance, tolerance, relative )  
```  
  
## Arguments
_distance_  
Is a **float** expression specifying the distance from the **geography** instance around which to calculate the buffer.  
  
The maximum distance of the buffer can't exceed 0.999 \* _π_  * minorAxis \* minorAxis / majorAxis (~0.999 \* 1/2 Earth's circumference) or the full globe.  
  
_tolerance_  
Is a **float** expression specifying the tolerance of the buffer distance.  
  
The maximum variation in the ideal buffer distance for the returned linear approximation is the _tolerance_ value.  
  
For example, the ideal buffer distance of a point is a circle, but this distance must be approximated by a polygon. The smaller the tolerance, the more points the polygon will have. This result increases the complexity of the result, but minimizes the error.  
  
The minimum limit is 0.1 percent of the distance, and any tolerance less than that will be rounded up to the minimum limit.  
  
_relative_  
Is a **bit** specifying whether the _tolerance_ value is relative or absolute. If the value is 'TRUE' or 1, tolerance is relative. This value is the product of the _tolerance_ parameter and the angular extent \* equatorial radius of the ellipsoid. The tolerance is absolute if the value is 'FALSE' or 0. This _tolerance_ value is the absolute maximum variation in the ideal buffer distance for the returned linear approximation.  
  
## Return Types  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
CLR return type: **SqlGeography**  
  
## Remarks  
This method throws an **ArgumentException** if the _distance_ isn't a number (NAN), or if _distance_ is positive or negative infinity.  This method also throws an **ArgumentException** if _tolerance_ is zero (0), not a number (NaN), negative, or positive or negative infinity.  
  
`STBuffer()` will return a **FullGlobe** instance in certain cases; for example, `STBuffer()` returns a **FullGlobe** instance on two poles when the buffer distance is greater than the distance from the equator to the poles.  
  
This method will throw an **ArgumentException** in **FullGlobe** instances where the distance of the buffer exceeds the following limitation:  
  
0.999 \* _π_ * minorAxis \* minorAxis / majorAxis (~0.999 \* 1/2 Earth's circumference)  
  
The error between the theoretical and computed buffer is max(tolerance, extents \* 1.E-7) where tolerance is the value of the _tolerance_ parameter. For more information on extents, see [geography Data Type Method Reference](./stequals-geography-data-type.md).  
  
This method isn't precise.  
  
## Examples  
The following example creates a `Point` instance and uses `BufferWithTolerance()` to obtain a rough buffer around it.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('POINT(-122.34900 47.65100)', 4326);  
SELECT @g.BufferWithTolerance(1, .5, 0).ToString();  
```  
  
## See Also  
[STBuffer &#40;geography Data Type&#41;](../../t-sql/spatial-geography/stbuffer-geography-data-type.md)   
[Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)  
  
