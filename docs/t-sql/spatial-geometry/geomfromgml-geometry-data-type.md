---
title: GeomFromGml (geometry Data Type)
description: "GeomFromGml (geometry Data Type)"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "GeomFromGML_TSQL"
  - "GeomFromGML"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "GeomFromGML (geometry Data Type)"
author: MladjoA
ms.author: mlandzic 
ms.reviewer: ""
ms.custom: ""
ms.date: "08/03/2017"
---

# GeomFromGml (geometry Data Type)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Construct a **geometry** instance given a representation in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] subset of the Geography Markup Language (GML).
  
For more information on the Geography Markup Language, see the following Open Geospatial Consortium Specifications:
  
[OGC Specifications, Geography Markup Language](https://go.microsoft.com/fwlink/?LinkId=93629)
  
## Syntax  
  
```  
  
GeomFromGml ( GML_input, SRID )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *GML_input*  
 Is an XML input from which the GML will return a value.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geometry** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 This method will throw a **FormatException** if the input is not well-formatted.  
  
## Examples  
 The following example uses `GeomFromGml()` to create a `geometry` instance.  
  
```sql
DECLARE @g geometry;  
DECLARE @x xml;  
SET @x = '<LineString xmlns="https://www.opengis.net/gml"> <posList>100 100 20 180 180 180</posList> </LineString>';  
SET @g = geometry::GeomFromGml(@x, 0);  
SELECT @g.ToString();  
```  
  
## See Also  
 [Extended Static Geometry Methods](../../t-sql/spatial-geometry/extended-static-geometry-methods.md)  
  
  

