---
title: "Broker:Forwarded Message Dropped Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Broker:Forwarded Message Dropped event class"
ms.assetid: ec242d0b-77b0-45f5-8b12-186a14b173a8
author: stevestein
ms.author: sstein
manager: craigg
---
# Broker:Forwarded Message Dropped Event Class
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a Broker:Forwarded Message Dropped event when Service Broker drops a message that was intended to be forwarded.  
  
## Broker:Forwarded Message Dropped Event Class Data Columns  
  
|Data column|Type|Description|Column number|Filterable|  
|-----------------|----------|-----------------|-------------------|----------------|  
|ApplicationName|`nvarchar`|The name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|BigintData1|`bigint`|Message sequence number.|52|No|  
|ClientProcessID|`int`|The ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|9|Yes|  
|DatabaseID|`int`|The ID of the database specified by the USE *database* statement, or the ID of the default database if no USE *database*statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the Server Name data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|DatabaseName|`nvarchar`|The name of the database in which the user statement is running.|35|Yes|  
|DBUserName|`nvarchar`|The broker instance identifier that this message is from.|40|No|  
|Error|`int`|The message id number in sys.messages for the text in the event.|31|No|  
|EventClass|`int`|The type of event class captured. Always 191 for Broker:Forwarded Message Dropped.|27|No|  
|EventSequence|`int`|Sequence number for this event.|51|No|  
|FileName|`nvarchar`|The name of the service that the message is to.|36|No|  
|GUID|`uniqueidentifier`|The conversation id of the dialog. This identifier is transmitted as part of the message, and is shared between both sides of the conversation.|54|No|  
|HostName|`nvarchar`|The name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the HOST_NAME function.|8|Yes|  
|IndexID|`int`|The number of hops remaining for the forwarded message.|24|No|  
|IntegerData|`int`|The fragment number of the forwarded message.|25|No|  
|LoginSid|`image`|The security identification number (SID) of the logged-in user. Each SID is unique for each login in the server.|41|Yes|  
|NTDomainName|`nvarchar`|The Windows domain to which the user belongs.|7|Yes|  
|NTUserName|`nvarchar`|The name of the user that owns the connection that generated this event.|6|Yes|  
|ObjectId|`int`|The time to live value of the forwarded message.|22|No|  
|ObjectName|`nvarchar`|Message ID of the forwarded message.|34|No|  
|OwnerName|`nvarchar`|The broker instance identifier for the destination of the message.|37|No|  
|RoleName|`nvarchar`|The role of the conversation handle. One of:<br /><br /> Initiator. This broker initiated the conversation.<br /><br /> Target. This broker is the target of the conversation.|38|No|  
|ServerName|`nvarchar`|The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|Severity|`int`|Severity number for the text in the event.|29|No|  
|SPID|`int`|The server process ID assigned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the process associated with the client.|12|Yes|  
|StartTime|`datetime`|The time at which the event started, when available.|14|Yes|  
|State|`int`|Indicates the location within the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] source code that produced the event. Each location that may produce this event has a different state code. A Microsoft support engineer can use this state code to find where the event was produced.|30|No|  
|Success|`int`|The amount of time that the message has been alive. When this value is greater than or equal to the time to live, the message is dropped.|23|No|  
|TargetLoginName|`nvarchar`|The network address that the message would have been forwarded to.|42|No|  
|TargetUserName|`nvarchar`|The name of the initiating service for the message.|39|No|  
|TextData|`ntext`|Description of the reason that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] dropped the message.|1|Yes|  
|Transaction ID|`bigint`|The system-assigned ID of the transaction.|4|No|  
  
 The TextData column of this event contains a description of the reason that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] dropped the message.  
  
## See Also  
 [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)  
  
  
