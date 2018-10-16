---
title: "sys.service_queue_usages (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.service_queue_usages"
  - "sys.service_queue_usages_TSQL"
  - "service_queue_usages"
  - "service_queue_usages_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.service_queue_usages catalog view"
ms.assetid: d58dcdaf-f82d-43d9-941b-f520581442bf
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sys.service_queue_usages (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This catalog view returns a row for each reference between service and service queue. A service can only be associated with one queue. A queue can be associated with multiple services.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**service_id**|**int**|Identifier of the service. Unique within the database. Not NULLABLE.|  
|**service_queue_id**|**int**|Identifier of the service queue used by the service. Not NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [sys.services &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-services-transact-sql.md)  
  
  
