---
title: "STLength (geography Data Type)"
description: "STLength (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STLength (geography Data Type)"
  - "STLength_TSQL"
helpviewer_keywords:
  - "STLength method"
dev_langs:
  - "TSQL"
---
# STLength (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the total length of the elements in a **geography** instance or the **geography** instances within a **GeometryCollection**.  
  
## Syntax  
  
```  
  
.STLength ( )  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **float**  
  
 CLR return type: **SqlDouble**  
  
## Remarks  
 If a **geography** instance is closed, its length is calculated as the total length around the instance; the length of any polygon is its perimeter, and the length of a point is 0. The length of a **GeometryCollection** is found by calculating the sum of the lengths of all of the **geography** instances contained within the collection.  
  
 STLength() works on both valid and invalid LineStrings. Typically a LineString is invalid due to overlapping segments, which may be caused by anomalies such as inaccurate GPS traces. STLength() does not remove overlapping or invalid segments. It includes overlapping and invalid segments in the length value that it returns. The MakeValid() method can remove overlapping segments from a LineString.  
  
## Examples  
 The following example creates a `LineString` instance and uses `STLength()` to find the length of the instance.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);  
SELECT @g.STLength();  
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
