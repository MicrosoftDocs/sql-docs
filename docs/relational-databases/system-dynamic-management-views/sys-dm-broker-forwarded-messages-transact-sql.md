---
title: "sys.dm_broker_forwarded_messages (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_broker_forwarded_messages"
  - "dm_broker_forwarded_messages"
  - "sys.dm_broker_forwarded_messages_TSQL"
  - "dm_broker_forwarded_messages_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_broker_forwarded_messages dynamic management view"
ms.assetid: 5576376d-6364-417a-8475-aa770e060845
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_broker_forwarded_messages (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a row for each Service Broker message that an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is in the process of forwarding.  
  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**conversation_id**|**uniqueidentifier**|ID of the conversation to which this message belongs. NULLABLE.|  
|**is_initiator**|**bit**|Indicates whether this message is from the initiator of the conversation.  NULLABLE.<br /><br /> 0 = Not from initiator<br /><br /> 1 = From initiator|  
|**to_service_name**|**nvarchar(512)**|Name of the service to which this message is sent. NULLABLE.|  
|**to_broker_instance**|**nvarchar(512)**|Identifier of the broker that hosts the service to which this message is sent. NULLABLE.|  
|**from_service_name**|**nvarchar(512)**|Name of the service that this message is from. NULLABLE.|  
|**from_broker_instance**|**nvarchar(512)**|Identifier of the broker that hosts the service that this message is from. NULLABLE.|  
|**adjacent_broker_address**|**nvarchar(512)**|Network address to which this message is being sent. NULLABLE.|  
|**message_sequence_number**|**bigint**|Sequence number of the message in the dialog box. NULLABLE.|  
|**message_fragment_number**|**int**|If the dialog message is fragmented, this is the fragment number that this transport message contains. NULLABLE.|  
|**hops_remaining**|**tinyint**|Number of times the message may be retransmitted before reaching the final destination. Every time the message is forwarded, this number decreases by 1. NULLABLE.|  
|**time_to_live**|**int**|Maximum time for the message to remain active. When this reaches 0, the message is discarded. NULLABLE.|  
|**time_consumed**|**int**|Time that the message has already been active. Every time the message is forwarded, this number is increased by the time it has taken to forward the message. Not NULLABLE.|  
|**message_id**|**uniqueidentifier**|ID of the message. NULLABLE.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Service Broker Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/service-broker-related-dynamic-management-views-transact-sql.md)  
  
  

