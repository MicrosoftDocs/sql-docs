---
title: "sp_help_log_shipping_secondary_primary (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_log_shipping_secondary_primary"
  - "sp_help_log_shipping_secondary_primary_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_log_shipping_secondary_primary"
ms.assetid: 1310fdaf-edb5-4294-9739-7fb37c2c2cb5
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# sp_help_log_shipping_secondary_primary (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This stored procedure retrieves the settings for a given primary database on the secondary server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_log_shipping_secondary_primary  
[ @primary_server = ] 'primary_server' OR  
[ @primary_database = ] 'primary_database'  
```  
  
## Arguments  
`[ @primary_server = ] 'primary_server'`
 The name of the primary instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration. *primary_server* is **sysname** and cannot be NULL.  
  
`[ @primary_database = ] 'primary_database'`
 Is the name of the database on the primary server. *primary_database* is **sysname**, with no default.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 The result set contains the columns **secondary_id**, **primary_server**, **primary_database**, **backup_source_directory**, **backup_destination_directory**, **file_retention_period**, **copy_job_id**, **restore_job_id**, **monitor_server**, **monitor_server_security_mode** from **log_shipping_secondary**.  
  
## Remarks  
 **sp_help_log_shipping_secondary_primary** must be run from the **master** database on the secondary server.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can run this procedure.  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
