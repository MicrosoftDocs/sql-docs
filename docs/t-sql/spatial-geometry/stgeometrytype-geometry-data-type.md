---
title: "STGeometryType (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "STGeometryType_TSQL"
  - "STGeometryType (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STGeometryType (geometry Data Type)"
ms.assetid: 224cdc83-aa83-4ad4-bb82-b7481031e910
caps.latest.revision: 32
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# STGeometryType (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the Open Geospatial Consortium (OGC) type name represented by a **geometry** instance.
  
## Syntax  
  
```  
  
.STGeometryType ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **nvarchar(4000)**  
  
 CLR return type: **SqlString**  
  
## Remarks  
 The OGC type names that can be returned by `STGeometryType()` are **Point**, **LineString**, **CircularString**, **CompoundCurve**, **Polygon, CurvePolygon**, **GeometryCollection**, **MultiPoint**, **MultiLineString**, and **MultiPolygon**.  
  
## Examples  
 The following example creates a `Polygon` instance and uses `STGeometryType()` to confirm that it is a polygon.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 3 0, 3 3, 0 3, 0 0))', 0);  
SELECT @g.STGeometryType();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

