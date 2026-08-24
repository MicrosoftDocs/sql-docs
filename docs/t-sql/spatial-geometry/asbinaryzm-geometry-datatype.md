---
title: "AsBinaryZM (geometry DataType)"
description: "AsBinaryZM (geometry DataType)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
helpviewer_keywords:
  - "AsBinaryZM geometry"
dev_langs:
  - "TSQL"
---
# AsBinaryZM (geometry DataType)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns the Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation of a **geometry** instance augmented with any **Z** (elevation) and **M** (measure) values carried by the instance.
  
## Syntax  
  
```  
  
.AsBinaryZM()  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **varbinary(max)**  
  
 CLR return type: **SqlBytes**  
  
## Remarks  
  
## Examples  
  
```sql  
DECLARE @g1 GEOMETRY = 'Point(1 1 2 3)';  
  
SELECT @g1.STAsBinary();  
-- Returns: 0x0101000000000000000000F03F000000000000F03F  
  
SELECT @g1.AsBinaryZM();  
--Returns: 0x01B90B0000000000000000F03F000000000000F03F00000000000000400000000000000840  
```  
  
## Related content

- [Extended methods on geometry instances](extended-methods-on-geometry-instances.md)
- [M (geometry Data Type)](m-geometry-data-type.md)
- [Z (geometry Data Type)](z-geometry-data-type.md)
