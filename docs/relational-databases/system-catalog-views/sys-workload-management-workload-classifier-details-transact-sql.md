---
title: "sys.workload_management_workload_classifier_details (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/01/2019"
ms.prod: ""
ms.prod_service: "sql-data-warehouse"
ms.reviewer: "jrasnick"
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
author: "ronortloff"
ms.author: "rortloff"
manager: craigg
monikerRange: "=azure-sqldw-latest||=sqlallproducts-allversions"
---
# sys.workload_management_workload_classifier_details (Transact-SQL)

[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

  Returns details for each classifier.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|
|classifier_id|**int**|ID of the classifier. Joinable to [sys.workload_management_workload_classifiers](sys-workload-management-workload-classifiers-transact-sql.md). Is not nullable.|
|classifier_type|**sysname**|The entity on which classification is being done. Is not nullable.|MEMBERNAME|
|classifier_value|**sysname**|The value of the classifier. Is not nullable.||

## Permissions

Requires VIEW SERVER STATE permission.

## Next steps
  
 For a list of all the catalog views for SQL Data Warehouse and Parallel Data Warehouse, see [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md). To create a workload classifier, see [CREATE WORKLOAD CLASSIFIER](../../t-sql/statements/create-workload-classifier-transact-sql.md). For more information on workload classification, see SQL Data Warehouse [Workload Classification](/azure/sql-data-warehouse/sql-data-warehouse-workload-classification) and [Workload Importance](/azure/sql-data-warehouse/sql-data-warehouse-workload-classification)
