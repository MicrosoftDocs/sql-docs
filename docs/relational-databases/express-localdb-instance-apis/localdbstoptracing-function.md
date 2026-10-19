---
title: "LocalDBStopTracing Function"
description: LocalDBStopTracing disables tracing of API calls for all the SQL Server Express LocalDB instances owned by the current Windows user.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 07/14/2025
ms.service: sql
ms.topic: "reference"
apilocation: "sqluserinstance.dll"
apiname: "LocalDBStopTracing"
apitype: "DLLExport"
---
# LocalDBStopTracing function

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Disables tracing of API calls for all the SQL Server Express LocalDB instances owned by the current Windows user.

**Header file:** `msoledbsql.h`

## Syntax

```cpp
HRESULT LocalDBStopTracing();
```

## Returns

`S_OK`: The function succeeded.

| Error | Description |
| --- | --- |
| [LOCALDB_ERROR_INTERNAL_ERROR](../express-localdb-error-messages/localdb-error-internal-error.md) | An unexpected error occurred. See the event log for details. |

## Remarks

For a code sample that uses LocalDB API, see [SQL Server Express LocalDB reference](../sql-server-express-localdb-reference.md).

## Related content

- [SQL Server Express LocalDB header and version information](sql-server-express-localdb-header-and-version-information.md)
