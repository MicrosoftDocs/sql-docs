---
title: "Broker:Conversation Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Broker:Conversation event class"
ms.assetid: 784707b5-cc67-46a3-8ae6-8f8ecf4b27c0
author: stevestein
ms.author: sstein
manager: craigg
---
# Broker:Conversation Event Class
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a **Broker:Conversation** event to report the progress of a Service Broker conversation.  
  
## Broker:Conversation Event Class Data Columns  
  
|Data column|Type|Description|Column number|Filterable|  
|-----------------|----------|-----------------|-------------------|----------------|  
|**ApplicationName**|`nvarchar`|The name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application instead of the displayed name of the program.|10|Yes|  
|**ClientProcessID**|`int`|The ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|9|Yes|  
|**DatabaseID**|`int`|The ID of the database that is specified by the USE *database* statement. If no USE *database*statement has been issued, the ID of the default database. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the **DB_ID** function.|3|Yes|  
|**EventClass**|`int`|The type of event class captured. Always **124** for **Broker:Conversation**.|27|No|  
|**EventSequence**|`int`|Sequence number for this event.|51|No|  
|**EventSubClass**|`nvarchar`|The type of event subclass. This provides more information about each event class.|21|Yes|  
|**GUID**|`uniqueidentifier`|The conversation ID of the dialog. This identifier is transmitted as part of the message, and is shared between both sides of the conversation.|54|No|  
|**HostName**|`nvarchar`|The name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the **HOST_NAME** function.|8|Yes|  
|**IsSystem**|`int`|Indicates whether the event occurred on a system process or a user process.<br /><br /> 0 = user<br /><br /> 1 = system|60|No|  
|**LoginSid**|`image`|The security identification number (SID) of the logged-in user. Each SID is unique for each login in the server.|41|Yes|  
|**MethodName**|`nvarchar`|The conversation group that the conversation belongs to.|47|No|  
|**NTDomainName**|`nvarchar`|The Windows domain to which the user belongs.|7|Yes|  
|**NTUserName**|`nvarchar`|The name of the user that owns the connection that generated this event.|6|Yes|  
|**ObjectName**|`nvarchar`|The conversation handle of the dialog.|34|No|  
|**Priority**|`int`|The priority level of the conversation|5|Yes|  
|**RoleName**|`nvarchar`|The role of the conversation handle. This is either **initiator** or **target**.|38|No|  
|**ServerName**|`nvarchar`|The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is being traced.|26|No|  
|**Severity**|`int`|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error severity, if this event reports an error.|29|No|  
|**SPID**|`int`|The server process ID that is assigned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the process that is associated with the client.|12|Yes|  
|**StartTime**|`datetime`|The time when the event started, when available.|14|Yes|  
|**TextData**|`ntext`|The current state of the conversation. One of the following:<br /><br /> **SO**. Started outbound. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] processed a BEGIN CONVERSATION for this conversation, but no messages have been sent.<br /><br /> **SI**. Started inbound. Another instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] started a new conversation with the current instance, but the current instance has not finished receiving the first message. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might create the conversation in this state if the first message is fragmented or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] receives messages out of order. However, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might create the conversation in the CO state if the first transmission that was received for the conversation contains the complete first message.<br /><br /> **CO**. Conversing. The conversation is established, and both sides of the conversation can send messages. Most communication for a typical service happens when the conversation is in this state.<br /><br /> **DI**. Disconnected inbound. The remote side of the conversation has issued an END CONVERSATION. The conversation remains in this state until the local side of the conversation issues an END CONVERSATION. An application can still receive messages for the conversation. Because the remote side of the conversation has ended the conversation, an application cannot send messages on this conversation. When an application issues an END CONVERSATION, the conversation moves to the Closed (CD) state.<br /><br /> **DO**. Disconnected outbound. The local side of the conversation has issued an END CONVERSATION. The conversation remains in this state until the remote side of the conversation acknowledges the END CONVERSATION. An application cannot send or receive messages for the conversation. When the remote side of the conversation acknowledges the END CONVERSATION, the conversation moves to the Closed (CD) state.<br /><br /> **ER**. Error. An error has occurred on this endpoint. The Error, Severity, and State columns contain information about the specific error that occurred.<br /><br /> **CD**. Closed. The conversation endpoint is no longer in use.|1|Yes|  
|**Transaction ID**|`bigint`|The system-assigned ID of the transaction.|4|No|  
  
 The following table lists the subclass values for this event class.  
  
