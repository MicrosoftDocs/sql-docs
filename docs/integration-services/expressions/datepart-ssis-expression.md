---
title: "DATEPART (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "dates [Integration Services], DATEPART"
  - "DATEPART function"
ms.assetid: 3e590094-fc49-4144-805f-fdc1bf2fe509
author: janinezhang
ms.author: janinez
manager: craigg
---
# DATEPART (SSIS Expression)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Returns an integer representing a datepart of a date.  
  
## Syntax  
  
```  
  
DATEPART(datepart, date)  
```  
  
## Arguments  
 *datepart*  
 Is the parameter that specifies for which part of the date to return a new value.  
  
 *date*  
 Is an expression that returns a valid date or a string in date format.  
  
## Result Types  
 DT_I4  
  
## Remarks  
 DATEPART returns a null result if the argument is null.  
  
 A date literal must be explicitly cast to one of the date data types. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
 The following table lists the dateparts and abbreviations recognized by the expression evaluator. Datepart names are not case sensitive.  
  
|Datepart|Abbreviations|  
|--------------|-------------------|  
|Year|yy, yyyy|  
|Quarter|qq, q|  
|Month|mm, m|  
|Dayofyear|dy, y|  
|Day|dd, d|  
|Week|wk, ww|  
|Weekday|dw|  
|Hour|Hh|  
|Minute|mi, n|  
|Second|ss, s|  
|Millisecond|Ms|  
  
## SSIS Expression Examples  
 This example returns the integer that represents the month in a date literal. If the date is in mm/dd/yyyy" format, this example returns 11.  
  
```  
DATEPART("month", (DT_DBTIMESTAMP)"11/04/2002")  
```  
  
 This example returns the integer that represents the day in the **ModifiedDate** column.  
  
```  
DATEPART("dd", ModifiedDate)  
```  
  
 This example returns the integer that represents the year of the current date.  
  
```  
DATEPART("yy",GETDATE())  
```  
  
## See Also  
 [DATEADD &#40;SSIS Expression&#41;](../../integration-services/expressions/dateadd-ssis-expression.md)   
 [DATEDIFF &#40;SSIS Expression&#41;](../../integration-services/expressions/datediff-ssis-expression.md)   
 [DAY &#40;SSIS Expression&#41;](../../integration-services/expressions/day-ssis-expression.md)   
 [MONTH &#40;SSIS Expression&#41;](../../integration-services/expressions/month-ssis-expression.md)   
 [YEAR &#40;SSIS Expression&#41;](../../integration-services/expressions/year-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
