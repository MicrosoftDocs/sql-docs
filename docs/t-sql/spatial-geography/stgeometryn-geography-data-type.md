---
title: "STGeometryN (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STGeometryN (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STGeometryN method"
ms.assetid: 53755f69-cd50-475b-b3b8-a1a9157cf03a
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STGeometryN (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns a specified **geography** element in a **GeometryCollection** or one of its subtypes. When STGeometryN() is used on a subtype of a **GeometryCollection**, such as **MultiPoint** or **MultiLineString**, this method returns the **geography** instance if called with N=1.  
  
## Syntax  
  
```  
  
.STGeometryN ( expression )  
```  
  
## Arguments  
 *expression*  
 Is an **int** expression between 1 and the number of **geography** instances in the **GeometryCollection**.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 This method returns null if the parameter is larger than the result of [STNumGeometries()](../../t-sql/spatial-geography/stnumgeometries-geography-data-type.md) and will throw an **ArgumentOutOfRangeException** if the *expression* parameter is less than 1.  
  
## Examples  
 The following example creates a `MultiPoint``geography` instance and uses `STGeometryN()` to find the second `geography` instance of the **GeometryCollection**.  
  
```  
DECLARE @g geography;  
SET @g = geography::STGeomFromText('MULTIPOINT(-122.360 47.656, -122.343 47.656)', 4326);  
SELECT @g.STGeometryN(2).ToString();  
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
