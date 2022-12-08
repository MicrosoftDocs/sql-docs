---
title: "AsTextZM (geometry Data Type)"
description: "AsTextZM (geometry Data Type)"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "AsTextZM_(geometry Data Type)"
  - "AsTextZM_(geometry_Data_Type)_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "AsTextZM (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.reviewer: ""
ms.custom: ""
ms.date: "08/03/2017"
---
# AsTextZM (geometry Data Type)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation of a geometry instance augmented with any **Z** (elevation) and **M** (measure) values carried by the instance.
  
## Syntax  
  
```sql  
.AsTextZM ()  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **nvarchar(max)**  
  
 CLR return type: **SqlChars**  
  
## Remarks  
  
## Examples  

The following example creates a `Point` instance that contains **Z** (elevation) and **M** (measure) values. `STAsText()` selects the WKT values, (1 2); `AsTextZM()` selects the same WKT values and also returns the values for **Z** and **M**, yielding (1 2 3 4).  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POINT(1 2 3 4)', 0);  
SELECT @g.STAsText();  
SELECT @g.AsTextZM();  
```  
  
## See Also  

- [Extended Methods on Geometry Instances](../../t-sql/spatial-geometry/extended-methods-on-geometry-instances.md)
- [M &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/m-geometry-data-type.md)
- [Z &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/z-geometry-data-type.md)