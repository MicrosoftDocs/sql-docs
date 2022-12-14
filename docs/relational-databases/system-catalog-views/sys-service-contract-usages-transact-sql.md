---
title: "sys.service_contract_usages (Transact-SQL)"
description: sys.service_contract_usages (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "service_contract_usages"
  - "sys.service_contract_usages"
  - "sys.service_contract_usages_TSQL"
  - "service_contract_usages_TSQL"
helpviewer_keywords:
  - "sys.service_contract_usages catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 20af425e-1152-4a46-b1ac-94cff5fc9f02
---
# sys.service_contract_usages (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This catalog view contains a row per (service, contract) pair.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**service_id**|**int**|Identifier of the service using the contract. Not NULLABLE.|  
|**service_contract_id**|**int**|Identifier of the contract used by the service. Not NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
