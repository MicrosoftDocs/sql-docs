---
title: "STMLineFromText (geometry Data Type)"
description: "STMLineFromText (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
f1_keywords:
  - "STMLineFromText (geometry Data Type)"
  - "STMLineFromText_TSQL"
helpviewer_keywords:
  - "STMLineFromText (geometry Data Type)"
dev_langs:
  - "TSQL"
---
# STMLineFromText (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns a **geometry** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
STMLineFromText ( 'multilinestring_tagged_text' , SRID )  
```  
  
## Arguments
 *multilinestring_tagged_text*  
 Is the WKT representation of the **geometryMultiLineString** instance you wish to return. *multilinestring_tagged_text* is an **nvarchar(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geometryMultiLineString** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
 OGC type: **MultiLineString**  
  
## Remarks  
 This method will throw a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `STMLineFromText()` to create a `geometry` instance.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STMLineFromText('MULTILINESTRING ((100 100, 200 200), (3 4, 7 8, 10 10))', 0);  
  
SELECT @g.ToString();
```
  
## Related content

- [OGC Static Geometry Methods](ogc-static-geometry-methods.md)
