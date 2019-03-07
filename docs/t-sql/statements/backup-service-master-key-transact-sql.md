---
title: "BACKUP SERVICE MASTER KEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "BACKUP SERVICE MASTER KEY"
  - "DUMP_SERVICE_MASTER_KEY_TSQL"
  - "DUMP SERVICE MASTER KEY"
  - "BACKUP_SERVICE_MASTER_KEY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "backing up service master keys [SQL Server]"
  - "BACKUP SERVICE MASTER KEY statement"
  - "cryptography [SQL Server], Service Master Key"
  - "exporting Service Master Keys"
  - "encryption [SQL Server], Service Master Key"
  - "service master key [SQL Server], exporting"
ms.assetid: f8356683-6680-4f1c-9eaf-5c29a9a9020d
author: VanMSFT
ms.author: vanto
manager: craigg
---
# BACKUP SERVICE MASTER KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Exports the service master key.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
BACKUP SERVICE MASTER KEY TO FILE = 'path_to_file'   
    ENCRYPTION BY PASSWORD = 'password'  
```  
  
## Arguments  
 FILE **='***path_to_file***'**  
 Specifies the complete path, including file name, to the file to which the service master key will be exported. This may be a local path or a UNC path to a network location.  
  
 PASSWORD **='***password***'**  
 Is the password used to encrypt the service master key in the backup file. This password is subject to complexity checks. For more information, see [Password Policy](../../relational-databases/security/password-policy.md).  
  
## Remarks  
 The service master key should be backed up and stored in a secure, off-site location. Creating this backup should be one of the first administrative actions performed on the server.  
  
## Permissions  
 Requires CONTROL SERVER permission on the server.  
  
## Examples  
 In the following example, the service master key is backed up to a file.  
  
```  
BACKUP SERVICE MASTER KEY TO FILE = 'c:\temp_backups\keys\service_master_key' ENCRYPTION BY PASSWORD = '3dH85Hhk003GHk2597gheij4';  
```  
  
## See Also  
 [ALTER SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-service-master-key-transact-sql.md)   
 [RESTORE SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-service-master-key-transact-sql.md)  
  
  
