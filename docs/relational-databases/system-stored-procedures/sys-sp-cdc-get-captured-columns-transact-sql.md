---
description: "sys.sp_cdc_get_captured_columns (Transact-SQL)"
title: "sys.sp_cdc_get_captured_columns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_cdc_get_captured_columns"
  - "sys.sp_cdc_get_captured_columns"
  - "sys.sp_cdc_get_captured_columns_TSQL"
  - "sp_cdc_get_captured_columns_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_cdc_get_captured_columns"
  - "sp_cdc_get_captured_columns"
  - "change data capture [SQL Server], querying metadata"
ms.assetid: d9e680be-ab9b-4e0c-b63a-90658f241df8
author: markingmyname
ms.author: maghan
---
# sys.sp_cdc_get_captured_columns (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns change data capture metadata information for the captured source columns tracked by the specified capture instance. Change data capture is not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.sp_cdc_get_captured_columns   
    [ @capture_instance = ] 'capture_instance'  
```  
  
## Arguments  
 [ @capture_instance = ] '*capture_instance*'  
 Is the name of the capture instance associated with a source table. *capture_instance* is **sysname** and cannot be NULL.  
  
 To report on the capture instances for the table, run the [sys.sp_cdc_help_change_data_capture](../../relational-databases/system-stored-procedures/sys-sp-cdc-help-change-data-capture-transact-sql.md) stored procedure.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|source_schema|**sysname**|Name of the source table schema.|  
|source_table|**sysname**|Name of the source table.|  
|capture_instance|**sysname**|Name of the capture instance.|  
|column_name|**sysname**|Name of the captured source column.|  
|column_id|**int**|ID of the column in the source table.|  
|column_ordinal|**int**|Position of the column within the source table.|  
|data_type|**sysname**|Column data type.|  
|character_maximum_length|**int**|Maximum character length of the character-based column; otherwise, NULL.|  
|numeric_precision|**tinyint**|Precision of the column if numeric-based; otherwise, NULL.|  
|numeric_precision_radix|**smallint**|Precision radix of the column if numeric-based; otherwise, NULL.|  
|numeric_scale|**int**|Scale of the column if numeric-based; otherwise, NULL.|  
|datetime_precision|**smallint**|Precision of the column if datetime-based; otherwise, NULL.|  
  
## Remarks  
 Use sys.sp_cdc_get_captured_columns to obtain column information about the captured columns returned by querying the capture instance query functions [cdc.fn_cdc_get_all_changes_<capture_instance>](../../relational-databases/system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql.md) or [cdc.fn_cdc_get_net_changes_<capture_instance>](../../relational-databases/system-functions/cdc-fn-cdc-get-net-changes-capture-instance-transact-sql.md). The column names, IDs, and position remain constant for the life of the capture instance. Only the column data type changes when the data type of the underlying source column in the tracked table changes. Columns that are added to or dropped from a source table have no impact on the captured columns of existing capture instances.  
  
 Use [sys.sp_cdc_get_ddl_history](../../relational-databases/system-stored-procedures/sys-sp-cdc-get-ddl-history-transact-sql.md) to obtain information about data definition language (DDL) statements applied to a source table. Any DDL changes that modified the structure of a tracked source column is returned in the result set.  
  
## Permissions  
 Requires membership in the db_owner fixed database role. For all other users, requires SELECT permission on all captured columns in the source table and, if a gating role for the capture instance was defined, membership in that database role. When the caller does not have permission to view the source data, the function returns error 22981 (Object does not exist or access is denied.).  
  
## Examples  
 The following example returns information about the captured columns in the `HumanResources_Employee` capture instance.  
  
```  
USE AdventureWorks2012;  
GO  
EXECUTE sys.sp_cdc_get_captured_columns   
    @capture_instance = N'HumanResources_Employee';  
GO  
```  
  
## See Also  
 [sys.sp_cdc_help_change_data_capture &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-help-change-data-capture-transact-sql.md)  
  
