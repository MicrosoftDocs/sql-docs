---
title: "+ (Concatenate) (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "concatenation [Integration Services]"
  - "+ (concatenate operator)"
  - "concatenate operator (+)"
ms.assetid: 0fed6334-7a4f-42dc-a611-191fcaa0e443
author: janinezhang
ms.author: janinez
manager: craigg
---
# + (Concatenate) (SSIS Expression)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Concatenates two expressions into one expression.  
  
## Syntax  
  
```  
  
character_expression1 + character_expression2  
  
```  
  
## Arguments  
 *expression1, expression2*  
 Is any valid DT_STR, DT_WSTR, DT_TEXT, DT_NTEXT, or DT_IMAGE data type expression.  
  
## Result Types  
 DT_WSTR  
  
## Remarks  
 The expression can use either or both of the DT_STR and DT_WSTR data types.  
  
 The concatenation of the DT_STR and DT_WSTR data types returns a result of the DT_WSTR type. The length of the string is the sum of the lengths of the original strings expressed in characters.  
  
 Only data with the string data types DT_STR and DT_WSTR or the Binary Large Object Block (BLOB) data types DT_TEXT, DT_NTEXT, and DT_IMAGE can be concatenated. Other data types must be explicitly converted to one of these data types before concatenation occurs. For more information about legal casts between data types, see [Cast &#40;SSIS Expression&#41;](../../integration-services/expressions/cast-ssis-expression.md).  
  
 Both expressions must be of the same data type, or one expression must be implicitly convertible to the data type of the other expression. For example, if the string "Order date is " and the column **OrderDate** are concatenated, the values in **OrderDate** are implicitly converted to a string data type. To concatenate two numeric values, both numeric values must be explicitly cast to a string data type.  
  
 A concatenation can use only one BLOB data type: DT_TEXT, DT_NTEXT, or DT_IMAGE.  
  
 If either element is null, the result is null.  
  
 String literals must be enclosed in quotation marks.  
  
## Expression Examples  
 This example concatenates the values in the **FirstName** and **LastName** columns and inserts a space between them.  
  
```  
FirstName + ' ' + LastName  
```  
  
 This example concatenates the variables **ZIPCode** and **ZIPCode+4**. Both variables have a string data type. **ZIPCode+4** must be enclosed in brackets because the variable name includes the + character.  
  
```  
@ZIPCcode + "-" + @[ZipCode+4]  
```  
  
## See Also  
 [Operator Precedence and Associativity](../../integration-services/expressions/operator-precedence-and-associativity.md)   
 [Operators &#40;SSIS Expression&#41;](../../integration-services/expressions/operators-ssis-expression.md)  
  
  
