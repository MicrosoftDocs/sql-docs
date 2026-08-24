---
title: "STStartPoint (geometry Data Type)"
description: "STStartPoint (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "STStartPoint_TSQL"
  - "STStartPoint (geometry Data Type)"
helpviewer_keywords:
  - "STStartPoint (geometry Data Type)"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# STStartPoint (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns the start point of a **geometry** instance.
  
## Syntax  
  
```  
  
.STStartPoint ( )  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
 Open Geospatial Consortium (OGC) type: **Point**  
  
## Remarks  
 `STStartPoint()` is the equivalent of [STPointN](../../t-sql/spatial-geometry/stpointn-geometry-data-type.md) (1).  
  
## Examples  
 The following example creates a `LineString` instance and uses `STStartPoint()` to retrieve the start point of the instance.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 2, 1 0)', 0);  
SELECT @g.STStartPoint().ToString();  
```  
  
## Related content

- [STPointN (geometry Data Type)](stpointn-geometry-data-type.md)
- [OGC methods on geometry instances](ogc-methods-on-geometry-instances.md)
