---
title: "Point (geography Data Type)"
description: "Point (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "10/10/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "Point"
  - "Point_TSQL"
helpviewer_keywords:
  - "Point method"
  - "Point (geography Data Type)"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Point (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Constructs a **geography** instance representing a **Point** instance from its latitude and longitude values and a spatial reference ID (SRID).
  
## Syntax  
  
```  
  
Point ( Lat, Long, SRID )  
```  
  
## Arguments
 *Lat*  
 Is a **float** expression representing the y-coordinate of the **Point** being generated.  
  
 *Long*  
 Is a **float** expression representing the x-coordinate of the **Point** being generated. For more information on valid latitude and longitude values, see [Point](../../relational-databases/spatial/point.md).  
  
 *SRID*  
 Is an **int** expression representing the [Spatial Reference Identifier](../../relational-databases/spatial/spatial-reference-identifiers-srids.md) of the **geography** instance you wish to return.  
  
> [!NOTE]  
>  Arguments for the Point (geography Data Type) method have coordinates reversed compared to WKT.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Examples  
 The following example uses `Point()` to create a `geography` instance.  
  
```sql
DECLARE @g geography;   
SET @g = geography::Point(47.65100, -122.34900, 4326)  
SELECT @g.ToString();  
```  
  
## Related content

- [Extended Static Geography Methods](extended-static-geography-methods.md)
