---
title: "&gt;= (Greater Than or Equal To) (MDX) | Microsoft Docs"
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
  - ">="
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - ">= (greater than or equal to operator)"
  - "greater than or equal to (>=)"
ms.assetid: 272f4614-9ba3-46bd-8306-181c26e798f1
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# &gt;= (Greater Than or Equal To) (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs a comparison operation that determines whether the value of one Multidimensional Expressions (MDX) expression is greater than or equal to the value of another MDX expression.  
  
## Syntax  
  
```  
  
MDX_Expression >= MDX_Expression  
```  
  
#### Parameters  
 *MDX_Expression*  
 A valid MDX expression.  
  
## Return Value  
 A Boolean value based on the following conditions:  
  
-   **true** if the first parameter has a value that is either greater than or equal to the value of the second parameter.  
  
-   **false** if the first parameter has a value that is lower than the value of the second parameter.  
  
-   **true** if both parameters are null or if one parameter is null and the other parameter is 0.  
  
## Examples  
 The following example demonstrates the use of this operator.  
  
```  
-- This query returns the gross profit margin (GPM)  
-- for Australia where the GPM is greater than or equal to 50%.  
With Member [Measures].[HighGPM] as  
  IIF(  
      [Measures].[Gross Profit Margin] >= .5,  
      [Measures].[Gross Profit Margin],  
      null)  
SELECT   
    NON EMPTY [Sales Territory].[Sales Territory Country].[Australia] ON 0,  
    NON EMPTY [Product].[Category].Members ON 1  
FROM  
    [Adventure Works]  
WHERE  
    ([Measures].[HighGPM])  
```  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
