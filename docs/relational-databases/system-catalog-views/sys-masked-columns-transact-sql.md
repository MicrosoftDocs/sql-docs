---
title: "sys.masked_columns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/25/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.masked_columns"
  - "masked_columns_tsql"
  - "sys.masked_columns_tsql"
  - "masked_columns"
helpviewer_keywords: 
  - "sys.masked_columns catalog view"
ms.assetid: 671577e4-d757-4b8d-9aa9-0fc8d51ea9ca
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.masked_columns (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Use the **sys.masked_columns** view to query for table-columns that have a dynamic data masking function applied to them. This view inherits from the **sys.columns** view. It returns all columns in the **sys.columns** view, plus the **is_masked** and **masking_function** columns, indicating if the column is masked, and if so, what masking function is defined. This view only shows the columns on which there is a masking function applied.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**int**|ID of the object to which this column belongs.|  
|name|**sysname**|Name of the column. Is unique within the object.|  
|column_id|**int**|ID of the column. Is unique within the object.<br /><br /> Column IDs might not be sequential.|  
|**sys.masked_columns** returns many more columns inherited from **sys.columns**.|various|See [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md) for more column definitions.|  
|is_masked|**bit**|Indicates if the column is masked. 1 indicates masked.|  
|masking_function|**nvarchar(4000)**|The masking function for the column.|  
  
## Remarks  
  
## Permissions  
 This view returns information about tables where the user has some sort of permission on the table or if the user has the VIEW ANY DEFINITION permission.  
  
## Example  
 The following query joins **sys.masked_columns** to **sys.tables** to return information about all masked columns.  
  
```  
SELECT tbl.name as table_name, c.name AS column_name, c.is_masked, c.masking_function  
FROM sys.masked_columns AS c  
JOIN sys.tables AS tbl   
    ON c.object_id = tbl.object_id  
WHERE is_masked = 1;  
```  
  
## See Also  
 [Dynamic Data Masking](../../relational-databases/security/dynamic-data-masking.md)   
 [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)  
  
  
