---
title: "RESTORE MASTER KEY (Transact-SQL)"
description: RESTORE MASTER KEY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
f1_keywords:
  - "RESTORE_MASTER_KEY_TSQL"
  - "RESTORE MASTER KEY"
  - "LOAD_MASTER_KEY_TSQL"
  - "LOAD MASTER KEY"
helpviewer_keywords:
  - "database master key [SQL Server], importing"
  - "encryption [SQL Server], Database Master Key"
  - "copying Database Master Keys"
  - "importing Database Master Keys"
  - "cryptography [SQL Server], Database Master Key"
  - "transferring Database Master Keys"
  - "RESTORE MASTER KEY statement"
dev_langs:
  - "TSQL"
---
# RESTORE MASTER KEY (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Imports a database master key from a backup file.  

> [!IMPORTANT]
> [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] introduces backup and restore support for the database master key to and from an Azure Blob storage. The `URL` syntax is only available for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later.
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
RESTORE MASTER KEY FROM 
  {
    FILE = 'path_to_file'
  | URL = 'Azure Blob storage URL'
  }  
    DECRYPTION BY PASSWORD = 'password'  
    ENCRYPTION BY PASSWORD = 'password'  
    [ FORCE ]  
```
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

 FILE ='*path_to_file*'  
 Specifies the complete path, including file name, to the stored database master key. *path_to_file* can be a local path or a UNC path to a network location.  

 URL **='***Azure Blob storage URL***'**   
 **Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later   
 Is the URL for your Azure Blob storage, in the format similar to `https://<storage_account_name>.blob.core.windows.net/<storage_container_name>/<backup_file_name>.bak`.
  
 DECRYPTION BY PASSWORD ='*password*'  
 Specifies the password that is required to decrypt the database master key that is being imported from a file.  
  
 ENCRYPTION BY PASSWORD ='*password*'  
 Specifies the password that is used to encrypt the database master key after it has been loaded into the database.  
  
 FORCE  
 Specifies that the RESTORE process should continue, even if the current database master key isn't open, or if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can't decrypt some of the private keys that are encrypted with it.  
  
## Remarks

 When the master key is restored, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] decrypts all the keys that are encrypted with the currently active master key, and then encrypts these keys with the restored master key. This resource-intensive operation should be scheduled during a period of low demand. If the current database master key isn't open or can't be opened, or if any of the keys that are encrypted by it can't be decrypted, the restore operation fails.  
  
 Use the FORCE option only if the master key is irretrievable or if decryption fails. Information that is encrypted only by an irretrievable key will be lost.  
  
 If the master key was encrypted by the service master key, the restored master key will also be encrypted by the service master key.  
  
 If there's no master key in the current database, RESTORE MASTER KEY creates a master key. The new master key won't be automatically encrypted with the service master key.  

 If you're using [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later, and want to restore the database master key from an Azure Blob storage, the following prerequisites apply:

 1. Have an [Azure storage account](/azure/storage/common/storage-account-create).
 1. [Create stored access policy and shared access storage](../../relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016.md#1---create-stored-access-policy-and-shared-access-storage).
 1. [Create a SQL Server credential using a shared access signature](../../relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016.md#2---create-a-sql-server-credential-using-a-shared-access-signature).

    For more information, see [Tutorial: Use Azure Blob Storage with SQL Server](../../relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016.md).
  
## Permissions

 Requires CONTROL permission on the database.  
  
## Examples

 The following example restores the database master key of the `AdventureWorks2012` database from a file.  
  
```sql  
USE AdventureWorks2012;  
RESTORE MASTER KEY   
    FROM FILE = 'c:\backups\keys\AdventureWorks2012_master_key'   
    DECRYPTION BY PASSWORD = '3dH85Hhk003#GHkf02597gheij04'   
    ENCRYPTION BY PASSWORD = '259087M#MyjkFkjhywiyedfgGDFD';  
GO  
```  
  
 The following example restores the database master key of the `AdventureWorks2012` database from an Azure Blob storage.  
  
```sql  
USE AdventureWorks2012;  
RESTORE MASTER KEY   
    FROM URL = 'https://mydocsteststorage.blob.core.windows.net/mytestcontainer/AdventureWorks2012_master_key.bak'   
    DECRYPTION BY PASSWORD = '3dH85Hhk003#GHkf02597gheij04'   
    ENCRYPTION BY PASSWORD = '259087M#MyjkFkjhywiyedfgGDFD';  
GO  
```  

## See also

 [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-master-key-transact-sql.md)   
 [ALTER MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-master-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
 [RESTORE SYMMETRIC KEY](restore-symmetric-key-transact-sql.md)