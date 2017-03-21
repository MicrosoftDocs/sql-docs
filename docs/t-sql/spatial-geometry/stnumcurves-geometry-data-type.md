---
title: "STNumCurves (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STNumCurves method (geometry)"
ms.assetid: 20c2fa0b-656b-4519-b34c-cc8f094290d4
caps.latest.revision: 15
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# STNumCurves (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  This method returns the number of curves in a **geometry** instance when the instance is a one-dimensional spatial data type. One-dimensional spatial data types include **LineString**, **CircularString**, and **CompoundCurve**. `STNumCurves`() works only on simple types; it does not work with **geometry** collections like **MultiLineString**.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|  
  
## Syntax  
  
```  
  
.STNumCurves()  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 An empty one-dimensional **geometry** instance returns 0. **NULL** is returned when the **geometry** instance is not a one-dimensional instance or is an uninitialized instance.  
  
## Examples  
  
### A. Using STNumCurves() on a CircularString instance  
 The following example shows how to get the number of curves in a `CircularString` instance:  
  
 `DECLARE @g geometry;`  
  
 `SET @g = geometry::Parse('CIRCULARSTRING(10 0, 0 10, -10 0, 0 -10, 10 0)');`  
  
 `SELECT @g.STNumCurves();`  
  
### B. Using STNumCurves() on a CompoundCurve instance  
 The following example uses `STNumCurves()` to return the number of curves in a `CompoundCurve` instance.  
  
 `DECLARE @g geometry;`  
  
 `SET @g = geometry::Parse('COMPOUNDCURVE(CIRCULARSTRING(10 0, 0 10, -10 0, 0 -10, 10 0))');`  
  
 `SELECT @g.STNumCurves();`  
  
## See Also  
 [Spatial Data Types Overview](../../relational-databases/spatial/spatial-data-types-overview.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  
