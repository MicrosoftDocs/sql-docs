---
title: "EXP (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "exponential functions"
  - "EXP function"
ms.assetid: 4cd96d3c-58c9-4a67-a6f6-b72758232912
author: janinezhang
ms.author: janinez
manager: craigg
---
# EXP (SSIS Expression)
  Returns the exponent to base e of a numeric expression. The EXP function complements the action of the LN function and is sometimes referred to as the antilogarithm.  
  
## Syntax  
  
```  
  
EXP(numeric_expression)  
```  
  
## Arguments  
 *numeric_expression*  
 Is a valid numeric expression.  
  
## Result Types  
 DT_R8  
  
## Remarks  
 The numeric expression is cast to the DT_R8 data type before the exponent is computed. For more information, see [Integration Services Data Types](../data-flow/integration-services-data-types.md).  
  
 The return result is always a positive number.  
  
## Expression Examples  
 These examples apply the EXP function to positive and negative values and to zero.  
  
```  
EXP(74)  
```  
  
 Returns 1.373382979540176E+32.  
  
```  
EXP(-27)  
```  
  
 Returns 1.879528816539083E-12.  
  
```  
EXP(0)  
```  
  
 Returns 1.  
  
## See Also  
 [LOG &#40;SSIS Expression&#41;](log-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](functions-ssis-expression.md)  
  
  
