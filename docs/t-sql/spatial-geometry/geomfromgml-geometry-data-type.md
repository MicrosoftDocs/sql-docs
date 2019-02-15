---
title: "GeomFromGml (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
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
  - "GeomFromGML (geometry Data Type)"
ms.assetid: a3f2c84b-a49f-4ce3-ba25-b903fb0c99b4
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# GeomFromGml (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Construct a **geometry** instance given a representation in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] subset of the Geography Markup Language (GML).
  
For more information on the Geography Markup Language, see the following Open Geospatial Consortium Specifications:
  
[OGC Specifications, Geography Markup Language](https://go.microsoft.com/fwlink/?LinkId=93629)
  
## Syntax  
  
```  
  
GeomFromGml ( GML_input, SRID )  
```  
  
## Arguments  
 *GML_input*  
 Is an XML input from which the GML will return a value.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geometry** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 This method will throw a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `GeomFromGml()` to create a `geometry` instance.  
  
```  
DECLARE @g geometry;  
DECLARE @x xml;  
SET @x = '<LineString xmlns="https://www.opengis.net/gml"> <posList>100 100 20 180 180 180</posList> </LineString>';  
SET @g = geometry::GeomFromGml(@x, 0);  
SELECT @g.ToString();  
```  
  
## See Also  
 [Extended Static Geometry Methods](../../t-sql/spatial-geometry/extended-static-geometry-methods.md)  
  
  

