---
title: "sys.service_contracts (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "service_contracts_TSQL"
  - "sys.service_contracts_TSQL"
  - "sys.service_contracts"
  - "service_contracts"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.service_contracts catalog view"
ms.assetid: 787dd47e-4210-439d-9c4a-57a727a0dbd8
caps.latest.revision: 30
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# sys.service_contracts (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This catalog view contains a row for each contract in the database.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).|  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the contract, unique within the database. Not NULLABLE.|  
|**service_contract_id**|**int**|Identifier of the contract. Not NULLABLE.|  
|**principal_id**|**int**|Identifier for the database principal that owns this contract. NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
