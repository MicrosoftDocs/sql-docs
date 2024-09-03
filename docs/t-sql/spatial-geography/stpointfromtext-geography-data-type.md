---
title: "STPointFromText (geography Data Type)"
description: "STPointFromText (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "07/30/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STPointFromText (geography Data Type)"
  - "STPointFromText_TSQL"
helpviewer_keywords:
  - "STPointFromText method"
dev_langs:
  - "TSQL"
---
# STPointFromText (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geography** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation, augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
STPointFromText ( 'point_tagged_text' , SRID )  
```  
  
## Arguments
 *point_tagged_text*  
 Is the WKT representation of the **geographyPoint** instance you wish to return. *point_tagged_text* is an **nvarchar(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geographyPoint** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
 OGC type: **Point**  
  
## Remarks  
 This method throws a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `STPointFromText()` to create a `geography` instance.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STPointFromText('POINT(-122.34900 47.65100)', 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geography Methods](../../t-sql/spatial-geography/ogc-static-geography-methods.md)  
  
  
