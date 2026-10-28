---
title: ODBC Diagnostics
description: Learn how to read ODBC return codes and diagnostic records and choose the right tracing layer.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: 10/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
helpviewer_keywords:
  - "ODBC diagnostics"
  - "SQLGetDiagRec"
  - "ODBC tracing"
---
# ODBC diagnostics

Use this guidance with Microsoft ODBC Driver 17 or 18. It describes the ODBC diagnostic contract; it doesn't define version-specific native error mappings.

An ODBC function returns a code that describes the outcome of that call. Diagnostic records provide more detail. The application programming interface (API) can report records from the Driver Manager, the driver, the data source, or more than one component. Don't treat the return code, SQLSTATE, native error code, and message as interchangeable.

## Interpret the return code

Check the return code before processing output or deciding whether to retry. For `SQL_ERROR` and `SQL_SUCCESS_WITH_INFO`, retrieve diagnostics from the handle used by the function. Other return codes can also have diagnostic records when the function contract allows them.

| Return code | Meaning and response |
| --- | --- |
| `SQL_SUCCESS` | The function completed successfully. Check the function's output contract. |
| `SQL_SUCCESS_WITH_INFO` | The function completed with warning information. Retrieve every diagnostic record and check whether output is complete, converted, or truncated. |
| `SQL_ERROR` | The function failed. Retrieve every diagnostic record before calling another function on the same handle. |
| `SQL_INVALID_HANDLE` | The function detected a null handle or a handle of the wrong type. ODBC doesn't provide diagnostic records for this return code. Check the function reference for additional handle requirements. |
| `SQL_NO_DATA` | No more data is available for the operation. Check the function reference because the meaning and any status records depend on the function. |

For more information about the complete return-code contract, see [ODBC return codes](../../odbc/reference/develop-app/return-codes-odbc.md).

## Retrieve every diagnostic record

Call `SQLGetDiagRec` for status records starting at record number 1. Continue until the function returns `SQL_NO_DATA`, or read the record count from header record 0 by calling `SQLGetDiagField` with `SQL_DIAG_NUMBER`. `SQLGetDiagRec` returns a record's SQLSTATE, native error code, and message text. Use `SQLGetDiagField` when you need other header or record fields.

Don't stop after the first record. An ODBC component can add a status record with information about its own layer while preserving diagnostics from another component. The record order and contents can help identify where an error originated.

Collect the records promptly. A later ODBC call that uses the same handle replaces its diagnostic area, except calls to `SQLGetDiagRec`, `SQLGetDiagField`, and the legacy `SQLError` function. Use `SQLGetDiagRec` and `SQLGetDiagField` in new code.

`SQLGetDiagRec` can return `SQL_SUCCESS_WITH_INFO` when its message buffer is too small. The record was returned, but its message text was truncated. Check the reported text length and retrieve the message with a larger buffer if you need the full text. Don't confuse this return code from `SQLGetDiagRec` with the return code from the original ODBC function.

For more information about diagnostic retrieval, see [Diagnostic records](../../odbc/reference/develop-app/diagnostic-records.md), [Using SQLGetDiagRec and SQLGetDiagField](../../odbc/reference/develop-app/using-sqlgetdiagrec-and-sqlgetdiagfield.md), and the [SQLGetDiagRec](../../odbc/reference/syntax/sqlgetdiagrec-function.md) and [SQLGetDiagField](../../odbc/reference/syntax/sqlgetdiagfield-function.md) references.

### Collect diagnostic records in C++

Use this C++11 helper with an existing ODBC handle. It calls `SQLGetDiagRecW` to collect SQLSTATE, native error code, and Unicode message text in memory without logging the messages.

```cpp
#ifdef _WIN32
#include <windows.h>
#endif
#include <sql.h>
#include <sqlext.h>
#include <algorithm>
#include <limits>
#include <stdexcept>
#include <string>
#include <utility>
#include <vector>

struct DiagnosticRecord {
    std::string sqlstate;
    SQLINTEGER native_error;
    std::vector<SQLWCHAR> message;
    bool message_truncated;
};

std::vector<DiagnosticRecord> ReadDiagnostics(SQLSMALLINT type, SQLHANDLE handle) {
    const SQLSMALLINT limit = (std::numeric_limits<SQLSMALLINT>::max)();
    std::vector<DiagnosticRecord> records;
    for (SQLINTEGER number = 1; ; ++number) {
        if (number > limit) {
            throw std::runtime_error("Diagnostic record number exceeds SQLSMALLINT.");
        }
        SQLWCHAR state[6] = {};
        SQLINTEGER native = 0;
        std::vector<SQLWCHAR> message(256);
        auto read = [&]() {
            return SQLGetDiagRecW(type, handle, static_cast<SQLSMALLINT>(number),
                state, &native, message.data(),
                static_cast<SQLSMALLINT>(message.size()), nullptr);
        };

        SQLRETURN result = read();
        if (result == SQL_NO_DATA) {
            return records;
        }
        if (result == SQL_SUCCESS_WITH_INFO) {
            message.resize(limit);
            result = read(); // Retrieve the same record, not the next one.
        }
        if (result != SQL_SUCCESS && result != SQL_SUCCESS_WITH_INFO) {
            throw std::runtime_error("SQLGetDiagRecW couldn't retrieve a diagnostic record.");
        }
        message.erase(std::find(message.begin(), message.end(), SQLWCHAR{0}), message.end());
        records.push_back({
            std::string(state, state + 5),
            native,
            std::move(message),
            result == SQL_SUCCESS_WITH_INFO
        });
    }
}
```

