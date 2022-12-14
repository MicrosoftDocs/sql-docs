---
description: "STLineFromText (geometry Data Type)"
title: "STLineFromText (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "10/11/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STLineFromText (geometry Data Type)"
  - "STLineFromText_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STLineFromText (geometry Data Type)"
ms.assetid: 430508ad-207b-4dee-a4d1-4ddf25e6b4a9
author: MladjoA
ms.author: mlandzic 
---
# STLineFromText (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a **geometry** instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
STLineFromText ( 'linestring_tagged_text' , SRID )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *linestring_tagged_text*  
 Is the WKT representation of the **geometryLineString** instance you want to return. *linestring_tagged_text* is an **nvarchar(max)** expression.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geometryLineString** instance you want to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
 OGC type: **LineString**  
  
## Remarks  
This method throws a **FormatException** if the input isn't well-formatted. Three-dimension and measured geometry WKT notation from the Open Geospatial Consortium (OGC) Simple Features for SQL Specification version 1.2.1 aren't supported. See examples for the supported representation of Z (elevation) and M (measure) values.
  
## Examples  
 The following examples use `STLineFromText()` to create a `geometry` instance.

### Example 1: Two-dimension geometry WKT
```sql
DECLARE @g geometry;  
SET @g = geometry::STLineFromText('LINESTRING (100 100, 200 200)', 0);  
SELECT @g.ToString();  
```  
  
### Example 2: Three-dimension geometry WKT
```sql
DECLARE @g geometry;  
SET @g = geometry::STLineFromText('LINESTRING (100 100 100, 200 200 200)', 0);  
SELECT @g.ToString();  
``` 

### Example 3: Two-dimension measured geometry WKT
```sql
DECLARE @g geometry;  
SET @g = geometry::STLineFromText('LINESTRING (100 100 NULL 100, 200 200 NULL 200)', 0);  
SELECT @g.ToString();  
``` 

### Example 4: Three-dimension measured geometry WKT
```sql
DECLARE @g geometry;  
SET @g = geometry::STLineFromText('LINESTRING (100 100 100 100, 200 200 200 200)', 0);  
SELECT @g.ToString();  
``` 
## See Also  
 [OGC Static Geometry Methods](../../t-sql/spatial-geometry/ogc-static-geometry-methods.md)  
  
  

