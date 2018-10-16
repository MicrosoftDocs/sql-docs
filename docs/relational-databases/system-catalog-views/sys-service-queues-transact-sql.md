---
title: "sys.service_queues (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.service_queues"
  - "service_queues"
  - "service_queues_TSQL"
  - "sys.service_queues_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.service_queues catalog view"
ms.assetid: 9fd9fa76-6128-410c-896f-741e6050143a
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sys.service_queues (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains a row for each object in the database that is a service queue, with **sys.objects.type** = SQ.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**||For a list of columns that this view inherits, see [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md).|  
|**max_readers**|**smallint**|Maximum number of the concurrent readers allowed in the queue.|  
|**activation_procedure**|**nvarchar(776)**|Three-part name of the activation procedure.|  
|**execute_as_principal_id**|**int**|ID of the EXECUTE AS database principal.<br /><br /> NULL by default or if EXECUTE AS CALLER.<br /><br /> ID of the specified principal if EXECUTE AS SELF EXECUTE AS \<principal>.<br /><br /> -2 = EXECUTE AS OWNER.|  
|**is_activation_enabled**|**bit**|1 = Activation is enabled.|  
|**is_receive_enabled**|**bit**|1 = Receive is enabled.|  
|**is_enqueue_enabled**|**bit**|1 = Enqueue is enabled.|  
|**is_retention_enabled**|**bit**|1 = Messages are retained until dialog end.|  
|**is_poison_message_handling_enabled**|**bit**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> 1 = Poison message handling is enabled.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
