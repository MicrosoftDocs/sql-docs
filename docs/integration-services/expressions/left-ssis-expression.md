---
title: "LEFT (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 5634dbfb-740d-4c93-8fd5-2854cc741327
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# LEFT (SSIS Expression)
  Returns the specified number of characters from the leftmost portion of the given character expression.  
  
## Syntax  
  
```  
  
LEFT(character_expression,number)  
```  
  
## Arguments  
 *character_expression*  
 Is a character expression from which to extract characters.  
  
 *number*  
 Is an integer expression that indicates the number of characters to be returned.  
  
## Result Types  
 DT_WSTR  
  
## Remarks  
 If *number* is greater than the length of *character_expression*, the function returns *character_expression*.  
  
 If *number* is zero, the function returns a zero-length string.  
  
 If *number* is a negative number, the function returns an error.  
  
 The *number* argument can take variables and columns.  
  
 LEFT works only with the DT_WSTR data type. A *character_expression* argument that is a string literal or a data column with the DT_STR data type is implicitly cast to the DT_WSTR data type before LEFT performs its operation. Other data types must be explicitly cast to the DT_WSTR data type. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md) and [Cast &#40;SSIS Expression&#41;](../../integration-services/expressions/cast-ssis-expression.md).  
  
 LEFT returns a null result if either argument is null.  
  
## Expression Examples  
 The following example uses a string literal. The return result is `"Mountain"`.  
  
```  
LEFT("Mountain Bike", 8)  
```  
  
## See Also  
 [RIGHT &#40;SSIS Expression&#41;](../../integration-services/expressions/right-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
