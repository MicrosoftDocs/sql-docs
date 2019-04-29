---
title: "STDifference (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STDifference_TSQL"
  - "STDifference (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STDifference (geography Data Type)"
ms.assetid: 1cde5054-b91a-41bb-812a-08c9308738af
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STDifference (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns an object that represents the point set from one **geography** instance that lies outside another **geography** instance.  
  
## Syntax  
  
```  
  
.STDifference ( other_geography )  
```  
  
## Arguments  
 *other_geography*  
 Is another **geography** instance indicating which points to remove from the instance on which STDifference() is being invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Exceptions  
 This method throws an **ArgumentException** if the instance contains an antipodal edge.  
  
## Remarks  
 This method always returns null if the spatial reference identifiers (SRIDs) of the **geography** instances do not match.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the set of possible results returned on the server has been extended to **FullGlobe** instances. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports spatial instances that are larger than a hemisphere. The result may contain circular arc segments only if the input instances contain circular arc segments. This method is not precise.  
  
## Examples  
  
### A. Computing the difference between two geography instances  
 The following example uses `STDifference()` to compute the difference between two **geography** instances.  
  
```  
DECLARE @g geography;  
DECLARE @h geography;  
SET @g = geography::STGeomFromText('POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))', 4326);  
SET @h = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);  
SELECT @g.STDifference(@h).ToString();  
```  
  
### B. Using a FullGlobe with STDifference()  
 The following example uses `FullGlobe` instance. The first result is an empty `GeometryCollection` and the second result is a `Polygon` instance. `STDifference()` returns an empty `GeometryCollection` when a `FullGlobe` instance is the parameter. Every point in an invoking `geography` instance is contained in a `FullGlobe` instance.  
  
```
 DECLARE @g geography = 'POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))';  
 DECLARE @h geography = 'FULLGLOBE';  
 SELECT @g.STDifference(@h).ToString(),  
 @h.STDifference(@g).ToString();
 ```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
