---
title: "HasZ (geometry DataType)"
description: "HasZ (geometry DataType)"
author: MladjoA
ms.author: mlandzic
ms.date: "05/05/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "HasZ geometry"
dev_langs:
  - "TSQL"
---
# HasZ (geometry DataType)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns 1 (true) if a spatial object contains at least one Z value; otherwise, it returns 0 (false).  
  
## Syntax  
  
```  
  
.HasZ  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **Boolean**  
  
## Remarks  
  
## Examples  
  
```sql  
DECLARE @p GEOMETRY = 'Point(1 1 1 1)'  
SELECT @p.HasZ   
--Returns: 1 (true)  
```  
  
## See Also  
 [Extended Methods on Geometry Instances](../../t-sql/spatial-geometry/extended-methods-on-geometry-instances.md)   
 [Z &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/z-geometry-data-type.md)  
  
  
