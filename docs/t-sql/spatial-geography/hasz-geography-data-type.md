---
title: "HasZ (geography Data Type)"
description: "HasZ (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "05/04/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "HasZ"
  - "HasZ_TSQL"
helpviewer_keywords:
  - "HasZ geography"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# HasZ (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

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
DECLARE @p GEOGRAPHY = 'Point(1 1 1 1)'  
SELECT @p.HasZ   
--Returns: 1 (true)  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)   
 [Z &#40;geography Data Type&#41;](../../t-sql/spatial-geography/z-geography-data-type.md)  
  
  
