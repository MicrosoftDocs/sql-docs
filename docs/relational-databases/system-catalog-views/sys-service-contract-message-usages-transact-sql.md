---
title: "sys.service_contract_message_usages (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "service_contract_message_usages_TSQL"
  - "sys.service_contract_message_usages"
  - "sys.service_contract_message_usages_TSQL"
  - "service_contract_message_usages"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.service_contract_message_usages catalog view"
ms.assetid: f783e662-126c-4595-8e22-f9d05191f5d0
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sys.service_contract_message_usages (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This catalog view contains a row per (contract, message type) pair.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**service_contract_id**|**int**|Identifier of the contract using the message type. Not NULLABLE.|  
|**message_type_id**|**int**|Identifier of the message type used by the contract. Not NULLABLE.|  
|**is_sent_by_initiator**|**bit**|Message type can be sent by the conversation initiator. Not NULLABLE.|  
|**is_sent_by_target**|**bit**|Message type can be sent by the conversation target. Not NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
