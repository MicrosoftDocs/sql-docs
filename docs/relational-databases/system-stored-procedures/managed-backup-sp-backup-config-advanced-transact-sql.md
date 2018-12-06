---
title: "managed_backup.sp_backup_config_advanced (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_backup_config_optional"
  - "sp_backup_config_optional_TSQL"
  - "managed_backup.sp_backup_config_optional_TSQL"
  - "managed_backup.sp_backup_config_optional"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_backup_config_optional"
  - "managed_backup.sp_backup_config_optional"
ms.assetid: 4fae8193-1f88-48fd-a94a-4786efe8d6af
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# managed_backup.sp_backup_config_advanced (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Configures advanced settings for [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```vb  
EXEC managed_backup.sp_backup_config_advanced   
    [@database_name = ] 'database_name'  
    ,[@encryption_algorithm = ] 'name of the encryption algorithm'  
    ,[@encryptor_type = ] {'CERTIFICATE' | 'ASYMMETRIC_KEY'}  
    ,[@encryptor_name = ] 'name of the certificate or asymmetric key'  
    ,[@local_cache_path = ] 'NOT AVAILABLE'  
```  
  
##  <a name="Arguments"></a> Arguments  
 @database_name  
 The database name for enabling managed backup on a specific database. If NULL or *, then this managed backup applies to all databases on the server.  
  
 @encryption_algorithm  
 The name of the encryption algorithm used during the backup to encrypt the backup file. The @encryption_algorithm is **SYSNAME**. It is a required parameter when configuring [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the first time for the database. Specify **NO_ENCRYPTION** if you do not wish to encrypt the backup file. When changing the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] configuration settings, this parameter is optional - if the parameter is not specified then the existing configuration values are retained. The allowed values for this parameter are:  
  
-   AES_128  
  
-   AES_192  
  
-   AES_256  
  
-   TRIPLE_DES_3KEY  
  
-   NO_ENCRYPTION  
  
 For more information on encryption algorithms, see [Choose an Encryption Algorithm](../../relational-databases/security/encryption/choose-an-encryption-algorithm.md).  
  
 @encryptor_type  
 The type of encryptor, which can be either 'CERTIFICATE' or 'ASYMMETRIC_KEY". The @encryptor_type is **nvarchar(32)**. This parameter is optional if you specify NO_ENCRYPTION for the @encryption_algorithm parameter.  
  
 @encryptor_name  
 The name of an existing certificate or asymmetric key to use to encrypt the backup. The @encryptor_name is **SYSNAME**. If using an asymmetric key, it must be configured with Extended Key Management (EKM). This parameter is optional if you specify NO_ENCRYPTION for the @encryption_algorithm parameter.  
  
 For more information, see [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md).  
  
 @local_cache_path  
 This parameter is not yet supported.  
  
## Return Code Value  
 0 (success) or 1 (failure)  
  
## Security  
  
### Permissions  
 Requires membership in **db_backupoperator** database role, with **ALTER ANY CREDENTIAL** permissions, and **EXECUTE** permissions on **sp_delete_backuphistory** stored procedure.  
  
## Examples  
 The following example sets advanced configuration options for [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] for the instance of SQL Server.  
  
```  
Use msdb;  
Go  
   EXEC managed_backup.sp_backup_config_advanced  
                @encryption_algorithm ='AES_128'  
                ,@encryptor_type = 'CERTIFICATE'  
                ,@encryptor_name = 'MyTestDBBackupEncryptCert'  
GO  
```  
  
## See Also  
 [managed_backup.sp_backup_config_basic (Transact-SQL)](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-basic-transact-sql.md)   
 [managed_backup.sp_backup_config_schedule &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/managed-backup-sp-backup-config-schedule-transact-sql.md)  
  
  
