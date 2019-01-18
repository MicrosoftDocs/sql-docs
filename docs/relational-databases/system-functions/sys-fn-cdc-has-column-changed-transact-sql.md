---
title: "sys.fn_cdc_has_column_changed (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.fn_cdc_has_column_changed_TSQL"
  - "sys.fn_cdc_has_column_changed"
  - "fn_cdc_has_column_changed_TSQL"
  - "fn_cdc_has_column_changed"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.fn_cdc_has_column_changed"
  - "fn_cdc_has_column_changed"
ms.assetid: 2b9e6278-050d-4ffc-8d1a-09606180facc
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# sys.fn_cdc_has_column_changed (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Identifies whether the specified update mask indicates that the specified column has been updated in the associated change row.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.fn_cdc_has_column_changed ( 'capture_instance','column_name' , update_mask )  
```  
  
## Arguments  
 **'** *capture_instance* **'**  
 Is the name of the capture instance. *capture_instance* is **sysname**.  
  
 **'** *column_name* **'**  
 Is the captured column of the specified capture instance to report on. *column_name* is **sysname**.  
  
 *update_mask*  
 Is the mask identifying updated columns in any associated change row. *update_mask* is **varbinary(128)**.  
  
## Return Type  
 **bit**  
  
## Remarks  
 You can use this function to extract information from an update mask returned in a query for change data. It is most useful in post-processing the update mask when you need to know whether a particular column in the associated change row has been modified. For more information, see [About Change Data Capture &#40;SQL Server&#41;](../../relational-databases/track-changes/about-change-data-capture-sql-server.md).  
  
 When this information will be returned as part of a change data query, we recommend that you use the functions [sys.fn_cdc_get_column_ordinal](../../relational-databases/system-functions/sys-fn-cdc-get-column-ordinal-transact-sql.md) and [sys.fn_cdc_is_bit_set](../../relational-databases/system-functions/sys-fn-cdc-is-bit-set-transact-sql.md) instead of this function. Use the function fn_cdc_get_column_ordinal before querying for change data so that the desired column ordinal is only computed once. Use fn_cdc_is_bit_set within the query to extract the information from the update mask for each returned row.  
  
## Permissions  
 Requires membership in the sysadmin fixed server role or db_owner fixed database role. For all other users, requires SELECT permission on all captured columns in the source table and, if a gating role for the capture instance was defined, membership in that database role.  
  
## See Also  
 [cdc.&#60;capture_instance&#62;_CT &#40;Transact-SQL&#41;](../../relational-databases/system-tables/cdc-capture-instance-ct-transact-sql.md)   
 [cdc.captured_columns &#40;Transact-SQL&#41;](../../relational-databases/system-tables/cdc-captured-columns-transact-sql.md)  
  
  
