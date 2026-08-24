---
title: "STArea (geography Data Type)"
description: "STArea (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "STArea (geography Data Type)"
  - "STArea_TSQL"
helpviewer_keywords:
  - "STArea method"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# STArea (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns the total surface area of a **geography** instance. Results for STArea() are the squared unit of measure used by the **geography** instance's spatial reference identifier. For example, if the SRID of the instance is 4326, STArea() returns results in square meters.  
  
## Syntax  
  
```  
  
.STArea ( )  
```  
  
## Return Types
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **float**  
  
CLR return type: **SqlDouble**  
  
## Remarks  
STArea() returns 0 if a **geography** instance contains only zero- and one-dimensional figures, or if it's empty.  
  
> [!NOTE]  
>  Methods on the **geography** data type that produce a metric return value will have different results based on the SRID of the instance used in the method. For more information on SRIDs, see [Spatial Reference Identifiers &#40;SRIDs&#41;](../../relational-databases/spatial/spatial-reference-identifiers-srids.md).  
  
## Examples  
The following example uses `STArea()` to create a `Polygon geography` instance and computes the area of the polygon.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))', 4326);  
SELECT @g.STArea();  
```  
  
## Related content

- [OGC methods on geography instances](ogc-methods-on-geography-instances.md)
