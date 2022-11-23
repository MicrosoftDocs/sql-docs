---
title: "BACKUP SERVICE MASTER KEY (Transact-SQL)"
description: BACKUP SERVICE MASTER KEY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "BACKUP SERVICE MASTER KEY"
  - "DUMP_SERVICE_MASTER_KEY_TSQL"
  - "DUMP SERVICE MASTER KEY"
  - "BACKUP_SERVICE_MASTER_KEY_TSQL"
helpviewer_keywords:
  - "backing up service master keys [SQL Server]"
  - "BACKUP SERVICE MASTER KEY statement"
  - "cryptography [SQL Server], Service Master Key"
  - "exporting Service Master Keys"
  - "encryption [SQL Server], Service Master Key"
  - "service master key [SQL Server], exporting"
dev_langs:
  - "TSQL"
---
# BACKUP SERVICE MASTER KEY (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Exports the service master key.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
BACKUP SERVICE MASTER KEY TO FILE = 'path_to_file'   
    ENCRYPTION BY PASSWORD = 'password'  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

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
  
```sql  
BACKUP SERVICE MASTER KEY TO FILE = 'c:\temp_backups\keys\service_master_key' ENCRYPTION BY PASSWORD = '3dH85Hhk003GHk2597gheij4';  
```  
  
## See Also  
 [ALTER SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-service-master-key-transact-sql.md)   
 [RESTORE SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-service-master-key-transact-sql.md)  
  
  
