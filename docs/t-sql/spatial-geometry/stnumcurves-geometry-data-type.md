---
title: "STNumCurves (geometry Data Type)"
description: "STNumCurves (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "STNumCurves method (geometry)"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# STNumCurves (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

This method returns the number of curves in a **geometry** instance when the instance is a one-dimensional spatial data type. One-dimensional spatial data types include **LineString**, **CircularString**, and **CompoundCurve**. `STNumCurves`() works only on simple types; it does not work with **geometry** collections like **MultiLineString**.
  
## Syntax  
  
```  
  
.STNumCurves()  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
 An empty one-dimensional **geometry** instance returns 0. **NULL** is returned when the **geometry** instance is not a one-dimensional instance or is an uninitialized instance.  
  
## Examples  
  
### A. Using STNumCurves() on a CircularString instance  
 The following example shows how to get the number of curves in a `CircularString` instance:  
  
```sql
 DECLARE @g geometry;  
 SET @g = geometry::Parse('CIRCULARSTRING(10 0, 0 10, -10 0, 0 -10, 10 0)');  
 SELECT @g.STNumCurves();
 ```  
  
### B. Using STNumCurves() on a CompoundCurve instance  
 The following example uses `STNumCurves()` to return the number of curves in a `CompoundCurve` instance.  
  
```sql
 DECLARE @g geometry;  
 SET @g = geometry::Parse('COMPOUNDCURVE(CIRCULARSTRING(10 0, 0 10, -10 0, 0 -10, 10 0))');  
 SELECT @g.STNumCurves();
 ```  
  
## Related content

- [Spatial Data Types Overview](../../relational-databases/spatial/spatial-data-types-overview.md)
- [OGC methods on geometry instances](ogc-methods-on-geometry-instances.md)
