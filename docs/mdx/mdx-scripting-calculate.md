---
description: "CALCULATE Statement (MDX)"
title: "CALCULATE Statement (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MDX Scripting - CALCULATE


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
 [MDX Scripting Fundamentals &#40;Analysis Services&#41;](/analysis-services/multidimensional-models/mdx/mdx-scripting-fundamentals-analysis-services)   
 [Define Assignments and Other Script Commands](/analysis-services/multidimensional-models/define-assignments-and-other-script-commands)  
  
