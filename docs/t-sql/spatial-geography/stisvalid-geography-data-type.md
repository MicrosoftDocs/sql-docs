---
title: "STIsValid (geography Data Type)"
description: "STIsValid (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
helpviewer_keywords:
  - "STIsValid method (geography)"
dev_langs:
  - "TSQL"
---
# STIsValid (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Returns true if a **geography** instance is well-formed and recognized as a valid geography object based on its Open Geospatial Consortium (OGC) type. Returns false if a **geography** instance is not well-formed. This method is precise.  
  
 This geography data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.  
  
## Syntax  
  
```  
  
.STIsValid ( )  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 The OGC type of a **geography** instance can be determined by invoking [STGeometryType()](../../t-sql/spatial-geography/stgeometrytype-geography-data-type.md).  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] produces only valid **geography** instances, but allows for the storage and retrieval of invalid instances. A valid instance representing the same point set of an invalid instance can be retrieved using the `MakeValid()` method.  
  
## Examples  
 The following example creates a `geography` instance and uses `STIsValid()` to test if the instance is valid.  
  
```sql
DECLARE @g geography = geography::STGeomFromText('LINESTRING(0 0, 2 2, 1 0)', 4326);  
SELECT @g.STIsValid();  
DECLARE @g geography  
```  
  
## Related content

- [STGeometryType (geography Data Type)](stgeometrytype-geography-data-type.md)
- [MakeValid (geography Data Type)](makevalid-geography-data-type.md)
- [OGC methods on geography instances](ogc-methods-on-geography-instances.md)
