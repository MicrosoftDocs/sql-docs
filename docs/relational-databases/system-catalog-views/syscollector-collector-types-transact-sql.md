---
title: "syscollector_collector_types (Transact-SQL)"
description: syscollector_collector_types (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "syscollector_collector_types"
  - "syscollector_collector_types_TSQL"
helpviewer_keywords:
  - "data collector view"
  - "syscollector_collector_types view"
dev_langs:
  - "TSQL"
ms.assetid: d5cd30bb-89fd-4814-a7e8-9074f043f90f
---
# syscollector_collector_types (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Provides information about a collector type for a collection item.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**collector_type_uid**|**uniqueidentifer**|The GUID for a collection type. Is not nullable.|  
|**name**|**sysname**|The name of the collection type. Is not nullable.|  
|**parameter_schema**|**xml**|The XML schema that describes what the configuration for the specified collector type looks like. This XML schema is used to validate the actual XML configuration associated with a particular collection item instance. Is nullable.|  
|**parameter_formatter**|**xml**|Determines the template to use to transform the XML for use in the collection set property page. Is nullable.|  
|**collection_package_id**|**uniqueidentifer**|The GUID for a collection package. Is not nullable.|  
|**collection_package_path**|**nvarchar(4000)**|Provides the path to the collection package. Is nullable.|  
|**collection_package_name**|**sysname**|The name of the collection package. Is not nullable.|  
|**upload_package_id**|**uniqueidentifer**|The GUID for the upload package. Is not nullable.|  
|**upload_package_path**|**nvarchar(4000)**|Provides the path to the upload package. Is nullable.|  
|**upload_package_name**|**sysname**|The name of the upload package. Is not nullable.|  
|**is_system**|**bit**|Turned on (1) or off (0) to indicate if the collector type was shipped with the data collector or if it was added later by the **dc_admin**. This could be a custom type developed in-house or by a third party. Is not nullable.|  
  
## Permissions  
 Requires SELECT for **dc_operator**, **dc_proxy**.  
  
## Change History  
  
|Updated content|  
|---------------------|  
|Updated **collection_type_uid** column name to **collector_type_uid**.|  
|Corrected the description for the **parameter_schema** column to indicate that the value is nullable.|  
|Added the **parameter_formatter** column.|  
|Corrected the data type for the **collection_package_path** column, and updated the description to indicate that the value is nullable.|  
|Corrected the data type for the **upload_package_path** column, and updated the description to indicate that the value is nullable.|  
  
## See Also  
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)   
 [Data Collector Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/data-collector-views-transact-sql.md)   
 [Data Collection](../../relational-databases/data-collection/data-collection.md)  
  
  
