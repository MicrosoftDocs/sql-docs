---
description: "Null (geometry Data Type)"
title: "Null (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "Null (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Null (geometry Data Type)"
ms.assetid: 67a4b019-9091-4443-85cc-f4836d0cb509
author: MladjoA
ms.author: mlandzic 
---
# Null (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

A read-only property providing a null instance of the **geometry** type.
  
## Syntax  
  
```  
  
Null  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
  

