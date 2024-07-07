---
title: "sp_control_dbmasterkey_password (Transact-SQL)"
description: sp_control_dbmasterkey_password adds or drops a credential containing the password needed to open a database master key.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_control_dbmasterkey_password"
  - "sp_control_dbmasterkey_password_TSQL"
helpviewer_keywords:
  - "sp_control_dbmasterkey_password"
dev_langs:
  - "TSQL"
---
# sp_control_dbmasterkey_password (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Adds or drops a credential containing the password needed to open a database master key (DMK).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_control_dbmasterkey_password @db_name = 'db_name'
    , @password = 'password'
    , @action = { N'add' | N'drop' }
```

## Arguments

#### @db_name = N'*db_name*'

Specifies the name of the database associated with this credential. Can't be a system database. *@db_name* is **nvarchar**.

#### @password = N'*password*'

Specifies the password of the DMK. *@password* is **nvarchar**.

#### @action = { N'add' | N'drop' }

Specifies an action for a credential for the specified database in the credential store. The value passed to *@action* is **nvarchar**.

| Action | Description |
| --- | --- |
| `add` | Specifies that a credential for the specified database will be added to the credential store. The credential contains the password of the DMK. |
| `drop` | Specifies that a credential for the specified database will be dropped from the credential store. |

## Remarks

When [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] needs a DMK to decrypt or encrypt a key, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] tries to decrypt the DMK with the service master key (SMK) of the instance. If the decryption fails, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] searches the credential store for credentials that have the same family GUID as the database for which it needs the key. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] then tries to decrypt the DMK with each matching credential until the decryption succeeds or there are no more credentials.

> [!CAUTION]  
> Don't create a master key credential for a database that must be inaccessible to `sa` and other highly-privileged server principals. You can configure a database so that its key hierarchy can't be decrypted by the SMK. This option is supported as a defense-in-depth for databases that contain encrypted information that shouldn't be accessible to `sa` or other highly privileged server principals. Creating a credential for such a database removes this defense-in-depth, enabling `sa` and other highly privileged server principals to decrypt the database.

Credentials that are created by using `sp_control_dbmasterkey_password` are visible in the [sys.master_key_passwords](../system-catalog-views/sys-master-key-passwords-transact-sql.md) catalog view. The names of credentials that are created for DMKs have the following format: `##DBMKEY_<database_family_guid>_<random_password_guid>##`. The password is stored as the credential secret. Each password added to the credential store has a matching row in `sys.credentials`.

You can't use `sp_control_dbmasterkey_password` to create a credential for the following system databases: `master`, `model`, `msdb`, or `tempdb`.

`sp_control_dbmasterkey_password` doesn't verify that the password can open the DMK of the specified database.

If you specify a password that is already stored in a credential for the specified database, `sp_control_dbmasterkey_password` fails.

Two databases from different server instances can share the same family GUID. If this occurs, the databases share the same DMK records in the credential store.

Parameters passed to `sp_control_dbmasterkey_password` don't appear in traces.

When you're using the credential that was added by using `sp_control_dbmasterkey_password` to open the DMK, the DMK is re-encrypted by the SMK. If the database is in read-only mode, the re-encryption operation fails, and the DMK remains unencrypted. For subsequent access to the DMK, you must use the `OPEN MASTER KEY` statement and a password. To avoid using a password, create the credential before you move the database to read-only mode.

### Potential backward compatibility issue

Currently, the stored procedure doesn't check whether a key exists. This functionality is permitted for backward compatibility, but displays a warning. This behavior is deprecated. In a future release, the key must exist and the password used in the stored procedure `sp_control_dbmasterkey_password` must be the same password as one of the passwords used to encrypt the DMK.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Create a credential for the AdventureWorks master key

The following example creates a credential for the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] DMK, and saves the master key password as the secret in the credential. Because all parameters that are passed to `sp_control_dbmasterkey_password` must be of data type **nvarchar**, the text strings are converted with the casting operator `N`.

```sql
EXEC sp_control_dbmasterkey_password
    @db_name = N'AdventureWorks2022',
    @password = N'sdfjlkj#mM00sdfdsf98093258jJlfdk4',
    @action = N'add';
GO
```

### B. Drop a credential for a database master key

The following example removes the credential created in example A. All parameters are required, including the password.

```sql
EXEC sp_control_dbmasterkey_password
    @db_name = N'AdventureWorks2022',
    @password = N'sdfjlkj#mM00sdfdsf98093258jJlfdk4',
    @action = N'drop';
GO
```

## Related content

- [Set Up an Encrypted Mirror Database](../../database-engine/database-mirroring/set-up-an-encrypted-mirror-database.md)
- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sys.credentials (Transact-SQL)](../system-catalog-views/sys-credentials-transact-sql.md)
- [Credentials (Database Engine)](../security/authentication-access/credentials-database-engine.md)
