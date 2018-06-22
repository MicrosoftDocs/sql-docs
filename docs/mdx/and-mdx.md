---
title: "AND (MDX) | Microsoft Docs"
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
# AND (MDX)


  Performs a logical conjunction on two numeric expressions.  
  
## Syntax  
  
```  
  
Expression1 AND Expression2  
```  
  
#### Parameters  
 *Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a numeric value.  
  
 *Expression2*  
 A valid MDX expression that returns a numeric value.  
  
## Return Value  
 A Boolean value that returns true if both parameters evaluate to **true**; otherwise, **false**.  
  
## Remarks  
 The **AND** operator treats both expressions as Boolean values (zero, 0, as **false**; otherwise, **true**) before the operator performs the logical conjunction. The following table illustrates how the **AND** operator performs the logical conjunction.  
  
|*Expression1*|*Expression2*|Return Value|  
|-------------------|-------------------|------------------|  
|**true**|**true**|**true**|  
|**true**|**false**|**false**|  
|**false**|**true**|**false**|  
|**false**|**false**|**false**|  
  
## Example  
  
```  
-- This query returns the gross profit margin (GPM)  
-- for clothing sales where the GPM is between 20% and 30%.  
With Member [Measures].[LowGPM] as  
  IIF(  
      [Measures].[Gross Profit Margin] <= .3 AND   
      [Measures].[Gross Profit Margin] >= .2,  
      [Measures].[Gross Profit Margin],  
      null)  
SELECT NON EMPTY  
    [Sales Territory].[Sales Territory Country].Members ON 0,  
    [Product].[Category].[Clothing] ON 1  
FROM  
    [Adventure Works]  
WHERE  
    ([Measures].[LowGPM])  
```  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
