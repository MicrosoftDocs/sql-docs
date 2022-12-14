---
title: "sys.masked_columns (Transact-SQL)"
description: sys.masked_columns (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "05/25/2021"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.masked_columns"
  - "masked_columns_tsql"
  - "sys.masked_columns_tsql"
  - "masked_columns"
helpviewer_keywords:
  - "sys.masked_columns catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 671577e4-d757-4b8d-9aa9-0fc8d51ea9ca
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.masked_columns (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  Use the **sys.masked_columns** view to query for table-columns that have a dynamic data masking function applied to them. This view inherits from the **sys.columns** view. It returns all columns in the **sys.columns** view, plus the **is_masked** and **masking_function** columns, indicating if the column is masked, and if so, what masking function is defined. This view only shows the columns on which there is a masking function applied.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**int**|ID of the object to which this column belongs.|  
|name|**sysname**|Name of the column. Is unique within the object.|  
|column_id|**int**|ID of the column. Is unique within the object.<br /><br /> Column IDs might not be sequential.|  
|**sys.masked_columns** returns many more columns inherited from **sys.columns**.|various|See [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md) for more column definitions.|  
|is_masked|**bit**|Indicates if the column is masked. 1 indicates masked.|  
|masking_function|**nvarchar(4000)**|The masking function for the column.|  
|generated_always_type|**tinyint**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)]. 7, 8, 9, 10 only applies to [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Identifies when the column value is generated (will always be 0 for columns in system tables):<br /><br /> 0 = NOT_APPLICABLE<br /> 1 = AS_ROW_START<br /> 2 = AS_ROW_END<br />7 = AS_TRANSACTION_ID_START<br />8 = AS_TRANSACTION_ID_END<br />9 = AS_SEQUENCE_NUMBER_START<br />10 = AS_SEQUENCE_NUMBER_END<br /><br /> For more information, see [Temporal Tables &#40;Relational databases&#41;](../../relational-databases/tables/temporal-tables.md).|
  
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
  
  
