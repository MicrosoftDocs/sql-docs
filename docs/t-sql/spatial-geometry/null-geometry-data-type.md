---
title: "Null (geometry Data Type)"
description: "Null (geometry Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "08/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "Null (geometry Data Type)"
helpviewer_keywords:
  - "Null (geometry Data Type)"
dev_langs:
  - "TSQL"
---
# Null (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

A read-only property providing a null instance of the **geometry** type.
  
## Syntax  
  
```  
  
Null  
```  
  
## Arguments
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **geometry**  
  
 CLR type: **SqlGeometry**  
  
## Remarks  
  
## Examples  
 The following example retrieves a null `geometry` instance.  
  
```sql
DECLARE @g geometry;   
SET @g = geometry::[Null];  
SELECT @g  
```  
  
## See Also  
 [Extended Static Geometry Methods](../../t-sql/spatial-geometry/extended-static-geometry-methods.md)  
  
  

