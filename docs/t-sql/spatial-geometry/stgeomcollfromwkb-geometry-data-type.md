---
title: "STGeomCollFromWKB (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STGeomCollFromWKB (geometry Data Type)"
  - "STGeomCollFromWKB_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STGeomCollFromWKB (geometry Data Type)"
ms.assetid: 6c55032c-7f5e-4181-8e67-c0265032db63
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STGeomCollFromWKB (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns a **geometrycollection** instance from an Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation.
  
## Syntax  
  
```  
  
STGeomCollFromWKB ( 'WKB_geometrycollection' , SRID )  
```  
  
## Arguments  
 *WKB_geometrycollection*  
 Is the WKB representation of the **geometrycollection** instance you wish to return. *WKB_geometrycollection* is a **varbinary(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geometry** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 The OGC type of the **geometry** instance returned by `STGeomCollFromWKB()` is set to **GeomCollection**, **MultiPolygon**, **MultiLineString**, or **MultiPoint**, depending on the corresponding WKB input.  
  
 This method will throw a FormatException exception if the input isn't well-formatted.  
  
## Examples  
 The following example uses `STGeomCollFromWKB()` to create a **geometry** instance.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomCollFromWKB(0x0107000000020000000103000000010000000400000000000000000014400000000000001440000000000000244000000000000014400000000000002440000000000000244000000000000014400000000000001440010100000000000000000024400000000000002440, 0);  
SELECT @g.STAsText();  
```  
  
## See Also  
 [OGC Static Geometry Methods](../../t-sql/spatial-geometry/ogc-static-geometry-methods.md)  
  
