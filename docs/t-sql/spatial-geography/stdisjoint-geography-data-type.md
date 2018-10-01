---
title: "STDisjoint (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STDisjoint (geography Data Type)"
  - "STDisjoint_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STDisjoint"
ms.assetid: 98328a02-e018-47d6-aa93-de162b8aef62
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STDisjoint (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns 1 if a **geography** instance is spatially disjoint from another **geography** instance. Returns 0 if it is not.  
  
## Syntax  
  
```  
  
.STDisjoint ( other_geography )  
```  
  
## Arguments  
 *other_geography*  
 Is another **geography** instance to compare against the instance on which STDisjoint() is invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 Two **geography** instances are disjoint if the intersection of their point sets is empty.  
  
 This method always returns null if the spatial reference IDs (SRIDs) of the **geography** instances do not match.  
  
## Examples  
 The following example uses `STDisjoint()` to test two `geography` instances to see if they are spatially disjoint.  
  
```  
DECLARE @g geography;  
DECLARE @h geography;  
SET @g = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);  
SET @h = geography::STGeomFromText('POINT(-122.34900 47.65100)', 4326);  
SELECT @g.STDisjoint(@h);  
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
