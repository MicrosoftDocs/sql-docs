---
title: "ROUND (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "rounding expressions"
  - "ROUND function [SSIS]"
ms.assetid: 376f1947-4fc5-4611-ad86-823e4db1b468
author: janinezhang
ms.author: janinez
manager: craigg
---
# ROUND (SSIS Expression)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Returns a numeric expression that is rounded to the specified length or precision. The length parameter must evaluate to an integer.  
  
## Syntax  
  
```  
  
ROUND(numeric_expression,length)  
```  
  
## Arguments  
 *numeric_expression*  
 Is an expression of a valid numeric type. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
 *length*  
 Is an integer expression. It is the precision to which *numeric_expression* is rounded.  
  
## Result Types  
 The same type as *numeric*_*expression.*  
  
## Remarks  
 The *length* argument must evaluate to a positive integer or zero.  
  
 ROUND returns a null result if the argument is null.  
  
## Expression Examples  
 These examples round numeric literals to a length of three. The first return result is 137.1570, the second 137.1580.  
  
```  
ROUND(137.1574,3)  
ROUND(137.1575,3)  
```  
  
## See Also  
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
