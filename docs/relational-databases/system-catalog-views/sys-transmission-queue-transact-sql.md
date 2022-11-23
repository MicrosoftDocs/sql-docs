---
title: "sys.transmission_queue (Transact-SQL)"
description: sys.transmission_queue (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "transmission_queue"
  - "sys.transmission_queue_TSQL"
  - "sys.transmission_queue"
  - "transmission_queue_TSQL"
helpviewer_keywords:
  - "sys.transmission_queue catalog view"
dev_langs:
  - "TSQL"
ms.assetid: f3515d1a-be8f-4a27-8058-8865f0919838
---
# sys.transmission_queue (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This catalog view contains a row for each message in the transmission queue, as shown in the following table:  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**conversation_handle**|**uniqueidentifier**|Identifier for the conversation that this message belongs to. Not NULLABLE.|  
|**to_service_name**|**nvarchar(256)**|Name of the service that this message is to. NULLABLE.|  
|**to_broker_instance**|**nvarchar(128)**|Identifier of the broker that hosts the service that this message is to. NULLABLE.|  
|**from_service_name**|**nvarchar(256)**|Name of the service that this message is from. NULLABLE.|  
|**service_contract_name**|**nvarchar(256)**|Name of the contract that the conversation for this message follows. NULLABLE.|  
|**enqueue_time**|**datetime**|Time at which the message entered the queue. This value uses UTC regardless of the local time zone for the instance. Not NULLABLE.|  
|**message_sequence_number**|**bigint**|Sequence number of the message. Not NULLABLE.|  
|**message_type_name**|**nvarchar(256)**|Message type name for the message. NULLABLE.|  
|**is_conversation_error**|**bit**|Whether this message is an error message.<br /><br /> 0 = Not an error message.<br /><br /> 1 = Error message.<br /><br /> Not NULLABLE.|  
|**is_end_of_dialog**|**bit**|Whether this message is an end of conversation message. Not NULLABLE.<br /><br /> 0 = Not an end of conversation message.<br /><br /> 1 = End of conversation message.<br /><br /> Not NULLABLE.|  
|**message_body**|**varbinary(max)**|The body of this message. NULLABLE.|  
|**transmission_status**|**nvarchar(4000)**|The reason this message is on the queue. This is generally an error message explaining why sending the message failed. If this is blank, the message has not been sent yet. NULLABLE.|  
|**priority**|**tinyint**|The priority level that is assigned to this message. Not NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
