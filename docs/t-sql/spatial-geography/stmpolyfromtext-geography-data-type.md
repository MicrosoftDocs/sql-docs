---
title: "STMPolyFromText (geography Data Type)"
description: "STMPolyFromText (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "07/30/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STMPolyFromText (geography Data Type)"
  - "STMPolyFromText_TSQL"
helpviewer_keywords:
  - "STMPolyFromText method"
dev_langs:
  - "TSQL"
---
# STMPolyFromText (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geography** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation, augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
STMPolyFromText ( 'multipolygon_tagged_text' , SRID )  
```  
  
## Arguments
 *multipolygon_tagged_text*  
 Is the WKT representation of the **geographyMultiPolygon** instance you wish to return. *multipolygon_tagged_text* is an **nvarchar(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geographyMultiPolygon** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **Sql Geography**  
  
 OGC type: **MultiPolygon**  
  
## Remarks  
 This method throws a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `STMPolyFromText()` to create a `geography` instance.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STMPolyFromText('MULTIPOLYGON(((-122.358 47.653, -122.348 47.649, -122.358 47.658, -122.358 47.653)), ((-122.341 47.656, -122.341 47.661, -122.351 47.661, -122.341 47.656)))', 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
