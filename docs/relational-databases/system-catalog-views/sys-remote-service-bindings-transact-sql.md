---
title: "sys.remote_service_bindings (Transact-SQL)"
description: sys.remote_service_bindings (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.remote_service_bindings_TSQL"
  - "remote_service_bindings_TSQL"
  - "remote_service_bindings"
  - "sys.remote_service_bindings"
helpviewer_keywords:
  - "sys.remote_service_bindings catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 4e1a885d-eed1-4993-9c87-e6fd781f437d
---
# sys.remote_service_bindings (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This catalog view contains a row per remote service binding. 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of this remote service binding. Not NULLABLE.|  
|**remote_service_binding_id**|**int**|ID of this remote service binding. Not NULLABLE.|  
|**principal_id**|**int**|ID of the database principal that owns this remote service binding. NULLABLE.|  
|**remote_service_name**|**nvarchar(256)**|Name of the remote service that this binding applies to. NULLABLE.|  
|**service_contract_id**|**int**|ID of the contract that this binding applies to. A value of 0 is a wildcard that means this binding applies to all contracts for the service. Not NULLABLE.|  
|**remote_principal_id**|**int**|ID for the user specified in the remote service binding. Service Broker uses a certificate owned by this user for communicating with the specified service on the specified contracts. NULLABLE.|  
|**is_anonymous_on**|**bit**|This remote service binding uses ANONYMOUS security. The identity of the user that begins the conversation is not provided to the target service. Not NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
