---
title: "Create identical symmetric keys on two servers"
description: Learn how to create identical symmetric keys on two servers in SQL Server by using Transact-SQL. This supports encryption in separate databases or servers.
ms.custom: ""
ms.date: "12/16/2021"
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "symmetric keys [SQL Server], creating"
ms.assetid: a13d0b21-a43b-43c0-9c22-7ba8f3d15e80
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create identical symmetric keys on two servers
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  This topic describes how to create identical symmetric keys on two different servers in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[tsql](../../../includes/tsql-md.md)]. In order to decrypt ciphertext, you need the key that was used to encrypt it. When both encryption and decryption occur in a single database, the key is stored in the database and it's available, depending on permissions, for both encryption and decryption. But when encryption and decryption occur in separate databases or on separate servers, the key stored in one database isn't available for use on the second database.
  
## Before you begin  
  
### Limitations and restrictions  
  
- When a symmetric key is created, the symmetric key must be encrypted by using at least one of the following: certificate, password, symmetric key, asymmetric key, or PROVIDER. The key can have more than one encryption of each type. In other words, a single symmetric key can be encrypted by using multiple certificates, passwords, symmetric keys, and asymmetric keys at the same time.  
  
- When a symmetric key is encrypted with a password instead of the public key of the database master key, the TRIPLE DES encryption algorithm is used. Because of this, keys that are created with a strong encryption algorithm, such as AES, are themselves secured by a weaker algorithm.  
  
## Security  
  
### Permissions  
 Requires ALTER ANY SYMMETRIC KEY permission on the database. If AUTHORIZATION is specified, requires IMPERSONATE permission on the database user or ALTER permission on the application role. If encryption is by certificate or asymmetric key, requires VIEW DEFINITION permission on the certificate or asymmetric key. Only Windows logins, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] logins, and application roles can own symmetric keys. Groups and roles can't own symmetric keys.  
  
## Using Transact-SQL  
  
### To create identical symmetric keys on two different servers  
  
1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2. On the Standard bar, click **New Query**.  
  
3. Create a key by running the following CREATE MASTER KEY, CREATE CERTIFICATE, and CREATE SYMMETRIC KEY statements.  
  
    ```sql
    CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'My p@55w0Rd';  
    GO  
    CREATE CERTIFICATE [cert_keyProtection] WITH SUBJECT = 'Key Protection';  
    GO  
    CREATE SYMMETRIC KEY [key_DataShare] WITH  
        KEY_SOURCE = 'My key generation bits. This is a shared secret!',  
        ALGORITHM = AES_256,   
        IDENTITY_VALUE = 'Key Identity generation bits. Also a shared secret'  
        ENCRYPTION BY CERTIFICATE [cert_keyProtection];  
    GO  
    ```  
  
4. Connect to a separate server instance, open a different Query Window, and run the SQL statements above to create the same key on the second server.  
  
5. Test the keys by first running the OPEN SYMMETRIC KEY statement and the SELECT statement below on the first server.  
  
    ```sql
    OPEN SYMMETRIC KEY [key_DataShare]   
        DECRYPTION BY CERTIFICATE cert_keyProtection;  
    GO  
    SELECT encryptbykey(key_guid('key_DataShare'), 'MyData' )  
    GO  
    -- For example, the output might look like this: 0x2152F8DA8A500A9EDC2FAE26D15C302DA70D25563DAE7D5D1102E3056CE9EF95CA3E7289F7F4D0523ED0376B155FE9C3  
    ```  
  
6. On the second server, paste the result of the previous SELECT statement into the following code as the value of `@blob` and run the following code to verify that the duplicate key can decrypt the ciphertext.  
  
    ```sql
    OPEN SYMMETRIC KEY [key_DataShare]   
        DECRYPTION BY CERTIFICATE cert_keyProtection;  
    GO  
    DECLARE @blob varbinary(8000);  
    SET @blob = SELECT CONVERT(varchar(8000), decryptbykey(@blob));  
    GO  
    ```  
  
7. Close the symmetric key on both servers.  
  
    ```sql
    CLOSE SYMMETRIC KEY [key_DataShare];  
    GO  
    ```  

### Encryption changes in SQL Server 2017 CU2

SQL Server 2016 uses the SHA1 hashing algorithm for its encryption work. Starting in SQL Server 2017, SHA2 is used instead. This means extra steps might be necessary to have your SQL Server 2017 installation decrypt items that were encrypted by SQL Server 2016. Here are the extra steps:

- Ensure your SQL Server 2017 is updated to at least Cumulative Update 2 (CU2).
  - See [Cumulative Update 2 (CU2) for SQL Server 2017](https://support.microsoft.com/help/4052574) for important details.
- After you install CU2, turn on trace flag 4631 in SQL Server 2017: `DBCC TRACEON(4631, -1);`
  - Trace flag 4631 is new in SQL Server 2017. Trace flag 4631 needs to be `ON` globally before you create the master key, certificate, or symmetrical key in SQL Server 2017. This enables these created items to interoperate with SQL Server 2016 and earlier versions. This trace flag should only be turned on temporarily to do the re-encryption of data with SHA2-derived keys.

For more information, see:

- [FIX: SQL Server 2017 can't decrypt data encrypted by earlier versions of SQL Server by using the same symmetric key](https://support.microsoft.com/help/4053407/sql-server-2017-cannot-decrypt-data-encrypted-by-earlier-versions)

## See also

-   [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-master-key-transact-sql.md)  
  
-   [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../../t-sql/statements/create-certificate-transact-sql.md)  
  
-   [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-symmetric-key-transact-sql.md)  
  
-   [ENCRYPTBYKEY &#40;Transact-SQL&#41;](../../../t-sql/functions/encryptbykey-transact-sql.md)  
  
-   [DECRYPTBYKEY &#40;Transact-SQL&#41;](../../../t-sql/functions/decryptbykey-transact-sql.md)  
  
-   [OPEN SYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/open-symmetric-key-transact-sql.md)  
