---
title: "| (Bitwise Inclusive OR) (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "| (bitwise inclusive OR)"
  - "bitwise inclusive OR (|)"
ms.assetid: 4dce9eb2-3680-4adc-81a3-816ea52cef49
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# | (Bitwise Inclusive OR) (SSIS Expression)
  Performs a bitwise OR operation of two integer values. It compares each bit of its first operand to the corresponding bit of its second operand. If either bit is 1, the corresponding result bit is set to 1. Otherwise, the corresponding result bit is set to zero (0).  
  
 Both conditions must be a signed integer data type or both conditions must be an unsigned integer data type.  
  
## Syntax  
  
```  
  
integer_expression1 | integer_expression2  
  
```  
  
## Arguments  
 *integer_expression1 ,integer_ expression2*  
 Is any valid expression of a signed or unsigned integer data type. For more information, see [Integration Services Data Types](../data-flow/integration-services-data-types.md).  
  
## Result Types  
 Determined by data types of the two arguments. For more information, see [Integration Services Data Types in Expressions](integration-services-data-types-in-expressions.md).  
  
## Remarks  
 If either condition is null, the expression result is null.  
  
## Expression Examples  
 This example performs a bitwise inclusive OR operation between the variables **NumberA** and **NumberB**. **NumberA** contains 3 (00000011) and **NumberB** contains 9 (00001001).  
  
```  
@NumberA | @NumberB  
```  
  
 The expression evaluates to 11 (00001011).  
  
 00000011  
  
 00001001  
  
 ----------\-  
  
 00001011  
  
 This example performs a bitwise inclusive OR operation between the **ReorderPoint** and **SafetyStockLevel** columns.  
  
```  
ReorderPoint | SafetyStockLevel  
```  
  
 If **ReorderPoint** is 10 and **SafetyStockLevel** is 8, the expression evaluates to 10 (00001010).  
  
 00001010  
  
 00001000  
  
 ----------\-  
  
 00001010  
  
 This example performs a bitwise inclusive OR operation between two integers.  
  
```  
3 | 5   
```  
  
 The expression evaluates to 7 (00000111).  
  
 00000011  
  
 00000101  
  
 ----------\-  
  
 00000111  
  
## See Also  
 [&#124;&#124; &#40;Logical OR&#41; &#40;SSIS Expression&#41;](logical-or-ssis-expression.md)   
 [^ &#40;Bitwise Exclusive OR&#41; &#40;SSIS Expression&#41;](bitwise-exclusive-or-ssis-expression.md)   
 [Operator Precedence and Associativity](operator-precedence-and-associativity.md)   
 [Operators &#40;SSIS Expression&#41;](operators-ssis-expression.md)  
  
  
