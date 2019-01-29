---
title: "IsNull (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "09/12/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "IsNull (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "IsNull (geometry Data Type)"
ms.assetid: f95813a5-26c0-48aa-bfb8-56d2a0980788
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# IsNull (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

The type of a **geometry** instance is null. Returns 0 if the instance is not null.
  
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
  
  

