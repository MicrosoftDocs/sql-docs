---
title: "managed_backup.fn_backup_db_config (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "smart_admin.fn_backup_db_config"
  - "smart_admin.fn_backup_db_config_TSQL"
  - "fn_backup_db_config"
  - "fn_backup_db_config_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "smart_admin.fn_backup_db_config"
  - "fn_backup_db_config"
ms.assetid: 7c755d8a-64dd-44b2-be5e-735d30758900
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# managed_backup.fn_backup_db_config (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Returns 0, 1 or more rows with [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] configuration settings. Returns 1 row for the specified database, or returns the information for all the databases configured with [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] on the instance.  
  
 Use this stored procedure to review or determine the current [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] configuration settings for a database or all the databases on an instance of SQL Server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
managed_backup.fn_backup_db_config ('database_name' | '' | NULL)  
```  
  
##  <a name="Arguments"></a> Arguments  
 @db_name  
 The name of the database. The @db_name parameter is **SYSNAME**. If an empty string or NULL value is passed to this parameter, the information about all the databases on the instance of SQL Server is returned.  
  
## Table Returned  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|db_name|SYSNAME|Database name.|  
|db_guid|UNIQUEIDENTIFIER|Identifier that uniquely identifies the database.|  
|is_availability_database|BIT|Whether the database is participating in Availability Group. A value of 1 indicates that the database is an Availability database and 0 that it is not.|  
|is_dropped|BIT|A value of 1 indicates that this is a dropped database.|  
|credential_name|SYSNAME|Name of the SQL Credential used to authenticate to the storage account. NULL value indicates that no SQL Credential has been set.|  
|retention_days|INT|The current retention period in days. NULL value indicates that [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] was never configured for this database.|  
|is_managed_backup_enabled|INT|Indicates whether [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is currently enabled for this database. A value of 1 indicates that [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is currently enabled, and a value of 0 indicates that [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] is disabled for this database.|  
|storage_url|NVARCHAR(1024)|The URL of the storage account.|  
|Encryption_algorithm|NCHAR(20)|Returns the current encryption algorithm to use when encrypting the backup.|  
|Encryptor_type|NCHAR(15)|Returns the encryptor setting: Certificate or Asymmetric Key.|  
|Encryptor_name|NCHAR(max_length_of_cert/asymm_key_name)|The name of the certificate or asymmetric key.|  
  
## Security  
  
### Permissions  
 Requires membership in the **db_backupoperator** database role with **ALTER ANY CREDENTIAL** permissions. The user should not be denied **VIEW ANY DEFINITION** permissions.  
  
## Examples  
 The following example returns the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] configuration for 'TestDB'  
  
 For each code snippet, select 'tsql' in the language attribute field.  
  
```  
Use msdb  
GO  
SELECT * FROM managed_backup.fn_backup_db_config('TestDB')  
```  
  
 The following example returns the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] configuration for all the databases on the instance of SQL Server it is executed on.  
  
```  
Use msdb  
GO  
SELECT * FROM managed_backup.fn_backup_db_config (NULL)  
```  
  
  
