---
title: "STPointFromWKB (geometry Data Type)"
description: "STPointFromWKB (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STPointFromWKB (geometry Data Type)"
  - "STPointFromWKB_TSQL"
helpviewer_keywords:
  - "STPointFromWKB (geometry Data Type)"
dev_langs:
  - "TSQL"
---
# STPointFromWKB (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geometryPoint** instance from an Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation.
  
## Syntax  
  
```  
  
STPointFromWKB ( 'WKB_point' , SRID )  
```  
  
## Arguments
 *WKB_point*  
 Is the WKB representation of the **geometryPoint** instance you wish to return. *WKB_point* is a **varbinary(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geometryPoint** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
 OGC type: **Point**  
  
## Remarks  
 This method will throw a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `STPointFromWKB()` to create a `geometry` instance.  
  
```sql
DECLARE @g geometry;   
SET @g = geometry::STPointFromWKB(0x010100000000000000000059400000000000005940, 0);  
SELECT @g.STAsText();  
```  
  
## See Also  
 [OGC Static Geometry Methods](../../t-sql/spatial-geometry/ogc-static-geometry-methods.md)  
  
  

