---
title: "Broker:Activation Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Broker:Activation event class"
ms.assetid: 481d5b13-657e-4b51-8783-ccac3595bd45
author: stevestein
ms.author: sstein
manager: craigg
---
# Broker:Activation Event Class
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a **Broker:Activation** event when a queue monitor starts an activation stored procedure, sends a QUEUE_ACTIVATION notification, or when an activation stored procedure started by a queue monitor exits.  
  
## Broker:Activation Event Class Data Columns  
  
|Data column|Type|Description|Column number|Filterable|  
|-----------------|----------|-----------------|-------------------|----------------|  
|**ClientProcessID**|`int`|The ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|9|Yes|  
|**DatabaseID**|`int`|The ID of the database specified by the USE *database* statement, or the ID of the default database if no USE *database*statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**EventClass**|`int`|The type of event class captured. Always **163** for **Broker:Activation**.|27|No|  
|**EventSequence**|`int`|Sequence number for this event.|51|No|  
|**EventSubClass**|`nvarchar`|The specific action that this event reports. One of the following values:<br /><br /> **start**: <br />                [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] started an activation stored procedure.<br /><br /> **ended**: The activation stored procedure exited normally.<br /><br /> **aborted**: The activation stored procedure exited with an error.|21|No|  
|**HostName**|`nvarchar`|The name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the HOST_NAME function.|8|Yes|  
|**IntegerData**|`int`|The number of tasks active on this queue.|25|No|  
|**IsSystem**|`int`|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|No|  
|**LoginSid**|`image`|The security identification number (SID) of the logged-in user. Each SID is unique for each login in the server.|41|Yes|  
|**NTDomainName**|`nvarchar`|The Windows domain to which the user belongs.|7|Yes|  
|**NTUserName**|`nvarchar`|The name of the user that owns the connection that generated this event.|6|Yes|  
|**ObjectID**|`int`|The queue associated with this event.|22|No|  
|**ServerName**|`nvarchar`|The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|**SPID**|`int`|The server process ID assigned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the process associated with the client.|12|Yes|  
|**StartTime**|`datetime`|The time at which the event started, when available.|14|Yes|  
|**TransactionID**|`bigint`|The system-assigned ID of the transaction.|4|No|  
  
  
