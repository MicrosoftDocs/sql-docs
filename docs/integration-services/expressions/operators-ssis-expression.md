---
description: "Operators (SSIS Expression)"
title: "Operators (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "SSIS, operators"
  - "SQL Server Integration Services, operators"
  - "operators [Integration Services]"
  - "expressions [Integration Services], operators"
ms.assetid: 33df3a3d-1f5c-429b-a3b9-52b7d8689089
author: chugugrace
ms.author: chugu
---
# Operators (SSIS Expression)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  This section describes the operators the expression language provides and the operator precedence and associativity that the expression evaluator uses.  
  
 The following table lists topics about operators in this section.  
  
|Operator|Description|  
|--------------|-----------------|  
|[Cast &#40;SSIS Expression&#41;](../../integration-services/expressions/cast-ssis-expression.md)|Converts an expression from one data type to a different data type.|  
|[&#40;&#41; &#40;Parentheses&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/parentheses-ssis-expression.md)|Identifies the evaluation order of expressions.|  
|[+ &#40;Add&#41; &#40;SSIS&#41;](../../integration-services/expressions/add-ssis.md)|Adds two numeric expressions.|  
|[+ &#40;Concatenate&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/concatenate-ssis-expression.md)|Concatenates two expressions.|  
|[- &#40;Subtract&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/subtract-ssis-expression.md)|Subtracts the second numeric expression from the first one.|  
|[- &#40;Negate&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/negate-ssis-expression.md)|Negates a numeric expression.|  
|[&#42; &#40;Multiply&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/multiply-ssis-expression.md)|Multiplies two numeric expressions.|  
|[/ (Divide) &#40;SSIS Expression&#41;](../../integration-services/expressions/divide-ssis-expression.md)|Divides the first numeric expression by the second one.|  
|[% &#40;Modulo&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/modulo-ssis-expression.md)|Provides the integer remainder after dividing the first numeric expression by the second one.|  
|[&#124;&#124; &#40;Logical OR&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/logical-or-ssis-expression.md)|Performs a logical OR operation.|  
|[&& &#40;Logical AND&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/logical-and-ssis-expression.md)|Performs a logical AND operation.|  
|[\! &#40;Logical NOT&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/logical-not-ssis-expression.md)|Negates a Boolean operand.|  
|[&#124; &#40;Bitwise Inclusive OR&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/bitwise-inclusive-or-ssis-expression.md)|Performs a bitwise OR operation of two integer values.|  
|[^ &#40;Bitwise Exclusive OR&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/bitwise-exclusive-or-ssis-expression.md)|Performs a bitwise exclusive OR operation of two integer values.|  
|[& &#40;Bitwise AND&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/bitwise-and-ssis-expression.md)|Performs a bitwise AND operation of two integer values.|  
|[~ &#40;Bitwise NOT&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/bitwise-not-ssis-expression.md)|Performs a bitwise negation of an integer.|  
|[== &#40;Equal&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/equal-ssis-expression.md)|Performs a comparison to determine if two expressions are equal.|  
|[\!= &#40;Unequal&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/unequal-ssis-expression.md)|Performs a comparison to determine if two expressions are not equal.|  
|[&#62; &#40;Greater Than&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/greater-than-ssis-expression.md)|Performs a comparison to determine if the first expression is greater than the second one.|  
|[&#60; &#40;Less Than&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/less-than-ssis-expression.md)|Performs a comparison to determine if the first expression is less than the second one.|  
|[&#62;= &#40;Greater Than or Equal To&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/greater-than-or-equal-to-ssis-expression.md)|Performs a comparison to determine if the first expression is greater than or equal to the second one.|  
|[&#60;= &#40;Less Than or Equal To&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/less-than-or-equal-to-ssis-expression.md)|Performs a comparison to determine if the first expression is less than or equal to the second one.|  
|[? : &#40;Conditional&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/conditional-ssis-expression.md)|Returns one of two expressions based on the evaluation of a Boolean expression.|  
  
 For information about the placement of each operator in the precedence hierarchy, see [Operator Precedence and Associativity](../../integration-services/expressions/operator-precedence-and-associativity.md).  
  
## See Also  
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)   
 [Examples of Advanced Integration Services Expressions](../../integration-services/expressions/examples-of-advanced-integration-services-expressions.md)   
 [Integration Services &#40;SSIS&#41; Expressions](../../integration-services/expressions/integration-services-ssis-expressions.md)  
  
  
