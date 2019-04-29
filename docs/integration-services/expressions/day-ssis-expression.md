---
title: "DAY (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "DAY function"
  - "dates [Integration Services], DAY"
ms.assetid: d8447187-49df-45b7-a98e-142ad44fd3e2
author: janinezhang
ms.author: janinez
manager: craigg
---
# DAY (SSIS Expression)
  Returns an integer that represents the day datepart of a date.  
  
## Syntax  
  
```  
  
DAY(date)  
```  
  
## Arguments  
 *date*  
 Is an expression that returns a valid date or a string in date format.  
  
## Result Types  
 DT_I4  
  
## Remarks  
 DAY returns a null result if the argument is null.  
  
 A date literal must be explicitly cast to one of the date data types. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
> [!NOTE]  
>  The expression fails to validate when a date literal is explicitly cast to one of these date data types: DT_DBTIMESTAMPOFFSET and DT_DBTIMESTAMP2.  
  
 Using the DAY function is briefer but equivalent to using DATEPART("Day", date).  
  
## Expression Examples  
 This example returns the number of the day in a date literal. If the date format is in "mm/dd/yyyy" format, this example returns 23.  
  
```  
DAY((DT_DBTIMESTAMP)"11/23/2002")  
```  
  
 This example returns the integer that represents the day in the **ModifiedDate** column.  
  
```  
DAY(ModifiedDate)  
```  
  
 This example returns the integer that represents the day of the current date.  
  
```  
DAY(GETDATE())  
```  
  
## See Also  
 [DATEADD &#40;SSIS Expression&#41;](../../integration-services/expressions/dateadd-ssis-expression.md)   
 [DATEDIFF &#40;SSIS Expression&#41;](../../integration-services/expressions/datediff-ssis-expression.md)   
 [DATEPART &#40;SSIS Expression&#41;](../../integration-services/expressions/datepart-ssis-expression.md)   
 [MONTH &#40;SSIS Expression&#41;](../../integration-services/expressions/month-ssis-expression.md)   
 [YEAR &#40;SSIS Expression&#41;](../../integration-services/expressions/year-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
