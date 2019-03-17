---
title: "Filter (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "Filter"
  - "Filter_TSQL"
  - "Filter (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Filter method"
ms.assetid: 82a8f54a-3a47-4e20-b13a-b148029c5448
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Filter (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  A method that offers a fast, index-only intersection method to determine if a **geography** instance intersects another **geography** instance, assuming an index is available.  
  
 Returns 1 if a **geography** instance potentially intersects another **geography** instance. This method may produce a false-positive return, and the exact result may be plan-dependent. Returns an accurate 0 value (true negative return) if there is no intersection of **geography** instances found.  
  
 In cases where an index is not available, or is not used, the method will return the same values as **STIntersects()** when called with the same parameters.  
  
## Syntax  
  
```  
  
.Filter ( other_geography )  
```  
  
## Arguments  
 *other_geography*  
 Is another **geography** instance to compare against the instance on which Filter() is invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 This method is not deterministic and is not precise.  
  
## Examples  
 The following example uses `Filter()` to determine if two `geography` instances intersect each other.  
  
```  
CREATE TABLE sample (id int primary key, g geography);  
INSERT INTO sample VALUES  
   (0, geography::Point(45, -120, 4326)),  
   (1, geography::Point(45, -120.1, 4326)),  
   (2, geography::Point(45, -120.2, 4326)),  
   (3, geography::Point(45, -120.3, 4326)),  
   (4, geography::Point(45, -120.4, 4326));  
  
CREATE SPATIAL INDEX sample_idx on sample(g);  
SELECT id  
FROM sample   
WHERE g.Filter(geography::Parse(  
   'POLYGON((-120.1 44.9, -119.9 44.9, -119.9 45.1, -120.1 45.1, -120.1 44.9))')) = 1;  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)   
 [STIntersects &#40;geography Data Type&#41;](../../t-sql/spatial-geography/stintersects-geography-data-type.md)  
  
  
