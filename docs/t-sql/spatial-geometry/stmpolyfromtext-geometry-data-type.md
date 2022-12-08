---
description: "STMPolyFromText (geometry Data Type)"
title: "STMPolyFromText (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STMPolyFromText (geometry Data Type)"
  - "STMPolyFromText_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STMPolyFromText (geometry Data Type)"
ms.assetid: f087a61c-f063-4fb8-8f1c-251a2fed76a1
author: MladjoA
ms.author: mlandzic 
---
# STMPolyFromText (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geometry** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
STMPolyFromText ( 'multipolygon_tagged_text' , SRID )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *multipolygon_tagged_text*  
 Is the WKT representation of the **geometryMultiPolygon** instance you wish to return. *multipolygon_tagged_text* is an **nvarchar(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geometryMultiPolygon** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **Sql Geometry**  
  
 OGC type: **MultiPolygon**  
  
## Remarks  
 This method will throw a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `STMPolyFromText()` to create a `geometry` instance.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STMPolyFromText('MULTIPOLYGON (((5 5, 10 5, 10 10, 5 5)), ((10 10, 100 10, 200 200, 30 30, 10 10)))', 0);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geometry Methods](../../t-sql/spatial-geometry/ogc-static-geometry-methods.md)  
  
  

