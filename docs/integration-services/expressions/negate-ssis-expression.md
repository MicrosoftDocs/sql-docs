---
description: "- (Negate) (SSIS Expression)"
title: "- (Negate) (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "- (negative)"
  - "negative operator (-)"
ms.assetid: f0118dfc-aced-4de2-953e-5ebf9c962b8d
author: chugugrace
ms.author: chugu
---
# - (Negate) (SSIS Expression)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Negates a numeric expression.  
  
## Syntax  
  
```  
  
-numeric_expression  
  
```  
  
## Arguments  
 *numeric_expression*  
 Is any valid expression of any numeric data type. Only signed numeric data types are supported. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## Result Types  
 Returns the data type of *numeric_expression*.  
  
## Expression Examples  
 This example negates the value of the **Counter** variable and adds the numeric literal 50.  
  
```  
-@Counter + 50  
```  
  
## See Also  
 [Operator Precedence and Associativity](../../integration-services/expressions/operator-precedence-and-associativity.md)   
 [Operators &#40;SSIS Expression&#41;](../../integration-services/expressions/operators-ssis-expression.md)  
  
  
