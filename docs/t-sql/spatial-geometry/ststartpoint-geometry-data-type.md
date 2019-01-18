---
title: "STStartPoint (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STStartPoint_TSQL"
  - "STStartPoint (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STStartPoint (geometry Data Type)"
ms.assetid: 049917db-3f76-4053-8cd2-bc54158e89bc
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STStartPoint (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the start point of a **geometry** instance.
  
## Syntax  
  
```  
  
.STStartPoint ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
 Open Geospatial Consortium (OGC) type: **Point**  
  
## Remarks  
 `STStartPoint()` is the equivalent of [STPointN](../../t-sql/spatial-geometry/stpointn-geometry-data-type.md) (1).  
  
## Examples  
 The following example creates a `LineString` instance and uses `STStartPoint()` to retrieve the start point of the instance.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 2, 1 0)', 0;  
SELECT @g.STStartPoint().ToString();  
```  
  
## See Also  
 [STPointN &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stpointn-geometry-data-type.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

