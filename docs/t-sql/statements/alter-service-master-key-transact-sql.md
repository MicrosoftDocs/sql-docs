---
title: "ALTER SERVICE MASTER KEY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_SERVICE_MASTER_KEY_TSQL"
  - "ALTER SERVICE MASTER KEY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "REGENERATE phrase"
  - "FORCE option"
  - "ALTER SERVICE MASTER KEY statement"
  - "cryptography [SQL Server], Service Master Key"
  - "modifying Service Master Key"
  - "decryption [SQL Server], Service Master Key"
  - "encryption [SQL Server], Service Master Key"
  - "service master key [SQL Server], modifying"
ms.assetid: a1e9be0e-4115-47d8-9d3a-3316d876a35e
author: VanMSFT
ms.author: vanto
manager: craigg
---
# ALTER SERVICE MASTER KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the service master key of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ALTER SERVICE MASTER KEY   
    [ { <regenerate_option> | <recover_option> } ] [;]  
  
<regenerate_option> ::=  
    [ FORCE ] REGENERATE  
  
<recover_option> ::=  
    { WITH OLD_ACCOUNT = 'account_name' , OLD_PASSWORD = 'password' }  
    |      
    { WITH NEW_ACCOUNT = 'account_name' , NEW_PASSWORD = 'password' }  
```  
  
## Arguments  
 FORCE  
 Indicates that the service master key should be regenerated, even at the risk of data loss. For more information, see [Changing the SQL Server Service Account](#_changing) later in this topic.  
  
 REGENERATE  
 Indicates that the service master key should be regenerated.  
  
 OLD_ACCOUNT **='***account_name***'**  
 Specifies the name of the old Windows service account.  
  
> [!WARNING]  
>  This option is obsolete. Do not use. Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager instead.  
  
 OLD_PASSWORD **='***password***'**  
 Specifies the password of the old Windows service account.  
  
> [!WARNING]  
>  This option is obsolete. Do not use. Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager instead.  
  
 NEW_ACCOUNT **='***account_name***'**  
 Specifies the name of the new Windows service account.  
  
> [!WARNING]  
>  This option is obsolete. Do not use. Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager instead.  
  
 NEW_PASSWORD **='***password***'**  
 Specifies the password of the new Windows service account.  
  
> [!WARNING]  
>  This option is obsolete. Do not use. Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager instead.  
  
## Remarks  
 The service master key is automatically generated the first time it is needed to encrypt a linked server password, credential, or database master key. The service master key is encrypted using the local machine key or the Windows Data Protection API. This API uses a key that is derived from the Windows credentials of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account.  
  
 [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] uses the AES encryption algorithm to protect the service master key (SMK) and the database master key (DMK). AES is a newer encryption algorithm than 3DES used in earlier versions. After upgrading an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] the SMK and DMK should be regenerated in order to upgrade the master keys to AES. For more information about regenerating the DMK, see [ALTER MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-master-key-transact-sql.md).  
  
##  <a name="_changing"></a> Changing the SQL Server Service Account  
 To change the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account, use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. To manage a change of the service account, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stores a redundant copy of the service master key protected by the machine account that has the necessary permissions granted to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service group. If the computer is rebuilt, the same domain user that was previously used by the service account can recover the service master key. This does not work with local accounts or the Local System, Local Service, or Network Service accounts. When you are moving [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to another computer, migrate the service master key by using backup and restore.  
  
 The REGENERATE phrase regenerates the service master key. When the service master key is regenerated, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] decrypts all the keys that have been encrypted with it, and then encrypts them with the new service master key. This is a resource-intensive operation. You should schedule this operation during a period of low demand, unless the key has been compromised. If any one of the decryptions fail, the whole statement fails.  
  
 The FORCE option causes the key regeneration process to continue even if the process cannot retrieve the current master key, or cannot decrypt all the private keys that are encrypted with it. Use FORCE only if regeneration fails and you cannot restore the service master key by using the [RESTORE SERVICE MASTER KEY](../../t-sql/statements/restore-service-master-key-transact-sql.md) statement.  
  
> [!CAUTION]  
>  The service master key is the root of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] encryption hierarchy. The service master key directly or indirectly protects all other keys and secrets in the tree. If a dependent key cannot be decrypted during a forced regeneration, the data the key secures will be lost.  
  
 If you move SQL to another machine, then you have to use the same service account to decrypt the SMK - SQL Server will fix the Machine account encryption automatically.  
  
## Permissions  
 Requires CONTROL SERVER permission on the server.  
  
## Examples  
 The following example regenerates the service master key.  
  
```  
ALTER SERVICE MASTER KEY REGENERATE;  
GO  
```  
  
## See Also  
 [RESTORE SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-service-master-key-transact-sql.md)   
 [BACKUP SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/backup-service-master-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
