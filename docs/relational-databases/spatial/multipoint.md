---
title: "MultiPoint"
description: "MultiPoint is a collection of zero or more points. The boundary of a **MultiPoint** instance is empty in SQL Database Engine spatial data."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mlandzic, jovanpop
ms.date: 11/04/2024
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "MultiPoint geometry subtype [SQL Server]"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# MultiPoint
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabric SQL endpoint Fabric DW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]  

  A **MultiPoint** is a collection of zero or more points. The boundary of a **MultiPoint** instance is empty.  
  
## Examples

### Example A.
The following example creates a `geometry MultiPoint` instance with SRID 23 and two points: one point with the coordinates (2, 3), one point with the coordinates (7, 8), and a Z value of 9.5.  
  
```sql  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('MULTIPOINT((2 3), (7 8 9.5))', 23);  
```  
  
### Example B.
The following example expresses the `MultiPoint` instance using `STMPointFromText()`.  
  
```sql  
DECLARE @g geometry;
SET @g = geometry::STMPointFromText('MULTIPOINT((2 3), (7 8 9.5))', 23);  
```  
  
### Example C.
The following example uses the method `STGeometryN()` to retrieve a description of the first point in the collection.  
  
```sql  
SELECT @g.STGeometryN(1).STAsText();  
```  
  
## Related content

- [Point](point.md)
- [Spatial Data](spatial-data-sql-server.md)
