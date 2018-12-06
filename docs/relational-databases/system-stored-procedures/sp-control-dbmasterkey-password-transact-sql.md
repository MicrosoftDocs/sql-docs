---
title: "sp_control_dbmasterkey_password (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/25/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_control_dbmasterkey_password"
  - "sp_control_dbmasterkey_password_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_control_dbmasterkey_password"
ms.assetid: 63979a87-42a2-446e-8e43-30481faaf3ca
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sp_control_dbmasterkey_password (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds or drops a credential containing the password needed to open a database master key.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_control_dbmasterkey_password @db_name = 'database_name,  
     @password = 'master_key_password' , @action = { 'add' | 'drop' }  
```  
  
## Arguments  
 @db_name=N'*database_name*'  
 Specifies the name of the database associated with this credential. Cannot be a system database. *database_name* is **nvarchar**.  
  
 @password=N'*password*'  
 Specifies the password of the master key. *password* is **nvarchar**.  
  
 @action=N'add'  
 Specifies that a credential for the specified database will be added to the credential store. The credential will contain the password of the database master key. The value passed to @action is **nvarchar**.  
  
 @action=N'drop'  
 Specifies that a credential for the specified database will be dropped from the credential store. The value passed to @action is **nvarchar**.  
  
## Remarks  
 When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] needs a database master key to decrypt or encrypt a key, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tries to decrypt the database master key with the service master key of the instance. If the decryption fails, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] searches the credential store for master key credentials that have the same family GUID as the database for which it needs the master key. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] then tries to decrypt the database master key with each matching credential until the decryption succeeds or there are no more credentials.  
  
> [!CAUTION]  
>  Do not create a master key credential for a database that must be inaccessible to sa and other highly-privileged server principals. You can configure a database so that its key hierarchy cannot be decrypted by the service master key. This option is supported as a defense-in-depth for databases that contain encrypted information that should not be accessible to sa or other highly privileged server principals. Creating a master key credential for such a database removes this defense-in-depth, enabling sa and other highly privileged server principals to decrypt the database.  
  
 Credentials that are created by using sp_control_dbmasterkey_password are visible in the [sys.master_key_passwords](../../relational-databases/system-catalog-views/sys-master-key-passwords-transact-sql.md) catalog view. The names of credentials that are created for database master keys have the following format: `##DBMKEY_<database_family_guid>_<random_password_guid>##`. The password is stored as the credential secret. For each password added to the credential store there is a row in sys.credentials.  
  
 You cannot use sp_control_dbmasterkey_password to create a credential for the following system databases: master, model, msdb, or tempdb.  
  
 sp_control_dbmasterkey_password does not verify that the password can open the master key of the specified database.  
  
 If you specify a password that is already stored in a credential for the specified database, sp_control_dbmasterkey_password will fail.  
  
> [!NOTE]  
>  Two databases from different server instances can share the same family GUID. If this occurs, the databases will share the same master key records in the credential store.  
  
 Parameters passed to sp_control_dbmasterkey_password do not appear in traces.  
  
> [!NOTE]  
>  When you are using the credential that was added by using sp_control_dbmasterkey_password to open the database master key, the database master key is re-encrypted by the service master key. If the database is in read-only mode, the re-encryption operation will fail, and the database master key will remain unencrypted. For subsequent access to the database master key, you must use the OPEN MASTER KEY statement and a password. To avoid using a password, create the credential before you move the database to read-only mode.  
  
 **Potential Backward Compatibility Issue:** Currently, the stored procedure does not check whether a master key exists. This is permitted for backward compatibility, but displays a warning. This behavior is deprecated. In a future release the master key must exist and the password used in the stored procedure **sp_control_dbmasterkey_password** must be the same password as one of the passwords used to encrypt the database master key.  
  
## Permissions  
 Requires CONTROL permission on the database.  
  
## Examples  
  
### A. Creating a credential for the AdventureWorks2012 master key  
 The following example creates a credential for the `AdventureWorks2012` database master key, and saves the master key password as the secret in the credential. Because all parameters that are passed to `sp_control_dbmasterkey_password` must be of data type **nvarchar**, the text strings are converted with the casting operator `N`.  
  
```  
EXEC sp_control_dbmasterkey_password @db_name = N'AdventureWorks2012',   
    @password = N'sdfjlkj#mM00sdfdsf98093258jJlfdk4', @action = N'add';  
GO  
```  
  
### B. Dropping a credential for a database master key  
 The following example removes the credential created in example A. Note that all parameters are required, including the password.  
  
```  
EXEC sp_control_dbmasterkey_password @db_name = N'AdventureWorks2012',   
    @password = N'sdfjlkj#mM00sdfdsf98093258jJlfdk4', @action = N'drop';  
GO  
```  
  
## See Also  
 [Set Up an Encrypted Mirror Database](../../database-engine/database-mirroring/set-up-an-encrypted-mirror-database.md)   
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [sys.credentials &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)   
 [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md)  
  
  
