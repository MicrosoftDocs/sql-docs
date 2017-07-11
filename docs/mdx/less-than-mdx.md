---
title: "&lt; (Less Than) (MDX) | Microsoft Docs"
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
  - "<"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "less than (<)"
  - "< (less than operator)"
ms.assetid: 53d86151-230b-4061-916f-ca8bb172d21e
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# &lt; (Less Than) (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs a comparison operation that determines whether the value of one Multidimensional Expressions (MDX) expression is less than the value of another MDX expression.  
  
## Syntax  
  
```  
  
MDX_Expression < MDX_Expression  
```  
  
#### Parameters  
 *MDX_Expression*  
 A valid MDX expression.  
  
## Return Value  
 A Boolean value based on the following conditions:  
  
-   **true** if both parameters are non-null, and the first parameter has a value that is lower than the value of the second parameter.  
  
-   **false** if both parameters are non-null, and the first parameter has a value that is either equal to or greater than the value of the second parameter.  
  
-   null if either or both parameters evaluate to a null value.  
  
## Examples  
 The following example demonstrates the use of this operator.  
  
```  
-- This query returns the gross profit margin (GPM)  
-- for clothing sales where the GPM is less than 30%.  
With Member [Measures].[LowGPM] as  
  IIF(  
      [Measures].[Gross Profit Margin] < .3,  
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
  
  
