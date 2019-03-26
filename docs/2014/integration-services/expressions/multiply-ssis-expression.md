---
title: "* (Multiply) (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "* (multiply operator)"
  - "multiply operator (*)"
ms.assetid: d457f052-ffbb-4485-833f-f4bed4349b69
author: janinezhang
ms.author: janinez
manager: craigg
---
# * (Multiply) (SSIS Expression)
  Multiplies two numeric expressions.  
  
## Syntax  
  
```  
  
numeric_expression1 * numeric_expression2  
  
```  
  
## Arguments  
 *numeric_expression1, numeric_expression2*  
 Is any valid expression of a numeric data type. For more information, see [Integration Services Data Types](../data-flow/integration-services-data-types.md).  
  
## Result Types  
 Determined by data types of the two arguments. For more information, see [Integration Services Data Types in Expressions](integration-services-data-types-in-expressions.md).  
  
## Remarks  
 If either operand is null, the result is null.  
  
## Expression Examples  
 This example multiplies numeric literals.  
  
```  
5 * 6.09  
```  
  
 This example multiplies values in the **ListPrice** column by 10 percent.  
  
```  
ListPrice * .10  
```  
  
 This example subtracts the result of an expression from the **ListPrice** column. The variable **Discount%** must be enclosed in brackets because the name includes the % character. For more information, see [Identifiers &#40;SSIS&#41;](identifiers-ssis.md).  
  
```  
ListPrice - (ListPrice * @[Discount%])  
```  
  
## See Also  
 [Operator Precedence and Associativity](operator-precedence-and-associativity.md)   
 [Operators &#40;SSIS Expression&#41;](operators-ssis-expression.md)  
  
  
