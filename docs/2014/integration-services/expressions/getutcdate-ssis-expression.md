---
title: "GETUTCDATE (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "dates [Integration Services], GETUTCDATE"
  - "current date"
  - "UTC time"
  - "GETUTCDATE function"
ms.assetid: 2282339c-c24f-493e-8e66-181ea8af5ad0
author: janinezhang
ms.author: janinez
manager: craigg
---
# GETUTCDATE (SSIS Expression)
  Returns the current date of the system in UTC time (Universal Time Coordinate or Greenwich Mean Time) using a DT_DBTIMESTAMP format. The GETUTCDATE function takes no arguments.  
  
## Syntax  
  
```  
  
GETUTCDATE()  
```  
  
## Arguments  
 None  
  
## Result Types  
 DT_DBTIMESTAMP  
  
## Expression Examples  
 This example returns the year of the current date in UTC time.  
  
```  
DATEPART("year",GETUTCDATE())  
```  
  
 This example returns the number of days between a date in the **ModifiedDate** column and the current UTC date.  
  
```  
DATEDIFF("dd",ModifiedDate,GETUTCDATE())  
```  
  
 This example adds three months to the current UTC date.  
  
```  
DATEADD("Month",3,GETUTCDATE())  
```  
  
## See Also  
 [GETDATE &#40;SSIS Expression&#41;](getdate-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](functions-ssis-expression.md)  
  
  
