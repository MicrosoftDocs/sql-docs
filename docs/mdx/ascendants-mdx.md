---
title: "Ascendants (MDX) | Microsoft Docs"
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
# Ascendants (MDX)


  Returns the set of the ascendants of a specified member, including the member itself.  
  
## Syntax  
  
```  
  
Ascendants(Member_Expression)  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 The **Ascendants** function returns all of the ancestors of a member from the member itself up to the top of the member's hierarchy; more specifically, it performs a post-order traversal of the hierarchy for the specified member, and then returns all ascendant members related to the member, including itself, in a set. This is in contrast to the [Ancestor](../mdx/ancestor-mdx.md) function, which returns a specific ascendant member, or ancestor, at a specific level.  
  
## Examples  
 The following example returns the count of reseller orders for the `[Sales Territory].[Northwest]` member and all the ascendants of that member from the **Adventure Works** cube. The **Ascendants** function constructs the set that includes the `[Sales Territory].[Northwest]` member and its ascendants for the ROWS axis.  
  
```  
SELECT  
   Measures.[Reseller Order Count] ON COLUMNS,  
   Order(  
      Ascendants(  
         [Sales Territory].[Northwest]  
      ),  
      DESC  
   ) ON ROWS  
FROM  
   [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
