---
title: "sp_enclave_send_keys (Transact-SQL)"
description: "sp_enclave_send_keys (Transact-SQL)"
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto, randolphwest
ms.date: 06/13/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_enclave_send_keys"
  - "sp_enclave_send_keys_TSQL"
  - "sys.sp_enclave_send_keys"
  - "sys.sp_enclave_send_keys_TSQL"
helpviewer_keywords:
  - "sp_enclave_send_keys"
dev_langs:
  - "TSQL"
monikerRange: ">= sql-server-ver15"
---
# sp_enclave_send_keys (Transact-SQL)

[!INCLUDE [sqlserver2019-windows-only](../../includes/applies-to-version/sqlserver2019-windows-only.md)]

Sends columns encryption keys, defined in the database, to the server-side secure enclave used with [Always Encrypted with secure enclaves](../security/encryption/always-encrypted-enclaves.md).

`sp_enclave_send_keys` only sends only the keys that are enclave-enabled and encrypt columns that use randomized encryption and have indexes. For a regular user query, a client driver provides the enclave with the keys needed for computations in the query. `sp_enclave_send_keys` sends all column encryption keys defined in the database and used for indexes encrypted columns.

`sp_enclave_send_keys` provides an easy way to send keys to the enclave and populate the column encryption key cache for subsequent indexing operations. Use `sp_enclave_send_keys` to enable:

- A DBA to rebuild or alter indexes or statistics on encrypted database columns, if the DBA doesn't have access to the column master key(s). See [Invoke indexing operations using cached column encryption keys](../security/encryption/always-encrypted-enclaves-create-use-indexes.md#invoke-indexing-operations-using-cached-column-encryption-keys).

- [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to complete the recovery of indexes on encrypted columns. See [Database Recovery](../security/encryption/always-encrypted-enclaves.md#database-recovery).

- An application using .NET Framework Data Provider for SQL Server to bulk load data to encrypted columns.

To successfully invoke `sp_enclave_send_keys`, you need to connect to the database with Always Encrypted and enclave computations enabled for the database connection. You also need to have access to column master keys, protecting the column encryption keys, you're going to send, and you need permissions to access Always Encrypted key metadata in the database.

## Syntax

```syntaxsql
sp_enclave_send_keys
[ ; ]
```

## Arguments

This stored procedure has no arguments.

## Return value

This stored procedure has no return value.

## Result set

This stored procedure has no result sets.

## Permissions

Require the `VIEW ANY COLUMN ENCRYPTION KEY DEFINITION` and `VIEW ANY COLUMN MASTER KEY DEFINITION` permissions in the database.

## Examples

```sql
EXEC sp_enclave_send_keys;
```

## Related content

- [Always Encrypted with secure enclaves](../security/encryption/always-encrypted-enclaves.md)
- [Create and use indexes on columns using Always Encrypted with secure enclaves](../security/encryption/always-encrypted-enclaves-create-use-indexes.md)
- [Tutorial: Create and use indexes on enclave-enabled columns using randomized encryption](../security/tutorial-creating-using-indexes-on-enclave-enabled-columns-using-randomized-encryption.md)
