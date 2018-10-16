---
title: "STX (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STX (geometry Data Type)"
  - "STX_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STX (geometry Data Type)"
ms.assetid: 2aef77e8-0460-43f9-bad6-2aae6d8c36f9
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STX (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

The  X-coordinate property of a **Point**instance.
  
## Syntax  
  
```  
  
.STX  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **float**  
  
 CLR type: **SqlDouble**  
  
## Remarks  
 The value of this property will be null if the **geometry** instance is not a point.  
  
 This property is read-only.  
  
## Examples  
 The following example creates a `Point` instance and uses `STX` to retrieve the X-coordinate of the instance.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POINT(3 8)', 0);  
SELECT @g.STX;  
```  
  
## See Also  
 [STY &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/sty-geometry-data-type.md)   
 [STSrid &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stsrid-geometry-data-type.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

