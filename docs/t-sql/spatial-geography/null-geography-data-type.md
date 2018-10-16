---
title: "Null (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "Null (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Null (geography Data Type)"
  - "Null method"
ms.assetid: bb464b06-86e0-4b8b-ad78-04bd33b6069c
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Null (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

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
  
```  
DECLARE @g geography;   
SET @g = geography::[Null];  
SELECT @g  
```  
  
## See Also  
 [Extended Static Geography Methods](../../t-sql/spatial-geography/extended-static-geography-methods.md)  
  
  
