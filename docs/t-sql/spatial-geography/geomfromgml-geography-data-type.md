---
title: "GeomFromGML (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "GeomFromGML_TSQL"
  - "GeomFromGML"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "GeomFromGML (geography Data Type)"
  - "GeomFromGML method"
ms.assetid: 470d0997-3cb0-4d34-9a45-b332fe432b14
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# GeomFromGML (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Constructs a **geography** instance given a representation in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] subset of the Geography Markup Language (GML).
  
For more information on GML, see the following Open Geospatial Consortium Specifications: [OGC Specifications, Geography Markup Language](https://go.microsoft.com/fwlink/?LinkId=93629)
  
This **geography** data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.
  
## Syntax  
  
```  
  
GeomFromGml ( GML_input, SRID )  
```  
  
## Arguments  
 *GML_input*  
 Is an XML input from which the GML will return a value.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geography** instance to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 This method throws a **FormatException** if the input is not well-formatted.  
  
 This method will throw **ArgumentException** if the input contains antipodal edge.  
  
## Examples  
 The following example uses `GeomFromGml()` to create a `geography` instance.  
  
```  
DECLARE @g geography;  
DECLARE @x xml;  
SET @x = '<LineString xmlns="https://www.opengis.net/gml"><posList>47.656 -122.36 47.656 -122.343</posList></LineString>';  
SET @g = geography::GeomFromGml(@x, 4326);  
SELECT @g.ToString();  
```  
  
 The following example uses `GeomFromGml()` to create a `FullGlobe``geography` instance.  
  
```  
DECLARE @g geography;  
DECLARE @x xml;  
SET @x = '<FullGlobe xmlns="https://schemas.microsoft.com/sqlserver/2011/geography" />';  
SET @g = geography::GeomFromGml(@x, 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [Extended Static Geography Methods](../../t-sql/spatial-geography/extended-static-geography-methods.md)  
  
  
