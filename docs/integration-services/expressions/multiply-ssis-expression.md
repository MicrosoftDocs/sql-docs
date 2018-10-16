---
title: "* (Multiply) (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "* (multiply operator)"
  - "multiply operator (*)"
ms.assetid: d457f052-ffbb-4485-833f-f4bed4349b69
author: "douglaslMS"
ms.author: "douglasl"
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
 Is any valid expression of a numeric data type. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## Result Types  
 Determined by data types of the two arguments. For more information, see [Integration Services Data Types in Expressions](../../integration-services/expressions/integration-services-data-types-in-expressions.md).  
  
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
  
 This example subtracts the result of an expression from the **ListPrice** column. The variable **Discount%** must be enclosed in brackets because the name includes the % character. For more information, see [Identifiers &#40;SSIS&#41;](../../integration-services/expressions/identifiers-ssis.md).  
  
```  
ListPrice - (ListPrice * @[Discount%])  
```  
  
## See Also  
 [Operator Precedence and Associativity](../../integration-services/expressions/operator-precedence-and-associativity.md)   
 [Operators &#40;SSIS Expression&#41;](../../integration-services/expressions/operators-ssis-expression.md)  
  
  
