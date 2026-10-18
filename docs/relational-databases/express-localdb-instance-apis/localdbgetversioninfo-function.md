---
title: "LocalDBGetVersionInfo Function"
description: LocalDBGetVersionInfo returns information for the specified SQL Server Express LocalDB version.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.topic: "reference"
apilocation: "sqluserinstance.dll"
apiname: "LocalDBGetVersionInfo"
apitype: "DLLExport"
---
# LocalDBGetVersionInfo function

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information for the specified SQL Server Express LocalDB version, such as whether it exists and the full LocalDB version number (including build and release numbers).

The information is returned in the form of a `struct` named `LocalDBVersionInfo`, which has the following definition.

```cpp
typedef struct _LocalDBVersionInfo
{
      // Contains the size of the LocalDBVersionInfo struct
      DWORD  cbLocalDBVersionInfoSize;

      // Holds the version name
      TLocalDBVersionwszVersion;

      // TRUE if the instance files exist on disk, FALSE otherwise
      BOOL   bExists;

      // Holds the LocalDB version for the instance in the format: major.minor.build.revision
      DWORD  dwMajor;
      DWORD  dwMinor;
      DWORD  dwBuild;
      DWORD  dwRevision;
} LocalDBVersionInfo;
```

**Header file:** `msoledbsql.h`

## Syntax

```cpp
HRESULT LocalDBGetVersionInfo(
           PCWSTR wszVersionName ,
           PLocalDBVersionInfo pVersionInfo ,
           DWORD dwVersionInfoSize);
```

## Arguments

#### *wszVersionName*

[Input] The LocalDB version name.

#### *pVersionInfo*

[Output] The buffer to store the information about the LocalDB version.

#### *dwVersionInfoSize*

[Input] Holds the size of the *VersionInfo* buffer.

## Returns

`S_OK`: The function succeeded.

| Error | Description |
| --- | --- |
| [LOCALDB_ERROR_NOT_INSTALLED](../express-localdb-error-messages/localdb-error-not-installed.md) | SQL Server Express LocalDB isn't installed on the computer. |
| [LOCALDB_ERROR_INVALID_PARAMETER](../express-localdb-error-messages/localdb-error-invalid-parameter.md) | One or more specified input parameters are invalid. |
| [LOCALDB_ERROR_UNKNOWN_VERSION](../express-localdb-error-messages/localdb-error-unknown-version.md) | The specified LocalDB version doesn't exist. |
| [LOCALDB_ERROR_INTERNAL_ERROR](../express-localdb-error-messages/localdb-error-internal-error.md) | An unexpected error occurred. See the event log for details. |

## Details

The rationale behind the introduction of the `struct` size argument (*lpVersionInfoSize*) is to enable the API to return different versions of the `LocalDBVersionInfostruct`, effectively enabling forward and backward compatibility.

If the `struct` size argument (*lpVersionInfoSize*) matches the size of a known version of the `LocalDBVersionInfostruct`, that version of the `struct` is returned. Otherwise, `LOCALDB_ERROR_INVALID_PARAMETER` is returned.

A typical example of `LocalDBGetVersionInfo` API usage looks like this:

```cpp
LocalDBVersionInfo vi;
LocalDBVersionInfo(L"11.0", &vi, sizeof(LocalDBVersionInfo));
```

## Remarks

For a code sample that uses LocalDB API, see [SQL Server Express LocalDB reference](../sql-server-express-localdb-reference.md).

## Related content

- [SQL Server Express LocalDB header and version information](sql-server-express-localdb-header-and-version-information.md)
