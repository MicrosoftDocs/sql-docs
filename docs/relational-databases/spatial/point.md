---
title: "Point"
description: "Point is a 0-dimensional object representing a single location and can contain Z (elevation) and M (measure) values in SQL Server spatial data."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mlandzic, jovanpop
ms.date: 11/04/2024
ms.service: sql
ms.topic: language-reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "Point geometry subtype [SQL Server]"
  - "geometry data type [SQL Server], spatial data"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Point
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabric SQL endpoint Fabric DW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]  

  In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] spatial data, a **Point** is a 0-dimensional object representing a single location and can contain Z (elevation) and M (measure) values.  
  
## Geography data type

 The **Point** type for the **geography** data type represents a single location where *Lat* represents latitude and *Long* represents longitude. The values for latitude and longitude are measured in degrees. Values for latitude always lie in the interval [-90, 90], and values that are inputted outside this range will throw an exception. Values for longitude always lie in the interval (-180, 180], and values inputted outside this range are wrapped around to fit in this range. For example, if 190 is inputted for longitude, then it will be wrapped to the value -170. *SRID* represents the spatial reference ID of the **geography** instance that you wish to return.  
  
## Geometry data type

 The **Point** type for the **geometry** data type represents a single location where *X* represents the X-coordinate of the Point being generated and *Y* represents the Y-coordinate of the Point being generated. *SRID* represents the spatial reference ID of the **geometry** instance that you wish to return.  
  
## Examples

### Example A.

The following example creates a geometry Point instance representing the point `(3, 4)` with an SRID of `0`.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POINT (3 4)', 0);  
```  
  
### Example B.

The following example creates a geometry Point instance representing the point (3, 4) with a Z (elevation) value of 7, an M (measure) value of 2.5, and the default SRID of 0.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::Parse('POINT(3 4 7 2.5)');  
```  
  
### Example C.

The following example returns the X, Y, Z, and M values for the geometry Point instance.  
  
```sql
SELECT @g.STX;  
SELECT @g.STY;  
SELECT @g.Z;  
SELECT @g.M;  
```  
  
### Example D.

Z and M values can be explicitly specified as `NULL`, as shown in the following example.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::Parse('POINT(3 4 NULL NULL)');  
```

## Related content

- [MultiPoint](multipoint.md)
- [STX (geometry Data Type)](../../t-sql/spatial-geometry/stx-geometry-data-type.md)
- [STY (geometry Data Type)](../../t-sql/spatial-geometry/sty-geometry-data-type.md)
- [Spatial Data](spatial-data-sql-server.md)
