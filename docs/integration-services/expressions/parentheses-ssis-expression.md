---
description: "() (Parentheses) (SSIS Expression)"
title: "() (Parentheses) (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "() (parentheses operator)"
  - "evaluation order [Integration Services]"
  - "parentheses operator ()"
ms.assetid: 931e10eb-1707-4121-b2f1-43565561119f
author: chugugrace
ms.author: chugu
---
# () (Parentheses) (SSIS Expression)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Identifies the evaluation order of expressions. Expressions enclosed in parentheses have the highest evaluation precedence. Nested expressions enclosed in parentheses are evaluated in inner-to-outer order.  
  
 Parentheses are also used to make complex expressions easier to understand.  
  
## Syntax  
  
```  
  
(expression)  
  
```  
  
## Arguments  
 *expression*  
 Is any valid expression.  
  
## Result Types  
 The data type of *expression*. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## Expression Examples  
 This example shows how the use of parenthesis modifies the precedence of operators. The first expression evaluates to 100, whereas the second one evaluates to 31.  
  
```  
(5 + 5) * (4 + 6)  
5 + 5 * 4 + 6  
  
```  
  
## See Also  
 [Operator Precedence and Associativity](../../integration-services/expressions/operator-precedence-and-associativity.md)   
 [Operators &#40;SSIS Expression&#41;](../../integration-services/expressions/operators-ssis-expression.md)  
  
  
