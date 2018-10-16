---
title: "sys.plan_guides (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.planguides_TSQL"
  - "plan_guides"
  - "sys.planguides"
  - "plan_guides_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.plan_guides catalog view"
ms.assetid: 3dde0397-ef6f-4b3f-8250-3f25584eb62b
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.plan_guides (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Contains a row for each plan guide in the database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**plan_guide_id**|**int**|Unique identifier of the plan guide in the database.|  
|**name**|**sysname**|Name of the plan guide.|  
|**create_date**|**datetime**|Date and time the plan guide was created.|  
|**modify_date**|**Datetime**|Date the plan guide was last modified.|  
|**is_disabled**|**bit**|1 = Plan guide is disabled.<br /><br /> 0 = Plan guide is enabled.|  
|**query_text**|**nvarchar(max)**|Text of the query on which the plan guide is created.|  
|**scope_type**|**tinyint**|Identifies the scope of the plan guide.<br /><br /> 1 = OBJECT<br /><br /> 2 = SQL<br /><br /> 3 = TEMPLATE|  
|**scope_type_desc**|**nvarchar(60)**|Description of scope of the plan guide.<br /><br /> OBJECT<br /><br /> SQL<br /><br /> TEMPLATE|  
|**scope_object_id**|**Int**|object_id of the object defining the scope of the plan guide, if the scope is OBJECT.<br /><br /> NULL if the plan guide is not scoped to OBJECT.|  
|**scope_batch**|**nvarchar(max)**|Batch text, if **scope_type** is SQL.<br /><br /> NULL if batch type is not SQL.<br /><br /> If NULL and **scope_type** is SQL, the value of **query_text** applies.|  
|**parameters**|**nvarchar(max)**|The string defining the list of parameters associated with the plan guide.<br /><br /> NULL = No parameter list is associated with the plan guide.|  
|**hints**|**nvarchar(max)**|The OPTION clause hints associated with the plan guide.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [sp_create_plan_guide &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql.md)   
 [sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)  
  
  
