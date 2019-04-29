---
title: "STIsEmpty (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STIsEmpty_TSQL"
  - "STIsEmpty (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STIsEmpty (geometry Data Type)"
ms.assetid: dcbd6ae1-5d63-485f-9d58-28bfd504524e
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STIsEmpty (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns 1 if a **geometry** instance is empty. Returns 0 if a **geometry** instance is not empty.
  
## Syntax  
  
```  
  
.STIsEmpty ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Examples  
 The following example creates an empty `geometry` instance and uses `STIsEmpty()` to test whether the instance is empty.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POLYGON EMPTY', 0);  
SELECT @g.STIsEmpty();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

