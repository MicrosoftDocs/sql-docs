---
title: "- (Subtract) (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "- (subtract)"
  - "subtract operator (-)"
ms.assetid: b48da086-37dd-460a-8a4b-912f52c9b158
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# - (Subtract) (SSIS Expression)
  Subtracts the second numeric expression from the first one.  
  
## Syntax  
  
```  
  
numeric_expression1 - numeric_expression2  
  
```  
  
## Arguments  
 *numeric_expression1, numeric_expression2*  
 Is any valid expression of a numeric data type. For more information, see [Integration Services Data Types](../data-flow/integration-services-data-types.md).  
  
## Result Types  
 Determined by the data types of the two arguments. For more information, see [Integration Services Data Types in Expressions](integration-services-data-types-in-expressions.md).  
  
## Remarks  
 Enclose the minus unary expression in parenthesis to ensure that the expression is evaluated in the correct order  
  
## Remarks  
 If either operand is null, the result is null.  
  
## Expression Examples  
 This example subtracts numeric literals.  
  
```  
5 - 6.09  
```  
  
 This example subtracts the value in the **StandardCost** column from the value in the **ListPrice** column.  
  
```  
ListPrice - StandardCost  
```  
  
 This example subtracts a calculated value from the **ListPrice** column. The variable **Discount%** must be enclosed in brackets because the name includes the % character. For more information, see [Identifiers &#40;SSIS&#41;](identifiers-ssis.md).  
  
```  
ListPrice - (ListPrice * @[Discount%])  
```  
  
## See Also  
 [Operator Precedence and Associativity](operator-precedence-and-associativity.md)   
 [Operators &#40;SSIS Expression&#41;](operators-ssis-expression.md)  
  
  
