---
title: "CalculationCurrentPass (MDX) | Microsoft Docs"
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
  - "CALCULATIONCURRENTPASS"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "CalculationCurrentPass function"
ms.assetid: 7069f7a0-8ec8-4293-8db3-b35b9327f437
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# CalculationCurrentPass (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the current calculation pass of a cube for the specified query context.  
  
## Syntax  
  
```  
  
CalculationCurrentPass()  
```  
  
## Remarks  
 The **CalculationCurrentPass** function returns the zero-based index of the calculation pass for the current query context. With automatic recursion resolution in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], this function has little practical use.  
  
## See Also  
 [CalculationPassValue &#40;MDX&#41;](../mdx/calculationpassvalue-mdx.md)   
 [IIf &#40;MDX&#41;](../mdx/iif-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
