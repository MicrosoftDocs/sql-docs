---
description: "sys.fn_cdc_map_lsn_to_time (Transact-SQL)"
title: "sys.fn_cdc_map_lsn_to_time (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.fn_cdc_map_lsn_to_time_TSQL"
  - "sys.fn_cdc_map_lsn_to_time"
  - "fn_cdc_map_lsn_to_time_TSQL"
  - "fn_cdc_map_lsn_to_time"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.fn_cdc_map_lsn_to_time"
  - "fn_cdc_map_lsn_to_time"
ms.assetid: 405aa29c-8bd8-42d3-9f39-7494b643fc6f
author: rwestMSFT
ms.author: randolphwest
---
# sys.fn_cdc_map_lsn_to_time (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the date and time value from the **tran_end_time** column in the [cdc.lsn_time_mapping](../../relational-databases/system-tables/cdc-lsn-time-mapping-transact-sql.md) system table for the specified log sequence number (LSN). You can use this function to systematically map LSN ranges to date ranges in a change table.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.fn_cdc_map_lsn_to_time ( lsn_value )  
```  
  
## Arguments  
 *lsn_value*  
 Is the LSN value to match against. *lsn_value* is **binary(10)**.  
  
## Return Type  
 **datetime**  
  
## Remarks  
 This function can be used to determine the time that a change was committed based upon the **__$start_lsn** value returned in the row of change data.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following example uses the function `sys.fn_cdc_map_lsn_to_time` to determine the commit time associated with the last change processed in the specified LSN interval for the `HumanResources_Employee` capture instance.  
  
```  
USE AdventureWorks2012;  
GO  
DECLARE @max_lsn binary(10);  
SELECT @max_lsn = MAX(__$start_lsn)  
FROM cdc.fn_cdc_get_all_changes_HumanResources_Employee(@from_lsn, @to_lsn, 'all');  
SELECT sys.fn_cdc_map_lsn_to_time(@max_lsn);  
GO   
```  
  
## See Also  
 [cdc.lsn_time_mapping &#40;Transact-SQL&#41;](../../relational-databases/system-tables/cdc-lsn-time-mapping-transact-sql.md)   
 [sys.fn_cdc_map_time_to_lsn &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-map-time-to-lsn-transact-sql.md)   
 [cdc.fn_cdc_get_net_changes_&#60;capture_instance&#62; &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-net-changes-capture-instance-transact-sql.md)   
 [cdc.fn_cdc_get_all_changes_&#60;capture_instance&#62;  &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql.md)  
  
  
