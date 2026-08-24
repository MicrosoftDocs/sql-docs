---
title: "MakeValid (geometry Data Type)"
description: "MakeValid (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
f1_keywords:
  - "MakeValid"
  - "MakeValid_TSQL"
helpviewer_keywords:
  - "MakeValid (geometry Data Type)"
dev_langs:
  - "TSQL"
---
# MakeValid (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Converts an invalid **geometry** instance into a **geometry** instance with a valid Open Geospatial Consortium (OGC) type.
  
## Syntax  
  
```  
  
.MakeValid ()  
```  
  
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
  
## Related content

- [STIsValid (geometry Data Type)](stisvalid-geometry-data-type.md)
- [Extended methods on geometry instances](extended-methods-on-geometry-instances.md)
