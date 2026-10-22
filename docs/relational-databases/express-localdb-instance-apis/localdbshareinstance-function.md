---
title: "LocalDBShareInstance Function"
description: LocalDBShareInstance shares the specified SQL Server Express LocalDB instance with other users of the computer.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.topic: "reference"
apilocation: "sqluserinstance.dll"
apiname: "LocalDBShareInstance"
apitype: "DLLExport"
---
# LocalDBShareInstance function

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Shares the specified SQL Server Express LocalDB instance with other users of the computer, using the specified shared name.

**Header file:** `msoledbsql.h`

## Syntax

```cpp
HRESULT LocalDBShareInstance(
           PSID pOwnerSID ,
           PCWSTR pInstancePrivateName ,
           PCWSTR pInstanceSharedName ,
           DWORD dwFlags
);
```

## Arguments

#### *pOwnerSID*

[Input] The SID of the instance owner.

#### *pInstancePrivateName*

[Input] The private name for the LocalDB instance to share.

#### *pInstanceSharedName*

[Input] The shared name for the LocalDB instance to share.

#### *dwFlags*

[Input] Reserved for future use. Currently should be set to `0`.

## Returns

`S_OK`: The function succeeded.

| Error | Description |
| --- | --- |
| [LOCALDB_ERROR_NOT_INSTALLED](../express-localdb-error-messages/localdb-error-not-installed.md) | SQL Server Express LocalDB isn't installed on the computer. |
| [LOCALDB_ERROR_INVALID_PARAMETER](../express-localdb-error-messages/localdb-error-invalid-parameter.md) | One or more specified input parameters are invalid. |
| [LOCALDB_ERROR_INVALID_INSTANCE_NAME](../express-localdb-error-messages/localdb-error-invalid-instance-name.md) | The specified instance name is invalid. |
| [LOCALDB_ERROR_UNKNOWN_INSTANCE](../express-localdb-error-messages/localdb-error-unknown-instance.md) | The specified instance doesn't exist. |
| [LOCALDB_ERROR_ADMIN_RIGHTS_REQUIRED](../express-localdb-error-messages/localdb-error-admin-rights-required.md) | Administrator privileges are required in order to execute this operation. |
| [LOCALDB_ERROR_SHARED_NAME_TAKEN](../express-localdb-error-messages/localdb-error-shared-name-taken.md) | The specified shared name is already taken. |
| [LOCALDB_ERROR_INSTANCE_ALREADY_SHARED](../express-localdb-error-messages/localdb-error-instance-already-shared.md) | The specified instance is already shared. |
| [LOCALDB_ERROR_INTERNAL_ERROR](../express-localdb-error-messages/localdb-error-internal-error.md) | An unexpected error occurred. See the event log for details. |

## Remarks

For a code sample that uses LocalDB API, see [SQL Server Express LocalDB reference](../sql-server-express-localdb-reference.md).

## Related content

- [SQL Server Express LocalDB header and version information](sql-server-express-localdb-header-and-version-information.md)
