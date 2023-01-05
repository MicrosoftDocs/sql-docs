---
title: "sys.service_queue_usages (Transact-SQL)"
description: sys.service_queue_usages (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.service_queue_usages"
  - "sys.service_queue_usages_TSQL"
  - "service_queue_usages"
  - "service_queue_usages_TSQL"
helpviewer_keywords:
  - "sys.service_queue_usages catalog view"
dev_langs:
  - "TSQL"
ms.assetid: d58dcdaf-f82d-43d9-941b-f520581442bf
---
# sys.service_queue_usages (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This catalog view returns a row for each reference between service and service queue. A service can only be associated with one queue. A queue can be associated with multiple services.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**service_id**|**int**|Identifier of the service. Unique within the database. Not NULLABLE.|  
|**service_queue_id**|**int**|Identifier of the service queue used by the service. Not NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [sys.services &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-services-transact-sql.md)  
  
  
