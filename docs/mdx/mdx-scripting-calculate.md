---
title: "CALCULATE Statement (MDX) | Microsoft Docs"
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
  - "CALCULATE"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "CALCULATE statement"
ms.assetid: 41e196a1-d49e-487b-a42a-73e5d441ed1b
caps.latest.revision: 42
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDX Scripting - CALCULATE
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Populates each cell in a cube with an aggregate value.  
  
## Syntax  
  
```  
  
CALCULATE  
```  
  
## Arguments  
 None  
  
## Remarks  
 The CALCULATE statement is automatically included as the first statement in a cube's MDX script when you create a cube by using [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. The CALCULATE statement tells each cell in the cube to aggregate from lower granularity cells. After a cell is aggregated, if you subsequently populate lower granularity cells by using expressions, it impacts the aggregated value of higher granularity cells. You almost always want this aggregation to happen, but you can remove it or cause other statements to execute before this statement.  
  
 The CALCULATE statement cannot be included in a nested subcube within the MDX script. A nested subcube is defined by using the SCOPE statement. For more information about the SCOPE statement, see [SCOPE Statement &#40;MDX&#41;](../mdx/mdx-scripting-scope.md).  
  
> [!NOTE]  
>  Calculated members are not aggregated.  
  
## See Also  
 [MDX Scripting Statements &#40;MDX&#41;](../mdx/mdx-scripting-statements-mdx.md)   
 [MDX Scripting Fundamentals &#40;Analysis Services&#41;](../analysis-services/multidimensional-models/mdx/mdx-scripting-fundamentals-analysis-services.md)   
 [Define Assignments and Other Script Commands](../analysis-services/multidimensional-models/define-assignments-and-other-script-commands.md)  
  
  
