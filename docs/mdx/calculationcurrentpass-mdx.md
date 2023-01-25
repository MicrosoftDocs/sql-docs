---
description: "CalculationCurrentPass (MDX)"
title: "CalculationCurrentPass (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# CalculationCurrentPass (MDX)


  Returns the current calculation pass of a cube for the specified query context.  
  
## Syntax  
  
```  
  
CalculationCurrentPass()  
```  
  
## Remarks  
 The **CalculationCurrentPass** function returns the zero-based index of the calculation pass for the current query context. With automatic recursion resolution, this function has little practical use.  
  
## See Also  
 [CalculationPassValue &#40;MDX&#41;](../mdx/calculationpassvalue-mdx.md)   
 [IIf &#40;MDX&#41;](../mdx/iif-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
