---
title: "sys.tables (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e53334c4-d6ca-4d82-bab4-0a690d8747d4
caps.latest.revision: 14
author: BarbKess
---
# sys.tables (SQL Server PDW)
Contains a row for each table object that a principal either owns or on which the principal has been granted some permission.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|<inherited columns>||For a list of columns that this view inherits, see [sys.objects](../sqlpdw/sys-objects-sql-server-pdw.md).||  
|lob_data_space_id|**int**||Always 0.|  
|filestream_data_space_id|**int**|Data space ID for a FILESTREAM filegroup or Information not available.|NULL|  
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
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
