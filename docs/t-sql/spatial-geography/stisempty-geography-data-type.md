---
title: "STIsEmpty (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "STIsEmpty_TSQL"
  - "STIsEmpty (geography Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STIsEmpty method"
ms.assetid: 4cbc66e3-9035-4ecf-8f5a-6301f168c26c
caps.latest.revision: 12
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# STIsEmpty (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns 1 if a **geography** instance is empty. Returns 0 if a **geography** instance is not empty.  
  
## Syntax  
  
```  
  
.STIsEmpty ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Examples  
 The following example creates an empty `geography` instance and uses `STIsEmpty()` to verify that the instance is empty.  
  
```  
DECLARE @g geography;  
SET @g = geography::STGeomFromText('POLYGON EMPTY', 4326);  
SELECT @g.STIsEmpty();  
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  