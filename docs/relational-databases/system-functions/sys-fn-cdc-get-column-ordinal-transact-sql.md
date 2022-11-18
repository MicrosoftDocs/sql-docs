---
description: "sys.fn_cdc_get_column_ordinal (Transact-SQL)"
title: "sys.fn_cdc_get_column_ordinal (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/25/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.fn_cdc_get_column_ordinal"
  - "fn_cdc_get_column_ordinal_TSQL"
  - "fn_cdc_get_column_ordinal"
  - "sys.fn_cdc_get_column_ordinal_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_cdc_get_column_ordinal"
  - "sys.fn_cdc_get_column_ordinal"
ms.assetid: 4bb21a57-2b94-4208-8bdf-6a3e2681d881
author: rwestMSFT
ms.author: randolphwest
---
# sys.fn_cdc_get_column_ordinal (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the column ordinal of the specified column as it appears in the [change table](../../relational-databases/system-tables/cdc-capture-instance-ct-transact-sql.md) associated with the specified capture instance.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.fn_cdc_get_column_ordinal ( 'capture_instance','column_name')  
```  
  
## Arguments  
 **'** *capture_instance* **'**  
 Is the name of the capture instance in which the specified column is identified as a captured column. *capture_instance* is **sysname**.  
  
 **'** *column_name* **'**  
 Is the column to report on. *column_name* is **sysname**.  
  
## Return Type  
 **int**  
  
## Remarks  
 This function is used to identify the ordinal position of a captured column within the change data capture update mask. It is principally used in conjunction with the function [sys.fn_cdc_is_bit_set](../../relational-databases/system-functions/sys-fn-cdc-is-bit-set-transact-sql.md) to extract information from the update mask when querying for change data.  
  
## Permissions  
 Requires SELECT permission on all captured columns of the source table. If a database role for the change data capture component is specified for the capture instance, membership in that role is also required.  
  
## Examples  
 The following example obtains the ordinal position of the `VacationHours` column in the update mask for the `HumanResources_Employee` capture instance. That value is then used in the call to `sys.fn_cdc_is_bit_set` to extract information from the returned update mask.  
  
```  
USE AdventureWorks2012;  
GO  
DECLARE @from_lsn binary(10), @to_lsn binary(10),  @VacationHoursOrdinal int;  
SET @from_lsn = sys.fn_cdc_get_min_lsn('HumanResources_Employee');  
SET @to_lsn = sys.fn_cdc_get_max_lsn();  
SET @VacationHoursOrdinal = sys.fn_cdc_get_column_ordinal   
    ( 'HumanResources_Employee','VacationHours');  
SELECT *, sys.fn_cdc_is_bit_set(@VacationHoursOrdinal,  
    __$update_mask) as 'VacationHours'  
FROM cdc.fn_cdc_get_net_changes_HumanResources_Employee  
    ( @from_lsn, @to_lsn, 'all with mask');  
GO  
```  
  
## See Also  
 [Change Data Capture Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-data-capture-functions-transact-sql.md)   
 [About Change Data Capture &#40;SQL Server&#41;](../../relational-databases/track-changes/about-change-data-capture-sql-server.md)   
 [sys.sp_cdc_help_change_data_capture &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-help-change-data-capture-transact-sql.md)   
 [sys.sp_cdc_get_captured_columns &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-get-captured-columns-transact-sql.md)   
 [sys.fn_cdc_is_bit_set &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-is-bit-set-transact-sql.md)  
  
  
