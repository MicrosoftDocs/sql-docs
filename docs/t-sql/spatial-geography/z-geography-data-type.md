---
title: "Z (geography Data Type)"
description: "Z (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
f1_keywords:
  - "Z (geography Data Type)"
  - "Z_(geography_Data_Type)_TSQL"
helpviewer_keywords:
  - "Z method"
dev_langs:
  - "TSQL"
---
# Z (geography Data Type)
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
 The value of this property is null if the **geography** instance is not a point, as well as for any **Point** instance for which it is not set.  
  
 This property is read-only.  
  
 Z-coordinates are not used in any calculations made by the library and are not carried through any library calculations.  
  
## Examples  
 The following example creates a `Point` instance with Z (elevation) and M (measure) values and uses `Z` to fetch the Z value of the instance.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('POINT(-122.34900 47.65100 10.3 12)', 4326);  
SELECT @g.Z;  
```  
  
## Related content

- [Extended methods on geography instances](extended-methods-on-geography-instances.md)
- [M (geography Data Type)](m-geography-data-type.md)
- [AsTextZM (geography Data Type)](astextzm-geography-data-type.md)
