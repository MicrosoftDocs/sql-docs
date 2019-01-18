---
title: "AsGml (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "AsGml(geometry_Data_Type)_TSQL"
  - "AsGml"
  - "AsGml(geometry Data Type)"
  - "AsGml_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "AsGml (geometry Data Type)"
ms.assetid: f6c2e130-05f3-4ef3-921b-d78b51437d48
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# AsGml (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the Geography Markup Language (GML) representation of a **geometry** instance.
  
For more information on Geography Markup Language, see the following Open Geospatial Consortium Specification:[OGC Specifications, Geography Markup Language.](https://go.microsoft.com/fwlink/?LinkId=93629)
  
## Syntax  
  
```  
  
.AsGml ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **xml**  
  
 CLR return type: **SqlXml**  
  
## Remarks  
  
## Examples  
 The following example creates a `LineString` instance and uses `AsGML()` to return the GML description of the instance.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 0 1, 1 0)', 0)  
SELECT @g.AsGml();  
```  
  
 This method returns the description as a `LineString` instance.  
  
```  
<LineString xmlns="https://www.opengis.net/gml">  
<posList>0 0 0 1 1 0</posList></LineString>  
```  
  
## See Also  
 [Extended Methods on Geometry Instances](../../t-sql/spatial-geometry/extended-methods-on-geometry-instances.md)  
  
  

