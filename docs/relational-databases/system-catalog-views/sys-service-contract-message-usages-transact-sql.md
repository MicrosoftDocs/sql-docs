---
title: "sys.service_contract_message_usages (Transact-SQL)"
description: sys.service_contract_message_usages (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "service_contract_message_usages_TSQL"
  - "sys.service_contract_message_usages"
  - "sys.service_contract_message_usages_TSQL"
  - "service_contract_message_usages"
helpviewer_keywords:
  - "sys.service_contract_message_usages catalog view"
dev_langs:
  - "TSQL"
ms.assetid: f783e662-126c-4595-8e22-f9d05191f5d0
---
# sys.service_contract_message_usages (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This catalog view contains a row per (contract, message type) pair.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**service_contract_id**|**int**|Identifier of the contract using the message type. Not NULLABLE.|  
|**message_type_id**|**int**|Identifier of the message type used by the contract. Not NULLABLE.|  
|**is_sent_by_initiator**|**bit**|Message type can be sent by the conversation initiator. Not NULLABLE.|  
|**is_sent_by_target**|**bit**|Message type can be sent by the conversation target. Not NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
