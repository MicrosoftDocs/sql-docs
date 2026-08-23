---
title: "ALTER CERTIFICATE (Transact-SQL)"
description: ALTER CERTIFICATE (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "01/30/2026"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "ALTER_CERTIFICATE_TSQL"
  - "ALTER CERTIFICATE"
helpviewer_keywords:
  - "ENCRYPTION BY PASSWORD option"
  - "encryption [SQL Server], certificates"
  - "modifying certificates"
  - "private keys [SQL Server]"
  - "ALTER CERTIFICATE statement"
  - "certificates [SQL Server], modifying"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# ALTER CERTIFICATE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-pdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-pdw-fabricsqldb.md)]

  Changes the password used to encrypt the private key of a certificate, removes the private key, or imports the private key if none is present. Changes the availability of a certificate to [!INCLUDE[ssSB](../../includes/sssb-md.md)].  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Syntax for SQL Server and Azure SQL Database  
  
ALTER CERTIFICATE certificate_name   
      REMOVE PRIVATE KEY  
    | WITH PRIVATE KEY ( <private_key_spec> )  
    | WITH ACTIVE FOR BEGIN_DIALOG = { ON | OFF }  
  
<private_key_spec> ::=   
      {   
        { FILE = 'path_to_private_key' | BINARY = private_key_bits }  
         [ , DECRYPTION BY PASSWORD = 'current_password' ]  
         [ , ENCRYPTION BY PASSWORD = 'new_password' ]  
      }  
    |  
      {  
         [ DECRYPTION BY PASSWORD = 'current_password' ]  
         [ [ , ] ENCRYPTION BY PASSWORD = 'new_password' ]  
      }  
```

```syntaxsql  
-- Syntax for Parallel Data Warehouse  
  
ALTER CERTIFICATE certificate_name   
{  
      REMOVE PRIVATE KEY  
    | WITH PRIVATE KEY (   
        FILE = '<path_to_private_key>',  
        DECRYPTION BY PASSWORD = '<key password>' )
}  
```

## Arguments

*certificate_name*  
 The unique name by which the certificate is known in the database.  
  
 REMOVE PRIVATE KEY  
 Specifies that the private key will no longer be maintained inside the database.  
  
 WITH PRIVATE KEY
 Specifies that the private key of the certificate is loaded into SQL Server.

FILE ='*path_to_private_key*'  
 Specifies the complete path, including file name, to the private key. This parameter can be a local path or a UNC path to a network location. The file is accessed within the security context of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account. When you use this option, make sure the service account has access to the specified file.

If you specify only a file name, the file is saved in the default user data folder for the instance. This folder might or might not be the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] DATA folder. For SQL Server Express LocalDB, the default user data folder for the instance is the path specified by the `%USERPROFILE%` environment variable for the account that created the instance.

BINARY ='*private_key_bits*'  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.  
  
 Private key bits specified as binary constant. These bits can be in encrypted form. If encrypted, you must provide a decryption password. SQL Server doesn't perform password policy checks on this password. The private key bits should be in a PVK file format.  
  
DECRYPTION BY PASSWORD ='*current_password*'  
 Specifies the password that is required to decrypt the private key.

ENCRYPTION BY PASSWORD ='*new_password*'  
 Specifies the password used to encrypt the private key of the certificate in the database. *new_password* must meet the Windows password policy requirements of the computer that is running the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Password Policy](../../relational-databases/security/password-policy.md).

ACTIVE FOR BEGIN_DIALOG **=** { ON | OFF }  
 Makes the certificate available to the initiator of a [!INCLUDE[ssSB](../../includes/sssb-md.md)] dialog conversation.

## Remarks

The private key must correspond to the public key specified by *certificate_name*.

You can omit the DECRYPTION BY PASSWORD clause if the password in the file is protected with a null password.

When you import the private key of a certificate that already exists in the database, the private key is automatically protected by the database master key. To protect the private key with a password, use the ENCRYPTION BY PASSWORD clause.

The REMOVE PRIVATE KEY option deletes the private key of the certificate from the database. You can remove the private key when you use the certificate to verify signatures or in [!INCLUDE[ssSB](../../includes/sssb-md.md)] scenarios that don't require a private key. Don't remove the private key of a certificate that protects a symmetric key. You need to restore the private key to sign any additional modules or strings that should be verified with the certificate, or to decrypt a value that was encrypted with the certificate.

You don't have to specify a decryption password when the private key is encrypted by using the database master key.

To change the password used for encrypting the private key, don't specify either the FILE or BINARY clauses.

> [!IMPORTANT]  
> Always make an archival copy of a private key before removing it from a database. For more information, see [BACKUP CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/backup-certificate-transact-sql.md) and [CERTPRIVATEKEY &#40;Transact-SQL&#41;](../../t-sql/functions/certprivatekey-transact-sql.md).

The WITH PRIVATE KEY option isn't available in a contained database.

## Limitations

Certificate names can't be changed after creation. ALTER CERTIFICATE doesn't support renaming certificates. If you need to use a different certificate name, you must create a new certificate and migrate dependencies.

### Workaround for TDE certificates

If you need to replace a Transparent Data Encryption (TDE) certificate with a different name:

1. **Back up the current certificate and private key**:

   ```sql
   BACKUP CERTIFICATE OldTDECert
   TO FILE = 'C:\Backup\OldTDECert.cer'
   WITH PRIVATE KEY (
       FILE = 'C:\Backup\OldTDECert.pvk',
       ENCRYPTION BY PASSWORD = '<password>'
   );
   ```

1. **Create the new certificate with the correct name**:

   ```sql
   CREATE CERTIFICATE NewTDECert
   WITH SUBJECT = 'TDE Certificate - Correct Name';
   ```

1. **For each TDE-encrypted database, change the encryption key**:

   ```sql
   USE EncryptedDB;
   GO
   
   ALTER DATABASE ENCRYPTION KEY
   ENCRYPTION BY SERVER CERTIFICATE NewTDECert;
   ```

1. **After all databases are migrated, drop the old certificate**:

   ```sql
   USE master;
   GO
   
   DROP CERTIFICATE OldTDECert;
   ```

> [!IMPORTANT]
> Always back up certificates and private keys before making TDE changes. Store backups in a secure location separate from the database server.

## Permissions

Requires ALTER permission on the certificate.  
  
## Examples  
  
### A. Removing the private key of a certificate  
  
```sql  
ALTER CERTIFICATE Shipping04   
    REMOVE PRIVATE KEY;  
GO  
```  
  
### B. Changing the password that is used to encrypt the private key  
  
```sql  
ALTER CERTIFICATE Shipping11   
    WITH PRIVATE KEY (DECRYPTION BY PASSWORD = '95hkjdskghFDGGG4%',  
    ENCRYPTION BY PASSWORD = '34958tosdgfkh##38');  
GO  
```  
  
### C. Importing a private key for a certificate that is already present in the database  
  
```sql  
ALTER CERTIFICATE Shipping13   
    WITH PRIVATE KEY (FILE = 'c:\importedkeys\Shipping13',  
    DECRYPTION BY PASSWORD = 'GDFLKl8^^GGG4000%');  
GO  
```  
  
### D. Changing the protection of the private key from a password to the database master key  
  
```sql  
ALTER CERTIFICATE Shipping15   
    WITH PRIVATE KEY (DECRYPTION BY PASSWORD = '95hk000eEnvjkjy#F%');  
GO  
```  

## Related content

- [CREATE CERTIFICATE (Transact-SQL)](create-certificate-transact-sql.md)
- [DROP CERTIFICATE (Transact-SQL)](drop-certificate-transact-sql.md)
- [BACKUP CERTIFICATE (Transact-SQL)](backup-certificate-transact-sql.md)
- [Encryption hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
- [CERTENCODED (Transact-SQL)](../functions/certencoded-transact-sql.md)
- [CERTPRIVATEKEY (Transact-SQL)](../functions/certprivatekey-transact-sql.md)
- [CERT_ID (Transact-SQL)](../functions/cert-id-transact-sql.md)
- [CERTPROPERTY (Transact-SQL)](../functions/certproperty-transact-sql.md)
