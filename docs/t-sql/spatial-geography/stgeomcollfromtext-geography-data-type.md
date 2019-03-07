---
title: "STGeomCollFromText (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STGeomCollFromText_TSQL"
  - "STGeomCollFromText (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STGeomCollFromText method"
ms.assetid: a5b3c344-1045-43a4-82fa-47f6206a288e
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STGeomCollFromText (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns a **geography** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation, augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
STGeomCollFromText ( 'geometrycollection_tagged_text' , SRID )  
```  
  
## Arguments  
 *geometrycollection_tagged_text*  
 Is the WKT representation of the **geography** instance you wish to return. *geometrycollection_tagged_text* is an **nvarchar(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geography** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 The OGC type of the **geography** instance returned by STGeomCollFromText() is set to the corresponding WKT input.  
  
 This method throws an **ArgumentException** if the input is not valid.  
  
## Examples  
 The following example uses `STGeomCollFromText()` to create a `geography` instance.  
  
```  
DECLARE @g geography;  
DECLARE @g geography;  
SET @g = geography::STGeomCollFromText('GEOMETRYCOLLECTION ( POINT(-122.34900 47.65100), LINESTRING(-122.360 47.656, -122.343 47.656) )', 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
