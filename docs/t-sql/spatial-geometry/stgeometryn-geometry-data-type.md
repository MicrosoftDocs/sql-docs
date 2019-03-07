---
title: "STGeometryN (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STGeometryN_TSQL"
  - "STGeometryN (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STGeometryN (geometry Data Type)"
ms.assetid: 348c7047-3442-4590-8879-fe841e79058c
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STGeometryN (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns a specified geometry in a **geometry collection**.
  
## Syntax  
  
```  
  
.STGeometryN ( expression )  
```  
  
## Arguments  
 *expression*  
 Is an **int** expression between 1 and the number of **geometry** instances in the **geometrycollection**.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 This method returns **null** if the parameter is larger than the result of `STNumGeometries()` and will throw an **ArgumentOutOfRangeException** if the *expression* parameter is less than 1.  
  
## Examples  
 The following example creates a `MultiPoint``geometry collection` and uses `STGeometryN()` to find the second `geometry` instance of the collection.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('MULTIPOINT(0 0, 13.5 2, 7 19)', 0);  
SELECT @g.STGeometryN(2).ToString();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

