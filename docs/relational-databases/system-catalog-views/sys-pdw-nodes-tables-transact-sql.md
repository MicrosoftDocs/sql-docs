---
title: "sys.pdw_nodes_tables (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: ""
ms.prod_service: "sql-data-warehouse, pdw"
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 473b5d14-171b-4a16-9195-acf36d3f786c
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.pdw_nodes_tables (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Contains a row for each table object that a principal either owns or on which the principal has been granted some permission.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|\<inherited columns>||For a list of columns that this view inherits, see [sys.objects](https://msdn.microsoft.com/c36fa71e-549a-4533-a6cd-1314d26f533f).||  
|lob_data_space_id|**int**||Always 0.|  
|filestream_data_space_id|**int**|Data space ID for a FILESTREAM filegroup or [!INCLUDE[ssInfoNA](../../includes/ssinfona-md.md)]|NULL|  
|max_column_id_used|**int**|Maximum column ID used by this table.||  
|lock_on_bulk_load|**bit**|Table is locked on bulk load.|TBD|  
|uses_ansi_nulls|**bit**|Table was created with the SET ANSI_NULLS database option ON.|1|  
|is_replicated|**bit**|1 = Table is published using replication.|0; replication is not supported.|  
|has_replication_filter|**bit**|1 = Table has a replication filter.|0|  
|is_merge_published|**bit**|1 = Table is published using merge replication.|0; not supported.|  
|is_sync_tran_subscribed|**bit**|1 = Table is subscribed using an immediate updating subscription.|0; not supported.|  
|has_unchecked_assembly_data|**bit**|1 = Table contains persisted data that depends on an assembly whose definition changed during the last ALTER ASSEMBLY. Will be reset to 0 after the next successful DBCC CHECKDB or DBCC CHECKTABLE.|0; no CLR support.|  
|text_in_row_limit|**int**|0 = Text in row option is not set.|Always 0.|  
|large_value_types_out_of_row|**bit**|1 = Large value types are stored out-of-row.|Always 0.|  
|is_tracked_by_cdc|**bit**|1 = Table is enabled for change data capture|Always 0; no CDC support.|  
|lock_escalation|**tinyint**|The value of the LOCK_ESCALATION option for the table: 2 = AUTO|Always 2.|  
|lock_escalation_desc|**nvarchar(60)**|A text description of the lock_escalation option.|Always ꞌAUTOꞌ.|  
|pdw_node_id|**int**|Unique identifier of a [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] node.|NOT NULL|  
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  
