---
title: "NOT (MDX) | Microsoft Docs"
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
  - "NOT"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "NOT operator [MDX]"
ms.assetid: c11bd3b0-54b3-4a6d-babc-6067722194db
caps.latest.revision: 26
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# NOT (MDX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Performs a logical negation on a numeric expression.  
  
## Syntax  
  
```  
  
NOT Expression1  
```  
  
#### Parameters  
 *Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a numeric value.  
  
## Return Value  
 A Boolean value that returns **false** if the argument evaluates to **true**; otherwise, **true**.  
  
## Remarks  
 The **NOT** operator treats the expression as a Boolean value (zero, 0, as **false**; otherwise, **true**) before the operator performs the logical negation. The following table illustrates how the **NOT** operator performs the logical negation.  
  
|*Expression1*|Return Value|  
|-------------------|------------------|  
|**true**|**false**|  
|**false**|**true**|  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
