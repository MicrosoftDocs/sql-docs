---
title: "sys.remote_logins (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.remote_logins_TSQL"
  - "remote_logins"
  - "remote_logins_TSQL"
  - "sys.remote_logins"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.remote_logins catalog view"
ms.assetid: 38477e91-d084-4df7-b1de-b930c5580189
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sys.remote_logins (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a row per remote-login mapping. This catalog view is used to map incoming local logins that claim to be coming from a corresponding server to an actual local login.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**server_id**|**int**|ID of the server in **sys.servers**. This name is supplied by the connection from the "remote" server.|  
|**remote_name**|**sysname**|Login name that the connection will supply to be mapped. If NULL, the login name that is specified in the connection is used.|  
|**local_principal_id**|**int**|ID of the server principal to whom the login is mapped. If 0, the remote login is mapped to the login with the same name.|  
|**modify_date**|**datetime**|Date the linked login was last changed.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Linked Servers Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/linked-servers-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
