---
title: "HasM (geography Data Type)"
description: "HasM (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "05/04/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "HasM_TSQL"
  - "HasM"
helpviewer_keywords:
  - "HasM geography"
dev_langs:
  - "TSQL"
---
# HasM (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns 1 (true) if a spatial object contains at least one M value; otherwise, it returns 0 (false).  
  
## Syntax  
  
```sql  
  
.HasM  
```  
  
## Return Types
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
CLR return type: **Boolean**  
  
## Remarks  
  
## Examples  
  
```sql  
DECLARE @p GEOGRAPHY = 'Point(1 1 1 1)'  
SELECT @p.HasM   
--Returns: 1 (true)  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)   
 [M &#40;geography Data Type&#41;](../../t-sql/spatial-geography/m-geography-data-type.md)  
  
  
