---
title: "sys.workload_management_workload_classifier_details (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: 11/05/2019
ms.prod: sql
ms.technology: system-objects
ms.prod_service: "sql-data-warehouse"
ms.reviewer: "jrasnick"
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
author: "ronortloff"
ms.author: "rortloff"
monikerRange: "=azure-sqldw-latest||=sqlallproducts-allversions"
---
# sys.workload_management_workload_classifier_details (Transact-SQL)

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

  Returns details for each classifier.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|
|classifier_id|**int**|ID of the classifier.  Is not nullable.|
|classifier_type|**sysname**|Joinable to [sys.workload_management_workload_classifiers](sys-workload-management-workload-classifiers-transact-sql.md).|`membername`</br>`wlm_label`</br>`wlm_context`</br>`start_time`</br>`end_time`|
|classifier_value|**sysname**|The value of the classifier. Is not nullable.||

## Permissions

Requires VIEW SERVER STATE permission.

## Next steps
  
For a list of all the catalog views for SQL Data Warehouse and Parallel Data Warehouse, see [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md). To create a workload classifier, see [CREATE WORKLOAD CLASSIFIER](../../t-sql/statements/create-workload-classifier-transact-sql.md). For more information on workload classification, see SQL Data Warehouse [Workload Classification](/azure/sql-data-warehouse/sql-data-warehouse-workload-classification) and [Workload Importance](/azure/sql-data-warehouse/sql-data-warehouse-workload-classification)
