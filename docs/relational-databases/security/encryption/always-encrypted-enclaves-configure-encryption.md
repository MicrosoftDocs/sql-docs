---
description: "Configure column encryption in-place using Always Encrypted with secure enclaves"
title: "Configure column encryption in-place using Always Encrypted with secure enclaves | Microsoft Docs"
ms.custom: ""
ms.date: 10/10/2019
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: "vanto"
ms.technology: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions" 
---
# Configure column encryption in-place using Always Encrypted with secure enclaves 
[!INCLUDE [sqlserver2019-windows-only](../../../includes/applies-to-version/sqlserver2019-windows-only.md)]

[Always Encrypted with secure enclaves](always-encrypted-enclaves.md) supports cryptographic operations on database columns in-place - inside a secure enclave in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. In-place encryption eliminates the need to move the data for such operations outside of the database, making the cryptographic operations faster and more reliable. 

> [!NOTE]
> Despite the performance benefits of in-place encryption, cryptographic operations on large tables can take a long time and consume substantial resources, potentially impacting and degrading performance and availability of your applications.

In-place encryption makes it also possible to trigger cryptographic operations using the [ALTER TABLE ALTER COLUMN (Transact-SQL)](../../../t-sql/statements/alter-table-transact-sql.md) statement, which is not possible without an enclave.

## Prerequisites
The supported cryptographic operations and the requirements for column encryption key(s), used for the operations, are:
- Encrypting a plaintext column. The column encryption key used to encrypt the column must be enclave-enabled.
- Re-encrypting an encrypted column using a new encryption type or/and a new column encryption key. Both the current column encryption key and the new column encryption key (if different than the current key) must be enclave-enabled.
- Decrypting an encrypted column - the column encryption key, protecting the column, must be enclave-enabled.

See [Manage keys for Always Encrypted with secure enclaves](always-encrypted-enclaves-manage-keys.md) for information how to ensure your column encryption keys are enclave-enabled.

In-place encryption also requires a SQL Server instance that has a correctly initialized secure enclave. See [Configure the enclave type for Always Encrypted Server Configuration Option](../../../database-engine/configure-windows/configure-column-encryption-enclave-type.md).

A user or an application triggering cryptographic operations must have permissions to make schema changes on the table containing the impacted columns and to access column master keys involved in the operations, and relevant key metadata in the database.

You can only trigger in-place encryption using [ALTER TABLE ALTER COLUMN (Transact-SQL)](../../../t-sql/statements/alter-table-transact-sql.md) from SQL Server Management Studio or your custom application. See [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md).

> [!NOTE]
> Currently, the [Always Encrypted wizard](always-encrypted-wizard.md) and the [Set-SqlColumnEncryption](https://docs.microsoft.com/powershell/module/sqlserver/set-sqlcolumnencryption) cmdlet do not support in-place encryption, and always download the data for cryptographic operations, even if your configuration meets the above requirements. 

## Next Steps
- [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md)
- [Create and use indexes on column using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md)
- [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)
