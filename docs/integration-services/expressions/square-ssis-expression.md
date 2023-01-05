---
description: "SQUARE (SSIS Expression)"
title: "SQUARE (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "SQUARE"
  - "square values"
ms.assetid: cecf1bb2-3d55-40a6-9688-ed67bcc150b4
author: chugugrace
ms.author: chugu
---
# SQUARE (SSIS Expression)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Returns the square of a numeric expression.  
  
## Syntax  
  
```  
  
SQUARE(numeric_expression)  
```  
  
## Arguments  
 *numeric_expression*  
 Is a numeric expression of any numeric data type. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## Result Types  
 DT_R8  
  
## Remarks  
 SQUARE returns a null result if the argument is null.  
  
 The argument is cast to the DT_R8 data type before the square operation.  
  
## Expression Examples  
 This example returns the square of 12. The return result is 144.  
  
```  
SQUARE(12)  
```  
  
 This example returns the square of the result of subtracting values in two columns. If **Value1** contains 12 and **Value2** contains 4, the SQUARE function returns 64.  
  
```  
SQUARE(Value1 - Value2)  
```  
  
 This example returns the length of the third side of a right angle triangle by applying the SQUARE function to two variables and then calculating the square root of their sum. If **Side1** contains 3 and **Side2** contains 4, the SQRT function returns 5.  
  
```  
SQRT(SQUARE(@Side1) + SQUARE(@Side2))  
```  
  
> [!NOTE]  
>  In expressions, variable names always include the \@ prefix.  
  
## See Also  
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
