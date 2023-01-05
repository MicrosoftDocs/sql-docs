---
description: "Divide (MDX)"
title: "Divide (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Divide (MDX)


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
  
  
