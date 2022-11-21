---
title: "sys.resource_governor_configuration (Transact-SQL)"
description: sys.resource_governor_configuration (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.resource_governor_configuration_TSQL"
  - "sys.resource_governor_configuration"
  - "resource_governor_configuration_TSQL"
  - "resource_governor_configuration"
helpviewer_keywords:
  - "sys.resource_governor_configuration catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 89099668-1dc6-4b07-9d8b-49bc95c7bfc0
---
# sys.resource_governor_configuration (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the stored Resource Governor state.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|classifier_function_id|**int**|The ID of the classifier function as it is stored in the metadata. Is not nullable.<br /><br /> **Note** This function is used to classify new sessions and uses rules to route the workload to the appropriate workload group. For more information, see [Resource Governor](../../relational-databases/resource-governor/resource-governor.md).|  
|is_enabled|**bit**|Indicates the current state of the Resource Governor:<br /><br /> 0 = Resource Governor is not enabled.<br /><br /> 1 = Resource Governor is enabled.<br /><br /> Is not nullable.|  
|max_outstanding_io_per_volume|**int**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br /> The maximum number of outstanding I/O per volume.|  
  
## Remarks  
 The catalog view displays the Resource Governor configuration as stored in metadata. To see the in-memory configuration use the corresponding dynamic management view.  
  
## Permissions  
 Requires VIEW ANY DEFINITION permission to view contents, requires CONTROL SERVER permission to change contents.  
  
## Examples  
 The following example shows how to get and compare the stored metadata values and the in-memory values of the Resource Governor configuration.  
  
```  
USE master;  
GO  
-- Get the stored metadata.  
SELECT   
object_schema_name(classifier_function_id) AS 'Classifier UDF schema in metadata',   
object_name(classifier_function_id) AS 'Classifier UDF name in metadata'  
FROM   
sys.resource_governor_configuration;  
GO  
-- Get the in-memory configuration.  
SELECT   
object_schema_name(classifier_function_id) AS 'Active classifier UDF schema',   
object_name(classifier_function_id) AS 'Active classifier UDF name'  
FROM   
sys.dm_resource_governor_configuration;  
GO  
```  
  
## See Also  
 [Resource Governor Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/resource-governor-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [sys.dm_resource_governor_configuration &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-configuration-transact-sql.md)   
 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)  
  
  
