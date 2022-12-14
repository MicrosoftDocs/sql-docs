---
description: "Operator Precedence and Associativity"
title: "Operator Precedence and Associativity | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "associativity [Integration Services]"
  - "precedence [Integration Services]"
ms.assetid: 5094164f-dabc-45b5-b611-384feb2b3fe3
author: chugugrace
ms.author: chugu
---
# Operator Precedence and Associativity

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Each operator in the set of operators that the expression evaluator supports has a designated precedence in the precedence hierarchy and includes a direction in which it is evaluated. The direction of evaluation for an operator is operator associativity. Operators with higher precedence are evaluated before operators with lower precedence. If a complex expression has multiple operators, operator precedence determines the order in which the operations are performed. The order of execution can significantly affect the resulting value. Some operators have equal precedence. If an expression contains multiple operators of equal precedence, the operators are evaluated directionally, from left to right or right to left.  
  
 The following table lists the precedence of operators in order of high to low. Operators at the same level have equal precedence.  
  
|Operator symbol|Type of Operation|Associativity|  
|---------------------|-----------------------|-------------------|  
|( )|Expression|Left to right|  
|-, !, ~|Unary|Right to left|  
|casts|Unary|Right to left|  
|*, / ,%|Multiplicative|Left to right|  
|+, -|Additive|Left to right|  
|\<, >, \<=, >=|Relational|Left to right|  
|==, !=|Equality|Left to right|  
|&|Bitwise AND|Left to right|  
|^|Bitwise exclusive OR|Left to right|  
|&#124;|Bitwise inclusive OR|Left to right|  
|&&|Logical AND|Left to right|  
|&#124;&#124;|Logical OR|Left to right|  
|? :|Conditional expression|Right to left|  
  
## See Also  
 [Operators &#40;SSIS Expression&#41;](../../integration-services/expressions/operators-ssis-expression.md)  
  
  
