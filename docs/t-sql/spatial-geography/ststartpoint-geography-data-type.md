---
title: "STStartPoint (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STStartPoint_TSQL"
  - "STStartPoint (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STStartPoint method"
ms.assetid: 7df18a5f-b9ee-4e36-b765-a0790c1dee3d
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STStartPoint (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the start point of a **geography** instance.  
  
## Syntax  
  
```  
  
.STStartPoint ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
 Open Geospatial Consortium (OGC) type: **Point**  
  
## Remarks  
 STStartPoint() is the equivalent of [STPointN](../../t-sql/spatial-geometry/stpointn-geometry-data-type.md)`(1)`.  
  
## Examples  
 The following example creates a `LineString` instance and uses `STStartPoint()` to retrieve the start point of the instance.  
  
```  
DECLARE @g geography;  
SET @g = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);  
SELECT @g.STStartPoint().ToString();  
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
