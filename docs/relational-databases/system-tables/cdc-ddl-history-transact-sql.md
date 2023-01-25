---
title: "cdc.ddl_history (Transact-SQL)"
description: cdc.ddl_history (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "cdc.ddl_history_TSQL"
  - "cdc.ddl_history"
helpviewer_keywords:
  - "cdc.ddl_history"
dev_langs:
  - "TSQL"
ms.assetid: cb97ea71-da2f-441a-bbd2-db1f5f48ab49
---
# cdc.ddl_history (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns one row for each data definition language (DDL) change made to tables that are enabled for change data capture. You can use this table to determine when a DDL change occurred on a source table and what the change was. Source tables that have not had DDL changes will not have entries in this table.  
  
 We recommend that you do not query the system tables directly. Instead, execute the [sys.sp_cdc_get_ddl_history](../../relational-databases/system-stored-procedures/sys-sp-cdc-get-ddl-history-transact-sql.md) stored procedure.  
   
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**source_object_id**|**int**|ID of the source table to which the DDL change was applied.|  
|**object_id**|**int**|ID of the change table associated with a capture instance for the source table.|  
|**required_column_update**|**bit**|Indicates that the data type of a captured column was modified in the source table. This modification altered the column in the change table.|  
|**ddl_command**|**nvarchar(max)**|DDL statement applied to the source table.|  
|**ddl_lsn**|**binary(10)**|Log sequence number (LSN) associated with the commitment of the DDL modification.|  
|**ddl_time**|**datetime**|Date and time that the DDL change was made to the source table.|  
  
## See Also  
 [sys.sp_cdc_help_change_data_capture &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-help-change-data-capture-transact-sql.md)   
 [cdc.fn_cdc_get_all_changes_&#60;capture_instance&#62;  &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql.md)  
  
  
