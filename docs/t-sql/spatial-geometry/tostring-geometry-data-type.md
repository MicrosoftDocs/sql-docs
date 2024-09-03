---
title: "ToString (geometry Data Type)"
description: "ToString (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ToString (geometry Data Type)"
helpviewer_keywords:
  - "ToString (geometry Data Type)"
dev_langs:
  - "TSQL"
---
# ToString (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation of a geometry instance augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
.ToString ()  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **nvarchar(max)**  
  
 CLR return type: **SqlString**  
  
## Remarks  
 This method will return the string "Null" when called on null instances.  
  
 On non-null instances, this method is equivalent to using `AsTextZM().`  
  
## Examples  
 The following example create a `LineString` instance and uses `ToString()` to fetch the text description of the instance.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 0 1, 1 0)', 0);  
SELECT @g.ToString();  
```  
  
## See Also  
 [STAsText &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stastext-geometry-data-type.md)   
 [Extended Methods on Geometry Instances](../../t-sql/spatial-geometry/extended-methods-on-geometry-instances.md)  
  
  

