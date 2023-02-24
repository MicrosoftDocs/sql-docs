---
description: "STGeomCollFromText (geometry Data Type)"
title: "STGeomCollFromText (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STGeomCollFromText_TSQL"
  - "STGeomCollFromText (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STGeomCollFromText (geometry Data Type)"
ms.assetid: 19e757b3-cb2e-4852-87b9-40a815ab707e
author: MladjoA
ms.author: mlandzic 
---
# STGeomCollFromText (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geometry** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
STGeomCollFromText ( 'geometrycollection_tagged_text' , SRID )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *geometrycollection_tagged_text*  
 Is the WKT representation of the **geometry** instance you wish to return. *geometry_tagged_text* is an **nvarchar(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geometry** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 The OGC type of the **geometry** instance returned by `STGeomCollFromText()` is set to the corresponding WKT input.  
  
 This method will throw an exception if the input is not valid.  
  
## Examples  
 The following example uses `STGeomCollFromText()` to create a geometry instance.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomCollFromText('GEOMETRYCOLLECTION ( POLYGON((5 5, 10 5, 10 10, 5 5)), POINT(10 10) )', 0);  
SELECT @g.ToString();  
```  
  
## See Also  
 [OGC Static Geometry Methods](../../t-sql/spatial-geometry/ogc-static-geometry-methods.md)  
  
  

