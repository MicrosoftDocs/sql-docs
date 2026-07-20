---
title: "sys.key_encryptions (Transact-SQL)"
description: sys.key_encryptions (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 04/30/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.key_encryptions"
  - "key_encryptions_TSQL"
  - "sys.key_encryptions_TSQL"
  - "key_encryptions"
helpviewer_keywords:
  - "sys.key_encryptions catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.key_encryptions (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns a row for each symmetric key encryption specified by using the `ENCRYPTION BY` clause of the `CREATE SYMMETRIC KEY` statement.

To protect the key material of the symmetric key, SQL Server and Azure SQL store the key material in encrypted form. Historically, this encryption utilized PKCS#1 v1.5 padding mode; starting with database compatibility level 170, the encryption uses OAEP-256 padding mode.

| Column names | Data types | Description |
| --- | --- | --- |
| `key_id` | **int** | ID of the encrypted key. |
| `thumbprint` | **varbinary(32)** | SHA-1 hash of the certificate with which the key is encrypted, or the GUID of the symmetric key with which the key is encrypted. |
| `crypt_type` | **char(4)** | Type of encryption:<br /><br />`ESKS` = Encrypted by symmetric key<br />`ESKP`, `ESP2`, or `ESP3` = Encrypted by password<br />`EPUC` = Encrypted by certificate<br />`EPUA` = Encrypted by asymmetric key<br />`ESKM` = Encrypted by master key<br />`C256` = Encrypted by certificate OAEP256<br />`A256` = Encrypted by asymmetric key OAEP256 |
| `crypt_type_desc` | **nvarchar(60)** | Description of encryption type:<br /><br />`ENCRYPTION BY SYMMETRIC KEY`<br />`ENCRYPTION BY PASSWORD`<br />Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]<br />`ENCRYPTION BY CERTIFICATE`<br />`ENCRYPTION BY ASYMMETRIC KEY`<br />`ENCRYPTION BY MASTER KEY` <sup>1</sup><br />`ENCRYPTION BY CERTIFICATE OAEP256`<br />`ENCRYPTION BY ASYMMETRIC KEY OAEP256` |
| `crypt_property` | **varbinary(max)** | Signed or encrypted bits. |

<sup>1</sup> Windows DPAPI is used to protect the SMK.

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Related content

- [System catalog views](catalog-views-transact-sql.md)
- [Security Catalog Views](security-catalog-views-transact-sql.md)
- [Encryption hierarchy](../security/encryption/encryption-hierarchy.md)
- [CREATE SYMMETRIC KEY](../../t-sql/statements/create-symmetric-key-transact-sql.md)

