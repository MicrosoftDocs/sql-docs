---
title: "sys.conversation_groups (Transact-SQL)"
description: sys.conversation_groups (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "conversation_groups_TSQL"
  - "conversation_groups"
  - "sys.conversation_groups"
  - "sys.conversation_groups_TSQL"
helpviewer_keywords:
  - "sys.conversation_groups catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 3f35815e-2de4-42a2-a972-8f0141dad0b3
---
# sys.conversation_groups (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This catalog view contains a row for each conversation group.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**conversation_group_id**|**uniqueidentifier**|Identifier for the conversation group. Not NULLABLE.|  
|**service_id**|**int**|Identifier of the service for conversations in this group. Not NULLABLE.|  
|**is_system**|**bit**|Indicates whether this is a system instance or not. NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
