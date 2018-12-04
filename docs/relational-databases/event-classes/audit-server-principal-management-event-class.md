---
title: "Audit Server Principal Management Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "Audit Server Principal Management event class"
ms.assetid: 7894850c-91fe-47c0-a03c-baacbc10d29c
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Audit Server Principal Management Event Class
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The **Audit Server Principal Management** event class occurs when server principals are created, altered, or dropped.  
  
## Audit Server Principal Management Event Class Data Columns  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|**ApplicationName**|**nvarchar**|Name of the client application that created the connection to an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|**DatabaseID**|**int**|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**DatabaseName**|**nvarchar**|Name of the database in which the user statement is running.|35|Yes|  
|**DBUserName**|**nvarchar**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user name of the client.|40|Yes|  
|**EventSequence**|**int**|Sequence of a given event within the request.|51|No|  
|**EventSubClass**|**int**|Type of event subclass.<br /><br /> 1=Create<br /><br /> 2=Alter<br /><br /> 3=Drop<br /><br /> 4=Dump<br /><br /> 5=Disable<br /><br /> 6=Enable<br /><br /> 11=Load|21|Yes|  
|**HostName**|**nvarchar**|Name of the computer on which the client is running. This data column is populated if the client provides the host name. To determine the host name, use the HOST_NAME function.|8|Yes|  
|**IsSystem**|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|Yes|  
|**LoginSid**|**image**|Security identification number (SID) of the logged-in user. You can find this information in the **sys.server_principals** catalog view. Each SID is unique for each login in the server.|41|Yes|  
|**NTDomainName**|**nvarchar**|Windows domain to which the user belongs.|7|Yes|  
|**NTUserName**|**nvarchar**|Windows user name.|6|Yes|  
|**ObjectName**|**nvarchar**|Name of the object being referenced.|34|Yes|  
|**ObjectType**|**int**|Value representing the type of the object involved in the event. This value corresponds to the type column in the **sys.objects** catalog view. For values, see [ObjectType Trace Event Column](../../relational-databases/event-classes/objecttype-trace-event-column.md).|28|Yes|  
|**OwnerName**|**nvarchar**|Database user name of the object owner.|37|Yes|  
|**RequestID**|**int**|ID of the request containing the statement.|49|Yes|  
|**ServerName**|**nvarchar**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|**SessionLoginName**|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, **SessionLoginName** shows Login1 and **LoginName** shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|**SPID**|**int**|ID of the session on which the event occurred.|12|Yes|  
|**StartTime**|**datetime**|Time at which the event started, if available.|14|Yes|  
|**Success**|**int**|1 = success. 0 = failure. For example, a value of 1 indicates success of a permissions check and a value of 0 indicates failure of that check.|23|Yes|  
|**XactSequence**|**bigint**|Token used to describe the current transaction.|50|Yes|  
  
## See Also  
 [Extended Events](../../relational-databases/extended-events/extended-events.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)  
  
  
