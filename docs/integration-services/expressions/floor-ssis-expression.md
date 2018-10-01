---
title: "FLOOR (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "largest integer less than or equal to expression"
  - "FLOOR function [SSIS]"
ms.assetid: 168084db-badd-40f2-87b4-1f5bc45c3e24
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# FLOOR (SSIS Expression)
  Returns the largest integer that is less than or equal to a numeric expression.  
  
## Syntax  
  
```  
  
FLOOR(numeric_expression)  
```  
  
## Arguments  
 *numeric_expression*  
 Is a valid numeric expression.  
  
## Result Types  
 The numeric data type of the argument expression. The result is the integer portion of the calculated value in the same data type as *numeric_expression.*  
  
## Remarks  
 FLOOR returns a null result if the argument is null.  
  
## Expression Examples  
 These examples apply the FLOOR function to positive, negative, and zero values.  
  
```  
FLOOR(123.45)  
```  
  
 Returns 123.00  
  
```  
FLOOR(-123.45)  
```  
  
 Returns -124.00  
  
```  
FLOOR(0.00)  
```  
  
 Returns 0.00  
  
## See Also  
 [CEILING &#40;SSIS Expression&#41;](../../integration-services/expressions/ceiling-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