|ID|Subclass|Description|  
|--------|--------------|-----------------|  
|1|SEND Message|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a **SEND Message** event when the [!INCLUDE[ssDE](../../includes/ssde-md.md)] executes a SEND statement.|  
|2|END CONVERSATION|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates an **END CONVERSATION** event when the [!INCLUDE[ssDE](../../includes/ssde-md.md)] executes an END CONVERSATION statement that does not include the WITH ERROR clause.|  
|3|END CONVERSATION WITH ERROR|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates an **END CONVERSATION WITH ERROR** event when the [!INCLUDE[ssDE](../../includes/ssde-md.md)] executes an END CONVERSATION statement that includes the WITH ERROR clause.|  
|4|Broker Initiated Error|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a **Broker Initiated Error** event whenever [!INCLUDE[ssSB](../../includes/sssb-md.md)] creates an error message. For example, when [!INCLUDE[ssSB](../../includes/sssb-md.md)] cannot successfully route a message for a dialog, the broker creates an error message for the dialog and generates this event. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not generate this event when an application program ends a conversation with an error.|  
|5|Terminate Dialog|[!INCLUDE[ssSB](../../includes/sssb-md.md)] terminated the dialog. [!INCLUDE[ssSB](../../includes/sssb-md.md)] terminates dialogs in response to conditions that prevent the dialog from continuing, but which are not errors or the normal end of a conversation. For example, dropping a service causes [!INCLUDE[ssSB](../../includes/sssb-md.md)] to terminate all dialogs for that service.|  
|6|Received Sequenced Message|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a **Received Sequenced Message** event class when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] receives a message that contains a message sequence number. All user-defined message types are sequenced messages. [!INCLUDE[ssSB](../../includes/sssb-md.md)] generates an unsequenced message in two cases:<br /><br /> Error messages generated by [!INCLUDE[ssSB](../../includes/sssb-md.md)] are unsequenced.<br /><br /> Message acknowledgements might be unsequenced. For efficiency, [!INCLUDE[ssSB](../../includes/sssb-md.md)] includes message any available acknowledgement as part of a sequenced message . However, if an application does not send a sequenced message to the remote endpoint within a certain period of time, [!INCLUDE[ssSB](../../includes/sssb-md.md)] creates an unsequenced message for the message acknowledgement.|  
|7|Received END CONVERSATION|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a Received END CONVERSATION event when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] receives an End Dialog message from the other side of the conversation.|  
|8|Received END CONVERSATION WITH ERROR|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a **Received END CONVERSATION WITH ERROR** event when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] receives a user-defined error from the other side of the conversation. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not generate this event when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] receives a broker-defined error.|  
|9|Received Broker Error Message|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a **Received Broker Error Message** event when [!INCLUDE[ssSB](../../includes/sssb-md.md)] receives a broker-defined error message from the other side of the conversation. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not generate this event when [!INCLUDE[ssSB](../../includes/sssb-md.md)] receives an error message that was generated by an application.<br /><br /> For example, if the current database contains a default route to a forwarding database, [!INCLUDE[ssSB](../../includes/sssb-md.md)] routes a message with an unknown service name to the forwarding database. If that database cannot route the message, the broker in that database creates an error message and returns that error message to the current database. When the current database receives the broker-generated error from the forwarding database, the current database generates a **Received Broker Error Message** event.|  
|10|Received END CONVERSATION Ack|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a **Received END CONVERSATION Ack** event class when the other side of a conversation acknowledges an End Dialog or Error message sent by this side of the conversation.|  
|11|BEGIN DIALOG|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a **BEGIN DIALOG** event when the Database Engine executes a BEGIN DIALOG command.|  
|12|Dialog Created|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a **Dialog Created** event when [!INCLUDE[ssSB](../../includes/sssb-md.md)] creates an endpoint for a dialog. [!INCLUDE[ssSB](../../includes/sssb-md.md)] creates an endpoint whenever a new dialog is established, regardless of whether the current database is the initiator or the target of the dialog.|  
|13|END CONVERSATION WITH CLEANUP|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates an END CONVERSATION WITH CLEANUP event when the [!INCLUDE[ssDE](../../includes/ssde-md.md)] executes an END CONVERSATION statement that includes the WITH CLEANUP clause.|  
  
## See Also  
 [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)  
  
  
