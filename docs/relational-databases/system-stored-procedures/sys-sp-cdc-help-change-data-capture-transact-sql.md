---
description: "sys.sp_cdc_help_change_data_capture (Transact-SQL)"
title: "sys.sp_cdc_help_change_data_capture (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_cdc_help_change_data_capture_TSQL"
  - "sys.sp_cdc_help_change_data_capture_TSQL"
  - "sp_cdc_help_change_data_capture"
  - "sys.sp_cdc_help_change_data_capture"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "change data capture [SQL Server], querying metadata"
  - "sys.sp_cdc_help_change_data_capture"
  - "sp_cdc_help_change_data_capture"
ms.assetid: 91fd41f5-1b4d-44fe-a3b5-b73eff65a534
author: markingmyname
ms.author: maghan
---
# sys.sp_cdc_help_change_data_capture (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the change data capture configuration for each table enabled for change data capture in the current database. Up to two rows can be returned for each source table, one row for each capture instance. Change data capture is not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.sp_cdc_help_change_data_capture   
  [ [ @source_schema = ] 'source_schema' ]  
  [, [ @source_name = ] 'source_name' ]  
```  
  
## Arguments  
 [ @source_schema = ] '*source_schema*'  
 Is the name of the schema in which the source table belongs. *source_schema* is **sysname**, with a default of NULL. When *source_schema* is specified, *source_name* must also be specified.  
  
 If non-NULL, *source_schema* must exist in the current database.  
  
 If *source_schema* is non-NULL, *source_name* must also be non-NULL.  
  
 [ @source_name = ] '*source_name*'  
 Is the name of the source table. *source_name* is **sysname**, with a default of NULL. When *source_name* is specified, *source_schema* must also be specified.  
  
 If non-NULL, *source_name* must exist in the current database.  
  
 If *source_name* is non-NULL, *source_schema* must also be non-NULL.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|source_schema|**sysname**|Name of the source table schema.|  
|source_table|**sysname**|Name of the source table.|  
|capture_instance|**sysname**|Name of the capture instance.|  
|object_id|**int**|ID of the change table associated with the source table.|  
|source_object_id|**int**|ID of the source table.|  
|start_lsn|**binary(10)**|Log sequence number (LSN) representing the low endpoint for querying the change table.<br /><br /> NULL = the low endpoint has not been established.|  
|end_lsn|**binary(10)**|LSN representing the high endpoint for querying the change table. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], this column is always NULL.|  
|supports_net_changes|**bit**|Net change support is enabled.|  
|has_drop_pending|**bit**|Not used in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].|  
|role_name|**sysname**|Name of the database role used to control access to the change data.<br /><br /> NULL = a role is not used.|  
|index_name|**sysname**|Name of the index used to uniquely identify rows in the source table.|  
|filegroup_name|**sysname**|Name of the filegroup in which the change table resides.<br /><br /> NULL = change table is in the default filegroup of the database.|  
|create_date|**datetime**|Date that the capture instance was enabled.|  
|index_column_list|**nvarchar(max)**|List of index columns used to uniquely identify rows in the source table.|  
|captured_column_list|**nvarchar(max)**|List of captured source columns.|  
  
## Remarks  
 When both *source_schema* and *source_name* default to NULL, or are explicitly set the NULL, this stored procedure returns information for all of the database capture instances that the caller has SELECT access to. When *source_schema* and *source_name* are non-NULL, only information on the specific named enabled table is returned.  
  
## Permissions  
 When *source_schema* and *source_name* are NULL, the caller's authorization determines which enabled tables are included in the result set. Callers must have SELECT permission on all of the captured columns of the capture instance and also membership in any defined gating roles for the table information to be included. Members of the db_owner database role can view information about all defined capture instances. When information for a specific enabled table is requested, the same SELECT and membership criteria are applied for the named table.  
  
## Examples  
  
### A. Returning change data capture configuration information for a specified table  
 The following example returns the change data capture configuration for the `HumanResources.Employee` table.  
  
```  
USE AdventureWorks2012;  
GO  
EXECUTE sys.sp_cdc_help_change_data_capture   
    @source_schema = N'HumanResources',   
    @source_name = N'Employee';  
GO  
```  
  
### B. Returning change data capture configuration information for all tables  
 The following example returns configuration information for all enabled tables in the database that contain change data that the caller is authorized to access.  
  
```  
USE AdventureWorks2012;  
GO  
EXECUTE sys.sp_cdc_help_change_data_capture;  
GO  
```  
  
