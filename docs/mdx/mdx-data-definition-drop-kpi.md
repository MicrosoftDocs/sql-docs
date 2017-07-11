---
title: "DROP KPI Statement (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "KPI"
  - "DROP"
  - "DROP KPI"
  - "DROP_KPI"
helpviewer_keywords: 
  - "DROP KPI statement"
  - "key performance indicators [MDX]"
ms.assetid: d19c6809-b8a6-459d-8554-b41854f7cc45
caps.latest.revision: 11
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDX Data Definition - DROP KPI
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Drops the specified key performance indicator (KPI) from the specified cube.  
  
## Syntax  
  
```  
  
DROP KPI CURRENTCUBE | Cube_Name.KPI_Name   
```  
  
## Arguments  
 *Cube_Name*  
 A valid string that specifies the cube name.  
  
 *KPI_Name*  
 A valid string that specifies the name of the KPI that is to be dropped.  
  
## See Also  
 [CREATE KPI Statement &#40;MDX&#41;](../mdx/mdx-data-definition-create-kpi.md)   
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
