---
title: "Audit Broker Conversation Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Audit Broker Conversation event class"
ms.assetid: d58e3577-e297-42e5-b8fe-206665a75d13
author: stevestein
ms.author: sstein
manager: craigg
---
# Audit Broker Conversation Event Class
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates an **Audit Broker Conversation** event to report audit messages related to Service Broker dialog security.  
  
## Audit Broker Conversation Event Class Data Columns  
  
|Data column|Type|Description|Column number|Filterable|  
|-----------------|----------|-----------------|-------------------|----------------|  
|**ApplicationName**|**nvarchar**|The name of the client application that created the connection to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This column is populated with the values passed by the application rather than the displayed name of the program.|10|Yes|  
|**BigintData1**|**bigint**|The message sequence number of the message.|52|No|  
|**ClientProcessID**|**int**|The ID assigned by the host computer to the process where the client application is running. This data column is populated if the client process ID is provided by the client.|9|Yes|  
|**DatabaseID**|**int**|The ID of the database specified by the USE *database* statement, or the ID of the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**Error**|**int**|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error number, if this event reports an error.|31|No|  
|**EventClass**|**int**|The type of event class captured. Always **158** for **Audit Broker Conversation**.|27|No|  
|**EventSubClass**|**int**|The type of event subclass, providing further information about each event class. The table below lists the event subclass values for this event.|21|Yes|  
|**FileName**|**nvarchar**|The reason for the login failure. If the login succeeded, this column is empty.|36|No|  
|**GUID**|**uniqueidentifier**|The conversation id of the dialog. This identifier is transmitted as part of the message, and is shared between both sides of the conversation.|54|No|  
|**HostName**|**nvarchar**|The name of the computer on which the client is running. This data column is populated if the host name is provided by the client. To determine the host name, use the **HOST_NAME** function.|8|Yes|  
|**IntegerData**|**int**|The fragment number of the message.|25|No|  
|**NTDomainName**|**nvarchar**|The Windows domain to which the user belongs.|7|Yes|  
|**NTUserName**|**nvarchar**|The name of the user that owns the connection that generated this event.|6|Yes|  
|**ObjectId**|**int**|The user ID of the target service.|22|No|  
|**RoleName**|**nvarchar**|The role of the conversation handle. This is either **initiator** or **target**.|38|No|  
|**ServerName**|**nvarchar**|The name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] being traced.|26|No|  
|**Severity**|**int**|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error severity, if this event reports an error.|29|No|  
|**SPID**|**int**|The server process ID assigned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the process associated with the client.|12|Yes|  
|**StartTime**|**datetime**|The time at which the event started, when available.|14|Yes|  
|**State**|**int**|Indicates the location within the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] source code that produced the event. Each location that may produce this event has a different state code. A Microsoft support engineer can use this state code to find where the event was produced.|30|No|  
|**TextData**|**ntext**|For errors, contains a message that describes the reason for the failure. One of the following values:<br /><br /> **Cert not found**. The user specified for dialog protocol security has no certificate.<br /><br /> **Not in valid time period**. The user specified for dialog protocol security has a certificate, but the certificate has expired.<br /><br /> **Cert too large for memory allocation**. The user specified for dialog protocol security has a certificate, but the certificate is too large. The maximum certificate size that Service Broker supports is 32,768 bytes.<br /><br /> **Private key not found**. The user specified for dialog protocol security has a certificate, but there is no private key associated with that certificate.<br /><br /> **The cert's private key size is incompatible with the crypto provider**. The private key for the certificate has a key size that cannot be successfully processed. The private key size must be a multiple of 64 bytes.<br /><br /> **The cert's public key size is incompatible with the crypto provider**. The public key for the certificate has a key size that cannot be successfully processed. The public key size must be a multiple of 64 bytes.<br /><br /> **The cert's private key size is incompatible with the encrypted key exchange key**. The key size specified in the key exchange key does not match the size of the private key for the certificate. This generally indicates that the certificate on the remote computer does not match the certificate in the database.<br /><br /> **The cert's public key size is incompatible with the security header's signature**. The security header contains a signature that cannot be validated with the certificate's public key. This generally indicates that the certificate on the remote computer does not match the certificate in the database.|1|Yes|  
  
 The table below lists the subclass values for this event class.  
  
|ID|Subclass|Description|  
|--------|--------------|-----------------|  
|1|No Security Header|During a secure conversation, Service Broker received a message that did not contain a session key. Once a secure conversation is established, the dialog protocol requires that all messages in the conversation contain a session key.|  
|2|No Certificate|Service Broker could not locate a usable certificate for one of the participants in the conversation. To secure a conversation, the database must contain a certificate for both the sender and the recipient of the conversation.|  
|3|Invalid Signature|Broker could not verify the message signature supplied by the sender using the public key in the sender's certificate. This may indicate that the message is corrupt, that the message has been tampered with, that the remote service and the local service are not configured with the same user certificate, or that the certificate is out of date.|  
|4|Run As Target Failure|The destination user does not have receive permissions on the destination queue. To prevent unauthorized users from receiving messages, Service Broker does not enqueue messages with a destination user that cannot receive from the queue, regardless of whether the initiating user has permission to enqueue messages.|  
  
## See Also  
 [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)  
  
  
