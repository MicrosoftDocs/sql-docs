---
title: "LocalDBGetVersions Function"
description: LocalDBGetVersions returns all SQL Server Express LocalDB versions available on the computer.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.topic: "reference"
apilocation: "sqluserinstance.dll"
apiname: "LocalDBGetVersions"
apitype: "DLLExport"
---
# LocalDBGetVersions function

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns all SQL Server Express LocalDB versions available on the computer.

**Header file:** `msoledbsql.h`

## Syntax

```cpp
#define MAX_LOCALDB_VERSION_LENGTH 43typedef WCHAR TLocalDBVersion [ MAX_LOCALDB_VERSION_LENGTH + 1 ] ;
typedef TLocalDBVersion* PTLocalDBVersion;
HRESULT LocalDBGetVersions(
           PTLocalDBVersion pVersion ,
           LPDWORD lpdwNumberOfVersions);
```

## Arguments

#### *pVersionNames*

[Output] Contains names of the LocalDB versions that are available on the user's workstation.

#### *lpdwNumberOfVersions*

[Input/Output] On input holds the number of slots for versions in the *pVersionNames* buffer.

On output, holds the number of existing LocalDB versions.

## Returns

`S_OK`: The function succeeded.

| Error | Description |
| --- | --- |
| [LOCALDB_ERROR_NOT_INSTALLED](../express-localdb-error-messages/localdb-error-not-installed.md) | SQL Server Express LocalDB isn't installed on the computer. |
| [LOCALDB_ERROR_INVALID_PARAMETER](../express-localdb-error-messages/localdb-error-invalid-parameter.md) | One or more specified input parameters are invalid. |
| [LOCALDB_ERROR_INSUFFICIENT_BUFFER](../express-localdb-error-messages/localdb-error-insufficient-buffer.md) | The input buffer is too short, and truncation wasn't requested. |
| [LOCALDB_ERROR_INTERNAL_ERROR](../express-localdb-error-messages/localdb-error-internal-error.md) | An unexpected error occurred. See the event log for details. |

## Remarks

For a code sample that uses LocalDB API, see [SQL Server Express LocalDB reference](../sql-server-express-localdb-reference.md).

## Related content

- [SQL Server Express LocalDB header and version information](sql-server-express-localdb-header-and-version-information.md)
