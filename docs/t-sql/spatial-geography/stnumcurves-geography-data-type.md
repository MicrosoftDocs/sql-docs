---
description: "STNumCurves (geography Data Type)"
title: "STNumCurves (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STNumCurves"
  - "STNumCurves_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STNumCurves method (geography)"
ms.assetid: e98a56c2-8496-4dfd-9b37-7f3c4ca9b2b5
author: MladjoA
ms.author: mlandzic 
---
# STNumCurves (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the number of curves in a one-dimensional **geography** instance.  
  
## Syntax  
  
```  
  
.STNumCurves()  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 One-dimensional spatial data types include **LineString**, **CircularString**, and **CompoundCurve**. An empty one-dimensional **geography** instance returns 0.  
  
 `STNumCurves`() works only on simple types; it does not work with **geography** collections like **MultiLineString**. **NULL** is returned when the **geography** instance is not a one-dimensional data type.  
  
 **Null** is returned for uninitialized **geography** instances.  
  
## Examples  
  
### A. Using STNumCurves() on a CircularString instance  
 The following example shows how to get the number of curves in a `CircularString` instance:  
  
```sql
 DECLARE @g geography; 
 SET @g = geography::Parse('CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)');  
 SELECT @g.STNumCurves();
```  
  
### B. Using STNumCurves() on a CompoundCurve instance  
 The following example uses `STNumCurves()` to return the number of curves in a `CompoundCurve` instance.  
  
```sql
 DECLARE @g geography;  
 SET @g = geography::Parse('COMPOUNDCURVE(CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))');  
 SELECT @g.STNumCurves();
```  
  
## See Also  
 [Spatial Data Types Overview](../../relational-databases/spatial/spatial-data-types-overview.md)   
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
