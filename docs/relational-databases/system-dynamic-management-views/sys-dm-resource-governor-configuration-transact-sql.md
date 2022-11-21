---
title: "sys.dm_resource_governor_configuration (Transact-SQL)"
description: sys.dm_resource_governor_configuration (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_resource_governor_configuration_TSQL"
  - "dm_resource_governor_configuration"
  - "sys.dm_resource_governor_configuration"
  - "sys.dm_resource_governor_configuration_TSQL"
helpviewer_keywords:
  - "sys.dm_resource_governor_configuration dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: c89aab6a-0434-4ce6-af8c-f8a1a3284e38
---
# sys.dm_resource_governor_configuration (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row that contains the current in-memory configuration state of Resource Governor.  
  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|classifier_function_id|**int**|The ID of the classifier function that is currently used by Resource Governor. Returns a value of 0 if no function is being used. Is not nullable.<br /><br /> **Note:** This function is used to classify new requests and uses rules to route these requests to the appropriate workload group. For more information, see [Resource Governor](../../relational-databases/resource-governor/resource-governor.md).|  
|is_reconfiguration_pending|**bit**|Indicates whether or not changes to a group or pool were made with the ALTER RESOURCE GOVERNOR RECONFIGURE statement but have not been applied to the in-memory configuration. The value returned is one of:<br /><br /> 0 - A reconfiguration statement is not required.<br /><br /> 1 - A reconfiguration statement or server restart is required in order for pending configuration changes to be applied.<br /><br /> **Note:** The value returned is always 0 when Resource Governor is disabled.<br /><br /> Is not nullable.|  
|max_outstanding_io_per_volume|**int**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br /> The maximum number of outstanding I/O per volume.|  
  
## Remarks  
 This dynamic management view shows the in-memory configuration. To see the stored configuration metadata, use the corresponding catalog view.  
  
 The following example shows how to get and compare the stored metadata values and the in-memory values of the Resource Governor configuration.  
  
```  
USE master;  
go  
-- Get the stored metadata.  
SELECT   
object_schema_name(classifier_function_id) AS 'Classifier UDF schema in metadata',   
object_name(classifier_function_id) AS 'Classifier UDF name in metadata'  
FROM   
sys.resource_governor_configuration;  
go  
-- Get the in-memory configuration.  
SELECT   
object_schema_name(classifier_function_id) AS 'Active classifier UDF schema',   
object_name(classifier_function_id) AS 'Active classifier UDF name'  
FROM   
sys.dm_resource_governor_configuration;  
go  
```  
  
## Permissions  
 Requires VIEW SERVER STATE permission.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [sys.resource_governor_configuration &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-resource-governor-configuration-transact-sql.md)   
 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)  
  
  

