---
description: "STCurveN (geography Data Type)"
title: "STCurveN (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STCurveN"
  - "STCurveN_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STCurveN method (geography)"
ms.assetid: 99ef7100-2c4b-4f07-8d66-b343da94b023
author: MladjoA
ms.author: mlandzic 
---
# STCurveN (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the curve specified from a **geography** instance that is a **LineString**, **CircularString**, or **CompoundCurve**.  
  
## Syntax  
  
```  
  
.STCurveN( n )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *n*  
 Is an **int** expression between 1 and the number of curves in the **geography** instance.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Exceptions  
 If n < 1 then an **ArgumentOutOfRangeException** is thrown.  
  
## Remarks  
 **NULL** is returned when the following criteria occurs.  
  
-   The **geography** instance is declared, but is not instantiated  
  
-   The **geography** instance is empty  
  
-   n exceeds the number of curves in the **geography** instance (See [STNumCurves &#40;geography Data Type&#41;](../../t-sql/spatial-geography/stnumcurves-geography-data-type.md)  
  
-   The dimension for the **geography** instance does not equal (See [STDimension &#40;geography Data Type&#41;](../../t-sql/spatial-geography/stdimension-geography-data-type.md)  
  
## Examples  
  
### A. Using STCurveN() on a CircularString  
 The following example returns the second curve in a **CircularString** instance:  
  
```sql
 DECLARE @g geography = 'CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)';  
 SELECT @g.STCurveN(2).ToString();
```  
  
 The example returns.  
  
 `CIRCULARSTRING (-122.348 47.658, -122.358 47.658, -122.358 47.653)`  
  
### B. Using STCurveN() on a CompoundCurve  
 The following example returns the second curve in a **CompoundCurve** instance:  
  
```sql
 DECLARE @g geography = 'COMPOUNDCURVE(CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))';  
 SELECT @g.STCurveN(2).ToString();
```  
  
 The example returns.  
  
 `CIRCULARSTRING (-122.348 47.658, -122.358 47.658, -122.358 47.653)`  
  
### C. Using STCurveN() on a CompoundCurve Containing Three CircularStrings  
 The following example uses a **CompoundCurve** instance that combines three separate **CircularString** instances into the same curve sequence as the previous example:  
  
```sql
 DECLARE @g geography = 'COMPOUNDCURVE (CIRCULARSTRING (-122.358 47.653, -122.348 47.649, -122.348 47.658), CIRCULARSTRING(-122.348 47.658, -122.358 47.658, -122.358 47.653))';  
 SELECT @g.STCurveN(2).ToString();
```  
  
 The example returns.  
  
 `CIRCULARSTRING (-122.348 47.658, -122.358 47.658, -122.358 47.653)`  
  
 `STCurveN()` returns the same results regardless of Well-Known Text (WKT) format that is used.  
  
### D. Testing for Validity Before Calling STCurve()  
 The following example shows how to make sure that *n* is valid before you call the STCurveN() method:  
  
```sql
 DECLARE @g geography;  
 DECLARE @n int;  
 SET @n = 2;  
 SET @g = geography::Parse('LINESTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)');  
 IF @n >= 1 AND @n <= @g.STNumCurves()  
 BEGIN  
 SELECT @g.STCurveN(@n).ToString();  
 END
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
