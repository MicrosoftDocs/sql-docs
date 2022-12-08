---
title: "ALTER SYMMETRIC KEY (Transact-SQL)"
description: ALTER SYMMETRIC KEY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER SYMMETRIC KEY"
  - "ALTER_SYMMETRIC_KEY_TSQL"
helpviewer_keywords:
  - "modifying symmetric keys"
  - "encryption [SQL Server], symmetric keys"
  - "symmetric keys [SQL Server], modifying"
  - "cryptography [SQL Server], symmetric keys"
  - "ALTER SYMMETRIC KEY statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017||=azure-sqldw-latest"
---
# ALTER SYMMETRIC KEY (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  Changes the properties of a symmetric key.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]
  
## Syntax  
  
```syntaxsql
ALTER SYMMETRIC KEY Key_name <alter_option>  
  
<alter_option> ::=  
   ADD ENCRYPTION BY <encrypting_mechanism> [ , ... n ]  
   |   
   DROP ENCRYPTION BY <encrypting_mechanism> [ , ... n ]  
<encrypting_mechanism> ::=  
   CERTIFICATE certificate_name  
   |  
   PASSWORD = 'password'  
   |  
   SYMMETRIC KEY Symmetric_Key_Name  
   |  
   ASYMMETRIC KEY Asym_Key_Name  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *Key_name*  
 Is the name by which the symmetric key to be changed is known in the database.  
  
 ADD ENCRYPTION BY  
 Adds encryption by using the specified method.  
  
 DROP ENCRYPTION BY  
 Drops encryption by the specified method. You cannot remove all the encryptions from a symmetric key.  
  
 CERTIFICATE *Certificate_name*  
 Specifies the certificate that is used to encrypt the symmetric key. This certificate must already exist in the database.  
  
 PASSWORD **='**_password_**'**  
 Specifies the password that is used to encrypt the symmetric key. *password* must meet the Windows password policy requirements of the computer that is running the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 SYMMETRIC KEY *Symmetric_Key_Name*  
 Specifies the symmetric key that is used to encrypt the symmetric key that is being changed. This symmetric key must already exist in the database and must be open.  
  
 ASYMMETRIC KEY *Asym_Key_Name*  
 Specifies the asymmetric key that is used to encrypt the symmetric key that is being changed. This asymmetric key must already exist in the database.  
  
## Remarks  
  
> [!CAUTION]  
>  When a symmetric key is encrypted with a password instead of with the public key of the database master key, the TRIPLE_DES encryption algorithm is used. Because of this, keys that are created with a strong encryption algorithm, such as AES, are themselves secured by a weaker algorithm.  
  
 To change the encryption of the symmetric key, use the ADD ENCRYPTION and DROP ENCRYPTION phrases. It is never possible for a key to be entirely without encryption. For this reason, the best practice is to add the new form of encryption before removing the old form of encryption.  
  
 To change the owner of a symmetric key, use [ALTER AUTHORIZATION](../../t-sql/statements/alter-authorization-transact-sql.md).  
  
> [!NOTE]  
>  The RC4 algorithm is only supported for backward compatibility. New material can only be encrypted using RC4 or RC4_128 when the database is in compatibility level 90 or 100. (Not recommended.) Use a newer algorithm such as one of the AES algorithms instead. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] material encrypted using RC4 or RC4_128 can be decrypted in any compatibility level.  
  
## Permissions  
 Requires ALTER permission on the symmetric key. If adding encryption by a certificate or asymmetric key, requires VIEW DEFINITION permission on the certificate or asymmetric key. If dropping encryption by a certificate or asymmetric key, requires CONTROL permission on the certificate or asymmetric key.  
  
## Examples  
 The following example changes the encryption method that is used to protect a symmetric key. The symmetric key `JanainaKey043` is encrypted using certificate `Shipping04` when the key was created. Because the key can never be stored unencrypted, in this example, encryption is added by password, and then encryption is removed by certificate.  
  
```sql  
CREATE SYMMETRIC KEY JanainaKey043 WITH ALGORITHM = AES_256   
    ENCRYPTION BY CERTIFICATE Shipping04;  
-- Open the key.   
OPEN SYMMETRIC KEY JanainaKey043 DECRYPTION BY CERTIFICATE Shipping04  
    WITH PASSWORD = '<enterStrongPasswordHere>';   
-- First, encrypt the key with a password.  
ALTER SYMMETRIC KEY JanainaKey043   
    ADD ENCRYPTION BY PASSWORD = '<enterStrongPasswordHere>';  
-- Now remove encryption by the certificate.  
ALTER SYMMETRIC KEY JanainaKey043   
    DROP ENCRYPTION BY CERTIFICATE Shipping04;  
CLOSE SYMMETRIC KEY JanainaKey043;  
```  
  
## See Also  
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)   
 [OPEN SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/open-symmetric-key-transact-sql.md)   
 [CLOSE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/close-symmetric-key-transact-sql.md)   
 [DROP SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-symmetric-key-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
