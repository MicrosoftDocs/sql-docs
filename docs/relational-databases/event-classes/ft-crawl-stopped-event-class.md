---
title: "FT:Crawl Stopped Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "Crawl Stopped event class"
ms.assetid: dbc91bf7-687c-4083-9694-02f3e102c175
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# FT:Crawl Stopped Event Class
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The **:Crawl Stopped** event class indicates that a full-text crawl (population) has stopped. The stop can be due to a successfully completed crawl or a fatal error.  
  
## FT:Crawl Stopped Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|**DatabaseID**|**int**|ID of the database in which the full-text crawl has stopped. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**EventClass**|**int**|Type of event = 156.|27|No|  
|**EventSequence**|**int**|Sequence of a given event within the request.|51|No|  
|**IsSystem**|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|**ObjectID**|**int**|System-assigned ID of the object. The full-text crawl has stopped for the full-text index on this object.|22|Yes|  
|**SessionLoginName**|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, **SessionLoginName** shows Login1 and **LoginName** shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows logins.|64|Yes|  
|**SPID**|**int**|ID of the session on which the event occurred.|12|Yes|  
|**StartTime**|**datetime**|Time at which the event started, if available.|14|Yes|  
|**TransactionID**|**bigint**|System-assigned ID of the transaction.|4|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)  
  
  
