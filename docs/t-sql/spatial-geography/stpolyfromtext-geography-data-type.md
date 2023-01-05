---
description: "STPolyFromText (geography Data Type)"
title: "STPolyFromText (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STPolyFromText_TSQL"
  - "STPolyFromText (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STPolyFromText method"
ms.assetid: d7e6a2bb-d301-49fb-9202-c70a9d169b4d
author: MladjoA
ms.author: mlandzic 
---
# STPolyFromText (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geography** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
STPolyFromText ( 'polygon_tagged_text' , SRID )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *polygon_tagged_text*  
 Is the WKT representation of the **geographyPolygon** instance you wish to return. *polygon_tagged_text* is an **nvarchar(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geographyPolygon** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
 OGC type: **Polygon**  
  
## Remarks  
 This method throws a **FormatException** if the input is not well-formatted.  

> [!Note]
> The order in which the points are listed matters for geography polygons. It determines if the polygon area is to the inside or outside of the given ring. See [Polygon](../../relational-databases/spatial/polygon.md#orientation-of-spatial-data) for more information.

## Examples  
 The following example uses `STPolyFromText()` to create a `geography` instance.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STPolyFromText('POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))', 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  