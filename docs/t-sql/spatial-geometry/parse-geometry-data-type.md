---
title: "Parse (geometry Data Type)"
description: "Parse (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "Parse (geometry Data Type)"
dev_langs:
  - "TSQL"
---
# Parse (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geometry** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation. `Parse()` is equivalent to [STGeomFromText()](../../t-sql/spatial-geometry/parse-geometry-data-type.md), with the exception that it assumes a spatial reference ID (SRID) of 0 as a parameter. The input may carry optional Z (elevation) and M (measure) values.
  
## Syntax  
  
```  
  
Parse ( 'geometry_tagged_text' )  
```  
  
## Arguments
 *geometry_tagged_text*  
 Is the WKT representation of the **geometry** instance you wish to return. *geometry_tagged_text* is an **nvarchar** expression.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 The OGC type of the **geometry** instance returned by `Parse()` is set to the corresponding WKT input.  
  
 The string 'Null' will be interpreted as a null **geometry** instance.  
  
 This method will throw a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `Parse()` to create a `geometry` instance.  
  
```sql
DECLARE @g geometry;   
SET @g = geometry::Parse('LINESTRING (100 100, 20 180, 180 180)');  
SELECT @g.ToString();  
```  
  
## See Also  
 [STGeomFromText](../../t-sql/spatial-geometry/parse-geometry-data-type.md)   
 [Extended Static Geometry Methods](../../t-sql/spatial-geometry/extended-static-geometry-methods.md)  
  
  

