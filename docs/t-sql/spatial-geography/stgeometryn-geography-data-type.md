---
title: "STGeometryN (geography Data Type)"
description: "STGeometryN (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
f1_keywords:
  - "STGeometryN (geography Data Type)"
helpviewer_keywords:
  - "STGeometryN method"
dev_langs:
  - "TSQL"
---
# STGeometryN (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Returns a specified **geography** element in a **GeometryCollection** or one of its subtypes. When STGeometryN() is used on a subtype of a **GeometryCollection**, such as **MultiPoint** or **MultiLineString**, this method returns the **geography** instance if called with N=1.  
  
## Syntax  
  
```  
  
.STGeometryN ( expression )  
```  
  
## Arguments
 *expression*  
 Is an **int** expression between 1 and the number of **geography** instances in the **GeometryCollection**.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 This method returns null if the parameter is larger than the result of [STNumGeometries()](../../t-sql/spatial-geography/stnumgeometries-geography-data-type.md) and will throw an **ArgumentOutOfRangeException** if the *expression* parameter is less than 1.  
  
## Examples  
 The following example creates a `MultiPoint``geography` instance and uses `STGeometryN()` to find the second `geography` instance of the **GeometryCollection**.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('MULTIPOINT(-122.360 47.656, -122.343 47.656)', 4326);  
SELECT @g.STGeometryN(2).ToString();  
```  
  
## Related content

- [OGC methods on geography instances](ogc-methods-on-geography-instances.md)
