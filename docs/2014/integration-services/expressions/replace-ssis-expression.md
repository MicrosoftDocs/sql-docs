---
title: "REPLACE (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "replacing string expression"
  - "REPLACE function"
ms.assetid: a6837043-ea70-4c6a-9c7a-6868b02b2adc
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# REPLACE (SSIS Expression)
  Returns a character expression after replacing a character string within the expression with either a different character string or an empty string.  
  
> [!NOTE]  
>  The REPLACE function frequently uses long strings. The consequences of truncation can be handled gracefully or cause a warning or an error. For more information, see [Syntax &#40;SSIS&#41;](syntax-ssis.md).  
  
## Syntax  
  
```  
  
REPLACE(character_expression,searchstring,replacementstring)  
```  
  
## Arguments  
 *character_expression*  
 Is a valid character expression that the function searches.  
  
 *searchstring*  
 Is a valid character expression that the function attempts to locate.  
  
 *replacementstring*  
 Is a valid character expression that is the replacement expression.  
  
## Result Types  
 DT_WSTR  
  
## Remarks  
 The length of *searchstring* must not be zero.  
  
 The length of *replacementstring* may be zero.  
  
 The *searchstring* and *replacementstring* arguments can use variables and columns.  
  
 REPLACE works only with the DT_WSTR data type. *character_expression1, character_expression2,* and *character_expression3* arguments that are string literals or data columns with the DT_STR data type are implicitly cast to the DT_WSTR data type before REPLACE performs its operation. Other data types must be explicitly cast to the DT_WSTR data type. For more information, see [Cast &#40;SSIS Expression&#41;](cast-ssis-expression.md).  
  
 REPLACE returns a null result if any argument is null.  
  
## Expression Examples  
 This example uses a string literal. The return result is "All Terrain Bike".  
  
```  
REPLACE("Mountain Bike", "Mountain","All Terrain")  
```  
  
 This example removes the string "Bike" from the **Product** column.  
  
```  
REPLACE(Product, "Bike","")  
```  
  
 This example replaces values in the **DaysToManufacture** column. The column has an integer data type and the expression includes casting **DaysToManufacture** to the DT_WSTR data type.  
  
```  
REPLACE((DT_WSTR,8)DaysToManufacture,"6","5")  
```  
  
## See Also  
 [SUBSTRING &#40;SSIS Expression&#41;](substring-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](functions-ssis-expression.md)  
  
  
