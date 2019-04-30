---
title: "LTRIM (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "leading blanks"
  - "LTRIM function"
ms.assetid: d082f42a-d7e7-49f5-a503-ac44ba630832
author: janinezhang
ms.author: janinez
manager: craigg
---
# LTRIM (SSIS Expression)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Returns a character expression after removing leading spaces.  
  
> [!NOTE]  
>  LTRIM does not remove white-space characters such as the tab or line feed characters. Unicode provides code points for many different types of spaces, but this function recognizes only the Unicode code point 0x0020. When double-byte character set (DBCS) strings are converted to Unicode they may include space characters other than 0x0020 and the function cannot remove such spaces. To remove all kinds of spaces, you can use the Microsoft Visual Basic .NET LTrim method in a script run from the Script component.  
  
## Syntax  
  
```  
  
LTRIM(character expression)  
```  
  
## Arguments  
 *character_expression*  
 Is a character expression from which to remove spaces.  
  
## Result Types  
 DT_WSTR  
  
## Remarks  
 LTRIM works only with the DT_WSTR data type. A *character_expression* argument that is a string literal or a data column with the DT_STR data type is implicitly cast to the DT_WSTR data type before LTRIM performs its operation. Other data types must be explicitly cast to the DT_WSTR data type. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md) and [Cast &#40;SSIS Expression&#41;](../../integration-services/expressions/cast-ssis-expression.md).  
  
 LTRIM returns a null result if the argument is null.  
  
## Expression Examples  
 This example removes leading spaces from a string literal. The return result is "Hello".  
  
```  
LTRIM("    Hello")  
```  
  
 This example removes leading spaces from the **FirstName** column.  
  
```  
LTRIM(FirstName)  
```  
  
 This example removes leading spaces from the **FirstName** variable.  
  
```  
LTRIM(@FirstName)  
```  
  
## See Also  
 [RTRIM &#40;SSIS Expression&#41;](../../integration-services/expressions/rtrim-ssis-expression.md)   
 [TRIM &#40;SSIS Expression&#41;](../../integration-services/expressions/trim-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
