---
title: "HEX (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "hexadecimal data"
  - "HEX function"
ms.assetid: f5d471ee-aeef-421c-b6e1-55b9676c3842
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# HEX (SSIS Expression)
  Returns a string representing the hexadecimal value of an integer.  
  
## Syntax  
  
```  
  
HEX(integer_expression)  
```  
  
## Arguments  
 *integer_expression*  
 Is a signed or unsigned integer.  
  
## Result Types  
 DT_WSTR  
  
## Remarks  
 HEX returns null if *integer_expression* is null.  
  
 The *integer_expression* argument must evaluate to an integer. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
 The return result does not include qualifiers such as the 0x prefix. To include a prefix, use the + (Concatenate) operator. For more information, see [+ &#40;Concatenate&#41; &#40;SSIS Expression&#41;](../../integration-services/expressions/concatenate-ssis-expression.md).  
  
 The letters A - F, used in HEX notations, appear as uppercase characters.  
  
 The length of the resulting string for integer data types is as follows:  
  
-   DT_I1 and DT_UI1 return a string with a maximum length of 2.  
  
-   DT_I2 and DT_UI2 return a string with a maximum length of 4.  
  
-   DT_I4 and DT_UI4 return a string with a maximum length of 8.  
  
-   DT_I8 and DT_UI8 return a string with a maximum length of 16.  
  
## Expression Examples  
 This example uses a numeric literal. The function returns the value 190.  
  
```  
HEX(400)   
```  
  
 This example uses the **ReorderPoint** column. The column data type is **smallint**. If **ReorderPoint** is 750, the function returns 2EE.  
  
```  
HEX(ReorderPoint)   
```  
  
 This example uses **LocaleID**, a system variable. If **LocaleID** is 1033, the function returns 409.  
  
```  
HEX(@LocaleID)  
```  
  
## See Also  
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
