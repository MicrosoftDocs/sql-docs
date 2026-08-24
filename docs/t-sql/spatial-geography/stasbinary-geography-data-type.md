---
title: "STAsBinary (geography Data Type)"
description: "STAsBinary (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "STAsBinary_TSQL"
  - "STAsBinary (geography Data Type)"
helpviewer_keywords:
  - "STAsBinary method"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# STAsBinary (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Returns the Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation of a **geography** instance.  
  
 This **geography** data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.  
  
## Syntax  
  
```
  
.STAsBinary ( )  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **varbinary(max)**  
  
 CLR return type: **SqlBytes**  
  
## Remarks  
 The OGC type of a **geography** instance can be determined by invoking [STGeometryType()](../../t-sql/spatial-geography/stgeometrytype-geography-data-type.md).  
  
## Examples  
 The following example uses `STAsBinary()` to create a `LineString``geography` instance from (-122.360, 47.656) to (-122.343, 47.656) from text. It then returns the result in WKB.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('LINESTRING( -122.360 47.656, -122.343 47.656)', 4326);  
SELECT @g.STAsBinary();  
```  
  
## Related content

- [OGC methods on geography instances](ogc-methods-on-geography-instances.md)
