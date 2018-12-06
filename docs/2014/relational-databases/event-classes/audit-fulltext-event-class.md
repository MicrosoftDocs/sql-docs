---
title: "Audit Fulltext Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
ms.assetid: 95e4c5fd-e16f-446e-b42b-105495a8f39a
author: stevestein
ms.author: sstein
manager: craigg
---
# Audit Fulltext Event Class
  The **Audit Fulltext** event class occurs when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connects to and communicates with the full-text filter daemon process.  
  
## Audit Fulltext Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|**Error**|**int**|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error number, if this event reports an error.|31|Yes|  
|**EventSequence**|**int**|The sequence of a given event within the request.|51|No|  
|**EventSubClass**|**int**|Type of connection used by the login. 1 = Nonpooled, 2 = Pooled.|21|Yes|  
|**IsSystem**|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|**SessionLoginName**|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, **SessionLoginName** shows Login1 and **LoginName** shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|**SPID**|**int**|ID of the session on which the event occurred.|12|Yes|  
|**StartTime**|**datetime**|Time at which the event started, if available.|14|Yes|  
|**Success**|**int**|1 = success. 0 = failure. For example, a value of 1 indicates success of a permissions check and a value of 0 indicates failure of that check.|23|Yes|  
|**TargetLoginName**|**int**|For actions that target a login (for example, adding a new login), the name of the targeted login.|42|Yes|  
|**TargetLoginSid**|**int**|For actions that target a login (for example, adding a new login), the security identification number (SID) of the targeted login.|43|Yes|  
|**TextData**|**ntext**|Text information about the Full-Text event. Typically this field provides information about the connection between the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process and the full-text filter daemon process|1|Yes|  
  
## See Also  
 [Extended Events](../extended-events/extended-events.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql)  
  
  
