---
title: "STPolyFromText (geometry Data Type)"
description: "STPolyFromText (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
f1_keywords:
  - "STPolyFromText_TSQL"
  - "STPolyFromText (geometry Data Type)"
helpviewer_keywords:
  - "STPolyFromText (geometry Data Type)"
dev_langs:
  - "TSQL"
---
# STPolyFromText (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns a **geometry** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
STPolyFromText ( 'polygon_tagged_text' , SRID )  
```  
  
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
  
## Related content

- [OGC Static Geometry Methods](ogc-static-geometry-methods.md)
