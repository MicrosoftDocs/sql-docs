---
title: "CREATE CERTIFICATE (Transact-SQL)"
description: CREATE CERTIFICATE (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
f1_keywords:
  - "CERTIFICATE"
  - "CREATE_CERTIFICATE_TSQL"
  - "sql13.swb.createcertificate.f1"
  - "CERTIFICATE_TSQL"
  - "CREATE CERTIFICATE"
  - "sql13.swb.certificateproperties.f1"
helpviewer_keywords:
  - "encryption [SQL Server], certificates"
  - "certificates [SQL Server], adding"
  - "certificates [SQL Server]"
  - "adding certificates"
  - "cryptography [SQL Server], certificates"
  - "CREATE CERTIFICATE statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017||=azure-sqldw-latest"
---
# CREATE CERTIFICATE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  Adds a certificate to a database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

 This feature is incompatible with database export using Data Tier Application Framework (DACFx). You must drop all certificates before exporting.  

> [!NOTE]
> In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], certificates with private keys can be backed up or restored directly to and from files or binary blobs using the public key pairs (PKCS) #12 or personal information exchange (PFX) format. All system-generated certificates have a minimum strength of RSA-3072 in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].
>
> The PKCS #12 or PFX format is a binary format for storing the server certificate, any intermediate certificates, and the private key in one file. PFX files usually have extensions such as `.pfx` and `.p12`. This makes it easier for customers to adhere to the current security best practice guidelines and compliance standards that prohibit RC4 encryption, by eliminating the need to use conversion tools such as PVKConverter (for the PVK or DER format).
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] 
  
## Syntax  
  
