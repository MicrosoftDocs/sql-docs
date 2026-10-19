---
title: "LocalDBCreateInstance Function"
description: LocalDBCreateInstance creates a new SQL Server Express LocalDB instance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.topic: "reference"
apilocation: "sqluserinstance.dll"
apiname: "LocalDBCreateInstance"
apitype: "DLLExport"
---
# LocalDBCreateInstance function

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates a new SQL Server Express LocalDB instance.

**Header file:** `msoledbsql.h`

## Syntax

```cpp
HRESULT LocalDBCreateInstance(
           PCWSTR wszVersion ,
           PCWSTR pInstanceName ,
           DWORD dwFlags
);
```

## Arguments

#### *wszVersion*

[Input] The LocalDB version, for example 11.0 or 11.0.1094.2.

#### *pInstanceName*

[Input] The name for the LocalDB instance to create.

#### *dwFlags*

[Input] Reserved for future use. Currently should be set to 0.

## Returns

`S_OK`: The function succeeded.

| Error | Description |
| --- | --- |
| [LOCALDB_ERROR_NOT_INSTALLED](../express-localdb-error-messages/localdb-error-not-installed.md) | SQL Server Express LocalDB isn't installed on the computer. |
| [LOCALDB_ERROR_INVALID_PARAMETER](../express-localdb-error-messages/localdb-error-invalid-parameter.md) | One or more specified input parameters are invalid. |
| [LOCALDB_ERROR_INVALID_INSTANCE_NAME](../express-localdb-error-messages/localdb-error-invalid-instance-name.md) | The specified instance name is invalid. |
| [LOCALDB_ERROR_INSTANCE_FOLDER_PATH_TOO_LONG](../express-localdb-error-messages/localdb-error-instance-folder-path-too-long.md) | The path where the instance should be stored is longer than MAX_PATH. |
| [LOCALDB_ERROR_INSTANCE_EXISTS_WITH_LOWER_VERSION](../express-localdb-error-messages/localdb-error-instance-exists-with-lower-version.md) | The specified instance already exists but its version is lower than requested. |
| [LOCALDB_ERROR_UNKNOWN_VERSION](../express-localdb-error-messages/localdb-error-unknown-version.md) | The specified version isn't available. |
| [LOCALDB_ERROR_VERSION_REQUESTED_NOT_INSTALLED](../express-localdb-error-messages/localdb-error-version-requested-not-installed.md) | The specified patch level isn't installed. |
| [LOCALDB_ERROR_CANNOT_CREATE_INSTANCE_FOLDER](../express-localdb-error-messages/localdb-error-cannot-create-instance-folder.md) | A folder can't be created under `%userprofile%`. |
| [LOCALDB_ERROR_CANNOT_GET_USER_PROFILE_FOLDER](../express-localdb-error-messages/localdb-error-cannot-get-user-profile-folder.md) | A user profile folder can't be retrieved. |
| [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_FOLDER](../express-localdb-error-messages/localdb-error-cannot-access-instance-folder.md) | An instance folder can't be accessed. |
| [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_REGISTRY](../express-localdb-error-messages/localdb-error-cannot-access-instance-registry.md) | An instance registry can't be accessed. |
| [LOCALDB_ERROR_CANNOT_MODIFY_INSTANCE_REGISTRY](../express-localdb-error-messages/localdb-error-cannot-modify-instance-registry.md) | An instance registry can't be modified. |
| [LOCALDB_ERROR_SQL_SERVER_STARTUP_FAILED](../express-localdb-error-messages/localdb-error-sql-server-startup-failed.md) | A SQL Server process is started but SQL Server startup failed. |
| [LOCALDB_ERROR_INSTANCE_CONFIGURATION_CORRUPT](../express-localdb-error-messages/localdb-error-instance-configuration-corrupt.md) | An instance configuration is corrupted. |
| [LOCALDB_ERROR_INTERNAL_ERROR](../express-localdb-error-messages/localdb-error-internal-error.md) | An unexpected error occurred. See the event log for details. |

## Remarks

If a fully functional LocalDB instance with the specified name already exists and its version is equal to or higher than requested, the result is `S_OK`.

In cases when an existing instance becomes corrupted, subsequent calls to the `LocalDBCreateInstance` API method fail. Corrupted instances must be fixed manually or explicitly deleted before they can be used again.

For a code sample that uses LocalDB API, see [SQL Server Express LocalDB reference](../sql-server-express-localdb-reference.md).

## Related content

- [SQL Server Express LocalDB header and version information](sql-server-express-localdb-header-and-version-information.md)
