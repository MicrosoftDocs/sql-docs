---
title: "SQRT (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "SQRT function"
  - "square root of given expression"
ms.assetid: 54a75389-c501-4e22-87b8-905f66d6a3a5
author: janinezhang
ms.author: janinez
manager: craigg
---
# SQRT (SSIS Expression)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Returns the square root of a numeric expression.  
  
## Syntax  
  
```  
  
SQRT(numeric_expression)  
```  
  
## Arguments  
 *numeric_expression*  
 Is a numeric expression of any numeric data type. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## Result Types  
 DT_R8  
  
## Remarks  
 SQRT returns a null result if the argument is null.  
  
 SQRT fails if the argument is a negative value.  
  
 The argument is cast to the DT_R8 data type before the square root operation.  
  
## Expression Examples  
 This example returns the square root of a numeric literal. The return result is 12.  
  
```  
SQRT(144)  
```  
  
 This example returns the square root of an expression, the result of subtracting values in the **Value1** and **Value2** columns.  
  
```  
SQRT(Value1 - Value2)  
```  
  
 This example returns the length of the third side of a right triangle by applying the SQUARE function to two variables and then calculating the square root of their sum. If **Side1** contains 3 and **Side2** contains 4, the SQRT function returns 5.  
  
```  
SQRT(SQUARE(@Side1) + SQUARE(@Side2))  
```  
  
> [!NOTE]  
>  In expressions, variable names always include the \@ prefix.  
  
## See Also  
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
