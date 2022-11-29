---
title: "BACKUP MASTER KEY (Transact-SQL)"
description: BACKUP MASTER KEY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
f1_keywords:
  - "BACKUP MASTER KEY"
  - "DUMP_MASTER_KEY_TSQL"
  - "BACKUP_MASTER_KEY_TSQL"
  - "DUMP MASTER KEY"
helpviewer_keywords:
  - "BACKUP MASTER KEY statement"
  - "exporting Database Master Keys"
  - "encryption [SQL Server], Database Master Key"
  - "cryptography [SQL Server], Database Master Key"
  - "backing up master keys [SQL Server]"
  - "database master key [SQL Server], exporting"
dev_langs:
  - "TSQL"
---
# BACKUP MASTER KEY (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Exports the database master key.  

> [!IMPORTANT]
> [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] introduces backup and restore support for the database master key to and from an Azure Blob storage. The `URL` syntax is only available for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
BACKUP MASTER KEY TO 
  {
    FILE = 'path_to_file'
  | URL = 'Azure Blob storage URL'
  }   
    ENCRYPTION BY PASSWORD = 'password'  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

 FILE **='***path_to_file***'**  
 Specifies the complete path, including file name, to the file to which the master key will be exported. The path may be a local path or a UNC path to a network location.  

 URL **='***Azure Blob storage URL***'**   
 **Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later   
 Is the URL for your Azure Blob storage, in the format similar to `https://<storage_account_name>.blob.core.windows.net/<storage_container_name>/<backup_file_name>.bak`.
  
 ENCRYPTION BY PASSWORD **='***password***'**  
 Is the password used to encrypt the master key in the file. This password is subject to complexity checks. For more information, see [Password Policy](../../relational-databases/security/password-policy.md).  
  
## Remarks

 The master key must be open and, therefore, decrypted before it's backed up. If it's encrypted with the service master key, the master key doesn't have to be explicitly opened. But if the master key is encrypted only with a password, it must be explicitly opened.  
  
 Back up the master key as soon as it's created, and store the backup in a secure, off-site location.  

## Authenticate to Azure Blob storage

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later.

To back up the database master key to an Azure Blob storage, the following prerequisites apply:

1. Have an [Azure storage account](/azure/storage/common/storage-account-create).
1. [Create stored access policy and shared access storage](../../relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016.md#1---create-stored-access-policy-and-shared-access-storage).
1. [Create a SQL Server credential using a shared access signature](../../relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016.md#2---create-a-sql-server-credential-using-a-shared-access-signature).

   For more information, see [Tutorial: Use Azure Blob Storage with SQL Server](../../relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016.md).

## Permissions

Requires CONTROL permission on the database.  
  
## Examples

The following example creates a backup of the `AdventureWorks2012` master key to a file. Because this master key isn't encrypted by the service master key, a password must be specified when it's opened.  
  
```sql  
USE AdventureWorks2012;  
OPEN MASTER KEY DECRYPTION BY PASSWORD = 'sfj5300osdVdgwdfkli7';  
BACKUP MASTER KEY TO FILE = 'c:\temp\AdventureWorks2012_master_key'   
    ENCRYPTION BY PASSWORD = 'sd092735kjn$&adsg';  
GO   
```  

The following example creates a backup of the `AdventureWorks2012` master key to an Azure Blob storage. Because this master key isn't encrypted by the service master key, a password must be specified when it's opened.  
  
```sql  
USE AdventureWorks2012;  
OPEN MASTER KEY DECRYPTION BY PASSWORD = 'sfj5300osdVdgwdfkli7';  
BACKUP MASTER KEY TO URL = 'https://mydocsteststorage.blob.core.windows.net/mytestcontainer/AdventureWorks2012_master_key.bak'  
    ENCRYPTION BY PASSWORD = 'sd092735kjn$&adsg';  
GO   
```  

## See also

 [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-master-key-transact-sql.md)   
 [OPEN MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/open-master-key-transact-sql.md)   
 [CLOSE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/close-master-key-transact-sql.md)   
 [RESTORE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-master-key-transact-sql.md)   
 [ALTER MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-master-key-transact-sql.md)   
 [DROP MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-master-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
 [BACKUP SYMMETRIC KEY](backup-symmetric-key-transact-sql.md)