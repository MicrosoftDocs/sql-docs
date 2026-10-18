---
title: "DECRYPTBYKEYAUTOASYMKEY (Transact-SQL)"
description: DECRYPTBYKEYAUTOASYMKEY decrypts encrypted data.
author: VanMSFT
ms.author: vanto
ms.reviewer: vanto
ms.date: 10/20/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DECRYPTBYKEYAUTOASYMKEY_TSQL"
  - "DECRYPTBYKEYAUTOASYMKEY"
helpviewer_keywords:
  - "DECRYPTBYKEYAUTOASYMKEY function"
dev_langs:
  - TSQL
---
# DECRYPTBYKEYAUTOASYMKEY (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This function decrypts encrypted data. It first decrypts a symmetric key with a separate asymmetric key, and then decrypts the encrypted data with the symmetric key extracted in the first step.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DECRYPTBYKEYAUTOASYMKEY ( akey_ID , akey_password
    , { 'ciphertext' | @ciphertext }
  [ , { add_authenticator | @add_authenticator }
  [ , { authenticator | @authenticator } ] ] )
```

## Arguments

#### *akey_ID*

The ID of the asymmetric key used to encrypt the symmetric key. *akey_ID* has an **int** data type.

#### *akey_password*

The password protecting the asymmetric key. *akey_password* can have a `NULL` value if the database master key (DMK) protects the asymmetric private key. *akey_password* has an **nvarchar** data type.

#### *ciphertext*

The data encrypted with the key. *ciphertext* has a **varbinary** data type.

#### @ciphertext

A variable of type **varbinary** containing data encrypted with the symmetric key.

#### *add_authenticator*

Indicates whether the original encryption process included, and encrypted, an authenticator together with the plaintext. Must match the value passed to [ENCRYPTBYKEY](encryptbykey-transact-sql.md) during the data encryption process. *add_authenticator* has a value of 1 if the encryption process used an authenticator. *add_authenticator* has an **int** data type.

#### @add_authenticator

A variable indicating whether the original encryption process included, and encrypted, an authenticator together with the plaintext. Must match the value passed to [ENCRYPTBYKEY](encryptbykey-transact-sql.md) during the data encryption process. *@add_authenticator* has an **int** data type.

#### *authenticator*

The data used as the basis for the generation of the authenticator. Must match the value supplied to [ENCRYPTBYKEY](encryptbykey-transact-sql.md). *authenticator* has a **sysname** data type.

#### @authenticator

A variable containing data from which an authenticator generates. Must match the value supplied to [ENCRYPTBYKEY](encryptbykey-transact-sql.md). *@authenticator* has a **sysname** data type.

#### @add_authenticator

A variable indicating whether the original encryption process included, and encrypted, an authenticator together with the plaintext. Must match the value passed to [ENCRYPTBYKEY](encryptbykey-transact-sql.md) during the data encryption process. *@add_authenticator* has an **int** data type.

#### *authenticator*

The data used as the basis for the generation of the authenticator. Must match the value supplied to [ENCRYPTBYKEY](encryptbykey-transact-sql.md). *authenticator* has a **sysname** data type.

#### @authenticator

A variable containing data from which an authenticator generates. Must match the value supplied to [ENCRYPTBYKEY](encryptbykey-transact-sql.md). *@authenticator* has a **sysname** data type.

## Return types

**varbinary**, with a maximum size of 8,000 bytes.

## Remarks

`DECRYPTBYKEYAUTOASYMKEY` combines the functionality of both `OPEN SYMMETRIC KEY` and `DECRYPTBYKEY`. In a single operation, it first decrypts a symmetric key, and then decrypts encrypted ciphertext with that key.

## Permissions

Requires `VIEW DEFINITION` permission on the symmetric key, and `CONTROL` permission on the asymmetric key.

## Examples

This example shows how `DECRYPTBYKEYAUTOASYMKEY` can simplify decryption code. This code should run on an [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database that doesn't already have a DMK. Replace `<password>` with a strong password.

```sql
--Create the keys and certificate.
USE AdventureWorks2022;

CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';
OPEN MASTER KEY DECRYPTION BY PASSWORD = '<password>';

CREATE ASYMMETRIC KEY SSN_AKey
    WITH ALGORITHM = RSA_2048;
GO

CREATE SYMMETRIC KEY SSN_Key_02
    WITH ALGORITHM = DES
    ENCRYPTION BY ASYMMETRIC KEY SSN_AKey;
GO

--
--Add a column of encrypted data.
ALTER TABLE HumanResources.Employee
    ADD EncryptedNationalIDNumber2 VARBINARY (128);

OPEN SYMMETRIC KEY SSN_Key_02 DECRYPTION BY ASYMMETRIC KEY SSN_AKey;

UPDATE HumanResources.Employee
    SET EncryptedNationalIDNumber2 = EncryptByKey(Key_GUID('SSN_Key_02'), NationalIDNumber);
GO

--Close the key used to encrypt the data.
CLOSE SYMMETRIC KEY SSN_Key_02;
--
--There are two ways to decrypt the stored data.
--
--OPTION ONE, using DecryptByKey()
--1. Open the symmetric key.
--2. Decrypt the data.
--3. Close the symmetric key.

OPEN SYMMETRIC KEY SSN_Key_02 DECRYPTION BY ASYMMETRIC KEY SSN_AKey;

SELECT NationalIDNumber,
       EncryptedNationalIDNumber2 AS 'Encrypted ID Number',
       CONVERT (NVARCHAR, DecryptByKey(EncryptedNationalIDNumber2)) AS 'Decrypted ID Number'
FROM HumanResources.Employee;

CLOSE SYMMETRIC KEY SSN_Key_02;

--OPTION TWO, using DECRYPTBYKEYAUTOASYMKEY()
SELECT NationalIDNumber,
       EncryptedNationalIDNumber2 AS 'Encrypted ID Number',
       CONVERT (NVARCHAR, DECRYPTBYKEYAUTOASYMKEY(AsymKey_ID('SSN_AKey'), NULL, EncryptedNationalIDNumber2)) AS 'Decrypted ID Number'
FROM HumanResources.Employee;
GO
```

## Related content

- [OPEN SYMMETRIC KEY (Transact-SQL)](../statements/open-symmetric-key-transact-sql.md)
- [ENCRYPTBYKEY (Transact-SQL)](encryptbykey-transact-sql.md)
- [DECRYPTBYKEY (Transact-SQL)](decryptbykey-transact-sql.md)
- [Encryption hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
