---
title: "Divide (MDX) | Microsoft Docs"
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
ms.assetid: 9fe4a47b-d5e8-4dc7-ad4a-3e47ab463f03
caps.latest.revision: 5
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# Divide (MDX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Performs division and returns alternate result or BLANK() on division by 0.  
  
## Syntax  
  
```  
  
Divide (<numerator>, <denominator> [,<alternateresult>])  
```  
  
## Arguments  
 *numerator*  
 The dividend or number to divide.  
  
 *denominator*  
 The divisor or number to divide by.  
  
 *alternateresult*  
 (Optional) The value returned when division by zero results in an error. When not provided, the default value is BLANK().  
  
## Remarks  
 Alternate result on divide by 0 must be a constant.  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
