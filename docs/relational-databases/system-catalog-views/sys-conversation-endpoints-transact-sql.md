---
title: "sys.conversation_endpoints (Transact-SQL)"
description: sys.conversation_endpoints (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "conversation_endpoints_TSQL"
  - "conversation_endpoints"
  - "sys.conversation_endpoints"
  - "sys.conversation_endpoints_TSQL"
helpviewer_keywords:
  - "sys.conversation_endpoints catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 2ed758bc-2a9d-4831-8da2-4b80e218f3ea
---
# sys.conversation_endpoints (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Each side of a [!INCLUDE[ssSB](../../includes/sssb-md.md)] conversation is represented by a conversation endpoint. This catalog view contains a row per conversation endpoint in the database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|conversation_handle|**uniqueidentifier**|Identifier for this conversation endpoint. Not NULLABLE.|  
|conversation_id|**uniqueidentifier**|Identifier for the conversation. This identifier is shared by both participants in the conversation. This together with the is_initiator column is unique within the database. Not NULLABLE.|  
|is_initiator|**tinyint**|Whether this endpoint is the initiator or the target of the conversation.  Not NULLABLE.<br /><br /> 1 = Initiator<br /><br /> 0 = Target|  
|service_contract_id|**int**|Identifier of the contract for this conversation. Not NULLABLE.|  
|conversation_group_id|**uniqueidentifier**|Identifier for the conversation group this conversation belongs to. Not NULLABLE.|  
|service_id|**int**|Identifier for the service for this side of the conversation. Not NULLABLE.|  
|lifetime|**datetime**|Expiration date/time for this conversation. Not NULLABLE.|  
|state|**char(2)**|The current state of the conversation. Not NULLABLE. One of:<br /><br /> SO   Started outbound. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] processed a BEGIN CONVERSATION for this conversation, but no messages have yet been sent.<br /><br /> SI   Started inbound. Another instance started a new conversation with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has not yet completely received the first message. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may create the conversation in this state if the first message is fragmented or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] receives messages out of order. However, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might create the conversation in the CO (conversing) state if the first transmission received for the conversation contains the entire first message.<br /><br /> CO   Conversing. The conversation is established, and both sides of the conversation may send messages. Most of the communication for a typical service takes place when the conversation is in this state.<br /><br /> DI   Disconnected inbound. The remote side of the conversation has issued an END CONVERSATION. The conversation remains in this state until the local side of the conversation issues an END CONVERSATION. An application might still receive messages for the conversation. Because the remote side of the conversation has ended the conversation, an application cannot send messages on this conversation. When an application issues an END CONVERSATION, the conversation moves to the CD (Closed) state.<br /><br /> DO   Disconnected outbound. The local side of the conversation has issued an END CONVERSATION. The conversation remains in this state until the remote side of the conversation acknowledges the END CONVERSATION. An application cannot send or receive messages for the conversation. When the remote side of the conversation acknowledges the END CONVERSATION, the conversation moves to the CD (Closed) state.<br /><br /> ER   Error. An error has occurred on this endpoint. The error message is placed in the application queue. If the application queue is empty, this indicates that the application already consumed the error message.<br /><br /> CD   Closed. The conversation endpoint is no longer in use.|  
|state_desc|**nvarchar(60)**|Description of endpoint conversation state. This column is NULLABLE. One of:<br /><br /> **STARTED_OUTBOUND**<br /><br /> **STARTED_INBOUND**<br /><br /> **CONVERSING**<br /><br /> **DISCONNECTED_INBOUND**<br /><br /> **DISCONNECTED_OUTBOUND**<br /><br /> **CLOSED**<br /><br /> **ERROR**|  
|far_service|**nvarchar(256)**|Name of the service on the remote side of conversation. Not NULLABLE.|  
|far_broker_instance|**nvarchar(128)**|The broker instance for the remote side of the conversation. NULLABLE.|  
|principal_id|**int**|Identifier of the principal whose certificate is used by the local side of the dialog. Not NULLABLE.|  
|far_principal_id|**int**|Identifier of the user whose certificate is used by the remote side of the dialog. Not NULLABLE.|  
|outbound_session_key_identifier|**uniqueidentifier**|Identifier for outbound encryption key for this dialog. Not NULLABLE.|  
|inbound_session_key_identifier|**uniqueidentifier**|Identifier for inbound encryption key for this dialog. Not NULLABLE.|  
|security_timestamp|**datetime**|Time at the local session key was created. Not NULLABLE.|  
|dialog_timer|**datetime**|The time at which the conversation timer for this dialog sends a DialogTimer message. Not NULLABLE.|  
|send_sequence|**bigint**|Next message number in the send sequence. Not NULLABLE.|  
|last_send_tran_id|**binary(6)**|Internal transaction ID of last transaction to send a message. Not NULLABLE.|  
|end_dialog_sequence|**bigint**|The sequence number of the End Dialog message. Not NULLABLE.|  
|receive_sequence|**bigint**|Next message number expected in message receive sequence. Not NULLABLE.|  
|receive_sequence_frag|**int**|Next message fragment number expected in message receive sequence. Not NULLABLE.|  
|system_sequence|**bigint**|The sequence number of the last system message for this dialog. Not NULLABLE.|  
|first_out_of_order_sequence|**bigint**|The sequence number of the first message in the out of order messages for this dialog. Not NULLABLE.|  
|last_out_of_order_sequence|**bigint**|The sequence number of the last message in the out of order messages for this dialog. Not NULLABLE.|  
|last_out_of_order_frag|**int**|Sequence number of the last message in the out of order fragments for this dialog. Not NULLABLE.|  
|is_system|**bit**|1 if this is a system dialog. Not NULLABLE.|  
|priority|**tinyint**|The conversation priority that is assigned to this conversation endpoint. Not NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
