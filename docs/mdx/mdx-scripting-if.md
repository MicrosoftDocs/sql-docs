---
title: "IF Statement  (MDX) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# MDX Scripting - IF


  Executes a statement if the condition is true.  
  
## Syntax  
  
```  
  
IF expression THEN assignment END IF  
```  
  
## Arguments  
 *expression*  
 A Multidimensional Expressions (MDX) expression that evaluates to a Boolean that returns true or false.  
  
 *assignment*  
 An MDX expression that assigns a value to either a subcube or a calculated property.  
  
## Remarks  
 Use the IF statement for control flow, which is unlike the [IIf &#40;MDX&#41;](../mdx/iif-mdx.md) function and the [CASE Statement &#40;MDX&#41;](../mdx/case-statement-mdx.md) that can only be used to return values or objects.  
  
## Examples  
 In the following example, the scope is restricted to the Country level of the Customers Geography hierarchy in the Customers dimension. If the current measure is Internet Sales Amount, then the Internet Sales Amount is set to 10:  
  
 `SCOPE ([Customer].[Customer Geography].[Country].MEMBERS);`  
  
 `IF Measures.CurrentMember IS [Measures].[Internet Sales Amount] THEN this = 10 END IF;`  
  
 `END SCOPE`;  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
