---
title: "Enable TDE on SQL Server Using EKM"
description: Enable transparent data encryption in SQL Server to protect a database key by using an asymmetric key in an extensible key management module with Transact-SQL.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 01/20/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: security
ms.topic: how-to
helpviewer_keywords:
  - "encryption [SQL Server], TDE using an EKM"
  - "TDE, EKM how to"
  - "EKM, TDE how to"
  - "Transparent Data Encryption, using EKM"
---
# Enable transparent data encryption on SQL Server Using EKM

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article describes how to enable transparent data encryption (TDE) in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] to protect a database encryption key by using an asymmetric key stored in an extensible key management (EKM) module with [!INCLUDE [tsql](../../../includes/tsql-md.md)].

TDE encrypts the storage of an entire database by using a symmetric key called the database encryption key. The database encryption key can also be protected using a certificate, which is protected by the database master key (DMK) of the `master` database. For more information about protecting the database encryption key by using the DMK, see [Transparent data encryption (TDE)](transparent-data-encryption.md). For information about configuring TDE when [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] is running on an Azure Virtual Machine, see [Extensible Key Management Using Azure Key Vault (SQL Server)](extensible-key-management-using-azure-key-vault-sql-server.md). For information about configuring TDE using a key in the Azure key vault, see [Use SQL Server Connector with SQL Encryption Features](use-sql-server-connector-with-sql-encryption-features.md).

## Limitations

You must be a high privileged user (such as a system administrator) to create a database encryption key and encrypt a database. The EKM module must be able to authenticate you.

At startup, the [!INCLUDE [ssDE](../../../includes/ssde-md.md)] must open the database. Create a credential that the EKM authenticates, and add it to a login based on an asymmetric key. Users can't sign in using that login, but the [!INCLUDE [ssDE](../../../includes/ssde-md.md)] can authenticate itself with the EKM device.

If you lose the asymmetric key stored in the EKM module, [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] can't open the database. If your EKM provider lets you back up the asymmetric key, create a backup and store it in a secure location.

The options and parameters required by your EKM provider might differ from what's provided in the following code example. For more information, see your EKM provider.

## Permissions

You need the following permissions:

- To change a configuration option and run the `RECONFIGURE` statement, you need the `ALTER SETTINGS` server-level permission. The **sysadmin** and **serveradmin** fixed server roles implicitly hold the `ALTER SETTINGS` permission.

- `ALTER ANY CREDENTIAL` permission.
- `ALTER ANY LOGIN` permission.
- `CREATE ASYMMETRIC KEY` permission.
- `CONTROL` permission on the database to encrypt the database.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

1. Copy the files supplied by the EKM provider to an appropriate location on the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] computer. In this example, we use the `C:\EKM_Files` folder.

1. Install certificates to the computer as required by your EKM provider.

   > [!NOTE]  
   > [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] doesn't supply an EKM provider. Each EKM provider can have different procedures for installing, configuring, and authorizing users. To complete this step, consult your EKM provider documentation.

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   ```sql
   -- Enable advanced options.
   EXECUTE sp_configure 'show advanced options', 1;
   GO

   RECONFIGURE;
   GO

   -- Enable EKM provider
   EXECUTE sp_configure 'EKM provider enabled', 1;
   GO

   RECONFIGURE;
   GO

   -- Create a cryptographic provider, which we have chosen to call "EKM_Prov," based on an EKM provider
   CREATE CRYPTOGRAPHIC PROVIDER EKM_Prov
       FROM FILE = 'C:\EKM_Files\KeyProvFile.dll';
   GO

   -- Create a credential that will be used by system administrators.
   CREATE CREDENTIAL sa_ekm_tde_cred
       WITH IDENTITY = 'Identity1',
       SECRET = '<password>' FOR CRYPTOGRAPHIC PROVIDER EKM_Prov;
   GO

   -- Add the credential to a high privileged user such as your
   -- own domain login in the format [DOMAIN\login].
   ALTER LOGIN [Contoso\Mary] ADD CREDENTIAL sa_ekm_tde_cred;
   GO

   -- create an asymmetric key stored inside the EKM provider
   USE master;
   GO

   CREATE ASYMMETRIC KEY ekm_login_key
        FROM PROVIDER [EKM_Prov]
            WITH ALGORITHM = RSA_2048,
            PROVIDER_KEY_NAME = 'SQL_Server_Key';
   GO

   -- Create a credential that will be used by the Database Engine.
   CREATE CREDENTIAL ekm_tde_cred
       WITH IDENTITY = 'Identity2', SECRET = '<secret>'
       FOR CRYPTOGRAPHIC PROVIDER EKM_Prov;

   -- Add a login used by TDE, and add the new credential to the login.
   CREATE LOGIN EKM_Login
       FROM ASYMMETRIC KEY ekm_login_key;
   GO

   ALTER LOGIN EKM_Login
       ADD CREDENTIAL ekm_tde_cred;
   GO

   -- Create the database encryption key that will be used for TDE.
   USE AdventureWorks2022;
   GO

   CREATE DATABASE ENCRYPTION KEY
       WITH ALGORITHM = AES_128
       ENCRYPTION BY SERVER ASYMMETRIC KEY ekm_login_key;
   GO

   -- Alter the database to enable transparent data encryption.
   ALTER DATABASE AdventureWorks2022
       SET ENCRYPTION ON;
   GO
   ```

## Related content

- [Transparent data encryption for SQL Database, SQL Managed Instance, and Azure Synapse Analytics](/azure/azure-sql/database/transparent-data-encryption-tde-overview)
- [sys.sp_configure (Transact-SQL)](../../system-stored-procedures/sp-configure-transact-sql.md)
- [CREATE CRYPTOGRAPHIC PROVIDER (Transact-SQL)](../../../t-sql/statements/create-cryptographic-provider-transact-sql.md)
- [CREATE CREDENTIAL (Transact-SQL)](../../../t-sql/statements/create-credential-transact-sql.md)
- [CREATE ASYMMETRIC KEY (Transact-SQL)](../../../t-sql/statements/create-asymmetric-key-transact-sql.md)
- [CREATE LOGIN (Transact-SQL)](../../../t-sql/statements/create-login-transact-sql.md)
- [CREATE DATABASE ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-database-encryption-key-transact-sql.md)
- [ALTER LOGIN (Transact-SQL)](../../../t-sql/statements/alter-login-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../../t-sql/statements/alter-database-transact-sql.md)
