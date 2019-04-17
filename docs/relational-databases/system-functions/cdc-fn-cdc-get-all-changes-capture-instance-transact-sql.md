---
title: "cdc.fn_cdc_get_all_changes_&lt;capture_instance&gt;  (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_cdc_get_all_changes_<capture_instance>"
  - "change data capture [SQL Server], querying metadata"
  - "cdc.fn_cdc_get_all_changes_<capture_instance>"
ms.assetid: c6bad147-1449-4e20-a42e-b51aed76963c
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# cdc.fn_cdc_get_all_changes_&lt;capture_instance&gt;  (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns one row for each change applied to the source table within the specified log sequence number (LSN) range. If a source row had multiple changes during the interval, each change is represented in the returned result set. In addition to returning the change data, four metadata columns provide the information you need to apply the changes to another data source. Row filtering options govern the content of the metadata columns as well as the rows returned in the result set. When the 'all' row filter option is specified, each change has exactly one row to identify the change. When the 'all update old' option is specified, update operations are represented as two rows: one containing the values of the captured columns before the update and another containing the values of the captured columns after the update.  
  
 This enumeration function is created at the time that a source table is enabled for change data capture. The function name is derived and uses the format **cdc.fn_cdc_get_all_changes_**_capture_instance_ where *capture_instance* is the value specified for the capture instance when the source table is enabled for change data capture.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
cdc.fn_cdc_get_all_changes_capture_instance ( from_lsn , to_lsn , '<row_filter_option>' )  
  
<row_filter_option> ::=  
{ all  
 | all update old  
}  
```  
  
## Arguments  
 *from_lsn*  
 The LSN value that represents the low endpoint of the LSN range to include in the result set. *from_lsn* is **binary(10)**.  
  
 Only rows in the [cdc.&#91;capture_instance&#93;_CT](../../relational-databases/system-tables/cdc-capture-instance-ct-transact-sql.md) change table with a value in **__$start_lsn** greater than or equal to *from_lsn* are included in the result set.  
  
 *to_lsn*  
 The LSN value that represents the high endpoint of the LSN range to include in the result set. *to_lsn* is **binary(10)**.  
  
 Only rows in the [cdc.&#91;capture_instance&#93;_CT](../../relational-databases/system-tables/cdc-capture-instance-ct-transact-sql.md) change table with a value in **__$start_lsn** less than or equal to *from_lsn* or equal to *to_lsn* are included in the result set.  
  
 <row_filter_option> ::= { all | all update old }  
 An option that governs the content of the metadata columns as well as the rows returned in the result set.  
  
 Can be one of the following options:  
  
 all  
 Returns all changes within the specified LSN range. For changes due to an update operation, this option only returns the row containing the new values after the update is applied.  
  
 all update old  
 Returns all changes within the specified LSN range. For changes due to an update operation, this option returns both the row containing the column values before the update and the row containing the column values after the update.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**__$start_lsn**|**binary(10)**|Commit LSN associated with the change that preserves the commit order of the change. Changes committed in the same transaction share the same commit LSN value.|  
|**__$seqval**|**binary(10)**|Sequence value used to order changes to a row within a transaction.|  
|**__$operation**|**int**|Identifies the data manipulation language (DML) operation needed to apply the row of change data to the target data source. Can be one of the following:<br /><br /> 1 = delete<br /><br /> 2 = insert<br /><br /> 3 = update (captured column values are those before the update operation). This value applies only when the row filter option 'all update old' is specified.<br /><br /> 4 = update (captured column values are those after the update operation)|  
|**__$update_mask**|**varbinary(128)**|A bit mask with a bit corresponding to each captured column identified for the capture instance. This value has all defined bits set to 1 when **__$operation** = 1 or 2. When **__$operation** = 3 or 4, only those bits corresponding to columns that changed are set to 1.|  
|**\<captured source table columns>**|varies|The remaining columns returned by the function are the captured columns identified when the capture instance was created. If no columns were specified in the captured column list, all columns in the source table are returned.|  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role or **db_owner** fixed database role. For all other users, requires SELECT permission on all captured columns in the source table and, if a gating role for the capture instance was defined, membership in that database role. When the caller does not have permission to view the source data, the function returns error 229 ("The SELECT permission was denied on the object 'fn_cdc_get_all_changes_...', database '\<DatabaseName>', schema 'cdc'.").  
  
## Remarks  
 If the specified LSN range does not fall within the change tracking timeline for the capture instance, the function returns error 208 ("An insufficient number of arguments were supplied for the procedure or function cdc.fn_cdc_get_all_changes.").  
  
 Columns of data type **image**, **text**, and **ntext** are always assigned a NULL value when **__$operation** = 1 or **__$operation** = 3. Columns of data type **varbinary(max)**, **varchar(max)**, or **nvarchar(max)** are assigned a NULL value when **__$operation** = 3 unless the column changed during the update. When **__$operation** = 1, these columns are assigned their value at the time of the delete. Computed columns that are included in a capture instance always have a value of NULL.  
  
## Examples  
 Several [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] templates are available that show how to use the change data capture query functions. These templates are available on the **View** menu in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. For more information, see [Template Explorer](../../ssms/template/template-explorer.md).  
  
 This example shows the `Enumerate All Changes for Valid Range Template`. It uses the function `cdc.fn_cdc_get_all_changes_HR_Department` to report all the currently available changes for the capture instance `HR_Department`, which is defined for the source table HumanResources.Department in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
-- ========  
-- Enumerate All Changes for Valid Range Template  
-- ========  
USE AdventureWorks2012;  
GO  
  
DECLARE @from_lsn binary(10), @to_lsn binary(10);  
SET @from_lsn = sys.fn_cdc_get_min_lsn('HR_Department');  
SET @to_lsn   = sys.fn_cdc_get_max_lsn();  
SELECT * FROM cdc.fn_cdc_get_all_changes_HR_Department  
  (@from_lsn, @to_lsn, N'all');  
GO  
```  
  
## See Also  
 [cdc.fn_cdc_get_net_changes_&#60;capture_instance&#62; &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-net-changes-capture-instance-transact-sql.md)   
 [sys.fn_cdc_map_time_to_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-map-time-to-lsn-transact-sql.md)   
 [sys.sp_cdc_get_ddl_history &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-get-ddl-history-transact-sql.md)   
 [sys.sp_cdc_get_captured_columns &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-get-captured-columns-transact-sql.md)   
 [sys.sp_cdc_help_change_data_capture &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-help-change-data-capture-transact-sql.md)   
 [About Change Data Capture &#40;SQL Server&#41;](../../relational-databases/track-changes/about-change-data-capture-sql-server.md)  
  
  