```syntaxsql
-- Syntax for SQL Server and Azure SQL Database  
  
CREATE CERTIFICATE certificate_name [ AUTHORIZATION user_name ]   
    { FROM <existing_keys> | <generate_new_keys> }  
    [ ACTIVE FOR BEGIN_DIALOG = { ON | OFF } ]  
  
<existing_keys> ::=   
    ASSEMBLY assembly_name  
    | {   
        [ EXECUTABLE ] FILE = 'path_to_file'  
        [ WITH [FORMAT = 'PFX',]
          PRIVATE KEY ( <private_key_options> ) ]   
      }  
    | {   
        BINARY = asn_encoded_certificate  
        [ WITH PRIVATE KEY ( <private_key_options> ) ]  
      }  
<generate_new_keys> ::=   
    [ ENCRYPTION BY PASSWORD = 'password' ]   
    WITH SUBJECT = 'certificate_subject_name'   
    [ , <date_options> [ ,...n ] ]   
  
<private_key_options> ::=  
      {   
        FILE = 'path_to_private_key'  
         [ , DECRYPTION BY PASSWORD = 'password' ]  
         [ , ENCRYPTION BY PASSWORD = 'password' ]    
      }  
    |  
      {   
        BINARY = private_key_bits  
         [ , DECRYPTION BY PASSWORD = 'password' ]  
         [ , ENCRYPTION BY PASSWORD = 'password' ]    
      }  
  
<date_options> ::=  
    START_DATE = 'datetime' | EXPIRY_DATE = 'datetime'  
```  
  
   
```syntaxsql
-- Syntax for Parallel Data Warehouse  
  
CREATE CERTIFICATE certificate_name   
    { <generate_new_keys> | FROM <existing_keys> }  
    [ ; ]  
  
<generate_new_keys> ::=   
    WITH SUBJECT = 'certificate_subject_name'   
    [ , <date_options> [ ,...n ] ]   
  
<existing_keys> ::=   
    {   
      FILE ='path_to_file'  
      WITH PRIVATE KEY   
         (   
           FILE = 'path_to_private_key'  
           , DECRYPTION BY PASSWORD ='password'   
         )  
    }  
  
<date_options> ::=  
    START_DATE ='datetime' | EXPIRY_DATE ='datetime'  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *certificate_name*  
 Is the name for the certificate in the database.  
  
 AUTHORIZATION *user_name*  
 Is the name of the user that owns this certificate.  
  
 ASSEMBLY *assembly_name*  
 Specifies a signed assembly that has already been loaded into the database.  
  
 [ EXECUTABLE ] FILE = '*path_to_file*'  
 Specifies the complete path, including file name, to a DER-encoded file that contains the certificate. If the EXECUTABLE option is used, the file is a DLL that has been signed by the certificate. *path_to_file* can be a local path or a UNC path to a network location. The file is accessed in the security context of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account. This account must have the required file-system permissions.  

> [!IMPORTANT]
> [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] does not support creating a certificate from a file or using private key files.
  
 BINARY = *asn_encoded_certificate*  
 ASN encoded certificate bytes specified as a binary constant.  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.  
  
 WITH FORMAT = *'PFX'*   
 **Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later   
 Specifies generating a certificate from a PFX file. This clause is optional.

 WITH PRIVATE KEY  
 Specifies that the private key of the certificate is loaded into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This clause is invalid when the certificate is being created from an assembly. To load the private key of a certificate created from an assembly, use [ALTER CERTIFICATE](../../t-sql/statements/alter-certificate-transact-sql.md).  
  
 FILE ='*path_to_private_key*'  
 Specifies the complete path, including file name, to the private key. *path_to_private_key* can be a local path or a UNC path to a network location. The file is accessed in the security context of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account. This account must have the necessary file-system permissions.  
  
> [!IMPORTANT]  
> This option is not available in a contained database or in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
 BINARY = *private_key_bits*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
 Private key bits specified as binary constant. These bits can be in encrypted form. If encrypted, the user must provide a decryption password. Password policy checks aren't performed on this password. The private key bits should be in a PVK file format.  
  
 DECRYPTION BY PASSWORD = '*key_password*'  
 Specifies the password required to decrypt a private key that is retrieved from a file. This clause is optional if the private key is protected by a null password. Saving a private key to a file without password protection isn't recommended. If a password is required but no password is specified, the statement fails.  
  
 ENCRYPTION BY PASSWORD = '*password*'  
 Specifies the password used to encrypt the private key. Use this option only if you want to encrypt the certificate with a password. If this clause is omitted, the private key is encrypted using the database master key. *password* must meet the Windows password policy requirements of the computer that is running the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Password Policy](../../relational-databases/security/password-policy.md).  
  
 SUBJECT = '*certificate_subject_name*'  
 The term *subject* refers to a field in the metadata of the certificate as defined in the X.509 standard. The subject should be no more than 64 characters long, and this limit is enforced for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Linux. For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Windows, the subject can be up to 128 characters long. Subjects that exceed 128 characters are truncated when they're stored in the catalog, but the binary large object (BLOB) that contains the certificate retains the full subject name.  
  
 START_DATE = '*datetime*'  
 Is the date on which the certificate becomes valid. If not specified, START_DATE is set equal to the current date. START_DATE is in UTC time and can be specified in any format that can be converted to a date and time.  
  
 EXPIRY_DATE = '*datetime*'  
 Is the date on which the certificate expires. If not specified, EXPIRY_DATE is set to a date one year after START_DATE. EXPIRY_DATE is in UTC time and can be specified in any format that can be converted to a date and time. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Service Broker checks the expiration date. Backup with Encryption using certificates also checks the expiration date and won't allow a new backup to be created with an expired certificate, but will allow restores with an expired certificate. However, expiration isn't enforced when the certificate is used for database encryption or Always Encrypted.  
  
 ACTIVE FOR BEGIN_DIALOG = { **ON** | OFF }  
 Makes the certificate available to the initiator of a [!INCLUDE[ssSB](../../includes/sssb-md.md)] dialog conversation. The default value is ON.  
  
## Remarks  
 A certificate is a database-level securable that follows the X.509 standard and supports X.509 V1 fields. `CREATE CERTIFICATE` can load a certificate from a file, a binary constant, or an assembly. This statement can also generate a key pair and create a self-signed certificate.  
  
 The Private Key must be \<= 2500 bytes in encrypted format. Private keys generated by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are 1024 bits long through [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and are 2048 bits long beginning with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]. Private keys imported from an external source have a minimum length of 384 bits and a maximum length of 4,096 bits. The length of an imported private key must be an integer multiple of 64 bits. Certificates used for TDE are limited to a private key size of 3456 bits.  
  
 The entire Serial Number of the certificate is stored but only the first 16 bytes appear in the sys.certificates catalog view.  
  
 The entire Issuer field of the certificate is stored but only the first 884 bytes in the sys.certificates catalog view.  
  
 The private key must correspond to the public key specified by *certificate_name*.  
  
 When you create a certificate from a container, loading the private key is optional. But when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a self-signed certificate, the private key is always created. By default, the private key is encrypted using the database master key. If the database master key doesn't exist and no password is specified, the statement fails.  
  
 The `ENCRYPTION BY PASSWORD` option isn't required when the private key is encrypted with the database master key. Use this option only when the private key is encrypted with a password. If no password is specified, the private key of the certificate will be encrypted using the database master key. If the master key of the database can't be opened, omitting this clause causes an error.  
  
 You don't have to specify a decryption password when the private key is encrypted with the database master key.  
  
> [!NOTE]  
> Built-in functions for encryption and signing do not check the expiration dates of certificates. Users of these functions must decide when to check certificate expiration.  
  
 A binary description of a certificate can be created by using the [CERTENCODED &#40;Transact-SQL&#41;](../../t-sql/functions/certencoded-transact-sql.md) and [CERTPRIVATEKEY &#40;Transact-SQL&#41;](../../t-sql/functions/certprivatekey-transact-sql.md) functions. For an example that uses **CERTPRIVATEKEY** and **CERTENCODED** to copy a certificate to another database, see example B in the article [CERTENCODED &#40;Transact-SQL&#41;](../../t-sql/functions/certencoded-transact-sql.md).  

The MD2, MD4, MD5, SHA, and SHA1 algorithms are deprecated in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]. Up to [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], a self-signed certificate is created using SHA1. Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], a self-signed certificate is created using SHA2_256.

## Permissions  
 Requires `CREATE CERTIFICATE` permission on the database. Only Windows logins, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins, and application roles can own certificates. Groups and roles can't own certificates.  
  
## Examples  
  
### A. Creating a self-signed certificate  
 The following example creates a certificate called `Shipping04`. The private key of this certificate is protected using a password.  
  
```sql  
CREATE CERTIFICATE Shipping04   
   ENCRYPTION BY PASSWORD = 'pGFD4bb925DGvbd2439587y'  
   WITH SUBJECT = 'Sammamish Shipping Records',   
   EXPIRY_DATE = '20201031';  
