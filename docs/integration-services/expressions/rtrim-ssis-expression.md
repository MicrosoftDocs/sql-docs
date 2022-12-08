---
description: "RTRIM (SSIS Expression)"
title: "RTRIM (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "RTRIM function"
  - "trailing blanks"
ms.assetid: 529bd43e-3f8a-4682-a33e-569176aa7fc4
author: chugugrace
ms.author: chugu
---
# RTRIM (SSIS Expression)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Returns a character expression after removing trailing spaces.  
  
> [!NOTE]  
>  RTRIM does not remove white space characters such as the tab or line feed characters. Unicode provides code points for many different types of spaces, but this function recognizes only the Unicode code point 0x0020. When double-byte character set (DBCS) strings are converted to Unicode they may include space characters other than 0x0020 and the function cannot remove such spaces. To remove all kinds of spaces, you can use the Microsoft Visual Basic .NET RTrim method in a script run from the Script component.  
  
## Syntax  
  
```  
  
RTRIM(character expression)  
```  
  
## Arguments  
 *character_expression*  
 Is a character expression from which to remove spaces.  
  
## Result Types  
 DT_WSTR  
  
## Remarks  
 RTRIM works only with the DT_WSTR data type. A *character_expression* argument that is a string literal or a data column with the DT_STR data type is implicitly cast to the DT_WSTR data type before RTRIM performs its operation. Other data types must be explicitly cast to the DT_WSTR data type. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md) and [Cast &#40;SSIS Expression&#41;](../../integration-services/expressions/cast-ssis-expression.md).  
  
 RTRIM returns a null result if the argument is null.  
  
## Expression Examples  
 This example removes trailing spaces from a string literal. The return result is "Hello".  
  
```  
RTRIM("Hello   ")  
```  
  
 This example removes trailing spaces from a concatenation of the **FirstName** and **LastName** columns.  
  
```  
RTRIM(FirstName + " " + LastName)  
```  
  
 This example removes trailing spaces from the **FirstName** variable.  
  
```  
RTRIM(@FirstName)  
```  
  
## See Also  
 [LTRIM &#40;SSIS Expression&#41;](../../integration-services/expressions/ltrim-ssis-expression.md)   
 [TRIM &#40;SSIS Expression&#41;](../../integration-services/expressions/trim-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
