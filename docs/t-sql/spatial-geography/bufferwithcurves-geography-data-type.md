---
description: "BufferWithCurves (geography Data Type)"
title: "BufferWithCurves (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/11/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "BufferWithCurves"
  - "BufferWithCurves_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "BufferWithCurves method (geography)"
ms.assetid: abf0a11c-c99c-4faa-bf80-3ae8e04d7bfb
author: MladjoA
ms.author: mlandzic 
---
# BufferWithCurves (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a **geography** instance that represents the set of all points whose distance from the calling **geography** instance is less than or equal to the *distance* parameter.  
  
## Syntax  
  
```  
  
.BufferWithCurves ( distance )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *distance*  
 Is a **float** indicating the maximum distance that points forming the buffer can be from the geography instance.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Exceptions  
 The following criteria will throw an **ArgumentException**.  
  
-   No parameter is passed to the method such as `@g.BufferWithCurves()`  
  
-   A non-numeric parameter is passed to the method such as `@g.BufferWithCurves('a')`  
  
-   **NULL** is passed to the method, such as `@g.BufferWithCurves(NULL)`  
  
## Remarks  
 The following table shows the results returned for different distance values.  
  
|distance Value|Type Dimensions|Spatial Type Returned|  
|--------------------|---------------------|---------------------------|  
|distance < 0|Zero or One|Empty **GeometryCollection** instance|  
|distance \< 0|Two or More|A **CurvePolygon** or **GeometryCollection** instance with a negative buffer.<br /><br /> Note: A negative buffer may create an empty **GeometryCollection**|
|distance = 0|All dimensions|Copy of the invoking **geography** instance|  
|distance > 0|All dimensions|**CurvePolygon** or **GeometryCollection** instance|  
  
> [!NOTE]  
>  Since *distance* is a **float**, a very small value can equate to zero in the calculations.  When this occurs, then a copy of the calling **geography** instance is returned.  
  
 If a **string** parameter is passed to the method, then it will be converted to a **float** or it will throw an `ArgumentException`.  
  
## Examples  
  
### A. Calling BufferWithCurves() with a parameter value < 0 on one-dimensional geography instance  
 The following example returns an empty `GeometryCollection` instance:  
  
 ```sql
 DECLARE @g geography= 'LINESTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)';  
 SELECT @g.BufferWithCurves(-1).ToString();
``` 
  
### B. Calling BufferWithCurves() with a parameter value < 0 on a two-dimensional geography instance  
 The following example returns a `CurvePolygon` instance with a negative buffer:  
  
 ```sql
 DECLARE @g geography = 'CURVEPOLYGON(CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))';  
 SELECT @g.BufferWithCurves(-1).ToString()
 ```  
  
### C. Calling BufferWithCurves() with a parameter value < 0 that returns an empty GeometryCollection  
 The following example shows what occurs when the *distance* parameter equals -2:  
  
 ```sql
 DECLARE @g geography = 'CURVEPOLYGON(CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))';  
 SELECT @g.BufferWithCurves(-2).ToString();
 ```  
  
 This **SELECT** statement returns `GEOMETRYCOLLECTION EMPTY`  
  
### D. Calling BufferWithCurves() with a parameter value = 0  
 The following example returns a copy of the calling **geography** instance:  

 ```sql
 DECLARE @g geography = 'LINESTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)';  
 SELECT @g.BufferWithCurves(0).ToString();
 ```  
  
### E. Calling BufferWithCurves() with a non-zero parameter value that is extremely small  
 The following example also returns a copy of the calling **geography** instance:  

 ```sql
 DECLARE @g geography = 'LINESTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)';  
 DECLARE @distance float = 1e-20;  
 SELECT @g.BufferWithCurves(@distance).ToString();
 ```  
  
### F. Calling BufferWithCurves() with a parameter value > 0  
 The following example returns a `CurvePolygon` instance:  

 ```sql
 DECLARE @g geography= 'LINESTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)';  
 SELECT @g.BufferWithCurves(2).ToString();
 ```  
### G. Passing a valid string parameter  
 The following example returns the same `CurvePolygon` instance as mentioned earlier, but a string parameter is passed to the method:  

 ```sql
 DECLARE @g geography= 'LINESTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)';  
 SELECT @g.BufferWithCurves('2').ToString();
```  
  
### H. Passing an invalid string parameter  
 The following example will throw an error:  

 ```sql
 DECLARE @g geography = 'LINESTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)'  
 SELECT @g.BufferWithCurves('a').ToString();
 ```  
  
 Note that the previous two examples passed a string literal to the `BufferWithCurves()` method. The first example works because the string literal can be converted to a numeric value. However, the second example throws an `ArgumentException`.  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)   
 [BufferWithCurves &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/bufferwithcurves-geometry-data-type.md)  
  
  
