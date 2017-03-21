---
title: "sys.services (Transact-SQL) | Microsoft Docs"
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
  - "sys.services"
  - "services"
  - "services_TSQL"
  - "sys.services_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.services catalog view"
ms.assetid: 16d0b0c5-5cce-469b-aa3d-4b9248e0c085
caps.latest.revision: 34
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.services (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This catalog view contains a row for each service in the database.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).|  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Case-sensitive name of service, unique within the database. Not NULLABLE.|  
|**service_id**|**int**|Identifier of the service. Not NULLABLE.|  
|**principal_id**|**int**|Identifier for the database principal that owns this service. NULLABLE.|  
|**service_queue_id**|**int**|Object id for the queue that this service uses. Not NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
