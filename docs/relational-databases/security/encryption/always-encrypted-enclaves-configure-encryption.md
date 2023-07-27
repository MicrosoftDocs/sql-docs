---
title: "Configure column encryption in-place using Always Encrypted with secure enclaves"
description: "Configure column encryption in-place using Always Encrypted with secure enclaves"
author: jaszymas
ms.author: jaszymas
ms.reviewer: "vanto"
ms.date: 02/15/2023
ms.service: sql
ms.subservice: security
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.alwaysencryptedwizard.f1"
helpviewer_keywords:
  - "Wizard, Always Encrypted"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
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

For information on how to ensure your column encryption keys are enclave-enabled, see [Manage keys for Always Encrypted with secure enclaves](always-encrypted-enclaves-manage-keys.md).

You also need to ensure that your environment meets the general [Prerequisites for running statements using secure enclaves](always-encrypted-enclaves-query-columns.md#prerequisites-for-running-statements-using-secure-enclaves).

A user or an application triggering cryptographic operations must have permissions to make schema changes on the table containing the impacted columns and to access column master keys involved in the operations, and relevant key metadata in the database.

You can trigger in-place encryption using one of the following methods:
- [ALTER TABLE ALTER COLUMN (Transact-SQL)](../../../t-sql/statements/alter-table-transact-sql.md) from SQL Server Management Studio or your custom application. See [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md).
- The [Always Encrypted wizard](always-encrypted-wizard.md)
- The [Set-SqlColumnEncryption](/powershell/module/sqlserver/set-sqlcolumnencryption) cmdlet. See [Configure column encryption in-place with PowerShell](always-encrypted-enclaves-configure-encryption-powershell.md).
- A [data-tier application (DAC) package](../../data-tier-applications/data-tier-applications.md). See [Configure column encryption in-place with DAC package](always-encrypted-enclaves-configure-encryption-dacpac.md).

## Next steps

- [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md)
- [Create and use indexes on column using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md)
- [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)

## See also

- [Troubleshoot common issues for Always Encrypted with secure enclaves](always-encrypted-enclaves-troubleshooting.md)
