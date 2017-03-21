---
title: "XOR (MDX) | Microsoft Docs"
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
  - "XOR"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "XOR operator"
ms.assetid: 17280f36-df0e-477e-9342-e8129ed5cc3c
caps.latest.revision: 27
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# XOR (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs a logical exclusion on two numeric expressions.  
  
## Syntax  
  
```  
  
Expression1 XOR Expression2  
  
```  
  
#### Parameters  
 *Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a numeric value.  
  
 *Expression2*  
 A valid MDX expression that returns a numeric value.  
  
## Return Value  
 A Boolean value that returns **true** if one and only one argument evaluates to **true**; otherwise, **false**.  
  
## Remarks  
 The **XOR** operator treats both parameters as Boolean values (zero, 0, as **false**; otherwise, **true**) before the operator performs the logical exclusion. The following table illustrates how the **XOR** operator performs the logical exclusion.  
  
|*Expression1*|*Expression2*|Return Value|  
|-------------------|-------------------|------------------|  
|**true**|**true**|**false**|  
|**true**|**false**|**true**|  
|**false**|**true**|**true**|  
|**false**|**false**|**false**|  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
