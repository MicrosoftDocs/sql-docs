---
title: "LocalDBFormatMessage Function"
description: LocalDBFormatMessage returns the localized textual description for the specified SQL Server Express LocalDB error.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.topic: "reference"
apilocation: "sqluserinstance.dll"
apiname: "LocalDBFormatMessage"
apitype: "DLLExport"
---
# LocalDBFormatMessage function

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the localized textual description for the specified SQL Server Express LocalDB error.

**Header file:** `msoledbsql.h`

## Syntax

```cpp
HRESULT LocalDBFormatMessage(
           HRESULT hrLocalDB ,
           DWORD dwFlags ,
           DWORD dwLanguageId ,
           LPWSTR wszMessage ,
           LPDWORD lpcchMessage
);
```

## Arguments

#### *hrLocalDB*

[Input] The LocalDB error code.

#### *dwFlags*

[Input] The flags specifying the behavior of this function.

Available flags:

#### LOCALDB_TRUNCATE_ERR_MESSAGE

If the input buffer is too short, the error message is truncated to fit the buffer.

#### *dwLanguageId*

[Input] The language desired (LANGID) or 0, in which case the Win32 FormatMessage language order is used.

#### *wszMessage*

[Output] The buffer to store the LocalDB error message.

#### *lpcchMessage*

[Input/Output] On input, this option contains the size of the *wszMessage* buffer in characters. On output, if the given buffer size is too small, contains the buffer size required in characters, including any trailing nulls. If the function succeeds, this option contains the number of characters in the message, excluding any trailing nulls.

## Returns

`S_OK`: The function succeeded.

| Error | Description |
| --- | --- |
| [LOCALDB_ERROR_NOT_INSTALLED](../express-localdb-error-messages/localdb-error-not-installed.md) | SQL Server Express LocalDB isn't installed on the computer. |
| [LOCALDB_ERROR_INVALID_PARAMETER](../express-localdb-error-messages/localdb-error-invalid-parameter.md) | One or more specified input parameters are invalid. |
| [LOCALDB_ERROR_UNKNOWN_ERROR_CODE](../express-localdb-error-messages/localdb-error-unknown-error-code.md) | The requested message doesn't exist. |
| [LOCALDB_ERROR_UNKNOWN_LANGUAGE_ID](../express-localdb-error-messages/localdb-error-unknown-language-id.md) | The message isn't available in the requested language. |
| [LOCALDB_ERROR_INSUFFICIENT_BUFFER](../express-localdb-error-messages/localdb-error-insufficient-buffer.md) | The input buffer *wszMessage* is too short, and truncation isn't requested. |
| [LOCALDB_ERROR_INTERNAL_ERROR](../express-localdb-error-messages/localdb-error-internal-error.md) | An unexpected error occurred. See the event log for details. |

## Remarks

For a code sample that uses LocalDB API, see [SQL Server Express LocalDB reference](../sql-server-express-localdb-reference.md).

## Related content

- [SQL Server Express LocalDB header and version information](sql-server-express-localdb-header-and-version-information.md)
