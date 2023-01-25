---
description: "LEN (SSIS Expression)"
title: "LEN (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "LEN function"
  - "number of characters"
ms.assetid: d961398b-e4d0-4936-be17-8f4c5882a640
author: chugugrace
ms.author: chugu
---
# LEN (SSIS Expression)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Returns the number of characters in a character expression. If the string includes leading and trailing blanks, the function includes them in the count. LEN returns identical values for the same string of single and double byte characters.  
  
## Syntax  
  
```  
  
LEN(character_expression)  
```  
  
## Arguments  
 *character_expression*  
 Is the expression to evaluate.  
  
## Result Types  
 DT_I4  
  
## Remarks  
 The *character_expression* argument can be a DT_WSTR, DT_TEXT, DT_NTEXT, or DT_IMAGE data type. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
 If *character_expression* is a string literal or a data column with the DT_STR data type, it is implicitly cast to the DT_WSTR data type before LEN performs its operation. Other data types must be explicitly cast to the DT_WSTR data type. For more information, see [Cast &#40;SSIS Expression&#41;](../../integration-services/expressions/cast-ssis-expression.md).  
  
 If the argument passed to the LEN function has a Binary Large Object Block (BLOB) data type, such as DT_TEXT, DT_NTEXT, or DT_IMAGE, the function returns a byte count.  
  
 LEN returns a null result if the argument is null.  
  
## Expression Examples  
 This example returns the length of a string literal. The return result is 12.  
  
```  
LEN("Ball Bearing")  
```  
  
 This example returns the difference between the length of values in the **FirstName** and **LastName** columns.  
  
```  
LEN(FirstName) - LEN(LastName)  
```  
  
 Returns the length of a computer name using the System variable **MachineName**.  
  
```  
LEN(@MachineName)  
```  
  
## See Also  
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
