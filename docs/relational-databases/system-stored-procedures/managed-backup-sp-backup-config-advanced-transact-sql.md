---
title: "managed_backup.sp_backup_config_advanced (Transact-SQL)"
description: Configures advanced settings for SQL Server Managed Backup to Azure.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/31/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_backup_config_optional"
  - "sp_backup_config_optional_TSQL"
  - "managed_backup.sp_backup_config_optional_TSQL"
  - "managed_backup.sp_backup_config_optional"
helpviewer_keywords:
  - "sp_backup_config_optional"
  - "managed_backup.sp_backup_config_optional"
dev_langs:
  - "TSQL"
---
# managed_backup.sp_backup_config_advanced (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Configures advanced settings for [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
EXEC managed_backup.sp_backup_config_advanced
    [ @database_name = ] 'database_name'
    , [ @encryption_algorithm = ] 'name of the encryption algorithm'
    , [ @encryptor_type = ] { 'CERTIFICATE' | 'ASYMMETRIC_KEY' }
    , [ @encryptor_name = ] 'name of the certificate or asymmetric key'
    , [ @local_cache_path = ] 'NOT AVAILABLE'
[ ; ]
```

## Arguments

#### [ @database_name = ] '*database_name*'

The database name for enabling managed backup on a specific database.

If *@database_name* is set to NULL, the settings are applied at instance level (applies to all new databases created on the instance).

#### [ @encryption_algorithm ] = '*name of the encryption algorithm*'

The name of the encryption algorithm used during the backup to encrypt the backup file. *@encryption_algorithm* is **sysname**. It is a required parameter when configuring [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] for the first time for the database. Specify `NO_ENCRYPTION` if you don't wish to encrypt the backup file. When you change the [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] configuration settings, this parameter is optional. If the parameter isn't specified, the existing configuration values are retained. The allowed values for this parameter are:

- AES_128
- AES_192
- AES_256
- TRIPLE_DES_3KEY
- NO_ENCRYPTION

For more information on encryption algorithms, see [Choose an Encryption Algorithm](../security/encryption/choose-an-encryption-algorithm.md).

#### [ @encryptor_type = ] { 'CERTIFICATE' | 'ASYMMETRIC_KEY' }

The type of encryptor, which can be either `CERTIFICATE` or `ASYMMETRIC_KEY`. *@encryptor_type* is **nvarchar(32)**. This parameter is optional if you specify `NO_ENCRYPTION` for the *@encryption_algorithm* parameter.

#### [ @encryptor_name = ] '*name of the certificate or asymmetric key*'

The name of an existing certificate or asymmetric key to use to encrypt the backup. *@encryptor_name* is **sysname**. If using an asymmetric key, it must be configured with Extensible Key Management (EKM). This parameter is optional if you specify `NO_ENCRYPTION` for the *@encryption_algorithm* parameter.

For more information, see [Extensible Key Management (EKM)](../security/encryption/extensible-key-management-ekm.md).

#### [ @local_cache_path = ] 'NOT AVAILABLE'

This parameter isn't yet supported.

## Return code values

`0` (success) or `1` (failure).

## Permissions

Requires membership in the **db_backupoperator** database role, with ALTER ANY CREDENTIAL permissions, and EXECUTE permissions on the `sp_delete_backuphistory` stored procedure.

## Examples

The following example sets advanced configuration options for [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] for the instance of SQL Server.

```sql
USE msdb;
GO

EXEC managed_backup.sp_backup_config_advanced @encryption_algorithm = 'AES_128',
    @encryptor_type = 'CERTIFICATE',
    @encryptor_name = 'MyTestDBBackupEncryptCert'
GO
```

## Related content

- [managed_backup.sp_backup_config_basic (Transact-SQL)](managed-backup-sp-backup-config-basic-transact-sql.md)
- [managed_backup.sp_backup_config_schedule (Transact-SQL)](managed-backup-sp-backup-config-schedule-transact-sql.md)
