---
title: "sys.routes (Transact-SQL) | Microsoft Docs"
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
  - "routes"
  - "routes_TSQL"
  - "sys.routes"
  - "sys.routes_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.routes catalog view"
ms.assetid: 8fc65915-8bd6-425b-95d9-6a8468cb1e48
caps.latest.revision: 33
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# sys.routes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This catalog views contains one row per route. Service Broker uses routes to locate the network address for a service.   
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the route, unique within the database. Not NULLABLE.|  
|**route_id**|**int**|Identifier for the route. Not NULLABLE.|  
|**principal_id**|**int**|Identifier for the database principal that owns the route. NULLABLE.|  
|**remote_service_name**|**nvarchar(256)**|Name of the remote service. NULLABLE.|  
|**broker_instance**|**nvarchar(128)**|Identifier of the broker that hosts the remote service. NULLABLE.|  
|**lifetime**|**datetime**|The date and time when the route expires. Notice that this value does not use the local time zone. Instead, the value shows the expiration time for UTC. NULLABLE.|  
|**address**|**nvarchar(256)**|Network address to which Service Broker sends messages for the remote service. NULLABLE.|  
|**mirror_address**|**nvarchar(256)**|Network address of the mirroring partner for the server specified in the address. NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
