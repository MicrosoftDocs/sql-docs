---
title: "Broker:Corrupted Message Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "Broker:Corrupted Message event class"
ms.assetid: 084bf198-2138-438e-bdc7-4ff1e04300f7
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Broker:Corrupted Message Event Class
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates a **Broker:Corrupted Message** event when Service Broker receives a corrupted message.  
  
## Broker:Corrupted Message Event Class Data Columns  
  
|Data column|Type|Description|Column number|Filterable|  
|-----------------|----------|-----------------|-------------------|----------------|  
|**ApplicationName**|**nvarchar**|The name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|**BigintData1**|**bigint**|The sequence number of this message.|52|No|  
|**BinaryData**|**image**|The message body of the message.|2|Yes|  
|**ClientProcessID**|**int**|The ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|9|Yes|  
|**DatabaseID**|**int**|The ID of the database specified by the USE *database* statement, or the ID of the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**Error**|**int**|The message id number in **sys.messages** for the text in the event.|31|No|  
|**EventClass**|**int**|The type of event class captured. Always **161** for **Broker:Corrupted Message**.|27|No|  
|**EventSequence**|**int**|Sequence number for this event.|51|No|  
|**FileName**|**nvarchar**|The network address of the remote endpoint.|36|No|  
|**GUID**|**uniqueidentifier**|The conversation ID of the conversation that the corrupted message belongs to. This identifier is transmitted as part of the message, and is shared between both sides of the conversation.|54|No|  
|**Host Name**|**nvarchar**|The name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the HOST_NAME function.|8|Yes|  
|**IntegerData**|**int**|The fragment number of this message.|25|Yes|  
|**IsSystem**|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|No|  
|**LoginSid**|**image**|The security identification number (SID) of the logged-in user. Each SID is unique for each login in the server.|41|Yes|  
|**NTDomainName**|**nvarchar**|The Windows domain to which the user belongs.|7|Yes|  
|**NTUserName**|**nvarchar**|The name of the user that owns the connection that generated this event.|6|Yes|  
|**ObjectName**|**nvarchar**|The service name of the other side of the conversation and the connection string that the remote database used to connect to this database.|34|No|  
|**RoleName**|**nvarchar**|The role of the endpoint receiving this message. One of the following values.<br /><br /> **initiator**: The receiving endpoint is the initiator of the conversation.<br /><br /> **target**:                 The receiving endpoint is the target of the conversation.|38|No|  
|**ServerName**|**nvarchar**|The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|**Severity**|**int**|If an error caused [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to drop the message, the severity of the error.|29|No|  
|**SPID**|**int**|The server process ID assigned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the process associated with the client.|12|Yes|  
|**StartTime**|**datetime**|The time at which the event started, when available.|14|Yes|  
|**State**|**int**|Indicates the location within the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] source code that produced the event. Each location that may produce this event has a different state code. A Microsoft support engineer can use this state code to find where the event was produced.|30|No|  
|**TextData**|**ntext**|Description of the corruption detected.|1|Yes|  
|**Transaction ID**|**bigint**|The system-assigned ID of the transaction.|4|No|  
  
 The **TextData** column of this event contains a message that describes the problem with the message.  
  
  
