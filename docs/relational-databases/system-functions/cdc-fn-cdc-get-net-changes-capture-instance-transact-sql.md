---
title: "cdc.fn_cdc_get_net_changes_&lt;capture_instance&gt; (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_cdc_get_net_changes_<capture_instance>"
  - "change data capture [SQL Server], querying metadata"
  - "cdc.fn_cdc_get_net_changes_<capture_instance>"
ms.assetid: 43ab0d1b-ead4-471c-85f3-f6c4b9372aab
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# cdc.fn_cdc_get_net_changes_&lt;capture_instance&gt; (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns one net change row for each source row changed within the specified Log Sequence Numbers (LSN) range.  
  
 **Wait, what is an LSN?** Every record in the [SQL Server transaction log](../logs/the-transaction-log-sql-server.md) is uniquely identified by a log sequence number (LSN). LSNs are ordered such that if LSN2 is greater than LSN1, the change described by the log record referred to by LSN2 occurred **after** the change described by the log record LSN.  
  
 The LSN of a log record where a significant event occurred can be useful for constructing correct restore sequences. Because LSNs are ordered, you can compare them for equality and inequality (that is, \<, >, =, \<=, >=). Such comparisons are useful when constructing restore sequences.  
  
 When a source row has multiple changes during the LSN range, a single row that reflects the final content of the row is returned by the enumeration function described below. For example, if a transaction inserts a row in the source table and a subsequent transaction within the LSN range updates one or more columns in that row, the function returns only **one** row, which includes the updated column values.  
  
 This enumeration function is created when a source table is enabled for change data capture and net tracking is specified. To enable net tracking, the source table must have a primary key or unique index. The function name is derived and uses the format cdc.fn_cdc_get_net_changes_*capture_instance*, where *capture_instance* is the value specified for the capture instance when the source table was enabled for change data capture. For more information, see [sys.sp_cdc_enable_table &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-enable-table-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
cdc.fn_cdc_get_net_changes_capture_instance ( from_lsn , to_lsn , '<row_filter_option>' )  
  
<row_filter_option> ::=  
{ all  
 | all with mask  
 | all with merge  
}  
```  
  
## Arguments  
 *from_lsn*  
 The LSN that represents the low endpoint of the LSN range to include in the result set. *from_lsn* is **binary(10)**.  
  
 Only rows in the [cdc.&#91;capture_instance&#93;_CT](../../relational-databases/system-tables/cdc-capture-instance-ct-transact-sql.md) change table with a value in __$start_lsn greater than or equal to *from_lsn* are included in the result set.  
  
 *to_lsn*  
 The LSN that represents the high endpoint of the LSN range to include in the result set. *to_lsn* is **binary(10)**.  
  
 Only rows in the [cdc.&#91;capture_instance&#93;_CT](../../relational-databases/system-tables/cdc-capture-instance-ct-transact-sql.md) change table with a value in __$start_lsn less than or equal to *from_lsn* or equal to *to_lsn* are included in the result set.  
  
 *<row_filter_option>* ::= { all | all with mask | all with merge }  
 An option that governs the content of the metadata columns as well as the rows returned in the result set. Can be one of the following options:  
  
 all  
 Returns the LSN of the final change to the row and the operation needed to apply the row in the metadata columns __$start_lsn and \_\_$operation. The column \_\_$update_mask is always NULL.  
  
 all with mask  
 Returns the LSN of the final change to the row and the operation needed to apply the row in the metadata columns __$start_lsn and \_\_$operation. In addition, when an update operation returns (\_\_$operation = 4) the captured columns modified in the update are marked in the value returned in \_\_$update_mask.  
  
 all with merge  
 Returns the LSN of the final change to the row in the metadata columns __$start_lsn. The column \_\_$operation will be one of two values: 1 for delete and 5 to indicate that the operation needed to apply the change is either an insert or an update. The column \_\_$update_mask is always NULL.  
  
 Because the logic to determine the precise operation for a given change adds to query complexity, this option is designed to improve query performance when it is sufficient to indicate that the operation needed to apply the change data is either an insert or an update, but it is not necessary to explicitly distinguish between the two. This option is most attractive in target environments where a merge operation is available directly, such as a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] environment.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|__$start_lsn|**binary(10)**|LSN associated with the commit transaction for the change.<br /><br /> All changes committed in the same transaction share the same commit LSN. For example, if an update operation on the source table modifies two columns in two rows, the change table will contain four rows, each with the same __$start_lsnvalue.|  
|__$operation|**int**|Identifies the data manipulation language (DML) operation needed to apply the row of change data to the target data source.<br /><br /> If the value of the row_filter_option parameter is all or all with mask, the value in this column can be one of the following values:<br /><br /> 1 = delete<br /><br /> 2 = insert<br /><br /> 4 = update<br /><br /> If the value of the row_filter_option parameter is all with merge, the value in this column can be one of the following values:<br /><br /> 1 = delete|  
|__$update_mask|**varbinary(128)**|A bit mask with a bit corresponding to each captured column identified for the capture instance. This value has all defined bits set to 1 when __$operation = 1 or 2. When \_\_$operation = 3 or 4, only those bits corresponding to columns that changed are set to 1.|  
|*\<captured source table columns>*|varies|The remaining columns returned by the function are the columns from the source table that were identified as captured columns when the capture instance was created. If no columns were specified in the captured column list, all columns in the source table are returned.|  
  
## Permissions  
 Requires membership in the sysadmin fixed server role or db_owner fixed database role. For all other users, requires SELECT permission on all captured columns in the source table and, if a gating role for the capture instance was defined, membership in that database role. When the caller does not have permission to view the source data, the function returns error 208 (Invalid object name).  
  
## Remarks  
 If the specified LSN range does not fall within the change tracking timeline for the capture instance, the function returns error 208 (Invalid object name).

 Modifications on the unique identifier of a row will cause fn_cdc_get_net_changes to show the initial UPDATE command with a DELETE and then INSERT command instead.  This behavior is necessary to track the key both before and after the change.
  
## Examples  
 The following example uses the function `cdc.fn_cdc_get_net_changes_HR_Department` to report the net changes made to the source table `HumanResources.Department` during a specific time interval.  
  
 First, the `GETDATE` function is used to mark the beginning of the time interval. After several DML statements are applied to the source table, the `GETDATE` function is called again to identify the end of the time interval. The function [sys.fn_cdc_map_time_to_lsn](../../relational-databases/system-functions/sys-fn-cdc-map-time-to-lsn-transact-sql.md) is then used to map the time interval to a change data capture query range bounded by LSN values. Finally, the function `cdc.fn_cdc_get_net_changes_HR_Department` is queried to obtain the net changes to the source table for the time interval. Notice that the row that is inserted and then deleted does not appear in the result set returned by the function. This is because a row that is first added and then deleted within a query window produces no net change on the source table for the interval. Before you run this example, you must first run example B in [sys.sp_cdc_enable_table &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-enable-table-transact-sql.md).  
  
```  
USE AdventureWorks2012;  
GO  
DECLARE @begin_time datetime, @end_time datetime, @from_lsn binary(10), @to_lsn binary(10);  
-- Obtain the beginning of the time interval.  
SET @begin_time = GETDATE() -1;  
-- DML statements to produce changes in the HumanResources.Department table.  
INSERT INTO HumanResources.Department (Name, GroupName)  
VALUES (N'MyDept', N'MyNewGroup');  
  
UPDATE HumanResources.Department  
SET GroupName = N'Resource Control'  
WHERE GroupName = N'Inventory Management';  
  
DELETE FROM HumanResources.Department  
WHERE Name = N'MyDept';  
  
-- Obtain the end of the time interval.  
SET @end_time = GETDATE();  
-- Map the time interval to a change data capture query range.  
SET @from_lsn = sys.fn_cdc_map_time_to_lsn('smallest greater than or equal', @begin_time);  
SET @to_lsn = sys.fn_cdc_map_time_to_lsn('largest less than or equal', @end_time);  
  
-- Return the net changes occurring within the query window.  
SELECT * FROM cdc.fn_cdc_get_net_changes_HR_Department(@from_lsn, @to_lsn, 'all');  
```  
  
## See Also  
 [cdc.fn_cdc_get_all_changes_&#60;capture_instance&#62;  &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql.md)   
 [sys.fn_cdc_map_time_to_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-map-time-to-lsn-transact-sql.md)   
 [sys.sp_cdc_enable_table &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-enable-table-transact-sql.md)   
 [sys.sp_cdc_help_change_data_capture &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-help-change-data-capture-transact-sql.md)   
 [About Change Data Capture &#40;SQL Server&#41;](../../relational-databases/track-changes/about-change-data-capture-sql-server.md)  
  
  
