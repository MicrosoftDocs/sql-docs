---
description: "Null (geography Data Type)"
title: "Null (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "Null (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Null (geography Data Type)"
  - "Null method"
ms.assetid: bb464b06-86e0-4b8b-ad78-04bd33b6069c
author: MladjoA
ms.author: mlandzic 
---
# Null (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

A read-only property providing a null instance of the **geography** type.
  
## Syntax  
  
```  
  
Null  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
  
