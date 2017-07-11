---
title: "HasZ (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "05/04/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "HasZ"
  - "HasZ_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "HasZ geography"
ms.assetid: 4c5e1669-a987-4dda-9ebf-f573ce615c34
caps.latest.revision: 7
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# HasZ (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns 1 (true) if a spatial object contains at least one Z value; otherwise, it returns 0 (false).  
  
## Syntax  
  
```  
  
.HasZ  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **Boolean**  
  
## Remarks  
  
## Examples  
  
```tsql  
DECLARE @p GEOGRAPHY = 'Point(1 1 1 1)'  
SELECT @p.HasZ   
--Returns: 1 (true)  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)   
 [Z &#40;geography Data Type&#41;](../../t-sql/spatial-geography/z-geography-data-type.md)  
  
  
