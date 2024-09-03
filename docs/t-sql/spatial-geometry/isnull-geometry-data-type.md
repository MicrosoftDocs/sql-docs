---
title: "IsNull (geometry Data Type)"
description: "IsNull (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "09/12/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "IsNull (geometry Data Type)"
helpviewer_keywords:
  - "IsNull (geometry Data Type)"
dev_langs:
  - "TSQL"
---
# IsNull (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The type of a **geometry** instance is null. Returns 0 if the instance isn't null.
  
## Syntax  
  
```  
.IsNull  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **bit**  
  
 CLR type: **SqlBoolean**  
  
## Remarks  
 `IsNull` can be used to test whether a **geometry** instance is null. `IsNull` returns 0 if the instance isn't null, but null if the instance is null.  
  
 This method is primarily used by the SQL Server infrastructure; it isn't recommended that you use `IsNull` to test whether an instance is null.  
  

## See Also  
 [Extended Methods on Geometry Instances](../../t-sql/spatial-geometry/extended-methods-on-geometry-instances.md)  
  
  

