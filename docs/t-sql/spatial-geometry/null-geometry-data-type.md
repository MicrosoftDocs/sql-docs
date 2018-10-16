---
title: "Null (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "Null (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Null (geometry Data Type)"
ms.assetid: 67a4b019-9091-4443-85cc-f4836d0cb509
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Null (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

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
  
```  
DECLARE @g geometry;   
SET @g = geometry::[Null];  
SELECT @g  
```  
  
## See Also  
 [Extended Static Geometry Methods](../../t-sql/spatial-geometry/extended-static-geometry-methods.md)  
  
  

