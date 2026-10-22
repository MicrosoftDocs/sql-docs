---
title: "LocalDBUnshareInstance Function"
description: LocalDBUnshareInstance stops the sharing of the specified SQL Server Express LocalDB instance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.topic: "reference"
apilocation: "sqluserinstance.dll"
apiname: "LocalDBUnshareInstance"
apitype: "DLLExport"
---
# LocalDBUnshareInstance function

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Stops the sharing of the specified SQL Server Express LocalDB instance.

**Header file:** `msoledbsql.h`

## Syntax

```cpp
HRESULT LocalDBUnShareInstance(
           PCWSTR pInstanceSharedName ,
           DWORD dwFlags
);
```

## Arguments

#### *pInstanceSharedName*

[Input] The shared name for the LocalDB instance to unshare.

#### *dwFlags*

[Input] Reserved for future use. Currently should be set to 0.

## Returns

`S_OK`: The function succeeded.

| Error | Description |
| --- | --- |
| [LOCALDB_ERROR_NOT_INSTALLED](../express-localdb-error-messages/localdb-error-not-installed.md) | SQL Server Express LocalDB isn't installed on the computer. |
| [LOCALDB_ERROR_INVALID_PARAMETER](../express-localdb-error-messages/localdb-error-invalid-parameter.md) | One or more specified input parameters are invalid. |
| [LOCALDB_ERROR_INVALID_INSTANCE_NAME](../express-localdb-error-messages/localdb-error-invalid-instance-name.md) | The specified instance name is invalid. |
| [LOCALDB_ERROR_UNKNOWN_INSTANCE](../express-localdb-error-messages/localdb-error-unknown-instance.md) | The specified instance doesn't exist. |
| [LOCALDB_ERROR_ADMIN_RIGHTS_REQUIRED](../express-localdb-error-messages/localdb-error-admin-rights-required.md) | Administrator privileges are required in order to execute this operation. |
| [LOCALDB_ERROR_INTERNAL_ERROR](../express-localdb-error-messages/localdb-error-internal-error.md) | An unexpected error occurred. See the event log for details. |

## Remarks

For a code sample that uses LocalDB API, see [SQL Server Express LocalDB reference](../sql-server-express-localdb-reference.md).

## Related content

- [SQL Server Express LocalDB header and version information](sql-server-express-localdb-header-and-version-information.md)
