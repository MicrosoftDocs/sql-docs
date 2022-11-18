---
description: "sys.fn_cdc_increment_lsn (Transact-SQL)"
title: "sys.fn_cdc_increment_lsn (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/29/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "fn_cdc_increment_lsn_TSQL"
  - "sys.fn_cdc_increment_lsn_TSQL"
  - "sys.fn_cdc_increment_lsn"
  - "fn_cdc_increment_lsn"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_cdc_increment_lsn"
  - "sys.fn_cdc_increment_lsn"
author: rwestMSFT
ms.author: randolphwest
---
# sys.fn_cdc_increment_lsn (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the next log sequence number (LSN) in the sequence based upon the specified LSN.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql 
  
sys.fn_cdc_increment_lsn ( lsn_value )  
```  
  
## Arguments  

#### *lsn_value*  
 LSN value. *lsn_value* is **binary(10)**.  
  
## Return Type  
 **binary(10)**  
  
## Remarks  
 The LSN value returned by the function is always greater than the specified value, and no LSN values exist between the two values.  
  
 To systematically query a stream of change data over time, you can repeat the query function call periodically, each time specifying a new query interval to bound the changes returned in the query. To help insure that no data is lost, the upper bound for the previous query is often used to generate the lower bound for the subsequent query. Because the query interval is a closed interval, the new lower bound must be larger than the previous upper bound, but small enough to ensure no changes have LSN values that lie between this value and the old upper bound. The function `sys.fn_cdc_increment_lsn` is used to obtain this value.  

## Permissions  
 Requires membership in the **public** database role.  
  
## Examples  
 The following example uses `sys.fn_cdc_increment_lsn` to generate a new lower bound value for a change data capture query based on the upper bound saved from a previous query and saved in the variable `@save_to_lsn`.  
  
```sql  
USE AdventureWorks2012;  
GO  
DECLARE @from_lsn binary(10), @to_lsn binary(10), @save_to_lsn binary(10);  
SET @save_to_lsn = <previous_upper_bound_value>;  
SET @from_lsn = sys.fn_cdc_increment_lsn(@save_to_lsn);  
SET @to_lsn = sys.fn_cdc_get_max_lsn();  
SELECT * from cdc.fn_cdc_get_all_changes_HumanResources_Employee( @from_lsn, @to_lsn, 'all' );  
GO  
```  
  
> [!NOTE] 
> Error 313 is expected if LSN range supplied is not appropriate when calling `cdc.fn_cdc_get_all_changes_<capture_instance>` or `cdc.fn_cdc_get_net_changes_<capture_instance>`. If the `lsn_value` parameter is beyond the time of lowest LSN or highest LSN, then execution of these functions will return in error 313: `Msg 313, Level 16, State 3, Line 1 An insufficient number of arguments were supplied for the procedure or function`. This error should be handled by the developer. Sample T-SQL for a workaround can be found [at ReplTalk on GitHub](https://github.com/ReplTalk/ReplScripts/tree/master/CDC). 

## See Also  
 - [sys.fn_cdc_decrement_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-decrement-lsn-transact-sql.md)   
 - [cdc.fn_cdc_get_all_changes_&#60;capture_instance&#62;  &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql.md)   
 - [cdc.fn_cdc_get_net_changes_&#60;capture_instance&#62; &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-net-changes-capture-instance-transact-sql.md)   
 - [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md)   
 - [About Change Data Capture &#40;SQL Server&#41;](../../relational-databases/track-changes/about-change-data-capture-sql-server.md)  
  
  
