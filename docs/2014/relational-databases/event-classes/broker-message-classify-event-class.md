---
title: "Broker:Message Classify Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Broker:Message Classify event class"
ms.assetid: e51f3351-1239-4c41-b87c-1dd86968e027
author: stevestein
ms.author: sstein
manager: craigg
---
# Broker:Message Classify Event Class
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a **Broker:Message Classify** event when Service Broker determines the routing for a message.  
  
## Broker:Message Classify Event Class Data Columns  
  
|Data column|Data type|Description|Column number|Filterable|  
|-----------------|---------------|-----------------|-------------------|----------------|  
|**ApplicationName**|**nvarchar**|The name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|**ClientProcessID**|**int**|The ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|9|Yes|  
|**DatabaseID**|**int**|The ID of the database specified by the USE *database* statement, or the ID of the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**EventClass**|**int**|The type of event class captured. Always **141** for **Broker:Message Classify**.|27|No|  
|**EventSequence**|**int**|Sequence number for this event.|51|No|  
|**EventSubClass**|**nvarchar**|The type of event subclass, providing further information about each event class. This column may contain the following values:<br /><br /> **Local**: The route chosen has the address LOCAL.<br /><br /> **Remote**: The route chosen has an address other than LOCAL.<br /><br /> **Delayed**: The message is delayed, either because forwarding is disabled or because there is no matching route present.|21|Yes|  
|**FileName**|**nvarchar**|The service name that the message is directed to.|36|No|  
|**GUID**|**uniqueidentifier**|The conversation id of the dialog. This identifier is transmitted as part of the message, and is shared between both sides of the conversation.|54|No|  
|**HostName**|**nvarchar**|The name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the HOST_NAME function.|8|Yes|  
|**IsSystem**|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|No|  
|**LoginSid**|**image**|The security identification number (SID) of the logged-in user. Each SID is unique for each login in the server.|41|Yes|  
|**NTDomainName**|**nvarchar**|The Windows domain to which the user belongs.|7|Yes|  
|**NTUserName**|**nvarchar**|The name of the user that owns the connection that generated this event.|6|Yes|  
|**OwnerName**|**nvarchar**|The broker identifier that the message is directed to.|37|No|  
|**RoleName**|**nvarchar**|Indicates whether the message was received from the network, or originated in this [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.|38|No|  
|**ServerName**|**nvarchar**|The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|**SPID**|**int**|The server process ID assigned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the process associated with the client.|12|Yes|  
|**Start Time**|**datetime**|The time at which the event started, when available.|14|Yes|  
|**TargetUserName**|**nvarchar**|The network address of the next hop broker.|39|No|  
|**TransactionID**|**bigint**|The system-assigned ID of the transaction.|4|No|  
  
## See Also  
 [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)  
  
  
