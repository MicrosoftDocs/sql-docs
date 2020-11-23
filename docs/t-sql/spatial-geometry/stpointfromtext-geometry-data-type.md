---
description: "STPointFromText (geometry Data Type)"
title: "STPointFromText (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STPointFromText_TSQL"
  - "STPointFromText (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STPointFromText (geometry Data Type)"
ms.assetid: 1d71dfd8-9d80-44c3-b6e1-64e99cde1fa0
author: MladjoA
ms.author: mlandzic 
---
# STPointFromText (geometry Data Type)
[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

Returns a **geometry** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
STPointFromText ( 'point_tagged_text' , SRID )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *point_tagged_text*  
 Is the WKT representation of the **geometryPoint** instance you wish to return. *point_tagged_text* is an **nvarchar(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geometryPoint** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
 OGC type: **Point**  
  
## Remarks  
 This method will throw a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `STPointFromText()` to create a `geometry` instance.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STPointFromText('POINT (100 100)', 0);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geometry Methods](../../t-sql/spatial-geometry/ogc-static-geometry-methods.md)  
  
  

