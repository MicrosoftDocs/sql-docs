---
title: "STNumPoints (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STNumPoints_TSQL"
  - "STNumPoints (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STNumPoints (geometry Data Type)"
ms.assetid: a19520fc-7f91-4a2c-856f-4d8b99a7e496
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STNumPoints (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns the sum of the number of points in each of the figures in a **geometry** instance.  
  
## Syntax  
  
```  
  
.STNumPoints ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **int**  
  
 CLR return type: **SqlInt32**  
  
## Remarks  
 This method counts the points in the description of a **geometry** instance. Duplicate points are counted. If this instance is a **collection** type, this method returns the sum of the points in each of its elements.  
  
## Examples  
 The following example creates a `LineString` instance and uses `STNumPoints()` to determine how many points were used in the description of the instance.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 2, 1 0)', 0);  
SELECT @g.STNumPoints();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  
