---
title: "STMLineFromText (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STMLineFromText (geography Data Type)"
  - "STMLineFromText_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STLineFromText method"
ms.assetid: 66dfd722-a9bd-45d3-9788-f1946dd23e17
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STMLineFromText (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns a **geography** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation, augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
STMLineFromText ( 'multilinestring_tagged_text' , SRID )  
```  
  
## Arguments  
 *multilinestring_tagged_text*  
 Is the WKT representation of the **geographyMultiLineString** instance you wish to return. *multilinestring_tagged_text* is an **nvarchar(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geographyMultiLineString** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
 OGC type: **MultiLineString**  
  
## Remarks  
 This method throws a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `STMLineFromText()` to create a `geography` instance.  
  
```  
DECLARE @g geography;  
SET @g = geography::STMLineFromText('MULTILINESTRING ((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653), (-122.357 47.654, -122.357 47.657, -122.349 47.657, -122.349 47.650, -122.357 47.654))', 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
