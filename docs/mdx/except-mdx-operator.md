---
description: "Except (MDX) Operator"
title: "- (Except) (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Except (MDX) Operator


  Performs a set operation that returns the difference between two sets, removing duplicate members.  
  
## Syntax  
  
```  
  
Set_Expression - Set_Expression  
```  
  
#### Parameters  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
## Return Value  
 A set that contains members that are not shared by both specified parameters.  
  
## Remarks  
 The **- (Except)** operator is functionally equivalent to the [Except](../mdx/except-mdx-function.md) function.  
  
## Examples  
 The following example demonstrates the use of this operator:  
  
```  
// This query shows the quantity of orders for all product categories  
// with the exception of Components.  
SELECT   
   [Measures].[Order Quantity] ON COLUMNS,  
   [Product].[Product Categories].[All].Children   
   - [Product].[Product Categories].[Components] ON ROWS  
FROM  
    [Adventure Works]  
```  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
