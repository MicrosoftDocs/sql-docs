---
title: "STIsValid (geometry Data Type)"
description: "STIsValid (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STIsValid (geometry Data Type)"
  - "STIsValid_TSQL"
helpviewer_keywords:
  - "STIsValid (geometry Data Type)"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# STIsValid (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns true if a **geometry** instance is well-formed, based on its Open Geospatial Consortium (OGC) type. Returns false if a **geometry** instance is not well-formed.
  
## Syntax  
  
```  
  
.STIsValid ( )  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 The OGC type of a **geometry** instance can be determined by invoking [STGeometryType()](../../t-sql/spatial-geometry/stgeometrytype-geometry-data-type.md).  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] produces only valid **geometry** instances, but allows for the storage and retrieval of invalid instances. A valid instance representing the same point set of any invalid instance can be retrieved using the `MakeValid()` method.  
  
## Examples  
 The following example creates a `geometry` instance and uses `STIsValid()` to test if the instance is valid.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 2, 1 0)', 0);  
SELECT @g.STIsValid();  
```  
  
## See Also  
 [STGeometryType &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stgeometrytype-geometry-data-type.md)   
 [MakeValid &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/makevalid-geometry-data-type.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

