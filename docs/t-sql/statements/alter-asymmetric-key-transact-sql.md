---
title: "ALTER ASYMMETRIC KEY (Transact-SQL) | Microsoft Docs"
ms.date: "04/12/2017"
ms.prod: sql
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_ASYMMETRIC_KEY_TSQL"
  - "ALTER ASYMMETRIC KEY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ENCRYPTION BY PASSWORD option"
  - "passwords [SQL Server], asymmetric keys"
  - "encryption [SQL Server], asymmetric keys"
  - "DECRYPTION BY PASSWORD option"
  - "ALTER ASYMMETRIC KEY statement"
  - "asymmetric keys [SQL Server], modifying"
ms.assetid: 958e95d6-fbe6-43e8-abbd-ccedbac2dbac
author: VanMSFT
ms.author: vanto
manager: craigg
---
# ALTER ASYMMETRIC KEY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Changes the properties of an asymmetric key.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
ALTER ASYMMETRIC KEY Asym_Key_Name <alter_option>  
  
<alter_option> ::=  
      <password_change_option>   
    | REMOVE PRIVATE KEY   

<password_change_option> ::=  
    WITH PRIVATE KEY ( <password_option> [ , <password_option> ] )  

<password_option> ::=  
      ENCRYPTION BY PASSWORD = 'strongPassword'  
    | DECRYPTION BY PASSWORD = 'oldPassword'  
```  
  
## Arguments  
 *Asym_Key_Name*  
 Is the name by which the asymmetric key is known in the database.  
  
 REMOVE PRIVATE KEY  
 Removes the private key from the asymmetric key The public key is not removed.  
  
 WITH PRIVATE KEY  
 Changes the protection of the private key.  
  
 ENCRYPTION BY PASSWORD **='***strongPassword***'**  
 Specifies a new password for protecting the private key. *password* must meet the Windows password policy requirements of the computer that is running the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If this option is omitted, the private key will be encrypted by the database master key.  
  
 DECRYPTION BY PASSWORD **='***oldPassword***'**  
 Specifies the old password, with which the private key is currently protected. Is not required if the private key is encrypted with the database master key.  
  
## Remarks  
 If there is no database master key the ENCRYPTION BY PASSWORD option is required, and the operation will fail if no password is supplied. For information about how to create a database master key, see [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-master-key-transact-sql.md).  
  
 You can use ALTER ASYMMETRIC KEY to change the protection of the private key by specifying PRIVATE KEY options as shown in the following table.  
  
|Change protection from|ENCRYPTION BY PASSWORD|DECRYPTION BY PASSWORD|  
|----------------------------|----------------------------|----------------------------|  
|Old password to new password|Required|Required|  
|Password to master key|Omit|Required|  
|Master key to password|Required|Omit|  
  
 The database master key must be opened before it can be used to protect a private key. For more information, see [OPEN MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/open-master-key-transact-sql.md).  
  
 To change the ownership of an asymmetric key, use [ALTER AUTHORIZATION](../../t-sql/statements/alter-authorization-transact-sql.md).  
  
## Permissions  
 Requires CONTROL permission on the asymmetric key if the private key is being removed.  
  
## Examples  
  
### A. Changing the password of the private key  
 The following example changes the password used to protect the private key of asymmetric key `PacificSales09`. The new password will be `<enterStrongPasswordHere>`.  
  
```  
ALTER ASYMMETRIC KEY PacificSales09   
    WITH PRIVATE KEY (  
    DECRYPTION BY PASSWORD = '<oldPassword>',  
    ENCRYPTION BY PASSWORD = '<enterStrongPasswordHere>');  
GO  
```  
  
### B. Removing the private key from an asymmetric key  
 The following example removes the private key from `PacificSales19`, leaving only the public key.  
  
```  
ALTER ASYMMETRIC KEY PacificSales19 REMOVE PRIVATE KEY;  
GO  
```  
  
### C. Removing password protection from a private key  
 The following example removes the password protection from a private key and protects it with the database master key.  
  
```  
OPEN MASTER KEY;  
ALTER ASYMMETRIC KEY PacificSales09 WITH PRIVATE KEY (  
    DECRYPTION BY PASSWORD = '<enterStrongPasswordHere>' );  
GO  
```  
  
## See Also  
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)   
 [DROP ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-asymmetric-key-transact-sql.md)   
 [SQL Server and Database Encryption Keys &#40;Database Engine&#41;](../../relational-databases/security/encryption/sql-server-and-database-encryption-keys-database-engine.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-master-key-transact-sql.md)   
 [OPEN MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/open-master-key-transact-sql.md)   
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)  
  
  
