---
title: "STEnvelope (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STEnvelope_TSQL"
  - "STEnvelope (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STEnvelope (geometry Data Type)"
ms.assetid: 781d22e9-38df-4c23-836f-6dd0bdef49c5
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STEnvelope (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the minimum axis-aligned bounding rectangle of the instance.
  
## Syntax  
  
```  
  
STEnvelope ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Examples  
 The following example uses `STGeomFromText()` to create a `LineString` instance from (0,0) to (2,3), and uses `STEnvelope()` to return the bounding box of the `LineString`.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 3)', 0);  
SELECT @g.STEnvelope().ToString();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

