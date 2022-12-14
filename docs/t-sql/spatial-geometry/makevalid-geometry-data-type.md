---
description: "MakeValid (geometry Data Type)"
title: "MakeValid (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "MakeValid"
  - "MakeValid_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MakeValid (geometry Data Type)"
ms.assetid: 38673010-ab77-4ebb-9c4d-b26b79e3b7ea
author: MladjoA
ms.author: mlandzic 
---
# MakeValid (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Converts an invalid **geometry** instance into a **geometry** instance with a valid Open Geospatial Consortium (OGC) type.
  
## Syntax  
  
```  
  
.MakeValid ()  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 This method may cause a change in the type of the **geometry** instance, as well as cause the points of a **geometry** instance to shift slightly.  
  
## Examples  
 The first example creates an invalid `LineString` instance that overlaps itself and uses `STIsValid()` to confirm that it is an invalid instance. `STIsValid()` returns the value of 0 for an invalid instance.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 2, 1 1, 1 0, 1 1, 2 2)', 0);  
SELECT @g.STIsValid();  
```  
  
 The second example uses `MakeValid()` to make the instance valid and to test that the instance is indeed valid. `STIsValid()` returns the value of 1 for a valid instance.  
  
```sql
SET @g = @g.MakeValid();  
SELECT @g.STIsValid();  
```  
  
 The third example verifies how the instance has been changed to make it a valid instance.  
  
```sql
SELECT @g.ToString();  
```  
  
 In this example, when the `LineString` instance is selected, the values are returned as a valid `MultiLineString` instance.  
  
```
MULTILINESTRING ((0 2, 1 1, 2 2), (1 1, 1 0))  
```  
  
 The following example converts the CircularString instance into a Point instance.  
  
```sql
DECLARE @g geometry = 'CIRCULARSTRING(1 1, 1 1, 1 1)';  
SELECT @g.MakeValid().ToString();  
```  
  
## See Also  
 [STIsValid &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stisvalid-geometry-data-type.md)   
 [Extended Methods on Geometry Instances](../../t-sql/spatial-geometry/extended-methods-on-geometry-instances.md)  
  
  

