---
title: "sys.workload_management_workload_classifiers (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/24/2019"
ms.prod: ""
ms.prod_service: "sql-data-warehouse"
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
author: "ronortloff"
ms.author: "rortloff"
manager: craigg
monikerRange: "= azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.workload_management_workload_classifiers (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

  Returns ...  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|
|classifier_id|**int**|Unique ID of the classifier. Is not nullable||
group_name|**sysname**|Name of the workload group the classifier is assigned to. Is not nullable. <br /><br />Joinable to [sys.workload_management_workload_groups](sys-workload-management-workload-groups-transact-sql.md)||
name|**sysname**|Name of the classifier. Must be unique to the instance. Is not nullable.||
|importance|**sysname**|Is the relative importance of a request in this workload group and across workload groups for shared resources.  Importance specified in the classifier overrides the workload group importance setting. Is not nullable. |low, below_normal, normal, above_normal, high |
|create_time|**datetime**|Time the classifier was created.||
modify_time|**datetime**|Time the classifier was last modified.||
is_enabled|**bit**|Displays whether the classifier is enabled or not. Is not nullable.|0 = the classifier is not enabled <br /><br /> 1 = the classifier is enabled|
 
  
## Permissions
Requires CONTROL DATABASE permission.
```

## Next steps  
 For a list of all the catalog views for SQL Data Warehouse and Parallel Data Warehouse, see [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md). To create a workload classifier see (provide link)  
  

