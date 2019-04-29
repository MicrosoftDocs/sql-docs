---
title: "ShortestLineTo (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ShortestLineTo_TSQL"
  - "ShortestLineTo"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ShortestLineTo method (geography)"
ms.assetid: 9d7c9885-5d1b-49ae-af31-5ef9fb8acaba
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# ShortestLineTo (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns a **LineString** instance with two points that represent the shortest distance between the two **geography** instances. The length of the **LineString** instance returned is the distance between the two **geography** instances.  
  
## Syntax  
  
```  
  
.ShortestLineTo ( geography_other )  
```  
  
## Arguments  
 *geography_other*  
 Specifies the second **geography** instance that the calling **geography** instance is trying to determine the shortest distance to.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 The method returns a **LineString** instance with endpoints lying on the borders of the two non-intersecting **geography** instances being compared. The length of the **LineString** returned equals the shortest distance between the two **geography** instances. An empty **LineString** instance is returned when the two **geography** instances intersect each other.  
  
## Examples  
  
### A. Calling ShortestLineTo() on non-intersecting instances  
 This example finds the shortest distance between a `CircularString` instance and a `LineString` instance and returns the `LineString` instance connecting the two points:  
  
 ```
 DECLARE @g1 geography = 'CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)';  
 DECLARE @g2 geography = 'LINESTRING(-119.119263 46.183634, -119.273071 47.107523, -120.640869 47.569114, -122.200928 47.454094)';  
 SELECT @g1.ShortestLineTo(@g2).ToString();
 ```  
  
### B. Calling ShortestLineTo() on intersecting instances  
 This example returns an empty `LineString` instance because the `LineString` instance intersects the `CircularString` instance:  
  
 ```
 DECLARE @g1 geography = 'CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)';  
 DECLARE @g2 geography = 'LINESTRING(-119.119263 46.183634, -119.273071 47.107523, -120.640869 47.569114, -122.348 47.649, -122.681 47.655)';  
 SELECT @g1.ShortestLineTo(@g2).ToString();
``` 
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)  
  
  
