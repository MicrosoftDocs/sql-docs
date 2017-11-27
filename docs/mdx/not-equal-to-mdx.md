---
title: "&lt;&gt; (Not Equal To) (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "<>"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "not equal to operator (<>)"
  - "<> (not equal to operator)"
ms.assetid: b4eb3f1c-8b68-4530-a8f3-e3b8414ac789
caps.latest.revision: 29
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# &lt;&gt; (Not Equal To) (MDX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Performs a comparison operation that determines whether the value of one Multidimensional Expressions (MDX) expression is not equal to the value of another MDX expression.  
  
## Syntax  
  
```  
  
MDX_Expression <> MDX_Expression  
```  
  
#### Parameters  
 *MDX_Expression*  
 A valid MDX expression.  
  
## Return Value  
 A Boolean value based on the following conditions:  
  
-   **true** if both parameters are non-null, and the first parameter is not equal to the second parameter.  
  
-   **false** if both parameters are non-null, and the first parameter is equal to the second parameter.  
  
-   null if either or both parameters evaluate to a null value.  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
