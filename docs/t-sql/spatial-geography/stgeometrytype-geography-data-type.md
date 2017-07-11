---
title: "STGeometryType (geography Data Type) | Microsoft Docs"
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
  - "STGeometryType (geography Data Type)"
  - "STGeometryType_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STGeometryType method"
ms.assetid: 3e169ead-a98e-44af-8d33-fd59a955cae4
caps.latest.revision: 17
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# STGeometryType (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the Open Geospatial Consortium (OGC) type name represented by a **geography** instance.  
  
## Syntax  
  
```  
  
.STGeometryType ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **nvarchar(4000)**  
  
 CLR return type: **SqlString**  
  
## Remarks  
 The OGC type names that can be returned by `STGeometryType()` are **Point**, **LineString**, **CircularString**, **CompoundCurve**, **Polygon**, **CurvePolygon**, **GeometryCollection**, **MultiPoint**, **MultiLineString**, and **MultiPolygon**.  
  
## Examples  
 The following example creates a `Polygon` instance and uses `STGeometryType()` to confirm that it is a polygon.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))', 4326);  
SELECT @g.STGeometryType();  
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  