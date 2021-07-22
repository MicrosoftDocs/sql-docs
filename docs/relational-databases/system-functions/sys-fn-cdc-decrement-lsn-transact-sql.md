---
description: "sys.fn_cdc_decrement_lsn (Transact-SQL)"
title: "sys.fn_cdc_decrement_lsn (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
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
ms.assetid: 83c182ad-4713-439b-8769-9b7408aec8b4
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# sys.fn_cdc_decrement_lsn (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the previous log sequence number (LSN) in the sequence based upon the specified LSN.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.fn_cdc_decrement_lsn ( lsn_value )  
```  
  
## Arguments  
 *lsn_value*  
 LSN value. *lsn_value* is **binary(10)**.  
  
## Return Type  
 **binary(10)**  
  
## Remarks  
 The LSN returned by the function is always less than the specified value, and no LSN values can exist between the two values.  
  
## Permissions  
 Requires membership in the **public** database role.  
  
## Examples  
 The following example uses `sys.fn_cdc_decrement_lsn` to set the upper LSN boundary in a query that returns change data rows that have LSN values less than the maximum LSN value.  
  
```  
Use AdventureWorks2012;  
GO  
DECLARE @from_lsn binary(10), @to_lsn binary(10);  
SET @from_lsn = sys.fn_cdc_get_min_lsn('HumanResources_Employee');  
SET @to_lsn = sys.fn_cdc_decrement_lsn(sys.fn_cdc_get_max_lsn());  
SELECT * FROM cdc.fn_cdc_get_all_changes_HumanResources_Employee( @from_lsn, @to_lsn, 'all');   
GO  
```  
**NOTE:**

 Error 313 is expected if LSN range supplied is not appropriate when calling cdc.fn_cdc_get_all_changes_<capture_instance> or cdc.fn_cdc_get_net_changes_<capture_instance> and it should be handled. This example shows how to handle this scenario for dbo.HR_Department table.

```
begin try
DECLARE @from_lsn binary(10), @to_lsn binary(10), @save_to_lsn binary(10); 
SET @save_to_lsn = sys.fn_cdc_get_min_lsn('dbo_HR_Department')
select @save_to_lsn
SET @from_lsn = sys.fn_cdc_increment_lsn(0x00000026000005F00003); 
select @from_lsn
SET @to_lsn = sys.fn_cdc_get_max_lsn(); 
select @to_lsn
SELECT * from cdc.fn_cdc_get_all_changes_dbo_Tab1( @from_lsn, @to_lsn, 'all' ); 
end try
begin catch
if @@ERROR = 313
RAISERROR ('Invalid range passed to fn_cdc_get_all_changes_dbo_HR_Department.', 16, 1); 
```

## See Also  
 [sys.fn_cdc_increment_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-increment-lsn-transact-sql.md)   
 [sys.fn_cdc_get_min_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-get-min-lsn-transact-sql.md)   
 [sys.fn_cdc_get_max_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-get-max-lsn-transact-sql.md)   
 [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md)   
 [About Change Data Capture &#40;SQL Server&#41;](../../relational-databases/track-changes/about-change-data-capture-sql-server.md)  
  
  
