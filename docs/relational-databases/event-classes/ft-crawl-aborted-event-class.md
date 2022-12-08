---
description: "FT:Crawl Aborted Event Class"
title: "FT:Crawl Aborted Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "Crawl Aborted event class"
ms.assetid: eead8ea6-5051-4689-ab30-4dfbfda01fb9
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# FT:Crawl Aborted Event Class
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  The **FT:Crawl Aborted** event class indicates that an exception has been encountered during a full-text crawl. The error usually causes the full-text crawl to stop. Check the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows event log or the crawl log for more detailed error information.  
  
## FT:Crawl Aborted Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|**DatabaseID**|**int**|ID of the database in which the full-text crawl is running. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**Error**|**int**|Error number of a given event. Often this is the error number stored in the **sysmessages** table.|31|Yes|  
|**EventClass**|**int**|Type of event = 157.|27|No|  
|**EventSequence**|**int**|Sequence of a given event within the request.|51|No|  
|**IsSystem**|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|**ObjectID**|**int**|System-assigned ID of the object on which the full-text crawl is running when the failure occurs.|22|Yes|  
|**SessionLoginName**|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, **SessionLoginName** shows Login1 and **LoginName** shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|**SPID**|**int**|ID of the session on which the event occurred.|12|Yes|  
|**StartTime**|**datetime**|Time at which the event started, if available.|14|Yes|  
|**State**|**int**|Equivalent to an error state code.|30|Yes|  
|**TransactionID**|**bigint**|System-assigned ID of the transaction.|4|Yes|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)  
  
  
