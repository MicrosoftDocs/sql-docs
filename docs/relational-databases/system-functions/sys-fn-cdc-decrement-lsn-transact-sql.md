---
description: "sys.fn_cdc_decrement_lsn (Transact-SQL)"
title: "sys.fn_cdc_decrement_lsn (Transact-SQL)"
ms.custom: ""
ms.date: "09/29/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "fn_cdc_decrement_lsn"
  - "sys.fn_cdc_decrement_lsn_TSQL"
  - "sys.fn_cdc_decrement_lsn"
  - "fn_cdc_decrement_lsn_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_cdc_decrement_lsn"
  - "sys.fn_cdc_decrement_lsn"
author: rwestMSFT
ms.author: randolphwest
---
# sys.fn_cdc_decrement_lsn (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the previous log sequence number (LSN) in the sequence based upon the specified LSN.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
  
sys.fn_cdc_decrement_lsn ( lsn_value )  
```  
  
## Arguments  

#### *lsn_value*  
 LSN value. *lsn_value* is **binary(10)**.  
  
## Return Type  
 **binary(10)**  
  
## Remarks  
 The LSN returned by the function is always less than the specified value, and no LSN values can exist between the two values.  

## Permissions  
 Requires membership in the **public** database role.  
  
## Examples  
 The following example uses `sys.fn_cdc_decrement_lsn` to set the upper LSN boundary in a query that returns change data rows that have LSN values less than the maximum LSN value.  
  
```sql  
Use AdventureWorks2012;  
GO  
DECLARE @from_lsn binary(10), @to_lsn binary(10);  
SET @from_lsn = sys.fn_cdc_get_min_lsn('HumanResources_Employee');  
SET @to_lsn = sys.fn_cdc_decrement_lsn(sys.fn_cdc_get_max_lsn());  
SELECT * FROM cdc.fn_cdc_get_all_changes_HumanResources_Employee( @from_lsn, @to_lsn, 'all');   
GO  
``` 

> [!NOTE] 
> Error 313 is expected if LSN range supplied is not appropriate when calling `cdc.fn_cdc_get_all_changes_<capture_instance>` or `cdc.fn_cdc_get_net_changes_<capture_instance>`. If the `lsn_value` parameter is beyond the time of lowest LSN or highest LSN, then execution of these functions will return in error 313: `Msg 313, Level 16, State 3, Line 1 An insufficient number of arguments were supplied for the procedure or function`. This error should be handled by the developer. Sample T-SQL for a workaround can be found [at ReplTalk on GitHub](https://github.com/ReplTalk/ReplScripts/tree/master/CDC). 
  
## See Also  
 - [sys.fn_cdc_increment_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-increment-lsn-transact-sql.md)   
 - [sys.fn_cdc_get_min_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-get-min-lsn-transact-sql.md)   
 - [sys.fn_cdc_get_max_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-get-max-lsn-transact-sql.md)   
 - [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md)   
 - [About Change Data Capture &#40;SQL Server&#41;](../../relational-databases/track-changes/about-change-data-capture-sql-server.md)  
  
  
