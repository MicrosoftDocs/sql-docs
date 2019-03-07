---
title: "STSrid (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STSrid (geometry Data Type)"
  - "STSrid_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STSrid (geometry Data Type)"
ms.assetid: 5e0de983-a0fe-48b7-9e08-30588d7271e2
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STSrid (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  **STSrid** is an integer representing the spatial reference identifier of the instance.  
  
This property can be modified.
  
## Syntax  
  
```  
  
STSrid  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **int**  
  
 CLR type: **SqlInt32**  
  
## Examples  
 The first example creates a **geometry** instance with the SRID value 13 and uses `STSrid` to confirm the SRID.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 3 0, 3 3, 0 3, 0 0))', 13);  
SELECT @g.STSrid;  
```  
  
 The second example uses `STSrid` to change the SRID value of the instance to 23 and then confirms the modified SRID value.  
  
```  
SET @g.STSrid = 23;  
SELECT @g.STSrid;  
```  
  
## See Also  
 [STX &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stx-geometry-data-type.md)   
 [STY &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/sty-geometry-data-type.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

