---
title: "LocalDBGetInstanceInfo Function"
description: LocalDBGetInstanceInfo returns information for the specified SQL Server Express LocalDB instance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.topic: "reference"
apilocation: "sqluserinstance.dll"
apiname: "LocalDBGetInstanceInfo"
apitype: "DLLExport"
---
# LocalDBGetInstanceInfo function

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information for the specified SQL Server Express LocalDB instance, such as whether it exists, the LocalDB version it uses, whether it's running, and so on.

The information is returned in a `struct` named `LocalDBInstanceInfo`, which has the following definition.

```cpp
typedef struct _LocalDBInstanceInfo
{
      // Contains the size of the LocalDBInstanceInfo struct
      DWORD  cbLocalDBInstanceInfoSize;

      // Holds the instance name
      TLocalDBInstanceNamewszInstanceName;

      // TRUE if the instance files exist on disk, FALSE otherwise
      BOOL   bExists;

      // TRUE if the instance configuration registry is corrupted, FALSE otherwise
      BOOLbConfigurationCorrupted;

      // TRUE if the instance is running at the moment, FALSE otherwise
      BOOL   bIsRunning;

      // Holds the LocalDB version for the instance in the format: major.minor.build.revision
      DWORD  dwMajor;
      DWORD  dwMinor;
      DWORD  dwBuild;
      DWORD  dwRevision;

      // Holds the date and time when the instance was started for the last time
      FILETIME ftLastStartUTC;

      // Holds the name of the TDS named pipe to connect to the instance
      WCHARwszConnection;

      // TRUE if the instance is shared, FALSE otherwise
      BOOLbIsShared;

      // Holds the shared name for the instance (if the instance is shared)
      TLocalDBInstanceNamewszSharedInstanceName;

      // Holds the SID of the instance owner (if the instance is shared)
      WCHARwszOwnerSID;

      // TRUE if the instance is Automatic, FALSE otherwise
      BOOLbIsAutomatic;
} LocalDBInstanceInfo;
```

**Header file:** `msoledbsql.h`

## Syntax

```cpp
HRESULT LocalDBGetInstanceInfo(
           PCWSTR wszInstanceName ,
           PLocalDBInstanceInfo pInstanceInfo ,
           DWORD dwInstanceInfoSize
);
```

## Arguments

#### *wszInstanceName*

[Input] The instance name.

#### *pInstanceInfo*

[Output] The buffer to store the information about the LocalDB instance.

#### *dwInstanceInfoSize*

[Input] Holds the size of the *InstanceInfo* buffer.

## Returns

`S_OK`: The function succeeded.

| Error | Description |
| --- | --- |
| [LOCALDB_ERROR_NOT_INSTALLED](../express-localdb-error-messages/localdb-error-not-installed.md) | SQL Server Express LocalDB isn't installed on the computer. |
| [LOCALDB_ERROR_INVALID_PARAMETER](../express-localdb-error-messages/localdb-error-invalid-parameter.md) | One or more specified input parameters are invalid. |
| [LOCALDB_ERROR_INVALID_INSTANCE_NAME](../express-localdb-error-messages/localdb-error-invalid-instance-name.md) | The specified instance name is invalid. |
| [LOCALDB_ERROR_UNKNOWN_INSTANCE](../express-localdb-error-messages/localdb-error-unknown-instance.md) | The instance doesn't exist. |
| [LOCALDB_ERROR_INSTANCE_FOLDER_PATH_TOO_LONG](../express-localdb-error-messages/localdb-error-instance-folder-path-too-long.md) | The path where the instance should be stored is longer than `MAX_PATH`. |
| [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_FOLDER](../express-localdb-error-messages/localdb-error-cannot-access-instance-folder.md) | An instance folder can't be accessed. |
| [LOCALDB_ERROR_CANNOT_ACCESS_INSTANCE_REGISTRY](../express-localdb-error-messages/localdb-error-cannot-access-instance-registry.md) | An instance registry can't be accessed. |
| [LOCALDB_ERROR_INSTANCE_CONFIGURATION_CORRUPT](../express-localdb-error-messages/localdb-error-instance-configuration-corrupt.md) | An instance configuration is corrupted. |
| [LOCALDB_ERROR_INTERNAL_ERROR](../express-localdb-error-messages/localdb-error-internal-error.md) | An unexpected error occurred. See the event log for details. |

## Details

The rationale behind the introduction of the `struct` size argument (*lpInstanceInfoSize*) is to enable the API to return different versions of the `LocalDBInstanceInfostruct`, effectively enabling forward and backward compatibility.

If the `struct` size argument (*lpInstanceInfoSize*) matches the size of a known version of the `LocalDBInstanceInfostruct`, that version of the `struct` is returned. Otherwise, `LOCALDB_ERROR_INVALID_PARAMETER` is returned.

A typical example of `LocalDBGetInstanceInfo` API usage looks like this:

```cpp
LocalDBInstanceInfo ii;
LocalDBInstanceInfo(L"Test", &ii, sizeof(LocalDBInstanceInfo));
```

For a code sample that uses LocalDB API, see [SQL Server Express LocalDB reference](../sql-server-express-localdb-reference.md).

## Related content

- [SQL Server Express LocalDB header and version information](sql-server-express-localdb-header-and-version-information.md)
