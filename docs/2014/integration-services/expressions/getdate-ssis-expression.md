---
title: "GETDATE (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "current date"
  - "GETDATE function"
  - "dates [Integration Services], GETDATE"
ms.assetid: 6d20ec93-3244-4d63-baf6-70eff7bd598c
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# GETDATE (SSIS Expression)
  Returns the current date of the system in a DT_DBTIMESTAMP format. The GETDATE function takes no arguments.  
  
> [!NOTE]  
>  The length of the return result from the GETDATE function is 29 characters.  
  
## Syntax  
  
```  
  
GETDATE()  
```  
  
## Arguments  
 None  
  
## Result Types  
 DT_DBTIMESTAMP  
  
## Expression Examples  
 This example returns the year of the current date.  
  
```  
DATEPART("year",GETDATE())  
```  
  
 This example returns the number of days between a date in the **ModifiedDate** column and the current date.  
  
```  
DATEDIFF("dd",ModifiedDate,GETDATE())  
```  
  
 This example adds three months to the current date.  
  
```  
DATEADD("Month",3,GETDATE())  
```  
  
## See Also  
 [GETUTCDATE &#40;SSIS Expression&#41;](getutcdate-ssis-expression.md)   
 [Functions &#40;SSIS Expression&#41;](functions-ssis-expression.md)  
  
  
