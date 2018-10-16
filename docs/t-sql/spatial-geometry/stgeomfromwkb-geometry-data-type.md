---
title: "STGeomFromWKB (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STGeomFromWKB (geometry Data Type)"
  - "STGeomFromWKB_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STGeomFromWKB (geometry Data Type)"
ms.assetid: 6546ddb0-4a5f-46e5-ba04-8007486c95ec
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STGeomFromWKB (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns a **geometry** instance from an Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation.
  
## Syntax  
  
```  
  
STGeomFromWKB ( 'WKB_geometry' , SRID )  
```  
  
## Arguments  
 *WKB_geometry*  
 Is the WKB representation of the **geometry** instance you wish to return. *WKB_geometry* is a **varbinary(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geometry** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 The OGC type of the **geometry** instance returned by `STGeomFromText()` is set to the corresponding WKB input.  
  
 This method will throw a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `STGeomFromWKB()` to create a **geometry** instance.  
  
```  
DECLARE @g geometry;   
SET @g = geometry::STGeomFromWKB(0x010200000003000000000000000000594000000000000059400000000000003440000000000080664000000000008066400000000000806640, 0);  
SELECT @g.STAsText();  
```  
  
## See Also  
 [OGC Static Geometry Methods](../../t-sql/spatial-geometry/ogc-static-geometry-methods.md)  
  
  

