---
description: "IsNull (geography Data Type)"
title: "IsNull (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "IsNull (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "IsNull method"
ms.assetid: c031074f-bfda-4584-a3bf-4e7c324f237f
author: MladjoA
ms.author: mlandzic 
---
# IsNull (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  A property that specifies if the **geography** instance is null. Returns 'TRUE' if the instance is null; returns 0 if the instance is not null.  
  
## Syntax  
  
```  
  
.IsNull  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **bit**  
  
 CLR type: **SqlBoolean**  
  
## Remarks  
 `IsNull` can be used to test whether a **geography** instance is null. This can produce somewhat confusing results, returning 0 if the instance is not null, but null if the instance is null.  
  
 This method is primarily used by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] infrastructure; it is recommended that you use the T-SQL predicate IS NULL to test whether a **geography** instance is null. For more information on the T-SQL predicate IS NULL, see [IS NULL &#40;Transact-SQL&#41;](../../t-sql/queries/is-null-transact-sql.md).  
  
## Examples  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)  
  
  
