---
title: "^ (Bitwise Exclusive OR) (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "^ (bitwise exclusive OR operator)"
  - "bitwise exclusive OR (^)"
ms.assetid: 6ac53cab-29c4-4835-9f87-371b058b2f38
author: janinezhang
ms.author: janinez
manager: craigg
---
# ^ (Bitwise Exclusive OR) (SSIS Expression)
  Performs a bitwise exclusive OR operation of two integer values. It compares each bit of its first operand to the corresponding bit of its second operand. If one bit is 0 and the other bit is 1, the corresponding result bit is set to 1. If both bits are 0 or both bits are 1, the corresponding result bit is set to 0.  
  
 Both conditions must be a signed integer data type or both conditions must be an unsigned integer data type.  
  
## Syntax  
  
```  
  
integer_expression1 ^ integer_expression2  
  
```  
  
## Arguments  
 *integer_expression1, integer_expression2*  
 Is any valid expression of a signed or unsigned integer data type. For more information, see [Integration Services Data Types](../data-flow/integration-services-data-types.md).  
  
## Result Types  
 Determined by data types of the two arguments. For more information, see [Integration Services Data Types in Expressions](integration-services-data-types-in-expressions.md).  
  
## Remarks  
 If either condition is null, the expression result is null.  
  
## Expression Examples  
 This example performs a bitwise exclusive OR operation between variables **NumberA** and **NumberB**. **NumberA** contains 3 (00000011) and **NumberB** contains 7 (00000111).  
  
```  
@NumberA ^ @NumberB  
```  
  
 The expression evaluates to 4 (00000100).  
  
 00000011  
  
 00000111  
  
 ----------\-  
  
 00000100  
  
 This example performs a bitwise exclusive OR operation between the **ReorderPoint** and **SafetyStockLevel** columns.  
  
```  
ReorderPoint ^ SafetyStockLevel  
```  
  
 If **ReorderPoint** is 10 and **SafetyStockLevel** is 8, the expression evaluates to 2 (00000010).  
  
 00001010  
  
 00001000  
  
 ----------\-  
  
 00000010  
  
 This example performs a bitwise exclusive OR operation between two integers.  
  
```  
3 ^ 5   
```  
  
 The expression evaluates to 6 (00000110).  
  
 00000011  
  
 00000101  
  
 ----------\-  
  
 00000110  
  
## See Also  
 [&#124;&#124; &#40;Logical OR&#41; &#40;SSIS Expression&#41;](logical-or-ssis-expression.md)   
 [&#124; &#40;Bitwise Inclusive OR&#41; &#40;SSIS Expression&#41;](bitwise-inclusive-or-ssis-expression.md)   
 [Operator Precedence and Associativity](operator-precedence-and-associativity.md)   
 [Operators &#40;SSIS Expression&#41;](operators-ssis-expression.md)  
  
  
