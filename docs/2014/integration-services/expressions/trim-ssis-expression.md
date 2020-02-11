---
title: "TRIM (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "leading blanks"
  - "TRIM function"
  - "trailing blanks"
ms.assetid: 7dd9081d-a3d4-483a-bf7e-bf2bd7692d39
author: janinezhang
ms.author: janinez
manager: craigg
---
# TRIM (SSIS Expression)
  Returns a character expression after removing leading and trailing spaces.  
  
> [!NOTE]  
>  TRIM does not remove white-space characters such as the tab or line feed characters. Unicode provides code points for many different types of spaces, but this function recognizes only the Unicode code point 0x0020. When double-byte character set (DBCS) strings are converted to Unicode they may include space characters other than 0x0020 and the function cannot remove such spaces. To remove all kinds of spaces, you can use the Microsoft Visual Basic .NET Trim method in a script run from the Script component.  
  
## Syntax  
  
```  
  
TRIM(character_expression)  
```  
  
## Arguments  
 *character_expression*  
 Is a character expression from which to remove spaces.  
  
## Result Types  
 DT_WSTR  
  
## Remarks  
 TRIM returns a null result if the argument is null.  
  
 TRIM works only with the DT_WSTR data type. A *character_expression* argument that is a string literal or a data column with the DT_STR data type is implicitly cast to the DT_WSTR data type before TRIM performs its operation. Other data types must be explicitly cast to the DT_WSTR data type. For more information, see [Integration Services Data Types](../data-flow/integration-services-data-types.md) and [Cast &#40;SSIS Expression&#41;](cast-ssis-expression.md).  
  
## Expression Examples  
 This example removes leading and trailing spaces from a string literal. The return result is "New York".  
  
```  
TRIM("   New York   ")  
```  
  
 This example removes leading and trailing spaces from the result of concatenating the **FirstName** and **LastName** columns. The empty string between **FirstName** and **LastName** is not removed.  
  
```  
TRIM(FirstName + " "+ LastName)  
```  
  
## See Also  
 [LTRIM &#40;SSIS Expression&#41;](trim-ssis-expression.md)   
 [RTRIM &#40;SSIS Expression&#41;](rtrim-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](functions-ssis-expression.md)  
  
  
