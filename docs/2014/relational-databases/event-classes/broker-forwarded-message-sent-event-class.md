---
title: "Broker:Forwarded Message Sent Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Broker:Forwarded Message Sent event class"
ms.assetid: d0ef74d9-a4ef-4918-aa21-6b267e85569f
author: stevestein
ms.author: sstein
manager: craigg
---
# Broker:Forwarded Message Sent Event Class
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a Broker:Forwarded Message Sent event when Service Broker forwards a message.  
  
## Broker:Forwarded Message Sent Event Class Data Columns  
  
|Data column|Type|Description|Column number|Filterable|  
|-----------------|----------|-----------------|-------------------|----------------|  
|ApplicationName|`nvarchar`|The name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|BigintData1|`bigint`|Message sequence number.|52|No|  
|ClientProcessID|`int`|The ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|9|Yes|  
|DatabaseID|`int`|The ID of the database specified by the USE *database* statement, or the ID of the default database if no USE *database*statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the Server Name data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DBUserName|`nvarchar`|The broker instance id for the service that the message is from.|40|No|  
|EventClass|`int`|The type of event class captured. Always 139 for Broker:Forwarded Message Sent.|27|No|  
|EventSequence|`int`|Sequence number for this event.|51|No|  
|FileName|`nvarchar`|The name of the service that the message is to.|36|No|  
|GUID|`uniqueidentifier`|The conversation id of the dialog. This identifier is transmitted as part of the message, and is shared between both sides of the conversation.|54|No|  
|HostName|`nvarchar`|The name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IndexID|`int`|The number of hops remaining for the forwarded message.|24|No|  
|IntegerData|`int`|The fragment number of the forwarded message.|25|No|  
|IsSystem|`int`|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|No|  
|LoginSid|`image`|The security identification number (SID) of the logged-in user. Each SID is unique for each login in the server.|41|Yes|  
|NTDomainName|`nvarchar`|The Windows domain to which the user belongs.|7|Yes|  
|NTUserName|`nvarchar`|The name of the user that owns the connection that generated this event.|6|Yes|  
|ObjectId|`int`|The time to live value for the forwarded message when the message was forwarded.|22|No|  
|ObjectName|`nvarchar`|Message ID of the forwarded message.|34|No|  
|OwnerName|`nvarchar`|The broker identifier that the message is directed to.|37|No|  
|RoleName|`nvarchar`|The role of the conversation handle. One of:<br /><br /> Initiator. This broker initiated the conversation.<br /><br /> Target. This broker is the target of the conversation.|38|No|  
|ServerName|`nvarchar`|The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|SPID|`int`|The server process ID assigned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the process associated with the client.|12|Yes|  
|StartTime|`datetime`|The time at which the event started, when available.|14|Yes|  
|Success|`int`|The amount of time consumed during the forwarding process.|23|No|  
|TargetLoginName|`nvarchar`|The network address that this instance sent the message to. Notice that this may be different than the final destination for the message.|42|No|  
|TargetUserName|`nvarchar`|The name of the initiating service for the message.|39|No|  
|TransactionID|`bigint`|The system-assigned ID of the transaction.|4|No|  
  
  
