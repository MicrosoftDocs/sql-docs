---
title: "YEAR (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "dates [Integration Services], YEAR"
  - "YEAR function"
ms.assetid: 9d88dead-ace8-44b9-b8e2-916c1842e155
author: janinezhang
ms.author: janinez
manager: craigg
---
# YEAR (SSIS Expression)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Returns an integer that represents the year datepart of a date.  
  
## Syntax  
  
```  
  
YEAR(date)  
```  
  
## Arguments  
 *date*  
 Is a date in any date format.  
  
## Result Types  
 DT_I4  
  
## Remarks  
 YEAR returns a null result if the argument is null.  
  
 A date literal must be explicitly cast to one of the date data types. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
> [!NOTE]  
>  The expression fails to validate when a date literal is explicitly cast to one of these date data types: DT_DBTIMESTAMPOFFSET and DT_DBTIMESTAMP2.  
  
 Using the YEAR function is briefer but equivalent to using the DATEPART("Year", date) function.  
  
## Expression Examples  
 This example returns the number of the year in a date literal. If the date is in mm/dd/yyyy format, this example returns "2002".  
  
```  
YEAR((DT_DBTIMESTAMP)"11/23/2002")  
```  
  
 This example returns the integer that represents the year in the **ModifiedDate** column.  
  
```  
YEAR(ModifiedDate)  
```  
  
 This example returns the integer that represents the year of the current date.  
  
```  
YEAR(GETDATE())  
```  
  
## See Also  
 [DATEADD &#40;SSIS Expression&#41;](../../integration-services/expressions/dateadd-ssis-expression.md)   
 [DATEDIFF &#40;SSIS Expression&#41;](../../integration-services/expressions/datediff-ssis-expression.md)   
 [DATEPART &#40;SSIS Expression&#41;](../../integration-services/expressions/datepart-ssis-expression.md)   
 [DAY &#40;SSIS Expression&#41;](../../integration-services/expressions/day-ssis-expression.md)   
 [MONTH &#40;SSIS Expression&#41;](../../integration-services/expressions/month-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
