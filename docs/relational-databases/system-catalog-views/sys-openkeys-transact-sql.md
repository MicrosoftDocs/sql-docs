---
title: "sys.openkeys (Transact-SQL) | Microsoft Docs"
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
  - "openkeys_TSQL"
  - "sys.openkeys_TSQL"
  - "openkeys"
  - "sys.openkeys"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.openkeys catalog view"
ms.assetid: 719a1259-2398-4fcb-ba05-aeabba7aec21
caps.latest.revision: 17
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.openkeys (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  This catalog view returns information about encryption keys that are open in the current session.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|**int**|ID of the database that contains the key.|  
|**database_name**|**sysname**|Name of the database that contains the key.|  
|**key_id**|**int**|ID of the key. The ID is unique within the database.|  
|**key_name**|**sysname**|Name of the key. Unique within the database.|  
|**key_guid**|**varbinary**|GUID of the key. Unique within the database.|  
|**opened_date**|**datetime**|Date and time when the key was opened.|  
|**status**|**int**|1 if the key is valid in metadata. 0 if the key is not found in metadata.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [OPEN SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/open-symmetric-key-transact-sql.md)  
  
  
