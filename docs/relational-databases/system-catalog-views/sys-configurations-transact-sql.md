---
title: "sys.configurations (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.configurations_TSQL"
  - "configurations"
  - "sys.configurations"
  - "configurations_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.configurations catalog view"
ms.assetid: c4709ed1-bf88-4458-9e98-8e9b78150441
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.configurations (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains a row for each server-wide configuration option value in the system.  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**configuration_id**|**int**|Unique ID for the configuration value.|  
|**name**|**nvarchar(35)**|Name of the configuration option.|  
|**value**|**sql_variant**|Configured value for this option.|  
|**minimum**|**sql_variant**|Minimum value for the configuration option.|  
|**maximum**|**sql_variant**|Maximum value for the configuration option.|  
|**value_in_use**|**sql_variant**|Running value currently in effect for this option.|  
|**description**|**nvarchar(255)**|Description of the configuration option.|  
|**is_dynamic**|**bit**|1 = The variable that takes effect when the RECONFIGURE statement is executed.|  
|**is_advanced**|**bit**|1 = The variable is displayed only when the **show advancedoption** is set.|  
  
 For a list of all server configuration options, see [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md).  
  
> [!NOTE]  
>  For database-level configuration options, see [ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md). To configure Soft-NUMA, see [Soft-NUMA &#40;SQL Server&#41;](../../database-engine/configure-windows/soft-numa-sql-server.md).  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Server-wide Configuration Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/server-wide-configuration-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
