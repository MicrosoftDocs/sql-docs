---
title: "RIGHT (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "RIGHT function"
ms.assetid: 83e70e75-4be5-4783-a8cf-032f82afe16e
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# RIGHT (SSIS Expression)
  Returns the specified number of characters from the rightmost portion of the given character expression.  
  
## Syntax  
  
```  
  
RIGHT(character_expression,integer_expression)  
```  
  
## Arguments  
 *character_expression*  
 Is a character expression from which to extract characters.  
  
 *integer_expression*  
 Is an integer expression that indicates the number of characters to be returned.  
  
## Result Types  
 DT_WSTR  
  
## Remarks  
 If *integer_expression* is greater than the length of *character_expression*, the function returns *character_expression*.  
  
 If *integer_expression* is zero, the function returns a zero-length string.  
  
 If *integer_expression* is a negative number, the function returns an error.  
  
 The *integer_expression* argument can take variables and columns.  
  
 RIGHT works only with the DT_WSTR data type. A *character_expression* argument that is a string literal or a data column with the DT_STR data type is implicitly cast to the DT_WSTR data type before RIGHT performs its operation. Other data types must be explicitly cast to the DT_WSTR data type. For more information, see [Integration Services Data Types](../data-flow/integration-services-data-types.md) and [Cast &#40;SSIS Expression&#41;](cast-ssis-expression.md).  
  
 RIGHT returns a null result if either argument is null.  
  
## Expression Examples  
 The following example uses a string literal. The return result is `"Bike"`.  
  
```  
RIGHT("Mountain Bike", 4)  
```  
  
 The following example returns the number of rightmost characters that is specified in the `Times` variable, from the `Name` column. If `Name` is `Touring Front Wheel` and `Times` is 5, the return result is `"Wheel"`.  
  
```  
RIGHT(Name, @Times)  
```  
  
 The following example also returns the number of rightmost characters that is specified in the `Times` variable, from the `Name` column. `Times` has a noninteger data type and the expression includes an explicit cast to the DT_I2 data type. If `Name` is `Touring Front Wheel` and `Times` is `4.32`, the return result is `"heel"` because the RIGHT function converts the value of 4.32 to 4, and then returns the rightmost four characters.  
  
```  
RIGHT(Name, (DT_I2)@Times))  
```  
  
## See Also  
 [LEFT &#40;SSIS Expression&#41;](left-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](functions-ssis-expression.md)  
  
  
