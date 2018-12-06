---
title: "blocked process threshold Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "thresholds [SQL Server]"
  - "blocked process threshold option"
ms.assetid: 3d46d143-bc6a-4220-8b55-6baa37547c25
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# blocked process threshold Server Configuration Option
  Use the **blocked process threshold** option to specify the threshold, in seconds, at which blocked process reports are generated. The threshold can be set from 0 to 86,400. By default, no blocked process reports are produced. This event is not generated for system tasks or for tasks that are waiting on resources that do not generate detectable deadlocks.  
  
 You can define an [alert](../../ssms/agent/alerts.md) to be executed when this event is generated. So for example, you can choose to page the administrator to take appropriate action to handle the blocking situation.  
  
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
 [sp_trace_setevent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql)   
 [Blocked Process Report Event Class](../../relational-databases/event-classes/blocked-process-report-event-class.md)  
  
  
