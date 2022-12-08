---
description: "Broker:Remote Message Ack Event Class"
title: "Broker:Remote Message Ack Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "Broker:Remote Message Ack event class"
ms.assetid: 3d67efe1-74b4-4633-b029-c6e05b19f4dc
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Broker:Remote Message Ack Event Class

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a **Broker:Remote Message Ack** event when [!INCLUDE[ssSB](../../includes/sssb-md.md)] sends or receives a message acknowledgement.  
  
## Broker:Remote Message Ack Event Class Data Columns  
  
|Data column|Type|Description|Column number|Filterable|  
|-----------------|----------|-----------------|-------------------|----------------|  
|**ApplicationName**|**nvarchar**|The name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values that are  passed by the application, instead of the displayed name of the program.|10|Yes|  
|**BigintData1**|**bigint**|The sequence number of the message that contains the acknowledgement.|52|No|  
|**BigintData2**|**bigint**|The sequence number of the message that is being acknowledged.|53|No|  
|**ClientProcessID**|**int**|The ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|9|Yes|  
|**DatabaseID**|**int**|The ID of the database that is specified by the USE *database* statement. If no USE *database* statement has been issued for a given instance, the ID of the default database. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**EventClass**|**int**|The type of event class captured. Always **149** for **Broker:Message Ack**.|27|No|  
|**EventSequence**|**int**|Sequence number for this event.|51|No|  
|**EventSubClass**|**nvarchar**|The type of event subclass, providing more information about each event class. This column can contain the following values:<br /><br /> **Message With Acknowledgement Sent**:<br />                    [!INCLUDE[ssSB](../../includes/sssb-md.md)] sent an acknowledgement as part of a normal sequenced message.<br /><br /> **Acknowledgement Sent**:<br />                    [!INCLUDE[ssSB](../../includes/sssb-md.md)] sent an acknowledgement outside a normal sequenced message.<br /><br /> **Message With Acknowledgement Received**:<br />                  [!INCLUDE[ssSB](../../includes/sssb-md.md)] received an acknowledgement as part of a normal sequenced message.<br /><br /> **Acknowledgement Received**:<br />                  [!INCLUDE[ssSB](../../includes/sssb-md.md)] received an acknowledgement outside a sequenced message.|21|Yes|  
|**GUID**|**uniqueidentifier**|The conversation ID of the dialog. This identifier is transmitted as part of the message, and is shared between both sides of the conversation.|54|No|  
|**HonorBrokerPriority**|**Int**|The current value of the database HONOR_BROKER_PRIORITY option: 0 = OFF, 1 = ON.|32|Yes|  
|**HostName**|**nvarchar**|The name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the HOST_NAME function.|8|Yes|  
|**IntegerData**|**int**|The fragment number of the message that contains the acknowledgement.|25|No|  
|**IntegerData2**|**int**|The fragment number of the message being acknowledged.|55|No|  
|**IsSystem**|**int**|Indicates whether the event occurred on a system process or a user process.<br /><br /> 0 = user<br /><br /> 1 = system|60|No|  
|**LoginSid**|**image**|The security identification number (SID) of the logged-in user. Each SID is unique for each login in the server.|41|Yes|  
|**NTDomainName**|**nvarchar**|The Windows domain to which the user belongs.|7|Yes|  
|**NTUserName**|**nvarchar**|The name of the user that owns the connection that generated this event.|6|Yes|  
|**Priority**|**int**|The priority level of the conversation.|5|Yes|  
|**RoleName**|**nvarchar**|The role of the instance that is acknowledging the message. This is either **initiator** or **target**.|38|No|  
|**ServerName**|**nvarchar**|The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is being traced.|26|No|  
|**SPID**|**int**|The server process ID assigned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the process that is associated with the client.|12|Yes|  
|**StartTime**|**datetime**|The time at which the event started, when available.|14|Yes|  
|**StarvationElevation**|**int**|The message was sent with a higher priority than the priority that was configured for the conversation: 0 = false, 1 = true.|33|Yes|  
|**TransactionID**|**bigint**|The system-assigned ID of the transaction.|4|No|  
  
  
