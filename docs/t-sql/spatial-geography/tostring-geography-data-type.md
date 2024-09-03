---
title: "ToString (geography Data Type)"
description: "ToString (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ToString (geography Data Type)"
helpviewer_keywords:
  - "ToString method"
dev_langs:
  - "TSQL"
---
# ToString (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation of a **geography** instance augmented with any Z (elevation) and M (measure) values carried by the instance.  
  
 This geography data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.  
  
## Syntax  
  
```  
  
.ToString ()  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **nvarchar(max)**  
  
 CLR return type: **SqlString**  
  
## Remarks  
 This method returns the string "Null" when called on null instances. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], the set of possible results on the server has been extended to **FullGlobe** instances. This method will return the same value as `AsTextZM()`.  
  
 This method is not precise.  
  
## Examples  
 The following example create a `LineString` instance and uses `ToString()` to return the text description of the instance.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);  
SELECT @g.ToString();  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)   
 [AsTextZM &#40;geography Data Type&#41;](../../t-sql/spatial-geography/astextzm-geography-data-type.md)  
  
  
