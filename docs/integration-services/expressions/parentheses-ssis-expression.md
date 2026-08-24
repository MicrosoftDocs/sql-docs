---
title: "() (Parentheses) (SSIS Expression)"
description: "() (Parentheses) (SSIS Expression)"
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: concept-article
helpviewer_keywords:
  - "() (parentheses operator)"
  - "evaluation order [Integration Services]"
  - "parentheses operator ()"
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
  
## Related content

- [Operator Precedence and Associativity](operator-precedence-and-associativity.md)
- [Operators (SSIS Expression)](operators-ssis-expression.md)
