---
title: "blocked process threshold Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "thresholds [SQL Server]"
  - "blocked process threshold option"
ms.assetid: 3d46d143-bc6a-4220-8b55-6baa37547c25
caps.latest.revision: 20
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# blocked process threshold Server Configuration Option
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Use the **blocked process threshold** option to specify the threshold, in seconds, at which blocked process reports are generated. The threshold can be set from 0 to 86,400. By default, no blocked process reports are produced. This event is not generated for system tasks or for tasks that are waiting on resources that do not generate detectable deadlocks.  
  
 You can define an [alert](http://msdn.microsoft.com/library/3f57d0f0-4781-46ec-82cd-b751dc5affef) to be executed when this event is generated. So for example, you can choose to page the administrator to take appropriate action to handle the blocking situation.  
  
 Blocked process threshold uses the deadlock monitor background thread to walk through the list of tasks waiting for a time greater than or multiples of the configured threshold. The event is generated once per reporting interval for each of the blocked tasks.  
  
 The blocked process report is done on a best effort basis. There is no guarantee of any real-time or even close to real-time reporting.  
  
 The setting takes effect immediately without a server stop and restart.  
  
## Examples  
 The following example sets the `blocked process threshold` to `20` seconds, generating a blocked process report for each task that is blocked.  
  
```  
sp_configure 'show advanced options', 1 ;  
GO  
RECONFIGURE ;  
GO  
sp_configure 'blocked process threshold', 20 ;  
GO  
RECONFIGURE ;  
GO  
```  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [Blocked Process Report Event Class](../../relational-databases/event-classes/blocked-process-report-event-class.md)  
  
  
