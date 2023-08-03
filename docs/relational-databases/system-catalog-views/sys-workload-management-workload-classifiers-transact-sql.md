---
title: "sys.workload_management_workload_classifiers (Transact-SQL)"
description: sys.workload_management_workload_classifiers (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: "wiassaf"
ms.date: 11/05/2019
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest"
---
# sys.workload_management_workload_classifiers (Transact-SQL)

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

 Returns details for workload classifiers.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|
|classifier_id|**int**|Unique ID of the classifier. Is not nullable||
group_name|**sysname**|Name of the workload group the classifier is assigned to. Is not nullable. Joinable to sys.workload_management_workload_groups ||
name|**sysname**|Name of the classifier. Must be unique to the instance. Is not nullable.||
|importance|**sysname**|Is the relative importance of a request in this workload group and across workload groups for shared resources.  Importance specified in the classifier overrides the workload group importance setting. Is nullable.  When null, the workload group importance setting is used.|low, below_normal, normal (default), above_normal, high |
|create_time|**datetime**|Time the classifier was created. Is not nullable.||
modify_time|**datetime**|Time the classifier was last modified. Is not nullable.||
is_enabled|**bit**|INTERNAL||
  
## Permissions

Requires VIEW SERVER STATE permission.

## Next steps

 For a list of all the catalog views for Azure Synapse Analytics and Parallel Data Warehouse, see [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md). To create a workload classifier, see [CREATE WORKLOAD CLASSIFIER](../../t-sql/statements/create-workload-classifier-transact-sql.md). For more information on workload classification, see [Workload Classification](/azure/sql-data-warehouse/sql-data-warehouse-workload-classification)
