---
title: "DECRYPTBYKEYAUTOCERT (Transact-SQL)"
description: DECRYPTBYKEYAUTOCERT decrypts data with a symmetric key.
author: VanMSFT
ms.author: vanto
ms.reviewer: vanto
ms.date: 10/20/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DECRYPTBYKEYAUTOCERT"
  - "DECRYPTBYKEYAUTOCERT_TSQL"
helpviewer_keywords:
  - "DECRYPTBYKEYAUTOCERT function"
dev_langs:
  - TSQL
---
# DECRYPTBYKEYAUTOCERT (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This function decrypts data with a symmetric key. That symmetric key automatically decrypts with a certificate.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DECRYPTBYKEYAUTOCERT ( cert_ID , cert_password
    , { 'ciphertext' | @ciphertext }
  [ , { add_authenticator | @add_authenticator }
  [ , { authenticator | @authenticator } ] ] )
```

## Arguments

#### *cert_ID*

The ID of the certificate used to protect the symmetric key. *cert_ID* has an **int** data type.

#### *cert_password*

The password used to encrypt the private key of the certificate. Can have a `NULL` value if the database master key (DMK) protects the private key. *cert_password* has an **nvarchar** data type.

#### '*ciphertext*'

The string of data encrypted with the key. *ciphertext* has a **varbinary** data type.

#### @ciphertext

A variable of type **varbinary** containing data encrypted with the key.

#### *add_authenticator*

Indicates whether the original encryption process included, and encrypted, an authenticator together with the plaintext. Must match the value passed to [ENCRYPTBYKEY](encryptbykey-transact-sql.md) during the data encryption process. *add_authenticator* has a value of 1 if the encryption process used an authenticator. *add_authenticator* has an **int** data type.

#### @add_authenticator

A variable indicating whether the original encryption process included, and encrypted, an authenticator together with the plaintext. Must match the value passed to [ENCRYPTBYKEY](encryptbykey-transact-sql.md) during the data encryption process. *\@add_authenticator* has an **int** data type.

#### *authenticator*

The data used as the basis for the generation of the authenticator. Must match the value supplied to [ENCRYPTBYKEY](encryptbykey-transact-sql.md). *authenticator* has a **sysname** data type.

#### @authenticator

A variable containing data from which an authenticator generates. Must match the value supplied to [ENCRYPTBYKEY](encryptbykey-transact-sql.md). *\@authenticator* has a **sysname** data type.

## Return types

**varbinary**, with a maximum size of 8,000 bytes.

## Remarks

`DECRYPTBYKEYAUTOCERT` combines the functionality of `OPEN SYMMETRIC KEY` and `DECRYPTBYKEY`. In a single operation, it first decrypts a symmetric key, and then decrypts encrypted ciphertext with that key.

## Permissions

Requires `VIEW DEFINITION` permission on the symmetric key, and `CONTROL` permission on the certificate.

## Examples

This example shows how `DECRYPTBYKEYAUTOCERT` can simplify decryption code. This code should run on an [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database that doesn't already have a DMK. Replace `<password>` with a strong password.

```sql
--Create the keys and certificate.
USE AdventureWorks2022;

CREATE MASTER KEY ENCRYPTION BY PASSWORD= '<password>';

OPEN MASTER KEY DECRYPTION BY PASSWORD = '<password>';

CREATE CERTIFICATE HumanResources037
    WITH SUBJECT = 'Sammamish HR', EXPIRY_DATE = '10/31/2035';

CREATE SYMMETRIC KEY SSN_Key_01
    WITH ALGORITHM = AES_256
    ENCRYPTION BY CERTIFICATE HumanResources037;
GO

----Add a column of encrypted data.
ALTER TABLE HumanResources.Employee
    ADD EncryptedNationalIDNumber VARBINARY (128);

OPEN SYMMETRIC KEY SSN_Key_01 DECRYPTION BY CERTIFICATE HumanResources037;

UPDATE HumanResources.Employee
    SET EncryptedNationalIDNumber = EncryptByKey(Key_GUID('SSN_Key_01'), NationalIDNumber);
GO

--
--Close the key used to encrypt the data.
CLOSE SYMMETRIC KEY SSN_Key_01;
--
--There are two ways to decrypt the stored data.
--
--OPTION ONE, using DecryptByKey()
--1. Open the symmetric key
--2. Decrypt the data
--3. Close the symmetric key

OPEN SYMMETRIC KEY SSN_Key_01 DECRYPTION BY CERTIFICATE HumanResources037;

SELECT NationalIDNumber,
       EncryptedNationalIDNumber AS 'Encrypted ID Number',
       CONVERT (NVARCHAR, DecryptByKey(EncryptedNationalIDNumber)) AS 'Decrypted ID Number'
FROM HumanResources.Employee;

CLOSE SYMMETRIC KEY SSN_Key_01;
--
--OPTION TWO, using DECRYPTBYKEYAUTOCERT()

SELECT NationalIDNumber,
       EncryptedNationalIDNumber AS 'Encrypted ID Number',
       CONVERT (NVARCHAR, DECRYPTBYKEYAUTOCERT(cert_ID('HumanResources037'), NULL, EncryptedNationalIDNumber)) AS 'Decrypted ID Number'
FROM HumanResources.Employee;
```

## Related content

- [OPEN SYMMETRIC KEY (Transact-SQL)](../statements/open-symmetric-key-transact-sql.md)
- [ENCRYPTBYKEY (Transact-SQL)](encryptbykey-transact-sql.md)
- [DECRYPTBYKEY (Transact-SQL)](decryptbykey-transact-sql.md)
- [Encryption hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
