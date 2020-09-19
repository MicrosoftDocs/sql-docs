---
title: "sys.dm_pdw_nodes_exec_sql_text (Transact-SQL) | Microsoft Docs"
description: Dynamic management view that returns the text of the SQL batch that is identified by the specified sql_handle. 
ms.custom: ""
ms.date: "10/14/2019"
ms.prod: sql 
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 
author: XiaoyuMSFT 
ms.author: xiaoyul
monikerRange: "=azure-sqldw-latest || = sqlallproducts-allversions"
---

# sys.pdw_nodes_dm_exec_sql_text (Transact-SQL)
[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Returns the text of the SQL batch that is identified by the specified *sql_handle*. This table-valued function replaces the system function **fn_get_sql**.  
   
## Table returned  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**pdw_node_id**|**int**|Unique numeric ID associated with the node.|
|**dbid**|**smallint**|ID of database.<br /><br /> For unplanned and prepared SQL statements, the ID of the database where the statements were compiled.|  
|**objectid**|**int**|ID of object.<br /><br /> Is NULL for ad hoc and prepared SQL statements.|  
|**number**|**smallint**|For a numbered stored procedure, this column returns the number of the stored procedure. For more information, see [sys.numbered_procedures &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-numbered-procedures-transact-sql.md).<br /><br /> Is NULL for ad hoc and prepared SQL statements.|  
|**encrypted**|**bit**|1: SQL text is encrypted.<br /><br /> 0: SQL text is not encrypted.|  
|**text**|**nvarchar(max)**|Text of the SQL query.<br /><br /> Is NULL for encrypted objects.|  

## Remarks  
The same remarks in [sys.dm_exec_sql_text](https://docs.microsoft.com/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql?view=sql-server-ver15) apply.  
  
## Permissions  
 Require **sysadmin** server role or `VIEW SERVER STATE` permission on the server.  
  
## See also  
 [SQL Data Warehouse and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  

  ## Next steps
 For more development tips, see [SQL Data Warehouse development overview](https://docs.microsoft.com/azure/sql-data-warehouse/sql-data-warehouse-overview-develop).