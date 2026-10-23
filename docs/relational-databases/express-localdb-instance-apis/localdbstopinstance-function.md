---
title: "LocalDBStopInstance Function"
description: "LocalDBStopInstance Function"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.topic: "reference"
apilocation: "sqluserinstance.dll"
apiname: "LocalDBStopInstance"
apitype: "DLLExport"
---
# LocalDBStopInstance function

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Stops the specified SQL Server Express LocalDB instance from running.

**Header file:** `msoledbsql.h`

## Syntax

```cpp
HRESULT LocalDBStopInstance(
           PCWSTR pInstanceName ,
           DWORD dwFlags ,
           ULONG ulTimeout
);
```

## Arguments

#### *pInstanceName*

[Input] The name of the LocalDB instance to stop.

#### *dwFlags*

[Input] One or a combination of the flag values specifying the way to stop the instance.

Available flags:

#### LOCALDB_SHUTDOWN_KILL_PROCESS

Shut down immediately using the terminate process operating system command.

#### LOCALDB_SHUTDOWN_WITH_NOWAIT

Shut down using the `WITH NOWAIT` option Transact-SQL command.

If none of the flags is set, the LocalDB instance will be shut down using the `SHUTDOWN` Transact-SQL command. If both flags are set, the `LOCALDB_SHUTDOWN_KILL_PROCESS` flag takes precedence.

#### *ulTimeout*

[Input] The time in seconds to wait for this operation to complete. If this value is `0`, this function will return immediately without waiting for the LocalDB instance to stop.

## Returns

`S_OK`: The function succeeded.

| Error | Description |
| --- | --- |
| [LOCALDB_ERROR_NOT_INSTALLED](../express-localdb-error-messages/localdb-error-not-installed.md) | SQL Server Express LocalDB isn't installed on the computer. |
| [LOCALDB_ERROR_INVALID_PARAMETER](../express-localdb-error-messages/localdb-error-invalid-parameter.md) | One or more specified input parameters are invalid. |
| [LOCALDB_ERROR_INVALID_INSTANCE_NAME](../express-localdb-error-messages/localdb-error-invalid-instance-name.md) | The specified instance name is invalid. |
| [LOCALDB_ERROR_UNKNOWN_INSTANCE](../express-localdb-error-messages/localdb-error-unknown-instance.md) | The instance doesn't exist. |
| [LOCALDB_ERROR_WAIT_TIMEOUT](../express-localdb-error-messages/localdb-error-wait-timeout.md) | A time-out occurred while trying to acquire the synchronization locks. |
| [LOCALDB_ERROR_INSTANCE_STOP_FAILED](../express-localdb-error-messages/localdb-error-instance-stop-failed.md) | The stop operation failed to complete within the given time. |
| [LOCALDB_ERROR_INSTANCE_FOLDER_PATH_TOO_LONG](../express-localdb-error-messages/localdb-error-instance-folder-path-too-long.md) | The path where the instance should be stored is longer than `MAX_PATH`. |
| [LOCALDB_ERROR_CANNOT_GET_USER_PROFILE_FOLDER](../express-localdb-error-messages/localdb-error-cannot-get-user-profile-folder.md) | A user profile folder can't be retrieved. |
| [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_FOLDER](../express-localdb-error-messages/localdb-error-cannot-access-instance-folder.md) | An instance folder can't be accessed. |
| [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_REGISTRY](../express-localdb-error-messages/localdb-error-cannot-access-instance-registry.md) | An instance registry can't be accessed. |
| [LOCALDB_ERROR_INSTANCE_CONFIGURATION_CORRUPT](../express-localdb-error-messages/localdb-error-instance-configuration-corrupt.md) | An instance configuration is corrupted. |
| [LOCALDB_ERROR_CALLER_IS_NOT_OWNER](../express-localdb-error-messages/localdb-error-caller-is-not-owner.md) | API caller isn't LocalDB instance owner. |
| [LOCALDB_ERROR_INTERNAL_ERROR](../express-localdb-error-messages/localdb-error-internal-error.md) | An unexpected error occurred. See the event log for details. |

## Remarks

For a code sample that uses LocalDB API, see [SQL Server Express LocalDB reference](../sql-server-express-localdb-reference.md).

## Related content

- [SQL Server Express LocalDB header and version information](sql-server-express-localdb-header-and-version-information.md)
