---
title: "sys.routes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/07/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
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
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# sys.routes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md.md)]

  This catalog views contains one row per route. Service Broker uses routes to locate the network address for a service.   

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the route, unique within the database. Not NULLABLE.|  
|**route_id**|**int**|Identifier for the route. Not NULLABLE.|  
|**principal_id**|**int**|Identifier for the database principal that owns the route. NULLABLE.|  
|**remote_service_name**|**nvarchar(256)**|Name of the remote service. NULLABLE.|  
|**broker_instance**|**nvarchar(128)**|Identifier of the broker that hosts the remote service. NULLABLE.|  
|**lifetime**|**datetime**|The date and time when the route expires. Notice that this value does not use the local time zone. Instead, the value shows the expiration time for UTC. NULLABLE.|  
|**address**|**nvarchar(256)**|Network address to which Service Broker sends messages for the remote service. NULLABLE. For SQL Database Managed Instance, address must be local.|  
|**mirror_address**|**nvarchar(256)**|Network address of the mirroring partner for the server specified in the address. NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
