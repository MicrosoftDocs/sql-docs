---
title: "managed_backup.fn_backup_instance_config (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "fn_backup_instance_config"
  - "smart_admin.fn_backup_instance_config_TSQL"
  - "fn_backup_instance_config_TSQL"
  - "smart_admin.fn_backup_instance_config"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "smart_admin.fn_backup_instance_config"
  - "fn_backup_instance_config"
ms.assetid: 2382a547-c0c9-4e1d-87c9-d8526192eb5a
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# managed_backup.fn_backup_instance_config (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Returns 1 row with the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] default configuration settings for the instance of SQL Server.  
  
 Use this stored procedure to review or determine the current [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] default configuration settings for the instance of SQL Server.  
  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
managed_backup.fn_backup_db_config ()  
```  
  
##  <a name="Arguments"></a> Arguments  
 None  
  
## Table Returned  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|is_smart_backup_enabled|INT|Displays 1 when [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is enabled, and 0 when [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is disabled.|  
|credential_name|SYSNAME|Default SQL Credential used to authenticate to the storage.|  
|retention_days|INT|Default retention period set at the instance level.|  
|storage_url|NVARCHAR(1024)|The default storage account URL set at the instance level.|  
|encryption_algorithm|SYSNAME|Name of the encryption algorithm. Is set to NULL if encryption is not specified.|  
|encryptor_type|NVARCHAR(32)|The type of encryptor used: Certificate or Asymmetric Key. Is set to NULL if there is no encryptor specified.|  
|encryptor_name|SYSNAME|The name of the certificate or asymmetric key. Is set to NULL if there is no name specified|  
  
## Security  
  
### Permissions  
 Requires membership in the **db_backupoperator** database role with **ALTER ANY CREDENTIAL** permissions. The user should not be denied **VIEW ANY DEFINITION** permissions.  
  
## Examples  
 The following example returns the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] default configuration settings for the instance it is executed on:  
  
```  
Use msdb;  
GO  
SELECT * FROM managed_backup.fn_backup_instance_config ();  
  
```  
  
  
