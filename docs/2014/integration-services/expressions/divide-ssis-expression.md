---
title: "Divide (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "/ (divide)"
  - "divide operator (/)"
ms.assetid: 5bde9223-872d-443e-8a27-57735e1d8f3d
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Divide (SSIS Expression)
  Divides the first numeric expression by the second one.  
  
## Syntax  
  
```  
  
dividend / divisor  
  
```  
  
## Arguments  
 *dividend*  
 Is the numeric expression to divide. *dividend* can be any valid numeric expression. For more information, see [Integration Services Data Types](../data-flow/integration-services-data-types.md).  
  
 *divisor*  
 Is the numeric expression to divide the dividend by. *divisor* can be any valid numeric expression except zero.  
  
## Result Types  
 Determined by data types of the two arguments. For more information, see [Integration Services Data Types in Expressions](integration-services-data-types-in-expressions.md).  
  
## Remarks  
 If either operand is null, the result is null.  
  
 Dividing by zero is not legal. Depending on how the *divisor* subexpression is evaluated, one of following errors occurs:  
  
-   If the *divisor* subexpression that evaluates to zero is a constant, the error appears at design time, causing the expression to fail validation.  
  
-   If the *divisor* subexpression that evaluates to zero contains variables, but no input columns, the component to which the expression belongs fails the pre-execution validation that occurs before the package runs.  
  
-   If the *divisor* subexpression that evaluates to zero contains input columns, the error appears at run time and is handled according to the error flow rules of the data flow component.  
  
## Expression Examples  
 This example divides two numeric literals.  
  
```  
25 / 5  
```  
  
 This example divides values in the **ListPrice** column by values in the **StandardCost** column.  
  
```  
ListPrice / StandardCost  
```  
  
## See Also  
 [Operator Precedence and Associativity](operator-precedence-and-associativity.md)   
 [Operators &#40;SSIS Expression&#41;](operators-ssis-expression.md)  
  
  
