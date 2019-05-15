---
title: "UPPER (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "UPPER function"
  - "converting lowercase to uppercase"
  - "uppercase characters [Integration Services]"
  - "lowercase characters"
ms.assetid: d33985f7-1048-4023-83e4-274090acda78
author: janinezhang
ms.author: janinez
manager: craigg
---
# UPPER (SSIS Expression)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Returns a character expression after converting lowercase characters to uppercase characters.  
  
## Syntax  
  
```  
  
UPPER(character_expression)  
```  
  
## Arguments  
 *character_expression*  
 Is a character expression to convert to uppercase characters.  
  
## Result Types  
 DT_WSTR  
  
## Remarks  
 UPPER works only with the DT_WSTR data type. A *character_expression* argument that is a string literal or a data column with the DT_STR data type is implicitly cast to the DT_WSTR data type before UPPER performs its operation. Other data types must be explicitly cast to the DT_WSTR data type. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md) and [Cast &#40;SSIS Expression&#41;](../../integration-services/expressions/cast-ssis-expression.md).  
  
 UPPER returns a null result if the argument is null.  
  
## Expression Examples  
 This example converts a string literal to uppercase characters. The return result is "HELLO".  
  
```  
UPPER("hello")  
```  
  
 This example converts the first character in the **FirstName** column to an uppercase character. If **FirstName** is anna, the return result is "A". For more information, see [SUBSTRING &#40;SSIS Expression&#41;](../../integration-services/expressions/substring-ssis-expression.md).  
  
```  
UPPER(SUBSTRING(FirstName, 1, 1))  
```  
  
 This example converts the value in the **PostalCode** variable to uppercase characters. If **PostalCode** is k4b1s2, the return result is "K4B1S2".  
  
```  
UPPER(@PostalCode)  
```  
  
## See Also  
 [LOWER &#40;SSIS Expression&#41;](../../integration-services/expressions/lower-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
