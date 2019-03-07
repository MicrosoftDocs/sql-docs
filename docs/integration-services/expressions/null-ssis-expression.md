---
title: "NULL (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "NULL function"
  - "null values [Integration Services]"
ms.assetid: df144237-3fbb-41ac-8624-efd92b6522b9
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# NULL (SSIS Expression)
  Returns a null value of a requested data type.  
  
## Syntax  
  
```  
  
NULL(typespec)  
```  
  
## Arguments  
 *typespec*  
 Is a valid data type. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## Result Types  
 Any valid data type with a null value.  
  
## Remarks  
 NULL returns a null result if the argument is null.  
  
 Parameters are required to request a null value for some data types. The following table lists these data types and their parameters.  
  
|Data type|Parameter|Example|  
|---------------|---------------|-------------|  
|DT_STR|*charcount*<br /><br /> *codepage*|(DT_STR,30,1252) casts 30 characters to the DT_STR data type using the 1252 code page.|  
|DT_WSTR|*charcount*|(DT_WSTR,20) casts 20 characters to the DT_WSTR data type.|  
|DT_BYTES|*bytecount*|(DT_BYTES,50) casts 50 bytes to the DT_BYTES data type.|  
|DT_DECIMAL|*scale*|(DT_DECIMAL,2) casts a numeric value to the DT_DECIMAL data type using a scale of 2.|  
|DT_NUMERIC|*precision*<br /><br /> *scale*|(DT_NUMERIC,10,3) casts a numeric value to the DT_NUMERIC data type using a precision of 10 and a scale of 3.|  
|DT_TEXT|*codepage*|(DT_TEXT,1252) casts a value to the DT_TEXT data type using the 1252 code page.|  
  
## Expression Examples  
 These examples return the null value of the data types: DT_STR, DT_DATE, and DT_BOOL.  
  
```  
NULL(DT_STR,10,1252)  
NULL(DT_DATE)  
NULL(DT_BOOL)  
```  
  
## See Also  
 [ISNULL &#40;SSIS Expression&#41;](../../integration-services/expressions/isnull-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
