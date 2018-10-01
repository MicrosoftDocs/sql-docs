---
title: "sys.linked_logins (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.linked_logins"
  - "sys.linked_logins_TSQL"
  - "linked_logins_TSQL"
  - "linked_logins"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.linked_logins catalog view"
ms.assetid: af57bf0c-a265-410f-9bab-63b78569b4a6
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sys.linked_logins (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a row per linked-server-login mapping, for use by RPC and distributed queries from local server to the corresponding linked server.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**server_id**|**int**|ID of the server in **sys.servers**.|  
|**local_principal_id**|**int**|Server-principal to whom mapping applies.<br /><br /> 0 = wildcard or public.|  
|**uses_self_credential**|**bit**|If 1, mapping indicates session should use its own credentials; otherwise, 0 indicates that session uses the name and password that are supplied.|  
|**remote_name**|**sysname**|Remote user name to use when connecting. Password is also stored, but not exposed in catalog view interfaces.|  
|**modify_date**|**datetime**|Date the linked login was last changed.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Linked Servers Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/linked-servers-catalog-views-transact-sql.md)  
  
  
