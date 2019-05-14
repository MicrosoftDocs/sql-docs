---
title: "ABS (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "ABS function"
  - "absolute positive value"
ms.assetid: 156747f6-e016-44cf-9a9f-ae8e4a1b4f17
author: janinezhang
ms.author: janinez
manager: craigg
---
# ABS (SSIS Expression)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Returns the absolute, positive value of a numeric expression.  
  
## Syntax  
  
```  
  
ABS(numeric_expression)  
```  
  
## Arguments  
 *numeric_expression*  
 Is a signed or unsigned numeric expression.  
  
## Result Types  
 The data type of the numeric expression submitted to the function.  
  
## Remarks  
 ABS returns a null result if the argument is null.  
  
## Expression Examples  
 These examples apply the ABS function to a positive and a negative number. Both return 1.23.  
  
```  
ABS(-1.23)  
ABS(1.23)  
```  
  
 This example applies the ABS function to an expression that subtracts values in the variables **HighTemperature** and **LowTempature**.  
  
```  
ABS(@HighTemperature - @LowTemperature)  
```  
  
## See Also  
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
