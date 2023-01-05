---
description: "Rotate enclave-enabled keys"
title: "Rotate  | Microsoft Docs"
ms.custom:
- event-tier1-build-2022
ms.date: 05/24/2022
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15"
---
# Rotate enclave-enabled keys

[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

In Always Encrypted, a key rotation is a process of replacing an existing column master key or a column encryption key with a new key. This article describes use cases and considerations for key rotation specific to  [Always Encrypted with secure enclaves](always-encrypted-enclaves.md) when either the initial key and/or the target (new) key is an enclave-enabled key. For general guidelines and processes for managing Always Encrypted keys, see [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md). 

You may need to rotate a key for security or compliance reasons. For example, if a key has been compromised or your organization's policies require you to replace keys periodically. In addition, Always Encrypted with secure enclaves key rotation provides a way to enable or disable the functionality of the server-side secure enclave for your encrypted columns.

- When you replace a key that isn't enclave-enabled with an enclave-enabled key, you unlock the functionality of the secure enclave to query on columns that are protected with the key. For more information, see [Enable Always Encrypted with secure enclaves for existing encrypted columns](always-encrypted-enclaves-enable-for-encrypted-columns.md).
- When you replace an enclave-enabled key with a key that isn't enclave-enabled, you disable the functionality of the secure enclave to query on columns that are protected with the key.

If you're rotating a key only for security/compliance reasons, and not to enable or disable enclave computations for your columns, make sure the target key has the same configuration regarding enclaves as the source key. For example, if the source key is enclave-enabled, the target key should also be enclave-enabled.

The below steps include links to detailed articles, depending on your rotation scenario:

1. Provision a new key (a column master key or a column encryption key).
    - To provision a new enclave-enclave enabled key, see [Provision enclave-enabled keys](always-encrypted-enclaves-provision-keys.md).
    - To provision a key that isn't enclave enabled, see [Provision Always Encrypted keys using SQL Server Management Studio](configure-always-encrypted-keys-using-ssms.md) and [Provision Always encrypted keys using PowerShell](configure-always-encrypted-keys-using-powershell.md).
2. Replace an existing key with the new key.
    - If you're rotating a column encryption key and both the source key and the target key are enclave-enabled, you can run the rotation (which involves re-encrypting your data) in-place. For more information, see [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md).
    - For detailed steps for rotating keys, see [Rotate Always Encrypted keys using SQL Server Management Studio](rotate-always-encrypted-keys-using-ssms.md) and [Rotate Always Encrypted keys using PowerShell](rotate-always-encrypted-keys-using-powershell.md).

## Next Steps

- [Run Transact-SQL statements using secure enclaves](always-encrypted-enclaves-query-columns.md)
- [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md)
- [Enable Always Encrypted with secure enclaves for existing encrypted columns](always-encrypted-enclaves-enable-for-encrypted-columns.md)
- [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)  

## See Also  
- [Manage keys for Always Encrypted with secure enclaves](always-encrypted-enclaves-manage-keys.md)
