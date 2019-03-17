---
title: "STCrosses (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STCrosses (geometry Data Type)"
  - "STCrosses_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STCrosses (geometry Data Type)"
ms.assetid: 3e3fc065-555a-4bee-8b71-e92f3dc62a4f
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STCrosses (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

Returns 1 if a **geometry** instance crosses another **geometry** instance. Returns 0 if it does not.
  
## Syntax  
  
```  
  
.STCrosses ( other_geometry )  
```  
  
## Arguments  
 *other_geometry*  
 Is another **geometry** instance to compare against the instance on which `STCrosses()` is invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 Two **geometry** instances cross if both of the following conditions are true:  
  
-   The intersection of the two **geometry** instances results in a geometry whose dimensions are less than the maximum dimension of the source **geometry** instances.  
  
-   The intersection set is interior to both source **geometry** instances.  
  
 This method always returns null if the spatial reference IDs (SRIDs) of the **geometry** instances do not match.  
  
## Examples  
 The following example uses `STCrosses()` to test two `geometry` instances to see if they cross.  
  
```  
DECLARE @g geometry;  
DECLARE @h geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 2, 2 0)', 0);  
SET @h = geometry::STGeomFromText('LINESTRING(0 0, 2 2)', 0);  
SELECT @g.STCrosses(@h);  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

