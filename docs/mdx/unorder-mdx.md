---
description: "Unorder (MDX)"
title: "Unorder (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Unorder (MDX)


  Removes any enforced ordering from a specified set.  
  
## Syntax  
  
```  
  
Unorder(Set_Expression)   
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
## Remarks  
 The **Unorder** function removes any ordering imposed on the tuples contained in the set by any other function or statement, such as the [Order](../mdx/order-mdx.md) function. The ordering of the tuples in the set returned by the **Unorder** function is indeterminate.  
  
 The **Unorder** function is used as a hint to for query optimization for set processing. If the order of tuples within a set is unimportant to a calculation or query, using the **Unorder** function can provide a performance benefit in such cases. For example, the [NonEmpty (MDX)](../mdx/nonempty-mdx.md) function may perform better when the set provided to this function is unordered than if [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] needs to preserve order, although with [!INCLUDE[ssASCurrent](../includes/ssascurrent-md.md)], the query processor attempts to perform this function automatically for many functions, such as **Sum** and **Aggregate**. The performance benefit of using **Unorder** is only likely to be noticeable on very large sets consisting of millions of tuples.  
  
## Example  
 The following pseudo-code illustrates the syntax for this function.  
  
```  
NonEmpty (UnOrder (<set_expression>))  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
