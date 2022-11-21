---
description: "sys.sp_cdc_disable_table (Transact-SQL)"
title: "sys.sp_cdc_disable_table (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.sp_cdc_disable_table"
  - "sp_cdc_disable_table"
  - "sys.sp_cdc_disable_table_TSQL"
  - "sp_cdc_disable_table_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_cdc_disable_table"
  - "sys.sp_cdc_disable_table"
  - "change data capture [SQL Server], disabling tables"
ms.assetid: da2156c0-504e-4d76-b9a0-4448becf9bda
author: markingmyname
ms.author: maghan
---
# sys.sp_cdc_disable_table (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Disables change data capture for the specified source table and capture instance in the current database. Change data capture is not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.sp_cdc_disable_table   
  [ @source_schema = ] 'source_schema' ,   
  [ @source_name = ] 'source_name'  
  [ , [ @capture_instance = ] 'capture_instance' | 'all' ]  
```  
  
## Arguments  
`[ @source_schema = ] 'source\_schema'`
 Is the name of the schema in which the source table is contained. *source_schema* is **sysname**, with no default, and cannot be NULL.  
  
 *source_schema* must exist in the current database.  
  
`[ @source_name = ] 'source\_name'`
 Is the name of the source table from which change data capture is to be disabled. *source_name* is **sysname**, with no default, and cannot be NULL.  
  
 *source_name* must exist in the current database.  
  
`[ @capture_instance = ] 'capture\_instance' | 'all'`
 Is the name of the capture instance to disable for the specified source table. *capture_instance* is **sysname** and cannot be NULL.  
  
 When 'all' is specified, all capture instances defined for *source_name* are disabled.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sys.sp_cdc_disable_table** drops the change data capture change table and system functions associated with the specified source table and capture instance. It deletes any rows associated with the specified capture instance from the change data capture system tables and sets the **is_tracked_by_cdc** column for the table entry in the [sys.tables](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md) catalog view to 0.  
  
## Permissions  
 Requires membership in the **db_owner** fixed database role.  
  
## Examples  
 The following example disables change data capture for the `HumanResources.Employee` table.  
  
```  
USE AdventureWorks2012;  
GO  
EXECUTE sys.sp_cdc_disable_table   
    @source_schema = N'HumanResources',   
    @source_name = N'Employee',  
    @capture_instance = N'HumanResources_Employee';  
```  
  
## See Also  
 [sys.sp_cdc_enable_table &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-enable-table-transact-sql.md)  
  
  
