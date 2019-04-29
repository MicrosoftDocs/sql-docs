---
title: "STY (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STY_TSQL"
  - "STY (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STY (geometry Data Type)"
ms.assetid: f72e0eaa-7d1d-4052-88fd-a172d8cb0d71
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STY (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

The Y-coordinate property of a **Point** instance.
  
## Syntax  
  
```  
  
.STY  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **float**  
  
 CLR type: **SqlDouble**  
  
## Remarks  
 The value of this property will be null if the **geometry** instance is a point. This property is read-only.  
  
## Examples  
 The following example creates a `Point` instance and uses `STY` to retrieve the Y-coordinate of the instance.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POINT(3 8)', 0);  
SELECT @g.STY;  
```  
  
## See Also  
 [STX &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stx-geometry-data-type.md)   
 [STSrid &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stsrid-geometry-data-type.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

