---
title: "DATEADD (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "dates [Integration Services], DATEADD"
  - "dates [Integration Services]"
  - "DATEADD function"
ms.assetid: fa5c37b1-2ddc-4857-8f8e-f6d5643b654f
author: janinezhang
ms.author: janinez
manager: craigg
---
# DATEADD (SSIS Expression)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Returns a new DT_DBTIMESTAMP value after adding a number that represents a date or time interval to the specified datepart in a date. The number parameter must evaluate to an integer, and the date parameter must evaluate to a valid date.  
  
## Syntax  
  
```  
  
DATEADD(datepart, number, date)  
```  
  
## Arguments  
 *datepart*  
 Is the parameter that specifies which part of the date to add a number to.  
  
 *number*  
 Is the value used to increment *datepart*. The value must be an integer value that is known when the expression is parsed.  
  
 *date*  
 Is an expression that returns a valid date or a string in date format.  
  
## Result Types  
 DT_DBTIMESTAMP  
  
## Remarks  
 The following table lists the dateparts and abbreviations recognized by the expression evaluator. Datepart names are not case sensitive.  
  
|Datepart|Abbreviations|  
|--------------|-------------------|  
|Year|yy, yyyy|  
|Quarter|qq, q|  
|Month|mm, m|  
|Dayofyear|dy, y|  
|Day|dd, d|  
|Week|wk, ww|  
|Weekday|dw, w|  
|Hour|Hh|  
|Minute|mi, n|  
|Second|ss, s|  
|Millisecond|Ms|  
  
 The *number* argument must be available when the expression is parsed. The argument can be a constant or a variable. You cannot use column values because the values are not known when the expression is parsed.  
  
 The *datepart* argument must be enclosed by quotation marks.  
  
 A date literal must be explicitly cast to one of the date data types. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
 DATEADD returns a null result if the argument is null.  
  
 Errors occur if a date is invalid, if the date or time unit is not a string, or if the increment is not a static integer.  
  
## SSIS Expression Examples  
 This example adds one month to the current date.  
  
```  
DATEADD("Month", 1,GETDATE())  
```  
  
 This example adds 21 days to the dates in the **ModifiedDate** column.  
  
```  
DATEADD("day", 21, ModifiedDate)  
```  
  
 This example adds 2 years to a literal date.  
  
```  
DATEADD("yyyy", 2, (DT_DBTIMESTAMP)"8/6/2003")  
```  
  
## See Also  
 [DATEDIFF &#40;SSIS Expression&#41;](../../integration-services/expressions/datediff-ssis-expression.md)   
 [DATEPART &#40;SSIS Expression&#41;](../../integration-services/expressions/datepart-ssis-expression.md)   
 [DAY &#40;SSIS Expression&#41;](../../integration-services/expressions/day-ssis-expression.md)   
 [MONTH &#40;SSIS Expression&#41;](../../integration-services/expressions/month-ssis-expression.md)   
 [YEAR &#40;SSIS Expression&#41;](../../integration-services/expressions/year-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
