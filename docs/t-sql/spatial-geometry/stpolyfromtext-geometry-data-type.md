---
description: "STPolyFromText (geometry Data Type)"
title: "STPolyFromText (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STPolyFromText_TSQL"
  - "STPolyFromText (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STPolyFromText (geometry Data Type)"
ms.assetid: a7c1c9f0-1dd5-493b-b206-83bbfa33452b
author: MladjoA
ms.author: mlandzic 
---
# STPolyFromText (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geometry** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
STPolyFromText ( 'polygon_tagged_text' , SRID )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *polygon_tagged_text*  
 Is the WKT representation of the **geometryPolygon** instance you wish to return. *polygon_tagged_text* is an **nvarchar(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geometryPolygon** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
 OGC type: **Polygon**  
  
## Remarks  
 This method will throw a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `STPolyFromText()` to create a `geometry` instance.  
  
```sql
DECLARE @g geometry;   
SET @g = geometry::STPolyFromText('POLYGON ((5 5, 10 5, 10 10, 5 5))', 0);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geometry Methods](../../t-sql/spatial-geometry/ogc-static-geometry-methods.md)  
  
  

