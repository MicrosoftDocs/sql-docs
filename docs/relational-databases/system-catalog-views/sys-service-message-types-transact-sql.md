---
title: "sys.service_message_types (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.service_message_types"
  - "service_message_types"
  - "sys.service_message_types_TSQL"
  - "service_message_types_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.service_message_types catalog view"
ms.assetid: 6a38709a-60fe-46f6-89da-718f74f15600
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sys.service_message_types (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This catalog view contains a row per message type registered in the service broker.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of message type, unique within the database. Not NULLABLE.|  
|**message_type_id**|**int**|Identifier of the message type, unique within the database. Not NULLABLE.|  
|**principal_id**|**int**|Identifier for the database principal that owns this message type. NULLABLE.|  
|**validation**|**char(2)**|Validation done by Broker prior to sending messages of this type. Not NULLABLE. One of:<br /><br /> N = None<br /><br /> X = XML<br /><br /> E = Empty|  
|**validation_desc**|**nvarchar(60)**|Description of the validation done by Broker prior to sending messages of this type. NULLABLE. One of:<br /><br /> NONE<br /><br /> XML<br /><br /> EMPTY|  
|**xml_collection_id**|**int**|For validation that uses an XML schema, the identifier for the schema collection used.<br /><br /> Otherwise, NULL.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
