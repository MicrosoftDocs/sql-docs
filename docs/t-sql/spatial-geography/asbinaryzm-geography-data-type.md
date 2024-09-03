---
title: "AsBinaryZM (geography Data Type)"
description: "AsBinaryZM (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "AsBinaryZM"
  - "AsBinaryZM_TSQL"
helpviewer_keywords:
  - "AsBinaryZM, geography"
  - "AsBinaryZM"
dev_langs:
  - "TSQL"
---
# AsBinaryZM (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

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
DECLARE @g1 GEOGRAPHY = 'Point(1 1 2 3)';  
  
SELECT @g1.STAsBinary();  
-- Returns: 0x0101000000000000000000F03F000000000000F03F  
  
SELECT @g1.AsBinaryZM();  
--Returns: 0x01B90B0000000000000000F03F000000000000F03F00000000000000400000000000000840  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)   
 [M &#40;geography Data Type&#41;](../../t-sql/spatial-geography/m-geography-data-type.md)   
 [Z &#40;geography Data Type&#41;](../../t-sql/spatial-geography/z-geography-data-type.md)  
  
  
