---
title: "STDimension (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STDimension (geography Data Type)"
  - "STDimension_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STDimension method"
ms.assetid: 4368b0f6-0678-4ade-87dc-b43d8b2e8d92
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STDimension (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the maximum dimension of a **geography** instance.  
  
## Syntax  
  
```  
  
.STDimension ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **int**  
  
 CLR return type: **SqlInt32**  
  
## Remarks  
 STDimension() returns -1 if the **geography** instance is empty.  
  
## Examples  
 The following example uses `STDimension()` to create a table variable to hold `geography` instances, and inserts a `Point`, a `LineString`, and a `Polygon`.  
  
```  
DECLARE @temp table ([name] varchar(10), [geom] geography);  
  
INSERT INTO @temp values ('Point', geography::STGeomFromText('POINT(-122.34900 47.65100)', 4326));  
INSERT INTO @temp values ('LineString', geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326));  
INSERT INTO @temp values ('Polygon', geography::STGeomFromText('POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))', 4326));  
  
SELECT [name], [geom].STDimension() as [dim]  
FROM @temp;  
```  
  
 The example then returns the dimensions of each `geography`instance.  
  
|name|dim|  
|----------|---------|  
|Point|0|  
|LineString|1|  
|Polygon|2|  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