The helper retries a truncated message with the largest positive buffer length that `SQLGetDiagRecW` accepts. If the message still doesn't fit, it retains the available text and sets `message_truncated`. It continues to the next record instead of treating that truncation as the end of the diagnostic list. It throws if retrieval fails or the record number exceeds the API's `SQLSMALLINT` range.

Each message contains `SQLWCHAR` code units without the null terminator. Don't reinterpret them as `wchar_t`, because the types can have different widths. Convert them to your application's text encoding when you need to process or display the message.

Call the helper immediately after the operation that returns an error or warning. This fragment assumes that `statement` is a valid, prepared statement handle and that asynchronous execution is disabled:

```cpp
#include <iostream>

SQLRETURN ExecuteWithDiagnostics(SQLHSTMT statement) {
    SQLRETURN result = SQLExecute(statement);
    if (result == SQL_ERROR || result == SQL_SUCCESS_WITH_INFO) {
        const auto records = ReadDiagnostics(SQL_HANDLE_STMT, statement);
        for (const auto& record : records) {
            std::cerr << "SQLExecute: SQLSTATE=" << record.sqlstate
                      << " native_error=" << record.native_error
                      << " message_truncated=" << record.message_truncated << '\n';
        }
    } else if (result == SQL_INVALID_HANDLE) {
        throw std::runtime_error("SQLExecute returned SQL_INVALID_HANDLE.");
    }
    return result;
}
```

The wrapper preserves the original return code, including `SQL_SUCCESS_WITH_INFO`. Apply the same pattern to other ODBC calls, using the handle type and handle associated with that operation. Handle other return codes according to that function's contract.

The output deliberately omits `record.message`. If you need message text in a log, redact it according to your application's data policy before writing it. The helper doesn't sanitize the messages held in memory.

## Read the diagnostic fields

The fields answer different questions:

| Field | What it tells you |
| --- | --- |
| SQLSTATE | A five-character code that identifies a standardized diagnostic condition. Its class is the first two characters. |
| Native error code | A value specific to the data source or ODBC component. It's not a universal error number, and you shouldn't assume it is a SQL Server error number. |
| Message text | A human-readable explanation. Read it with the SQLSTATE and native error code; don't parse it as a stable machine-readable contract. |

The Driver Manager, driver, and data source can each generate diagnostics. A component that adds a record must not replace or mask another component's error or warning. A native error code might be unavailable or have no useful meaning for a component-generated diagnostic. For more information about the component rules, see [Diagnostic handling rules](../../odbc/reference/develop-app/diagnostic-handling-rules.md). For more information about SQLSTATE classes and values, see [SQLSTATEs](../../odbc/reference/develop-app/sqlstates.md).

Use the records together with the function return code and operation. For example, `SQL_SUCCESS_WITH_INFO` means the function completed, but it doesn't guarantee that every output value is complete. A truncation warning requires you to check the returned length and decide whether to retrieve the remaining data. For more information, see [Data length, buffer length, and truncation](../../odbc/reference/develop-app/data-length-buffer-length-and-truncation.md).

## Collect safe evidence

For a support case, record the function name, handle type, return code, and every diagnostic record. Include the driver and Driver Manager versions, operating system, architecture, and data source type. Redact credentials, access tokens, connection-string secrets, query text, parameter values, and personal or regulated data before saving or sharing output. Diagnostic messages and trace logs can contain application data.

Limit tracing to the shortest reproduction you need. Store logs in a directory accessible only to the account running the application, and use an explicit log path instead of a shared or temporary default. Stop tracing when the reproduction ends, then remove the trace file when you no longer need it.

## Choose the tracing layer

Application diagnostics and tracing answer different questions:

| Layer | What it captures | Use it for |
| --- | --- | --- |
| Application diagnostics | Return codes and diagnostic records from individual ODBC calls | Identify the function that failed or returned a warning, and preserve all its diagnostic fields. |
| ODBC Driver Manager trace | ODBC API calls between the application, Driver Manager, and driver | See which calls the application made and what return codes came back. Trace arguments can expose sensitive values. |
| Driver-specific trace | Internal driver events, when the driver documents a supported control | Investigate driver behavior that an API trace can't show. Don't assume a Driver Manager trace enables this facility. |

On Windows, the ODBC Driver Manager supports tracing through the **Tracing** tab in ODBC Data Source Administrator and the `SQL_ATTR_TRACE` and `SQL_ATTR_TRACEFILE` attributes. Tracing is per-user by default. Machine-wide tracing requires local administrator permissions and can capture calls from applications running under other local identities. Prefer per-user tracing, and stop it as soon as the reproduction ends. For more information, see [Setting tracing options](../../odbc/admin/setting-tracing-options.md), [Enabling tracing](../../odbc/reference/develop-app/enabling-tracing.md), and [SQLSetConnectAttr](../../odbc/reference/syntax/sqlsetconnectattr-function.md).

On Linux and macOS, see [Data access tracing with the ODBC Driver on Linux and macOS](linux-mac/data-access-tracing-with-the-odbc-driver-on-linux.md) for the unixODBC Driver Manager configuration.

The existing Linux and macOS [Programming Guidelines](linux-mac/programming-guidelines.md) retain a historical statement that built-in diagnostics (BID) tracing wasn't verified before ODBC Driver 17.3. That note doesn't describe a current driver-specific trace control. The unixODBC procedure captures Driver Manager API calls, not BID events.

## Related content

- [ODBC tracing](../../odbc/reference/develop-app/tracing.md)
- [ODBC diagnostic tools](../../odbc/reference/develop-app/diagnostic-tools.md)
- [Data access tracing with the ODBC Driver on Linux and macOS](linux-mac/data-access-tracing-with-the-odbc-driver-on-linux.md)
