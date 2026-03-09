---
title: "sys.master_key_passwords (Transact-SQL)"
description: sys.master_key_passwords returns a row for each database master key password added by using the sp_control_dbmasterkey_password stored procedure.
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/05/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.master_key_passwords_TSQL"
  - "master_key_passwords_TSQL"
  - "sys.master_key_passwords"
  - "master_key_passwords"
helpviewer_keywords:
  - "sys.master_key_passwords catalog view"
dev_langs:
  - "TSQL"
---
# sys.master_key_passwords (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns a row for each database master key password added by using the `sp_control_dbmasterkey_password` stored procedure. The passwords that are used to protect the master keys are stored in the credential store. The credential name follows this format: ##DBMKEY_<database_family_guid>_<random_password_guid>##. The password is stored as the credential secret. For each password added by using `sp_control_dbmasterkey_password`, there's a row in `sys.credentials`.

Each row in this view shows a `credential_id` and the `family_guid` of a database the master key of which is protected by the password associated with that credential. A join with `sys.credentials` on the `credential_id` returns useful fields, such as the `create_date` and credential name.

| Column name | Data type | Description |  
| --- | --- | --- |
| `credential_id` | **int** | ID of the credential to which the password belongs. This ID is unique within the server instance. |
| `family_guid` | **uniqueidentifier** | Unique ID of the original database at creation. This GUID remains the same after the database is restored or attached, even if the database name is changed.<br /><br />If automatic decryption by the service master key fails, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the `family_guid` to identify credentials that might contain the password used to protect the database master key. |  
  
## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require VIEW SERVER SECURITY STATE permission on the server.

## Related content

- [Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)
- [sp_control_dbmasterkey_password (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-control-dbmasterkey-password-transact-sql.md)
- [Security Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)
- [CREATE SYMMETRIC KEY (Transact-SQL)](../../t-sql/statements/create-symmetric-key-transact-sql.md)
- [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
