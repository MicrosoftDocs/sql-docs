---
title: "sys.resource_usage (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: ""
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.resource_usage_TSQL"
  - "resource_usage"
  - "sys.resource_usage"
  - "resource_usage_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "resource_usage"
  - "sys.resource_usage"
ms.assetid: b90147a3-fd8e-408e-961d-5c7000e068ad
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sys.resource_usage (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

    
> [!IMPORTANT]
>  This feature is in a preview state. Do not take a dependency on the specific implementation of this feature because the feature might be changed or removed in a future release.  
> 
>  While in a preview state, the [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] operations team might turn data collection off and on for this DMV:  
> 
>  -   When turned on, the DMV returns current data as it is aggregated.  
> -   When turned off, the DMV returns historical data, which might be stale.  
  
 Provides hourly summary of resource usage data for user databases in the current server. Historical data is retained for 90 days.  
  
 For each user database, there is one row for every hour in continuous fashion. Even if the database was idle during that hour, there is one row, and the usage_in_seconds value for that database will be 0. Storage usage and SKU information is rolled up for the hour appropriately.  
  
|Columns|Data Type|Description|  
|-------------|---------------|-----------------|  
|time|**datetime**|Time (UTC) in hour increments.|  
|database_name|**nvarchar**|Name of user database.|  
|sku|**nvarchar**|Name of the SKU. The following are the possible values:<br /><br /> Web<br /><br /> Business<br /><br /> Basic<br /><br /> Standard<br /><br /> Premium|  
|usage_in_seconds|**int**|Sum of CPU time used in the hour.<br /><br /> Note: This column is deprecated for V11 and does not apply to V12. **Value is always set to 0.**|  
|storage_in_megabytes|**decimal**|Maximum storage size for the hour, including database data, indexes, stored procedures and metadata.|  
  
## Permissions  
 This view is available to all user roles with permissions to connect to the virtual **master** database.  
  
  
