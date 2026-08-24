---
title: "Divide (MDX)"
description: "Divide (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
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
  
## Related content

- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
