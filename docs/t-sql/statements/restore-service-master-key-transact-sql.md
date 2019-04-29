---
title: "RESTORE SERVICE MASTER KEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "RESTORE SERVICE MASTER KEY"
  - "RESTORE_SERVICE_MASTER_KEY_TSQL"
  - "LOAD SERVICE MASTER KEY"
  - "LOAD_SERVICE_MASTER_KEY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "importing Service Master Keys"
  - "copying Service Master Keys"
  - "service master key [SQL Server], importing"
  - "RESTORE SERVICE MASTER KEY statement"
  - "transferring Service Master Keys"
ms.assetid: a68fd0ee-70ce-4104-aca0-fcae5f41fc38
author: VanMSFT
ms.author: vanto
manager: craigg
---
# RESTORE SERVICE MASTER KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Imports a service master key from a backup file.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
RESTORE SERVICE MASTER KEY FROM FILE = 'path_to_file'   
    DECRYPTION BY PASSWORD = 'password' [FORCE]  
```  
  
## Arguments  
 FILE **='**_path\_to\_file_**'**  
 Specifies the complete path, including file name, to the stored service master key. *path_to_file* can be a local path or a UNC path to a network location.  
  
 PASSWORD **='**_password_**'**  
 Specifies the password required to decrypt the service master key that is being imported from a file.  
  
 FORCE  
 Forces the replacement of the service master key, even at the risk of data loss.  
  
## Remarks  
 When the service master key is restored, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] decrypts all the keys and secrets that have been encrypted with the current service master key, and then encrypts them with the service master key loaded from the backup file.  
  
 If any one of the decryptions fails, the restore will fail. You can use the FORCE option to ignore errors, but this option will cause the loss of any data that cannot be decrypted.  
  
> [!CAUTION]  
>  The service master key is the root of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] encryption hierarchy. The service master key directly or indirectly secures all other keys in the tree. If a dependent key cannot be decrypted during a forced restore, data that is secured by that key will be lost.  
  
 Regenerating the encryption hierarchy is a resource-intensive operation. You should schedule this during a period of low demand.  
  
## Permissions  
 Requires CONTROL SERVER permission on the server.  
  
## Examples  
 The following example restores the service master key from a backup file.  
  
```  
RESTORE SERVICE MASTER KEY   
    FROM FILE = 'c:\temp_backups\keys\service_master_key'   
    DECRYPTION BY PASSWORD = '3dH85Hhk003GHk2597gheij4';  
GO  
```  
  
## See Also  
 [Service Master Key](../../relational-databases/security/encryption/service-master-key.md)   
 [ALTER SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-service-master-key-transact-sql.md)   
 [BACKUP SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/backup-service-master-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)