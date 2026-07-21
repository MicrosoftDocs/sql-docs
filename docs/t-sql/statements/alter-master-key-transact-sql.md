---
title: "ALTER MASTER KEY (Transact-SQL)"
description: ALTER MASTER KEY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: 10/28/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - fasttrack-edit
  - ignite-2025
f1_keywords:
  - "ALTER MASTER KEY"
  - "ALTER_MASTER_KEY_TSQL"
helpviewer_keywords:
  - "REGENERATE phrase"
  - "ALTER MASTER KEY statement"
  - "decryption [SQL Server], Database Master Key"
  - "FORCE option"
  - "encryption [SQL Server], Database Master Key"
  - "database master key [SQL Server], modifying"
  - "cryptography [SQL Server], Database Master Key"
  - "modifying Database Master Key"
  - "service master key [SQL Server], modifying"
  - "DROP ENCRYPTION BY SERVICE MASTER KEY phrase"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# ALTER MASTER KEY (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

Changes the properties of a database master key.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

```syntaxsql
-- Syntax for SQL Server
ALTER MASTER KEY <alter_option>

<alter_option> ::=
    <regenerate_option> | <encryption_option>

<regenerate_option> ::=
    [ FORCE ] REGENERATE WITH ENCRYPTION BY PASSWORD = 'password'

<encryption_option> ::=
    ADD ENCRYPTION BY { SERVICE MASTER KEY | PASSWORD = 'password' }
    |
    DROP ENCRYPTION BY { SERVICE MASTER KEY | PASSWORD = 'password' }
```

Syntax for [!INCLUDE[ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]

```syntaxsql
-- Syntax for Azure SQL Database
-- Note: DROP ENCRYPTION BY SERVICE MASTER KEY is not supported on Azure SQL Database.

ALTER MASTER KEY <alter_option>

<alter_option> ::=
    <regenerate_option> | <encryption_option>

<regenerate_option> ::=
    [ FORCE ] REGENERATE WITH ENCRYPTION BY PASSWORD = 'password'

<encryption_option> ::=
    ADD ENCRYPTION BY { SERVICE MASTER KEY | PASSWORD = 'password' }
    |
    DROP ENCRYPTION BY { PASSWORD = 'password' }
```

Syntax for [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[sspdw-md](../../includes/sspdw-md.md)]

```syntaxsql
-- Syntax for Azure Synapse Analytics and Analytics Platform System

ALTER MASTER KEY <alter_option>

<alter_option> ::=
    <regenerate_option> | <encryption_option>

<regenerate_option> ::=
    [ FORCE ] REGENERATE WITH ENCRYPTION BY PASSWORD ='password'

<encryption_option> ::=
    ADD ENCRYPTION BY SERVICE MASTER KEY
    |
    DROP ENCRYPTION BY SERVICE MASTER KEY
```

## Arguments

#### PASSWORD ='*password*'

Specifies a password with which to encrypt or decrypt the database master key. *password* must meet the Windows password policy requirements of the computer that is running the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Remarks

The REGENERATE option re-creates the database master key and all the keys it protects. The keys are first decrypted with the old master key, and then encrypted with the new master key. This resource-intensive operation should be scheduled during a period of low demand, unless the master key is compromised.

[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] uses the AES encryption algorithm to protect the service master key (SMK) and the database master key (DMK). AES is a newer encryption algorithm than 3DES used in earlier versions. After upgrading an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], the SMK and DMK should be regenerated in order to upgrade the master keys to AES. For more information about regenerating the SMK, see [ALTER SERVICE MASTER KEY](../../t-sql/statements/alter-service-master-key-transact-sql.md).

When the FORCE option is used, key regeneration continues even if the master key is unavailable or the server cannot decrypt all the encrypted private keys. If the master key cannot be opened, use the [RESTORE MASTER KEY](../../t-sql/statements/restore-master-key-transact-sql.md) statement to restore the master key from a backup. Use the FORCE option only if the master key is irretrievable or if decryption fails. Information that is encrypted only by an irretrievable key is lost.

The DROP ENCRYPTION BY SERVICE MASTER KEY option removes the encryption of the database master key by the service master key.

DROP ENCRYPTION BY SERVICE MASTER KEY is not supported on Azure SQL Database.

ADD ENCRYPTION BY SERVICE MASTER KEY causes a copy of the master key to be encrypted using the service master key and stored in both the current database and in master.

## Permissions

Requires CONTROL permission on the database. If the database master key is encrypted with a password, knowledge of that password is also required.

## Examples

The following example creates a new database master key for `AdventureWorks` and reencrypts the keys below it in the encryption hierarchy. Replace `<secure password>` with a strong, unique password.

```sql
USE AdventureWorks2022;
ALTER MASTER KEY REGENERATE WITH ENCRYPTION BY PASSWORD = '<secure password>';
GO
```

## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

The following example creates a new database master key for `AdventureWorksPDW2012` and re-encrypts the keys below it in the encryption hierarchy. Replace `<secure password>` with a strong, unique password.

```sql
USE master;
ALTER MASTER KEY REGENERATE WITH ENCRYPTION BY PASSWORD = '<secure password>';
GO
```

## Related content

- [CREATE MASTER KEY](../../t-sql/statements/create-master-key-transact-sql.md)
- [OPEN MASTER KEY](../../t-sql/statements/open-master-key-transact-sql.md)
- [CLOSE MASTER KEY](../../t-sql/statements/close-master-key-transact-sql.md)
- [BACKUP MASTER KEY](../../t-sql/statements/backup-master-key-transact-sql.md)
- [RESTORE MASTER KEY](../../t-sql/statements/restore-master-key-transact-sql.md)
- [DROP MASTER KEY](../../t-sql/statements/drop-master-key-transact-sql.md)
- [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [Database Detach and Attach](../../relational-databases/databases/database-detach-and-attach-sql-server.md)
