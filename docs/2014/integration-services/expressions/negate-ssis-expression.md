---
title: "- (Negate) (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "- (negative)"
  - "negative operator (-)"
ms.assetid: f0118dfc-aced-4de2-953e-5ebf9c962b8d
author: janinezhang
ms.author: janinez
manager: craigg
---
# - (Negate) (SSIS Expression)
  Negates a numeric expression.  
  
## Syntax  
  
```  
  
-numeric_expression  
  
```  
  
## Arguments  
 *numeric_expression*  
 Is any valid expression of any numeric data type. Only signed numeric data types are supported. For more information, see [Integration Services Data Types](../data-flow/integration-services-data-types.md).  
  
## Result Types  
 Returns the data type of *numeric_expression*.  
  
## Expression Examples  
 This example negates the value of the **Counter** variable and adds the numeric literal 50.  
  
```  
-@Counter + 50  
```  
  
## See Also  
 [Operator Precedence and Associativity](operator-precedence-and-associativity.md)   
 [Operators &#40;SSIS Expression&#41;](operators-ssis-expression.md)  
  
  
