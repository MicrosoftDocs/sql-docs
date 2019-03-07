---
title: "STAsText (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STAsText_TSQL"
  - "STAsText (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STAsText (geometry Data Type)"
ms.assetid: e0decf5e-2858-4c56-b61a-6123f47fb51c
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STAsText (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

Returns the Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation of a **geometry** instance. This text will not contain any Z (elevation) or M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
.STAsText ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **nvarchar(max)**  
  
 CLR return type: **SqlChars**  
  
## Examples  
 The following example creates a `LineString` geometry instance from (0,0) to (2,3) from text. `STAsText()` returns the result in text.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 3)', 0);  
SELECT @g.STAsText();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

