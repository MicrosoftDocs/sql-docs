---
title: "MSSQL_ENG020574 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "MSSQL_ENG02574 error"
ms.assetid: 4e98f8de-287c-4090-81ee-dc8f80dfa6a1
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQL_ENG020574
    
## Message Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|20574|  
|Event Source|MSSQLSERVER|  
|Component|[!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]|  
|Symbolic Name||  
|Message Text|Subscriber '%s' subscription to article '%s' in publication '%s' failed data validation.|  
  
## Explanation  
 The data at the Subscriber was validated against the data at the Publisher, and the data did not match; therefore validation failed. For more information about validation, see [Validate Replicated Data](validate-data-at-the-subscriber.md).  
  
## User Action  
 We recommend that you do the following:  
  
-   Determine why validation failed.  
  
-   Correct the underlying issue that caused the failure.  
  
-   Bring the data into convergence by reinitializing the subscription or through another method.  
  
## See Also  
 [Errors and Events Reference &#40;Replication&#41;](errors-and-events-reference-replication.md)  
  
  
