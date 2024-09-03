---
title: "Null (geography Data Type)"
description: "Null (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "07/30/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "Null (geography Data Type)"
helpviewer_keywords:
  - "Null (geography Data Type)"
  - "Null method"
dev_langs:
  - "TSQL"
---
# Null (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

A read-only property providing a null instance of the **geography** type.
  
## Syntax  
  
```  
  
Null  
```  

## Arguments
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **geography**  
  
 CLR type: **SqlGeography**  
  
## Remarks  
  
## Examples  
 The following example retrieves a null `geography` instance.  
  
```sql
DECLARE @g geography;   
SET @g = geography::[Null];  
SELECT @g  
```  
  
## See Also  
 [Extended Static Geography Methods](../../t-sql/spatial-geography/extended-static-geography-methods.md)  
  
  
