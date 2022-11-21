---
description: "Crossjoin  - MDX Operator Reference"
title: "* (Crossjoin) (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Crossjoin  - MDX Operator Reference


  Performs a set operation that returns the cross product of two sets.  
  
## Syntax  
  
```  
  
Set_Expression * Set_Expression  
```  
  
## Parameter  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
## Return Value  
 A set that contains the cross product of both specified parameters.  
  
## Remarks  
 The **\* (Crossjoin)** operator is functionally equivalent to the [Crossjoin](../mdx/crossjoin-mdx.md) function.  
  
## Examples  
 The following example demonstrates the use of this operator.  
  
```  
-- This query returns the gross profit margin for product types  
-- and reseller types crossjoined by year.  
SELECT   
    [Date].[Calendar].[Calendar Year].Members *  
    [Reseller].[Reseller Type].Children ON 0,  
    [Product].[Category].[Category].Members ON 1  
FROM  
    [Adventure Works]  
WHERE  
    ([Measures].[Gross Profit Margin])  
```  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
