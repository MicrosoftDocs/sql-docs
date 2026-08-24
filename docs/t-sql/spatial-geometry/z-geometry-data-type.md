---
title: "Z (geometry Data Type)"
description: "Z (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
f1_keywords:
  - "Z (geometry Data Type)"
  - "Z_(geometry_Data_Type)_TSQL"
helpviewer_keywords:
  - "Z (geometry Data Type)"
dev_langs:
  - "TSQL"
---
# Z (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The Z (elevation) value of the instance. The semantics of the elevation value are user-defined.
  
## Syntax  
  
```  
  
.Z  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **float**  
  
 CLR type: **SqlDouble**  
  
## Remarks  
 The value of this property will be null if the geometry instance is not a point, as well as for any **Point** instance for which it is not set.  
  
 This property is read-only.  
  
 Z-coordinates are not used in any calculations made by the library and is not carried through any library calculations.  
  
## Examples  
 The following example creates a `Point` instance with Z (elevation) and M (measure) values and uses `Z` to fetch the Z value of the instance.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POINT(1 2 3 4)', 0);  
SELECT @g.Z;  
```  
  
## Related content

- [M (geometry Data Type)](m-geometry-data-type.md)
- [AsTextZM (geometry Data Type)](astextzm-geometry-data-type.md)
- [Extended methods on geometry instances](extended-methods-on-geometry-instances.md)
