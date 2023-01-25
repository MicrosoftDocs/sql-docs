---
title: "sys.remote_logins (Transact-SQL)"
description: sys.remote_logins (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.remote_logins_TSQL"
  - "remote_logins"
  - "remote_logins_TSQL"
  - "sys.remote_logins"
helpviewer_keywords:
  - "sys.remote_logins catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 38477e91-d084-4df7-b1de-b930c5580189
---
# sys.remote_logins (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

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
  
  
