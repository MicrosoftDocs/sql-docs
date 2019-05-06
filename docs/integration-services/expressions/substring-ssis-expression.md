---
title: "SUBSTRING (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "SUBSTRING function"
  - "part of expression returned [Integration Services]"
ms.assetid: 3a46748a-f5f8-4a6c-9108-673666754068
author: janinezhang
ms.author: janinez
manager: craigg
---
# SUBSTRING (SSIS Expression)
  Returns the part of a character expression that starts at the specified position and has the specified length. The *position* parameter and the *length* parameter must evaluate to integers.  
  
## Syntax  
  
```  
  
SUBSTRING(character_expression, position, length)  
```  
  
## Arguments  
 *character_expression*  
 Is a character expression from which to extract characters.  
  
 *position*  
 Is an integer that specifies where the substring begins.  
  
 *length*  
 Is an integer that specifies the length of the substring as number of characters.  
  
## Result Types  
 DT_WSTR  
  
## Remarks  
 SUBSTRING uses a one-based index. If *position* is 1, the substring begins with the first character in *character_expression*.  
  
 SUBSTRING works only with the DT_WSTR data type. A *character_expression* argument that is a string literal or a data column with the DT_STR data type is implicitly cast to the DT_WSTR data type before SUBSTRING performs its operation. Other data types must be explicitly cast to the DT_WSTR data type. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md) and [Cast &#40;SSIS Expression&#41;](../../integration-services/expressions/cast-ssis-expression.md).  
  
 SUBSTRING returns a null result if the argument is null.  
  
 All arguments in the expression can use variables and columns.  
  
 The *length* argument can exceed the length of the string. In that case, the function returns the remainder of the string.  
  
## Expression Examples  
 This example returns two characters, beginning with character 4, from a string literal. The return result is "ph".  
  
```  
SUBSTRING("elephant",4,2)  
```  
  
 This example returns the remainder of a string literal, beginning at the fourth character. The return result is "phant". It is not an error for the *length* argument to exceed the length of the string.  
  
```  
SUBSTRING ("elephant",4,50)  
```  
  
 This example returns the first letter from the **MiddleName** column.  
  
```  
SUBSTRING(MiddleName,1,1)  
```  
  
 This example uses variables in the *position* and *length* arguments. If **Start** is 1 and **Length** is 5, the function returns the first five characters in the **Name** column.  
  
```  
SUBSTRING(Name,@Start,@Length)  
```  
  
 This example returns the last four characters from the **PostalCode** variable beginning at the sixth character.  
  
```  
SUBSTRING (@PostalCode,6,4)  
```  
  
 This example returns a zero-length string from a string literal.  
  
```  
SUBSTRING ("Redmond",4,0)  
```  
  
## See Also  
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
