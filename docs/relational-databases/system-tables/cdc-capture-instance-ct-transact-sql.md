---
title: "cdc.&lt;capture_instance&gt;_CT (Transact-SQL)"
description: cdc.&lt;capture_instance&gt;_CT (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/01/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "cdc"
  - "cdc_TSQL"
helpviewer_keywords:
  - "cdc.<capture_instance>_CT"
dev_langs:
  - "TSQL"
ms.assetid: 979c8110-3c54-4e76-953c-777194bc9751
---
# cdc.&lt;capture_instance&gt;_CT (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Is the change table created when change data capture is enabled on a source table. The table returns one row for each insert and delete operation performed against the source table, and two rows for each update operation performed against the source table. When the name of the change table is not specified at the time the source table is enabled, the name is derived. The format of the name is cdc.*capture_instance*_CT where *capture_instance* is the schema name of the source table and the source table name in the format *schema_table*. For example, if the table **Person.Address** in the **AdventureWorks** sample database is enabled for change data capture, the derived change table name would be **cdc.Person_Address_CT**.  
  
 We recommend that you **do not query the system tables directly**. Instead, execute the [cdc.fn_cdc_get_all_changes_<capture_instance>](../../relational-databases/system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql.md) and [cdc.fn_cdc_get_net_changes_<capture_instance>](../../relational-databases/system-functions/cdc-fn-cdc-get-net-changes-capture-instance-transact-sql.md) functions.  
  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**__$start_lsn**|**binary(10)**|Log sequence number (LSN) associated with the commit transaction for the change.<br /><br /> All changes committed in the same transaction share the same commit LSN. For example, if a delete operation on the source table removes two rows, the change table will contain two rows, each with the same **__$start_lsn** value.|  
|**__$end_lsn**|**binary(10)**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]<br /><br /> In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], this column is always NULL.|  
|**__$seqval**|**binary(10)**|Sequence value used to order the row changes within a transaction.|  
|**__$operation**|**int**|Identifies the data manipulation language (DML) operation associated with the change. Can be one of the following:<br /><br /> 1 = delete<br /><br /> 2 = insert<br /><br /> 3 = update (old values)<br /><br /> Column data has row values before executing the update statement.<br /><br /> 4 = update (new values)<br /><br /> Column data has row values after executing the update statement.|  
|**__$update_mask**|**varbinary(128)**|A bit mask based upon the column ordinals of the change table identifying those columns that changed.|  
|*\<captured source table columns>*|varies|The remaining columns in the change table are the columns from the source table that were identified as captured columns when the capture instance was created. If no columns were specified in the captured column list, all columns in the source table are included in this table.|  
|**__$command_id** |**int** |Tracks the order of operations within a transaction. |  
  
## Remarks  

The `__$command_id` column was column was introduced in a cumulative update in versions 2012 through 2016. For version and download information, see KB article 3030352 at [FIX: The change table is ordered incorrectly for updated rows after you enable change data capture for a Microsoft SQL Server database](https://support.microsoft.com/help/3030352/fix-the-change-table-is-ordered-incorrectly-for-updated-rows-after-you).  For more information, see [CDC functionality may break after upgrading to the latest CU for SQL Server 2012, 2014 and 2016](/archive/blogs/sql_server_team/cdc-functionality-may-break-after-upgrading-to-the-latest-cu-for-sql-server-2012-2014-and-2016).

## Captured Column Data Types  
 Captured columns included in this table have the same data type and value as their corresponding source columns with the following exceptions:  
  
-   **Timestamp** columns are defined as **binary(8)**.  
  
-   **Identity** columns are defined as either **int** or **bigint**.  
  
 However, the values in these columns are the same as the source column values.  
  
### Large Object Data Types  
 Columns of data type **image**, **text**, and **ntext** are always assigned a **NULL** value when __$operation = 1 or \_\_$operation = 3. Columns of data type **varbinary(max)**, **varchar(max)**, or **nvarchar(max)** are assigned a **NULL** value when \_\_$operation = 3 unless the column changed during the update. When \_\_$operation = 1, these columns are assigned their value at the time of the delete. Computed columns that are included in a capture instance always have a value of **NULL**.  
  
 By default, the maximum size that can be added to a captured column in a single INSERT, UPDATE, WRITETEXT, or UPDATETEXT statement is 65,536 bytes or 64 KB. To increase this size to support larger LOB data, use the [Configure the max text repl size Server Configuration Option](../../database-engine/configure-windows/configure-the-max-text-repl-size-server-configuration-option.md) to specify a larger maximum size. For more information, see [Configure the max text repl size Server Configuration Option](../../database-engine/configure-windows/configure-the-max-text-repl-size-server-configuration-option.md).  
  
## Data Definition Language Modifications  
 DDL modifications to the source table, such as adding or dropping columns, are recorded in the [cdc.ddl_history](../../relational-databases/system-tables/cdc-ddl-history-transact-sql.md) table. These changes are not applied to the change table. That is, the definition of the change table remains constant. When inserting rows into the change table, the capture process ignores those columns that do not appear in the captured column list associated with the source table. If a column appears in the captured column list that is no longer in the source table, the column is assigned a null value.  
  
 Changing the data type of a column in the source table is also recorded in the [cdc.ddl_history](../../relational-databases/system-tables/cdc-ddl-history-transact-sql.md) table. However, this change does alter the definition of the change table. The data type of the captured column in the change table is modified when the capture process encounters the log record for the DDL change made to the source table.  
  
 If you need to modify the data type of a captured column in the source table in a way that decreases the size of the data type, use the following procedure to ensure that the equivalent column in the change table can be successfully modified.  
  
1.  In the source table, update the values in the column to be modified to fit into the planned data type size. For example, if you change the data type from **int** to **smallint**, update the values to a size that fits in the **smallint** range, -32,768 to 32,767.  
  
2.  In the change table, perform the same update operation to the equivalent column.  
  
3.  Alter the source table by specifying the new data type. The data type change is propagated successfully to the change table.  

## Data Manipulation Language Modifications  
 When insert, update, and delete operations are performed on a change data capture enabled source table, a record of those DML operations appears in the database transaction log. The change data capture process retrieves information about those changes from the transaction log, and adds either one or two rows to the change table to record the change. Entries are added to the change table in the same order that they were committed to the source table. That said, the commit of change table entries must typically be performed on a group of changes rather than performed per each entry.
 
 An insert operation results in one row added to the change table; a delete operation results in one row added to the change table; if SQL Server implements an update as a "deferred update", which means as a pair of delete and insert operations, the update operation results in two rows added to the change table: the first row reflecting the deletion of the captured data, and the second row reflecting the insertion of the updated, captured data; if SQL Server does not implement an update as a "deferred update", the update operation results in two rows added to the change table: the first row reflecting the captured data before the update, and the second row reflecting the captured data after the update.

 Within the change table entry, the **__$start_lsn** column is used to record the commit LSN that is associated with the change to the source table, and the **__$seqval column** is used to order the change within its transaction. Together, these metadata columns can be used to make sure that the commit order of the source changes is preserved. Because the capture process obtains its change information from the transaction log, it is important to note that change table entries do not appear synchronously with their corresponding source table changes. Instead, corresponding changes appear asynchronously, after the capture process has processed the relevant change entries from the transaction log.  
  
 For insert and delete operations, all the bits in the update mask are set. For update operations, the update mask in both the update old and update new rows will be modified to reflect the columns that changed during update.  
  
## See Also  
 [sys.sp_cdc_enable_table &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-enable-table-transact-sql.md)   
 [sys.sp_cdc_get_ddl_history &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-get-ddl-history-transact-sql.md)  
  
