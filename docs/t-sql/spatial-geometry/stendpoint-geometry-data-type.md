---
title: "STEndpoint (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STEndpoint (geometry Data Type)"
  - "STEndpoint_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STEndpoint (geometry Data Type)"
ms.assetid: 61773c45-b568-4e0c-94da-1310c42de7f5
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STEndpoint (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the end point of a **geometry** instance.
  
## Syntax  
  
```  
  
.STEndPoint ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
 Open Geospatial Consortium (OGC) type: **Point**  
  
## Remarks  
 `STEndPoint()` is the equivalent of [STPointN](../../t-sql/spatial-geometry/stpointn-geometry-data-type.md) (x.NumPoints()).  
  
 This method returns null if called on an empty **geometry** instance.  
  
## Examples  
 The following example creates a `LineString` instance with `STGeomFromText()` and uses `STEndpoint()` to retrieve the end point of the `LineString`.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 2, 1 0)', 0);  
SELECT @g.STEndPoint().ToString();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

