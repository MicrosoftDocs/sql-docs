---
title: "LN (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "LN function"
  - "natural logarithm of expression [Integration Services]"
ms.assetid: 55d7b657-b5fd-4753-9c81-54ed7575e720
author: janinezhang
ms.author: janinez
manager: craigg
---
# LN (SSIS Expression)
  Returns the natural logarithm of a numeric expression.  
  
## Syntax  
  
```  
  
LN(numeric_expression)  
```  
  
## Arguments  
 *numeric_expression*  
 Is a valid non-zero and non-negative numeric expression.  
  
## Result Types  
 DT_R8  
  
## Remarks  
 The numeric expression is cast to the DT_R8 data type before the logarithm is computed. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
 If *numeric_expression* evaluates to zero or a negative value, the return result is null.  
  
## Expression Examples  
 This example uses a numeric literal. The function returns the value 3.737766961828337.  
  
```  
LN(42)  
```  
  
 This example uses the column **Length**. If the column value is 53.99, the function returns 3.9887988442302.  
  
```  
LN(Length)   
```  
  
 This example uses the variable **Length**. The variable must have a numeric data type or the expression must include an explicit cast to a numeric data type. If **Length** is 234.567, the function returns 5.45774126708797.  
  
```  
LN(@Length)   
```  
  
## See Also  
 [LOG &#40;SSIS Expression&#41;](../../integration-services/expressions/log-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
