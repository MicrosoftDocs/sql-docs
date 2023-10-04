---
title: "sys.dm_pdw_nodes_exec_sql_text (Transact-SQL)"
description: Dynamic management view that returns the text of the SQL batch that is identified by the specified sql_handle.
author: jacinda-eng
ms.author: jacindaeng
ms.reviewer: wiassaf
ms.date: "10/14/2019"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest"
---

# sys.dm_pdw_nodes_exec_sql_text (Transact-SQL)
[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Returns the text of the SQL batch that is identified by the specified `sql_handle`. This table-valued function replaces the system function `fn_get_sql`.


> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
   
## Table returned  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**pdw_node_id**|**int**|Unique numeric ID associated with the node.|
|**dbid**|**smallint**|ID of database.<br /><br /> For unplanned and prepared SQL statements, the ID of the database where the statements were compiled.|  
|**objectid**|**int**|ID of object.<br /><br /> Is `NULL` for improvised and prepared SQL statements.|  
|**number**|**smallint**|For a numbered stored procedure, this column returns the number of the stored procedure. For more information, see [sys.numbered_procedures &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-numbered-procedures-transact-sql.md).<br /><br /> Is NULL for improvised and prepared SQL statements.|  
|**encrypted**|**bit**|1: SQL text is encrypted.<br /><br /> 0: SQL text is not encrypted.|  
|**text**|**nvarchar(max)**|Text of the SQL query.<br /><br /> Is NULL for encrypted objects.|  

## Remarks  
The same remarks in [sys.dm_exec_sql_text](./sys-dm-exec-sql-text-transact-sql.md) apply.  
  
## Permissions  
 Require **sysadmin** server role or `VIEW SERVER STATE` permission on the server.  
  
## See also  
 [Azure Synapse Analytics and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  

  ## Next steps
 For more development tips, see [Azure Synapse Analytics development overview](/azure/sql-data-warehouse/sql-data-warehouse-overview-develop).
