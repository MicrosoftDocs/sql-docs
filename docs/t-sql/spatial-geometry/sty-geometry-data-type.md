---
title: "STY (geometry Data Type)"
description: "STY provides the Y-coordinate property of a Point instance."
author: MladjoA
ms.author: mlandzic
ms.date: "12/06/2024"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "STY_TSQL"
  - "STY (geometry Data Type)"
helpviewer_keywords:
  - "STY (geometry Data Type)"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# STY (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The Y-coordinate property of a **Point** instance.
  
## Syntax  
  
```syntaxsql
.STY  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **float**  
  
 CLR type: **SqlDouble**  
  
## Remarks  
 
 The value of this property will be null if the geometry instance is NOT a point. This property is read-only.
 
## Examples
 The following example creates a `Point` instance and uses `STY` to retrieve the Y-coordinate of the instance.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POINT(3 8)', 0);  
SELECT @g.STY;  
```  
  
## Related content
 [STX &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stx-geometry-data-type.md)   
 [STSrid &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stsrid-geometry-data-type.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  
