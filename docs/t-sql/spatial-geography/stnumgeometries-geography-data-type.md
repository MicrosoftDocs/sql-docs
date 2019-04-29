---
title: "STNumGeometries (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STNumGeometries (geography Data Type)"
  - "STNumGeometries_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STNumGeometries method"
ms.assetid: 6ae7fac2-62f1-420f-9fc9-a09606be9605
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STNumGeometries (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the number of **geometries** that make up a **geography** instance.  
  
## Syntax  
  
```  
  
.STNumGeometries ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **int**  
  
 CLR return type: **SqlInt32**  
  
## Remarks  
 This method returns 1 if the **geography** instance is not a **MultiPoint**, **MultiLineString**, **MultiPolygon**, or **GeometryCollection** instance, or 0 if the **geography** instance is empty.  
  
## Examples  
 The following example creates a `MultiPoint` instance and uses `STNumGeometries()` to find out how many **geometries** the instance contains.  
  
```  
DECLARE @g geography;  
SET @g = geography::STGeomFromText('MULTIPOINT((-122.360 47.656), (-122.343 47.656))', 4326);  
SELECT @g.STNumGeometries();  
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
