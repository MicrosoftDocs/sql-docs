---
title: "CODEPOINT (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "CODEPOINT function"
  - "leftmost character of expression"
ms.assetid: 0783d05e-7f35-42fb-a2c4-9621c46effd6
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# CODEPOINT (SSIS Expression)
  Returns the Unicode code point of the leftmost character of a character expression.  
  
## Syntax  
  
```  
  
CODEPOINT(character_expression)  
```  
  
## Arguments  
 *character_expression*  
 Is a character expression whose leftmost character will be evaluated.  
  
## Result Types  
 DT_UI2  
  
## Remarks  
 *character_expression* must have the DT_WSTR data type.  
  
 CODEPOINT returns a null result if *character_expression* is null or an empty string.  
  
## Expression Examples  
 This example uses a string literal. The return result is 77, the Unicode code point for M.  
  
```  
CODEPOINT("Mountain Bike")  
```  
  
 This example uses a variable. If **Name** is Touring Front Wheel, the return result is 84, the Unicode code point for T.  
  
```  
CODEPOINT(@Name)  
```  
  
## See Also  
 [Functions &#40;SSIS Expression&#41;](functions-ssis-expression.md)  
  
  
