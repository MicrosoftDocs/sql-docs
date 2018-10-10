---
title: "sys.server_triggers (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "server_triggers"
  - "sys.server_triggers_TSQL"
  - "server_triggers_TSQL"
  - "sys.server_triggers"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.server_triggers catalog view"
ms.assetid: 25926ff4-9271-45bf-bc32-d5d3344bd47a
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.server_triggers (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains the set of all server-level DDL triggers with object_type of TR or TA. In the case of CLR triggers, the assembly must be loaded into the **master** database. All server-level DDL trigger names exist in a single, global scope.  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the trigger.|  
|**object_id**|**int**|ID of the object.|  
|**parent_class**|**tinyint**|Class of parent. Is always:<br /><br /> 100 = Server|  
|**parent_class_desc**|**nvarchar(60)**|Description of class of parent. Is always:<br /><br /> SERVER.|  
|**parent_id**|**int**|Always 0 for triggers on the SERVER.|  
|**type**|**char(2)**|Object type:<br /><br /> TA = Assembly (CLR) trigger<br /><br /> TR = SQL trigger|  
|**type_desc**|**nvarchar(60)**|Description of the class of the object type.<br /><br /> CLR_TRIGGER<br /><br /> SQL_TRIGGER|  
|**create_date**|**datetime**|Date the trigger was created.|  
|**modify_date**|**datetime**|Date the trigger was last modified by using an ALTER statement.|  
|**is_ms_shipped**|**bit**|Trigger created on behalf of the user by an internal [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component.|  
|**is_disabled**|**bit**|1 = Trigger is disabled.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
