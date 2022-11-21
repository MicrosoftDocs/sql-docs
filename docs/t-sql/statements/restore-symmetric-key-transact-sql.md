---
title: "RESTORE SYMMETRIC KEY (Transact-SQL)"
description: RESTORE SYMMETRIC KEY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
---

# RESTORE SYMMETRIC KEY (Transact-SQL)

[!INCLUDE [SQL Server 2022](../../includes/applies-to-version/sqlserver2022.md)]

> [!NOTE]
> [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] introduces support for exporting and importing symmetric keys, either to or from Azure Blob storage or file.

Imports the symmetric key.

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md) 

## Syntax  
  
```syntaxsql
RESTORE SYMMETRIC KEY key_name FROM 
  {
    FILE = 'path_to_file'
  | URL = 'Azure Blob storage URL'
  }
      DECRYPTION BY PASSWORD = 'password'
      ENCRYPTION BY PASSWORD = 'password' 
```

## Arguments

 FILE **='***path_to_file***'**  
 Specifies the complete path, including file name, to the file to which the symmetric key will be exported. The path may be a local path or a UNC path to a network location.  
  
 URL **='***Azure Blob storage URL***'**
 Is the URL for your Azure Blob storage, in the format similar to `https://<storage_account_name>.blob.core.windows.net/<storage_container_name>/<backup_file_name>.bak`.

 DECRYPTION BY PASSWORD **='***password***'** 
 Specifies the password that is required to decrypt the symmetric key that is being imported from a file.  
  
 ENCRYPTION BY PASSWORD **='***password***'**  
 Specifies the password that is used to encrypt the symmetric key after it has been loaded into the database. This password is subject to complexity checks. For more information, see [Password Policy](../../relational-databases/security/password-policy.md).  
  
## Prerequisite

In order to restore the symmetric key from an Azure Blob storage, you need to:

1. Have an [Azure storage account](/azure/storage/common/storage-account-create) with the symmetric key backup.
1. [Create stored access policy and shared access storage](../../relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016.md#1---create-stored-access-policy-and-shared-access-storage).
1. [Create a SQL Server credential using a shared access signature](../../relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016.md#2---create-a-sql-server-credential-using-a-shared-access-signature).

For more information, see [Tutorial: Use Azure Blob Storage with SQL Server](../../relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016.md).

## Permissions

Requires ALTER permission on the symmetric key. If adding encryption by a certificate or asymmetric key, requires VIEW DEFINITION permission on the certificate or asymmetric key. If dropping encryption by a certificate or asymmetric key, requires CONTROL permission on the certificate or asymmetric key.
  
## Examples

In the following example, the symmetric key is restored from a file.

```sql  
RESTORE SYMMETRIC KEY symmetric_key
   FROM FILE = 'c:\temp_backups\keys\symmetric_key' 
   DECRYPTION BY PASSWORD = '3dH85Hhk003#GHkf02597gheij04'   
   ENCRYPTION BY PASSWORD = '259087M#MyjkFkjhywiyedfgGDFD'; 
```

In the following example, the symmetric key is restored from an Azure Blob storage.

```sql
RESTORE SYMMETRIC KEY symmetric_key 
   FROM URL = 'https://mydocsteststorage.blob.core.windows.net/mytestcontainer/symmetric_key.bak'
   DECRYPTION BY PASSWORD = '3dH85Hhk003#GHkf02597gheij04'   
   ENCRYPTION BY PASSWORD = '259087M#MyjkFkjhywiyedfgGDFD'; 
```

## See also

[BACKUP SYMMETRIC KEY](backup-symmetric-key-transact-sql.md)