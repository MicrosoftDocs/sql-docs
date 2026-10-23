---
title: "LocalDBDeleteInstance Function"
description: LocalDBDeleteInstance removes the specified SQL Server Express LocalDB instance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.topic: "reference"
apilocation: "sqluserinstance.dll"
apiname: "LocalDBDeleteInstance"
apitype: "DLLExport"
---
# LocalDBDeleteInstance function

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes the specified SQL Server Express LocalDB instance.

**Header file:** `msoledbsql.h`

## Syntax

```cpp
HRESULT LocalDBDeleteInstance(
           PCWSTR pInstanceName ,
           DWORD dwFlags
);
```

## Arguments

#### *pInstanceName*

[Input] The name of the LocalDB instance to remove.

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
| [LOCALDB_ERROR_INSTANCE_BUSY](../express-localdb-error-messages/localdb-error-instance-busy.md) | The specified instance is running. |
| [LOCALDB_ERROR_WAIT_TIMEOUT](../express-localdb-error-messages/localdb-error-wait-timeout.md) | A timeout occurred while trying to acquire synchronization locks. |
| [LOCALDB_ERROR_INSTANCE_FOLDER_PATH_TOO_LONG](../express-localdb-error-messages/localdb-error-instance-folder-path-too-long.md) | The path where the instance should be stored is longer than MAX_PATH. |
| [LOCALDB_ERROR_CANNOT_GET_USER_PROFILE_FOLDER](../express-localdb-error-messages/localdb-error-cannot-get-user-profile-folder.md) | A user profile folder can't be retrieved. |
| [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_FOLDER](../express-localdb-error-messages/localdb-error-cannot-access-instance-folder.md) | An instance folder can't be accessed. |
| [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_REGISTRY](../express-localdb-error-messages/localdb-error-cannot-access-instance-registry.md) | An instance registry can't be accessed. |
| [LOCALDB_ERROR_CANNOT_MODIFY_INSTANCE_REGISTRY](../express-localdb-error-messages/localdb-error-cannot-modify-instance-registry.md) | An instance registry can't be modified. |
| [LOCALDB_ERROR_INSTANCE_CONFIGURATION_CORRUPT](../express-localdb-error-messages/localdb-error-instance-configuration-corrupt.md) | An instance configuration is corrupted. |
| [LOCALDB_ERROR_CALLER_IS_NOT_OWNER](../express-localdb-error-messages/localdb-error-caller-is-not-owner.md) | API caller isn't Local Database instance owner. |
| [LOCALDB_ERROR_INTERNAL_ERROR](../express-localdb-error-messages/localdb-error-internal-error.md) | An unexpected error occurred. See the event log for details. |

## Remarks

For a code sample that uses LocalDB API, see [SQL Server Express LocalDB reference](../sql-server-express-localdb-reference.md).

## Related content

- [SQL Server Express LocalDB header and version information](sql-server-express-localdb-header-and-version-information.md)
