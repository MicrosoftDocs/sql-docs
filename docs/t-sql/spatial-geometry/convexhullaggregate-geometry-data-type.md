---
title: "ConvexHullAggregate (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
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
  - "ConvexHullAggregate method (geometry)"
ms.assetid: ca3d3b55-e02d-4599-8817-a54f5e047db8
caps.latest.revision: 13
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# ConvexHullAggregate (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns a convex hull for a given set of **geometry** objects.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [current version](http://msdn.microsoft.com/library/bb500435.aspx)), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|  
  
## Syntax  
  
```  
  
ConvexHullAggregate ( geometry_operand )  
```  
  
## Arguments  
 *geometry_operand*  
 Is a **geometry** type table column that represents a set of geometry objects.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
## Exception  
 Throws a `FormatException` when there are input values that are not valid. See [STIsValid &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stisvalid-geometry-data-type.md)  
  
## Remarks  
 Method returns **null** when the input is empty or the input has different SRIDs. See [Spatial Reference Identifiers &#40;SRIDs&#41;](../../relational-databases/spatial/spatial-reference-identifiers-srids.md)  
  
 Method ignores **null** inputs.  
  
> [!NOTE]  
>  Method returns **null** if all inputted values are **null**.  
  
## Examples  
 The following example returns a convex hull of the set of geometry objects in a table variable column.  
  
 `-- Setup table variable for ConvexHullAggregate example`  
  
 `DECLARE @Geom TABLE`  
  
 `(`  
  
 `shape geometry,`  
  
 `shapeType nvarchar(50)`  
  
 `)`  
  
 `INSERT INTO @Geom(shape,shapeType) VALUES('CURVEPOLYGON(CIRCULARSTRING(2 3, 4 1, 6 3, 4 5, 2 3))', 'Circle'),`  
  
 `('POLYGON((1 1, 4 1, 4 5, 1 5, 1 1))', 'Rectangle');`  
  
 `-- Perform ConvexHullAggregate on @Geom.shape column`  
  
 `SELECT geometry::ConvexHullAggregate(shape).ToString()`  
  
 `FROM @Geom;`  
  
## See Also  
 [Extended Static Geometry Methods](../../t-sql/spatial-geometry/extended-static-geometry-methods.md)  
  
  