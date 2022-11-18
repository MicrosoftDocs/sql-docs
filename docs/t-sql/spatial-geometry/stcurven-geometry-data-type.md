---
description: "STCurveN (geometry Data Type)"
title: "STCurveN (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STCurveN method (geometry)"
ms.assetid: 64adf1a1-3a41-41fb-b7d1-44390c3e4ea9
author: MladjoA
ms.author: mlandzic 
---
# STCurveN (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the curve specified from a **geometry** instance that is a **LineString**, **CircularString**, **CompoundCurve**, or **MultiLineString**.
  
## Syntax  
  
```  
  
.STCurveN ( curve_index )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *curve_index*  
 Is an **int** expression between 1 and the number of curves in the **geometry** instance.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Exceptions  
 If *curve_index* < 1 then an `ArgumentOutOfRangeException` is thrown.  
  
## Remarks  
 **NULL** is returned when any of the following occurs:  
  
-   the **geometry** instance is declared, but not instantiated  
  
-   the **geometry** instance is empty  
  
-   *curve_index* exceeds the number of curves in the **geometry** instance  
  
-   the **geometry** instance is a **Point**, **MultiPoint**, **Polygon**, **CurvePolygon**, or **MultiPolygon**  
  
## Examples  
  
### A. Using STCurveN() on a CircularString instance  
 The following example returns the second curve in a `CircularString` instance:  
  
```sql
 DECLARE @g geometry = 'CIRCULARSTRING(0 0, 1 2.1082, 3 6.3246, 0 7, -3 6.3246, -1 2.1082, 0 0)';  
 SELECT @g.STCurveN(2).ToString();
 ```  
  
 The example earlier in this topic returns:  
  
 `CIRCULARSTRING (3 6.3246, 0 7, -3 6.3246)`  
  
### B. Using STCurveN() on a CompoundCurve instance with one CircularString instance  
 The following example returns the second curve in a `CompoundCurve` instance:  
  
```sql
 DECLARE @g geometry = 'COMPOUNDCURVE(CIRCULARSTRING(0 0, 1 2.1082, 3 6.3246, 0 7, -3 6.3246, -1 2.1082, 0 0))';  
 SELECT @g.STCurveN(2).ToString();
 ```  
  
 The example earlier in this topic returns:  
  
 `CIRCULARSTRING (3 6.3246, 0 7, -3 6.3246)`  
  
### C. Using STCurveN() on a CompoundCurve instance with three CircularString instances  
 The following example uses a `CompoundCurve` instance that combines three separate `CircularString` instances into the same curve sequence as the previous example:  
  
```sql
 DECLARE @g geometry = 'COMPOUNDCURVE (CIRCULARSTRING (0 0, 1 2.1082, 3 6.3246), CIRCULARSTRING(3 6.3246, 0 7, -3 6.3246), CIRCULARSTRING(-3 6.3246, -1 2.1082, 0 0))';  
 SELECT @g.STCurveN(2).ToString();
 ```  
  
 The example earlier in this topic returns:  
  
 `CIRCULARSTRING (3 6.3246, 0 7, -3 6.3246)`  
  
 Notice that the results are the same for the previous three examples. Whichever WKT (Well-known Text) format is used to enter the same curve sequence, the results returned by `STCurveN()` are the same when a `CompoundCurve` instance is used.  
  
### D. Validating the parameter before calling STCurveN()  
 The following example shows how to make sure `@n` is valid before you call the `STCurveN()`method:  
  
```sql
 DECLARE @g geometry;  
 DECLARE @n int;  
 SET @n = 3;  
 SET @g = geometry::Parse('CIRCULARSTRING(0 0, 1 2.1082, 3 6.3246, 0 7, -3 6.3246, -1 2.1082, 0 0)');  
 IF @n >= 1 AND @n <= @g.STNumCurves()  
 BEGIN  
 SELECT @g.STCurveN(@n).ToString();  
 END
 ```  
  
## See Also  
 [STNumCurves &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stnumcurves-geometry-data-type.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

