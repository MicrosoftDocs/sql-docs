---
description: "Configure column encryption in-place using Always Encrypted with secure enclaves"
title: "Configure column encryption in-place using Always Encrypted with secure enclaves | Microsoft Docs"
ms.custom:
- event-tier1-build-2022
ms.date: 05/24/2022
ms.service: sql
ms.reviewer: "vanto"
ms.subservice: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15" 
---
# Configure column encryption in-place using Always Encrypted with secure enclaves 
[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

[Always Encrypted with secure enclaves](always-encrypted-enclaves.md) supports cryptographic operations on database columns in-place - inside a secure enclave in the [!INCLUDE[ssde-md](../../../includes/ssde-md.md)]. In-place encryption eliminates the need to move the data for such operations outside of the database, making the cryptographic operations faster and more reliable. 

> [!NOTE]
> Despite the performance benefits of in-place encryption, cryptographic operations on large tables can take a long time and consume substantial resources, potentially impacting and degrading performance and availability of your applications.

In-place encryption makes it also possible to trigger cryptographic operations using the [ALTER TABLE ALTER COLUMN (Transact-SQL)](../../../t-sql/statements/alter-table-transact-sql.md) statement, which isn't possible without an enclave.

## Prerequisites
The supported cryptographic operations and the requirements for column encryption key(s), used for the operations, are:
- Encrypting a plaintext column. The column encryption key used to encrypt the column must be enclave-enabled.
- Re-encrypting an encrypted column using a new encryption type or/and a new column encryption key. Both the current column encryption key and the new column encryption key (if different than the current key) must be enclave-enabled.
- Decrypting an encrypted column - the column encryption key, protecting the column, must be enclave-enabled.

See [Manage keys for Always Encrypted with secure enclaves](always-encrypted-enclaves-manage-keys.md) for information how to ensure your column encryption keys are enclave-enabled.

You also need ensure that your environment meets the general [Prerequisites for running statements using secure enclaves](always-encrypted-enclaves-query-columns.md#prerequisites-for-running-statements-using-secure-enclaves).

A user or an application triggering cryptographic operations must have permissions to make schema changes on the table containing the impacted columns and to access column master keys involved in the operations, and relevant key metadata in the database.

You can only trigger in-place encryption using [ALTER TABLE ALTER COLUMN (Transact-SQL)](../../../t-sql/statements/alter-table-transact-sql.md) from SQL Server Management Studio or your custom application. See [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md).

> [!NOTE]
> Currently, the [Always Encrypted wizard](always-encrypted-wizard.md) and the [Set-SqlColumnEncryption](/powershell/module/sqlserver/set-sqlcolumnencryption) cmdlet do not support in-place encryption, and always download the data for cryptographic operations, even if your configuration meets the above requirements. 

## Next Steps
- [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md)
- [Create and use indexes on column using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md)
- [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)

## See Also  
- [Troubleshoot common issues for Always Encrypted with secure enclaves](always-encrypted-enclaves-troubleshooting.md)
