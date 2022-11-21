---
description: "STGeomCollFromText (geography Data Type)"
title: "STGeomCollFromText (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STGeomCollFromText_TSQL"
  - "STGeomCollFromText (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STGeomCollFromText method"
ms.assetid: a5b3c344-1045-43a4-82fa-47f6206a288e
author: MladjoA
ms.author: mlandzic 
---
# STGeomCollFromText (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geography** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation, augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
STGeomCollFromText ( 'geometrycollection_tagged_text' , SRID )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
```sql
DECLARE @g geography;  
DECLARE @g geography;  
SET @g = geography::STGeomCollFromText('GEOMETRYCOLLECTION ( POINT(-122.34900 47.65100), LINESTRING(-122.360 47.656, -122.343 47.656) )', 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
