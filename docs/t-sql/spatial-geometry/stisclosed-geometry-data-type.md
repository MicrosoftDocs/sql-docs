---
title: "STIsClosed (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STIsClosed_TSQL"
  - "STIsClosed (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STIsClosed (geometry Data Type)"
ms.assetid: 14edbb22-df7b-4b8a-b16c-ac477a5d32c1
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STIsClosed (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns 1 if the start and end points of the given **geometry** instance are the same. Returns 1 for **geometrycollection** types if each contained **geometry** instance is closed. Returns 0 if the instance is not closed.
  
## Syntax  
  
```  
  
.STIsClosed ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 This method returns 0 if any figures of a **geometry** instance are points, or if the instance is empty.  
  
 All **Polygon** instances are considered closed.  
  
## Examples  
 The following example creates a `LineString` instance and uses `STIsClosed()` to test if the `LineString` is closed.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 2, 1 0)', 0);  
SELECT @g.STIsClosed();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

