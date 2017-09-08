---
title: "STNumCurves (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "STNumCurves"
  - "STNumCurves_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STNumCurves method (geography)"
ms.assetid: e98a56c2-8496-4dfd-9b37-7f3c4ca9b2b5
caps.latest.revision: 10
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# STNumCurves (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns the number of curves in a one-dimensional **geography** instance.  
  
## Syntax  
  
```  
  
.STNumCurves()  
```  
  
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
  
 `DECLARE @g geography;`  
  
 `SET @g = geography::Parse('CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)');`  
  
 `SELECT @g.STNumCurves();`  
  
### B. Using STNumCurves() on a CompoundCurve instance  
 The following example uses `STNumCurves()` to return the number of curves in a `CompoundCurve` instance.  
  
 `DECLARE @g geography;`  
  
 `SET @g = geography::Parse('COMPOUNDCURVE(CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))');`  
  
 `SELECT @g.STNumCurves();`  
  
## See Also  
 [Spatial Data Types Overview](../../relational-databases/spatial/spatial-data-types-overview.md)   
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
