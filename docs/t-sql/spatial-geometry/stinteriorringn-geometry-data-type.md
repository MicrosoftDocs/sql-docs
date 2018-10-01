---
title: "STInteriorRingN (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STInteriorRingN_TSQL"
  - "STInteriorRingN (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STInteriorRingN (geometry Data Type)"
ms.assetid: 47310f9f-2cdb-41e0-a6da-7c3cfbf139ac
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STInteriorRingN (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the specified interior ring of a **Polygongeometry** instance.
  
## Syntax  
  
```  
  
.STInteriorRingN ( expression )  
```  
  
## Arguments  
 *expression*  
 Is an **int** expression between 1 and the number of interior rings in the **geometry** instance.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
 Open Geospatial Consortium (OGC) type: **LineString**  
  
## Remarks  
 This method returns **null** if the **geometry** instance is not a polygon. This method will also throw an **ArgumentOutOfRangeException** if the expression is larger than the number of rings. The number of rings can be returned using `STNumInteriorRing``()`.  
  
## Examples  
 The following example creates a `Polygon` instance and uses `STInteriorRingN()` to return the interior ring of the polygon as a **LineString**.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 3 0, 3 3, 0 3, 0 0),(2 2, 2 1, 1 1, 1 2, 2 2))', 0);  
SELECT @g.STInteriorRingN(1).ToString();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

