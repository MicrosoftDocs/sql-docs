---
title: "KPIWeight (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "KPIWeight function"
ms.assetid: 6d585c76-b4c4-437f-8433-cfa4e6057af1
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# KPIWeight (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the weight of the specified Key Performance Indicator (KPI).  
  
## Syntax  
  
```  
  
KPIWeight(KPI_Name)  
```  
  
## Arguments  
 *KPI_Name*  
 A valid string expression that specifies the name of the KPI.  
  
## Remarks  
 The value returned is the contribution of the KPI to the parent.  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
