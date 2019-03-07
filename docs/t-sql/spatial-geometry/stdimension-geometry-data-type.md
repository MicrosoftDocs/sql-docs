---
title: "STDimension (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STDimension_TSQL"
  - "STDimension (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STDimension (geometry Data Type)"
ms.assetid: 4fbd27dd-317b-4916-a8ae-4df1b8a6f27c
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STDimension (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the maximum dimension of a **geometry** instance.
  
## Syntax  
  
```  
  
.STDimension ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **int**  
  
 CLR return type: **SqlInt32**  
  
## Remarks  
 `STDimension()` returns -1 if the **geometry** instance is empty.  
  
## Examples  
 The following example creates a table variable to hold **geometry** instances and inserts a `Point`, a `LineString`, and a `Polygon`.  It then uses `STDimension()` to return the dimensions of each **geometry** instance.  
  
```  
DECLARE @temp table ([name] varchar(10), [geom] geometry);  
INSERT INTO @temp values ('Point', geometry::STGeomFromText('POINT(3 3)', 0));  
INSERT INTO @temp values ('LineString', geometry::STGeomFromText('LINESTRING(0 0, 3 3)', 0));  
INSERT INTO @temp values ('Polygon', geometry::STGeomFromText('POLYGON((0 0, 3 0, 0 3, 0 0))', 0));  
SELECT [name], [geom].STDimension() as [dim]  
FROM @temp;  
```  
  
 The example then returns the dimensions of each `geometry` instance.  
  
|name|dim|  
|----------|---------|  
|Point|0|  
|LineString|1|  
|Polygon|2|  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

