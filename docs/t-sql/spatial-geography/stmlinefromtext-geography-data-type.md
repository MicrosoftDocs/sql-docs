---
title: "STMLineFromText (geography Data Type)"
description: "STMLineFromText (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "07/30/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STMLineFromText (geography Data Type)"
  - "STMLineFromText_TSQL"
helpviewer_keywords:
  - "STLineFromText method"
dev_langs:
  - "TSQL"
---
# STMLineFromText (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geography** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation, augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
STMLineFromText ( 'multilinestring_tagged_text' , SRID )  
```  
  
## Arguments
 *multilinestring_tagged_text*  
 Is the WKT representation of the **geographyMultiLineString** instance you wish to return. *multilinestring_tagged_text* is an **nvarchar(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geographyMultiLineString** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
 OGC type: **MultiLineString**  
  
## Remarks  
 This method throws a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `STMLineFromText()` to create a `geography` instance.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STMLineFromText('MULTILINESTRING ((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653), (-122.357 47.654, -122.357 47.657, -122.349 47.657, -122.349 47.650, -122.357 47.654))', 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
