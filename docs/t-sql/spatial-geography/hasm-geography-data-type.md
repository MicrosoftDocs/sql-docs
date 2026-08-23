---
title: "HasM (geography Data Type)"
description: "HasM (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "05/04/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "HasM_TSQL"
  - "HasM"
helpviewer_keywords:
  - "HasM geography"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# HasM (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

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
  
## Related content

- [Extended methods on geography instances](extended-methods-on-geography-instances.md)
- [M (geography Data Type)](m-geography-data-type.md)
