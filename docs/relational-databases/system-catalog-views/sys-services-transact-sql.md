---
title: "sys.services (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.services"
  - "services"
  - "services_TSQL"
  - "sys.services_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.services catalog view"
ms.assetid: 16d0b0c5-5cce-469b-aa3d-4b9248e0c085
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.services (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This catalog view contains a row for each service in the database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Case-sensitive name of service, unique within the database. Not NULLABLE.|  
|**service_id**|**int**|Identifier of the service. Not NULLABLE.|  
|**principal_id**|**int**|Identifier for the database principal that owns this service. NULLABLE.|  
|**service_queue_id**|**int**|Object id for the queue that this service uses. Not NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
