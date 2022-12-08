---
description: "sys.sp_cdc_cleanup_change_table (Transact-SQL)"
title: "sys.sp_cdc_cleanup_change_table (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_cdc_cleanup_change_table"
  - "sp_cdc_cleanup_change_table_TSQL"
  - "sys.sp_cdc_cleanup_change_table"
  - "sys.sp_cdc_cleanup_change_table_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_cdc_cleanup_change_tables"
  - "sp_cdc_cleanup_change_tables"
ms.assetid: 02295794-397d-4445-a3e3-971b25e7068d
author: briancarrig
ms.author: brcarrig
---
# sys.sp_cdc_cleanup_change_table (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes rows from the change table in the current database based on the specified *low_water_mark* value. This stored procedure is provided for users who want to directly manage the change table cleanup process. Caution should be used, however, because the procedure affects all consumers of the data in the change table.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
``` 
sys.sp_cdc_cleanup_change_table   
  [ @capture_instance = ] 'capture_instance', 
  [ @low_water_mark = ] low_water_mark ,  
  [ @threshold = ]'delete threshold',
  [ @fCleanupFailed = ] 'cleanup failed' OUTPUT
```

## Arguments
[ @capture_instance = ] '*capture_instance*'  
Is the name of the capture instance associated with the change table. *capture_instance* is **sysname**, with no default, and cannot be NULL.  
  
*capture_instance* must name a capture instance that exists in the current database.  
  
[ @low_water_mark = ] *low_water_mark*  
Is a log sequence number (LSN) that is to be used as the new low watermark for the *capture instance*. *low_water_mark* is **binary(10)**, with no default.  
  
If the value is nonnull, it must appear as the start_lsn value of a current entry in the [cdc.lsn_time_mapping](../../relational-databases/system-tables/cdc-lsn-time-mapping-transact-sql.md) table. If other entries in cdc.lsn_time_mapping share the same commit time as the entry identified by the new low watermark, the smallest LSN associated with that group of entries is chosen as the low watermark.  
  
If the value is explicitly set to NULL, the current *low watermark* for the *capture instance* is used to define the upper bound for the cleanup operation.  
  
[ @threshold= ] '*delete threshold*'  
Is the maximum number of delete entries that can be deleted by using a single statement on cleanup. *delete_threshold* is **bigint**, with a default of 5000.

[ @fCleanupFailed = ] '*cleanup failed*' OUTPUT
Is an OUTPUT parameter indicating whether the cleanup operation failed or not. *cleanup failed* is a **bit**, with a default of 0.
 
## Result Sets
None, unless the optional **bit** @fCleanupFailed OUTPUT parameter is used.

## Return Code Values  
 **0** (success) or **1** (failure)

## Example
```
--== Declaring a variable and Setting to zero first
SELECT @cleanup_failed_bit = 0;

--== Execute cleanup and obtain output bit
EXEC @retcode = sys.sp_cdc_cleanup_change_table
  @capture_instance = '<CaptureInstance>',
  @low_water_mark = @LSN, --== LSN to be used for new low watermark for capture instance
  @threshold = 1,
  @fCleanupFailed = @cleanup_failed_bit OUTPUT;

--== Leverage @cleanup_failed_bit output to check the status.
SELECT IIF(@cleanup_failed_bit > 0, 'CLEANUP FAILURE', 'CLEANUP SUCCESS');
```

```
Result
--------
CLEANUP SUCCESS
``` 
  
## Remarks  
*sys.sp_cdc_cleanup_change_table* performs the following operations:  
  
1.  If the *@low_water_mark*  parameter is NULL, the start_lsn value for the *capture instance* is left unchanged. However, if the current low watermark is greater than the low watermark value specified using the *@low_water_mark* parameter for the procedure, the [Error 22957](../errors-events/database-engine-events-and-errors.md) is thrown. The error message for *Error 22957* is ```LSN %s, specified as the new low endpoint for the change table associated with capture instance '%s', is not within the Change Data Capture timeline [%s, %s].```
  
    > [!NOTE]  
    >  The new low watermark might not be the low watermark that is specified in the stored procedure call. If other entries in the cdc.lsn_time_mapping table share the same commit time, the smallest start_lsn represented in the group of entries is selected as the adjusted low watermark. If the @low_water_mark parameter is NULL or the current low watermark is greater than the new lowwatermark, the start_lsn value for the capture instance is left unchanged.

2.  Change table entries with __$start_lsn values less than the low watermark are then deleted. The delete threshold is used to limit the number of rows deleted in a single transaction. A failure to successfully delete entries is reported, but does not affect any change to the capture instance low watermark that might have been made based on the call.

3.  If the *sys.sp_cdc_cleanup_change_table* stored procedure times out after updating the start_lsn for the capture instance but without deleting the change table data, increasing the data retention value using the stored procedure [sys.sp_cdc_change_job](sys-sp-cdc-change-job-transact-sql.md) before the next execution of the stored procedure *sys.sp_cdc_cleanup_change_table* will not retain data for the specified retention period. The start_lsn value in [cdc.change_tables](../system-tables/cdc-change-tables-transact-sql.md) should be treated as the new low watermark. The *sys.sp_cdc_cleanup_change_table* stored procedure will not set the start_lsn value to match the newly specified data retention period. The procedure always performs cleanup based on the low watermark. Specifying a value for the *@low_water_mark* parameter that is equal to or higher than the start_lsn value in [cdc.change_tables](../system-tables/cdc-change-tables-transact-sql.md) will avoid generating *Error 22957*.

4. If you use *sys.sp_cdc_cleanup_change_table* to manage the cleanup table process and a deadlock occurs between the CDC scan and CDC cleanup when *sys.sp_cdc_cleanup_change_table* is invoked, [Error 22852](../errors-events/database-engine-events-and-errors.md) is logged with severity 10 (informational message). The error message for *Error 22852* is ```Could not delete change table entries made obsolete by a change in one or more low water marks for capture instances of database <DatabaseName>. The failure occurred when executing the command <CommandName>. The error returned was <ErrorInfo>. Use the action and error to determine the cause of the failure and resubmit the request.```

 Use *sys.sp_cdc_cleanup_change_table* in the following circumstances:  
  
-   The cleanup Agent job reports delete failures.  
  
     An administrator can run this stored procedure explicitly to retry a failed operation. To retry cleanup for a given capture instance, execute *sys.sp_cdc_cleanup_change_table*, and specify NULL for the *@low_water_mark* parameter.  
  
-   The simple retention-based policy used by the cleanup Agent job is not adequate.  
  
     Because this stored procedure does cleanup for a single capture instance, it can be used to build a custom cleanup strategy that tailors the rules for cleanup to the individual capture instance. 

## Permissions  
 Requires membership in the db_owner fixed database role.  
  
## See Also  
 [cdc.fn_cdc_get_all_changes_&#60;capture_instance&#62;  &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql.md)   
 [sys.fn_cdc_get_min_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-get-min-lsn-transact-sql.md)   
 [sys.fn_cdc_increment_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-increment-lsn-transact-sql.md)  
  
  
