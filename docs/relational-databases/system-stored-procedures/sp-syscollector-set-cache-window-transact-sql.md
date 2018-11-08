---
title: "sp_syscollector_set_cache_window (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_syscollector_set_cache_window"
  - "sp_syscollector_set_cache_window_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_syscollector_set_cache_window stored procedure"
  - "data collector [SQL Server], stored procedures"
ms.assetid: 660f2749-392f-46bf-89f3-27764d848507
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_syscollector_set_cache_window (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Sets the number of times to attempt a data upload in case of failure. Retrying an upload in the event of a failure mitigates the risk of losing collected data.  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syscollector_set_cache_window [ @cache_window = ] cache_window   
```  
  
## Arguments  
 [ @cache_window = ] *cache_window*  
 Is the number of times a failed data upload to the management data warehouse is retried without losing data. *cache_window* is **int** with a default value of 1. *cache_window* can have one of the following values:  
  
|Value|Description|  
|-----------|-----------------|  
|-1|Cache all the upload data from the previous upload failures.|  
|0|Do not cache any data from an upload failure.|  
|*n*|Cache data from n previous upload failures, where *n* >= 1.|  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 You must disable the data collector before changing the cache window configuration. This stored procedure fails if the data collector is enabled. For more information, see [Enable or Disable Data Collection](../../relational-databases/data-collection/enable-or-disable-data-collection.md), and [Manage Data Collection](../../relational-databases/data-collection/manage-data-collection.md).  
  
## Permissions  
 Requires membership in the dc_admin (with EXECUTE permission) fixed database role to execute this procedure.  
  
## Examples  
 The following example disables the data collector, configures the cache window to retain data for up to three failed uploads, and then enables to data collector.  
  
```sql  
USE msdb;  
GO  
EXECUTE dbo.sp_syscollector_disable_collector;  
GO  
EXECUTE dbo.sp_syscollector_set_cache_window 3;  
GO  
EXECUTE dbo.sp_syscollector_enable_collector;  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [sp_syscollector_set_cache_directory &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-set-cache-directory-transact-sql.md)  
  
  
