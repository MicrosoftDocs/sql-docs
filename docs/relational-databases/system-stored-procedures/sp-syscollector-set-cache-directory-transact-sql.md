---
title: "sp_syscollector_set_cache_directory (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_syscollector_set_cache_directory_TSQL"
  - "sp_syscollector_set_cache_directory"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "data collector [SQL Server], stored procedures"
  - "sp_syscollector_set_cache_directory stored procedure"
ms.assetid: df56d5a5-8961-494f-a745-d752ca63805a
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_syscollector_set_cache_directory (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Specifies the directory where collected data is stored before it is uploaded to the management data warehouse.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syscollector_set_cache_directory [ @cache_directory = ] 'cache_directory'  
```  
  
## Arguments  
`[ @cache_directory = ] 'cache_directory'`
 The directory in the file system where collected data is stored temporarily. *cache_directory* is **nvarchar(255)**, with a default value of NULL. If no value is specified, the default temporary [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directory is used.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 You must disable the data collector before changing the cache directory configuration. This stored procedure fails if the data collector is enabled. For more information, see [Enable or Disable Data Collection](../../relational-databases/data-collection/enable-or-disable-data-collection.md), and [Manage Data Collection](../../relational-databases/data-collection/manage-data-collection.md).  
  
 The specified directory does not need to exist at the time the sp_syscollector_set_cache_directory is executed; however, data cannot be successully cached and uploaded until the directory is created. We recommend creating the directory before executing this stored procedure.  
  
## Permissions  
 Requires membership in the dc_admin (with EXECUTE permission) fixed database role to execute this procedure.  
  
## Examples  
 The following example disables the data collector, sets the cache directory for the data collector to `D:\tempdata`,and then enables the data collector.  
  
```sql  
USE msdb;  
GO  
EXECUTE dbo.sp_syscollector_disable_collector;  
GO  
EXEC dbo.sp_syscollector_set_cache_directory N'D:\tempdata';  
GO  
EXECUTE dbo.sp_syscollector_enable_collector;  
GO  
```  
  
## See Also  
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)   
 [sp_syscollector_set_cache_window &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-set-cache-window-transact-sql.md)  
  
  