GO  
```  
  
### B. Creating a certificate from a file  
 The following example creates a certificate in the database, loading the key pair from files.  
  
```sql  
CREATE CERTIFICATE Shipping11   
    FROM FILE = 'c:\Shipping\Certs\Shipping11.cer'   
    WITH PRIVATE KEY (FILE = 'c:\Shipping\Certs\Shipping11.pvk',   
    DECRYPTION BY PASSWORD = 'sldkflk34et6gs%53#v00');  
GO   
```  

> [!IMPORTANT]
> [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] does not support creating a certificate from a file.
   
### C. Creating a certificate from a signed executable file  
  
```sql  
CREATE CERTIFICATE Shipping19   
    FROM EXECUTABLE FILE = 'c:\Shipping\Certs\Shipping19.dll';  
GO  
```  
  
 Alternatively, you can create an assembly from the `dll` file, and then create a certificate from the assembly.  
  
```sql  
CREATE ASSEMBLY Shipping19   
    FROM 'c:\Shipping\Certs\Shipping19.dll'   
    WITH PERMISSION_SET = SAFE;  
GO  
CREATE CERTIFICATE Shipping19 FROM ASSEMBLY Shipping19;  
GO  
``` 

> [!IMPORTANT]
> [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] does not support creating a certificate from a file.

> [!IMPORTANT]
> Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], the ['CLR strict security'](../../database-engine/configure-windows/clr-strict-security.md) server configuration option prevents loading assemblies without first setting up the security for them. Load the certificate, create a login from it, grant `UNSAFE ASSEMBLY` to that login, and then load the assembly.

### D. Creating a self-signed certificate  
 The following example creates a certificate called `Shipping04` without specifying an encryption password. This example can be used with [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].
  
```sql  
CREATE CERTIFICATE Shipping04   
   WITH SUBJECT = 'Sammamish Shipping Records';  
GO  
```

### E. Creating a certificate from a PFX file

```sql
CREATE CERTIFICATE Shipping04
    FROM FILE = 'c:\storedcerts\shipping04cert.pfx'
    WITH 
    FORMAT = 'PFX', 
	PRIVATE KEY (
        DECRYPTION BY PASSWORD = '9n34khUbhk$w4ecJH5gh'
	);  
```
  
## See Also  
 [ALTER CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-certificate-transact-sql.md)   
 [DROP CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-certificate-transact-sql.md)   
 [BACKUP CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/backup-certificate-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [CERTENCODED &#40;Transact-SQL&#41;](../../t-sql/functions/certencoded-transact-sql.md)   
 [CERTPRIVATEKEY &#40;Transact-SQL&#41;](../../t-sql/functions/certprivatekey-transact-sql.md)  
 [CERT_ID &#40;Transact-SQL&#41;](../../t-sql/functions/cert-id-transact-sql.md)  
 [CERTPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/certproperty-transact-sql.md)  
  
  
