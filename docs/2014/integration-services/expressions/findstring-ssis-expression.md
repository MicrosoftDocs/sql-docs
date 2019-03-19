---
title: "FINDSTRING (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "FINDSTRING function"
ms.assetid: c83cb1b1-3c52-4496-b518-4c9253b9336d
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# FINDSTRING (SSIS Expression)
  Returns the location of the specified occurrence of a string within a character expression. The return result is the one-based index of the occurrence. The string parameter must evaluate to a character expression, and the occurrence parameter must evaluate to an integer. If the string is not found, the return value is 0. If the string occurs fewer times than the occurrence argument specifies, the return value is 0.  
  
## Syntax  
  
```  
  
FINDSTRING(character_expression, searchstring, occurrence)  
```  
  
## Arguments  
 *character_expression*  
 Is the character string to search in.  
  
 *searchstring*  
 Is the character string to search for.  
  
 *occurrence*  
 Is a signed or unsigned integer specifying which occurrence of *searchstring* to report.  
  
## Result Types  
 DT_I4  
  
## Remarks  
 FINDSTRING works only with the DT_WSTR data type.  *character_expression* and *searchstring* arguments that are string literals or data columns with the DT_STR data type are implicitly cast to the DT_WSTR data type before FINDSTRING performs its operation. Other data types must be explicitly cast to the DT_WSTR data type. For more information, see [Integration Services Data Types](../data-flow/integration-services-data-types.md) and [Cast &#40;SSIS Expression&#41;](cast-ssis-expression.md).  
  
 FINDSTRING returns null if either *character_expression* or *searchstring* are null.  
  
 Use a value of 1 in the *occurrence* argument to get the index of the first occurrence, 2 for the second occurrence and so forth.  
  
 The *occurrence* must be an integer with a value greater than 0.  
  
## Expression Examples  
 This example uses a string literal. It returns the value 11.  
  
```  
FINDSTRING("New York, NY, NY", "NY", 1)   
```  
  
 This example uses a string literal. Because the string "NY" occurs only two times, the return result is 0.  
  
```  
FINDSTRING("New York, NY, NY", "NY", 3)   
```  
  
 This example uses the **Name** column. It returns the location of the value n in the **Name** column. The return result varies depending on the value in **Name**. If **Name** contains Anderson, the function returns 8.  
  
```  
FINDSTRING(Name,"n", 2)   
```  
  
 This example uses the **Name** and **Size** columns. It returns the location of the leftmost character of the **Size** value in the **Name** column. The return result varies depending on column values. If **Name** contains Mountain,500Red,42 and **Size** contains 42, the return result is 17.  
  
```  
FINDSTRING(Name,Size,1)   
```  
  
## See Also  
 [REPLACE &#40;SSIS Expression&#41;](replace-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](functions-ssis-expression.md)  
  
  
