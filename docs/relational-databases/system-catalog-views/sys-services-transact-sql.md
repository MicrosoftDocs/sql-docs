---
title: "sys.services (Transact-SQL)"
description: sys.services (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.services"
  - "services"
  - "services_TSQL"
  - "sys.services_TSQL"
helpviewer_keywords:
  - "sys.services catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 16d0b0c5-5cce-469b-aa3d-4b9248e0c085
---
# sys.services (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This catalog view contains a row for each service in the database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Case-sensitive name of service, unique within the database. Not NULLABLE.|  
|**service_id**|**int**|Identifier of the service. Not NULLABLE.|  
|**principal_id**|**int**|Identifier for the database principal that owns this service. NULLABLE.|  
|**service_queue_id**|**int**|Object id for the queue that this service uses. Not NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
