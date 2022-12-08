---
description: "STGeomFromText (geography Data Type)"
title: "STGeomFromText (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STGeomFromText (geography Data Type)"
  - "STGeomFromText_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "full globe"
  - "STGeomFromText method"
ms.assetid: 3717987b-77d8-4ccf-a1db-5a8016ac1083
author: MladjoA
ms.author: mlandzic 
---
# STGeomFromText (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geography** instance from an Open Geospatial Consortium (OGC)Well-Known Text (WKT) representation augmented with any Z (elevation) and M (measure) values carried by the instance.
  
This **geography** data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.
  
## Syntax  
  
```  
  
STGeomFromText ( 'geography_tagged_text' , SRID )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *geography_tagged_text*  
 Is the WKT representation of the **geography** instance to return. *geography_tagged_text* is an **nvarchar(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geography** instance to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 The OGC type of the **geography** instance returned by STGeomFromText() is set to the corresponding WKT input.  
  
 This method will throw an **ArgumentException** if the input contains an antipodal edge.  

> [!Note]
> The order in which the points are listed matters for geography polygons. It determines if the polygon area is to the inside or outside of the given ring. See [Polygon](../../relational-databases/spatial/polygon.md#orientation-of-spatial-data) for more information.
  
## Examples  
 The following example uses `STGeomFromText()` to create a `geography` instance.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
