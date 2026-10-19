---
title: "LocalDBGetInstances Function"
description: LocalDBGetInstances returns all SQL Server Express LocalDB instances with the given version.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.topic: "reference"
apilocation: "sqluserinstance.dll"
apiname: "LocalDBGetInstances"
apitype: "DLLExport"
---
# LocalDBGetInstances function

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns all SQL Server Express LocalDB instances with the given version.

**Header file:** `msoledbsql.h`

## Syntax

```cpp
#define MAX_LOCALDB_INSTANCE_NAME_LENGTH 128typedef WCHAR TLocalDBInstanceName [ MAX_LOCALDB_INSTANCE_NAME_LENGTH + 1 ] ;
typedef TLocalDBInstanceName* PTLocalDBInstanceName;
HRESULT LocalDBGetInstances(
           PTLocalDBInstanceName pInstanceNames ,
           LPDWORD lpdwNumberOfInstances
);
```

## Arguments

#### *pInstanceNames*

[Output] When this function returns, contains the names of both named and default LocalDB instances on the user's workstation.

#### *lpdwNumberOfInstances*

[Input/Output] On input, this option contains the number of slots for instance names in the *pInstanceNames* buffer. On output, this option contains the number of LocalDB instances found on the user's workstation.

## Returns

`S_OK`: The function succeeded.

| Error | Description |
| --- | --- |
| [LOCALDB_ERROR_NOT_INSTALLED](../express-localdb-error-messages/localdb-error-not-installed.md) | SQL Server Express LocalDB isn't installed on the computer. |
| [LOCALDB_ERROR_INVALID_PARAMETER](../express-localdb-error-messages/localdb-error-invalid-parameter.md) | One or more specified input parameters are invalid. |
| [LOCALDB_ERROR_INSUFFICIENT_BUFFER](../express-localdb-error-messages/localdb-error-insufficient-buffer.md) | The input buffer is too short, and truncation wasn't requested. |
| [LOCALDB_ERROR_INSTANCE_FOLDER_PATH_TOO_LONG](../express-localdb-error-messages/localdb-error-instance-folder-path-too-long.md) | The path where the instance should be stored is longer than `MAX_PATH`. |
| [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_REGISTRY](../express-localdb-error-messages/localdb-error-cannot-access-instance-registry.md) | An instance registry can't be accessed. |
| [LOCALDB_ERROR_INSTANCE_CONFIGURATION_CORRUPT](../express-localdb-error-messages/localdb-error-instance-configuration-corrupt.md) | An instance configuration is corrupted. |
| [LOCALDB_ERROR_INTERNAL_ERROR](../express-localdb-error-messages/localdb-error-internal-error.md) | An unexpected error occurred. See the event log for details. |

## Remarks

For a code sample that uses LocalDB API, see [SQL Server Express LocalDB reference](../sql-server-express-localdb-reference.md).

## Related content

- [SQL Server Express LocalDB header and version information](sql-server-express-localdb-header-and-version-information.md)
