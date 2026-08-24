---
title: "IsNull (geography Data Type)"
description: "IsNull (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "IsNull (geography Data Type)"
helpviewer_keywords:
  - "IsNull method"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# IsNull (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  A property that specifies if the **geography** instance is null. Returns 'TRUE' if the instance is null; returns 0 if the instance is not null.  
  
## Syntax  
  
```  
  
.IsNull  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **bit**  
  
 CLR type: **SqlBoolean**  
  
## Remarks  
 `IsNull` can be used to test whether a **geography** instance is null. This can produce somewhat confusing results, returning 0 if the instance is not null, but null if the instance is null.  
  
 This method is primarily used by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] infrastructure; it is recommended that you use the T-SQL predicate IS NULL to test whether a **geography** instance is null. For more information on the T-SQL predicate IS NULL, see [IS NULL &#40;Transact-SQL&#41;](../../t-sql/queries/is-null-transact-sql.md).  
  
## Examples  
  
## Related content

- [Extended methods on geography instances](extended-methods-on-geography-instances.md)
