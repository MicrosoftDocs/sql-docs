---
title: Develop C and C++ Applications with the ODBC Driver
description: Set up headers, libraries, and include order for the Microsoft ODBC Driver for SQL Server on Windows, Linux, and macOS, then choose between asynchronous execution and threads.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, sunilbs, mcimfl
ms.date: 09/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
helpviewer_keywords:
  - "ODBC Driver for SQL Server, header files"
  - "ODBC Driver for SQL Server, library files"
  - "msodbcsql.h"
  - "msodbcsql18.lib"
  - "ODBC applications, creating"
  - "asynchronous operations [ODBC Driver for SQL Server]"
  - "multithreaded applications [ODBC Driver for SQL Server]"
  - "SQLCancel function"
---
# Develop C and C++ applications with the ODBC driver

[!INCLUDE [ODBC_Current_Version](../../includes/odbc-latest-release.md)]

To call the ODBC API from C or C++, include `sql.h`, `sqlext.h`, and `sqltypes.h`, then link against the driver manager import library. To use the SQL Server extensions that the Microsoft ODBC Driver for SQL Server adds on top of the ODBC standard, also include `msodbcsql.h`, and include it after the core ODBC headers.

**Applies to**: Microsoft ODBC Driver 18 for SQL Server on Windows, Linux, and macOS. Version 17 uses the same header name with a `170` install path and a `msodbcsql17` library name.

## Headers and libraries

The platform provides the core ODBC headers and the driver manager, not the driver package. On Windows, they ship in the Windows SDK. On Linux and macOS, they ship in the unixODBC development package. The driver SDK provides only `msodbcsql.h` and the bulk copy import library.

| What you're calling | Headers | Windows | Linux | macOS |
| --- | --- | --- | --- | --- |
| ODBC API | `sql.h`, `sqlext.h`, `sqltypes.h` | `odbc32.lib` | `-lodbc` | `-lodbc` |
| ODBC API, Unicode entry points | Add `sqlucode.h` | `odbc32.lib` | `-lodbc` | `-lodbc` |
| ODBC installer API | Add `odbcinst.h` | `odbccp32.lib` | `-lodbcinst` | `-lodbcinst` |
| SQL Server driver extensions | Add `msodbcsql.h` | No extra library | No extra library | No extra library |
| Bulk copy (`bcp_*`) functions | Add `msodbcsql.h` | `msodbcsql18.lib` | `-lmsodbcsql-18` | `-lmsodbcsql.18` |

The bulk copy link name differs by platform because the file names differ. On Linux, `-lmsodbcsql-18` resolves through a `libmsodbcsql-18.so` symlink in `/usr/lib`, which the linker already searches, so you don't need `-L`. On macOS, the driver ships as `libmsodbcsql.18.dylib`, which `-lmsodbcsql.18` matches, but Homebrew's library directory isn't on the default search path on Apple silicon. Add `-L$(brew --prefix)/lib` when you link the bulk copy functions.

Only the bulk copy functions need the driver's own library. Connection attributes, statement attributes, column attributes, and SQL Server type identifiers are macros and type definitions, so including `msodbcsql.h` is enough for them.

The installer API is a separate library from the ODBC API. Calling a function such as `SQLGetPrivateProfileString` without `-lodbcinst` on Linux or macOS fails at link time with an undefined reference, not at compile time.

To install the unixODBC development package that provides the core headers on Linux and macOS, see [Install the unixODBC driver manager](linux-mac/installing-the-driver-manager.md).

## Include wchar.h before msodbcsql.h in C code on Linux and macOS

The Linux and macOS versions of `msodbcsql.h` declare the Always Encrypted keystore provider interface by using `wchar_t`, but they don't include a header that defines this type. In C++, `wchar_t` is a keyword, so C++ translation units build without needing extra headers. In C, `wchar_t` is a typedef, so you need to include `<wchar.h>` first in a C translation unit:

```c
#include <wchar.h>
```

If you don't include `<wchar.h>`, the compiler reports `unknown type name 'wchar_t'` errors from inside `msodbcsql.h`. Adding the include is harmless on Windows, so add it to the shared source rather than placing it behind a platform guard.

## Include msodbcsql.h after the core ODBC headers

Everything that `msodbcsql.h` defines beyond the driver name macros is inside an `#ifdef ODBCVER` block, and `sql.h` is what defines `ODBCVER`. If you include `msodbcsql.h` first, the preprocessor skips that whole block and the header contributes nothing. The compiler doesn't issue a warning.

```c
/* Correct order. */
#ifdef _WIN32
#include <windows.h>
#endif

#include <wchar.h>
#include <sql.h>
#include <sqlext.h>
#include <sqltypes.h>
#include <msodbcsql.h>
```

Including `msodbcsql.h` before `sql.h` leaves everything inside the `ODBCVER` block undefined. The compiler reports the error at the point of use, not at the include:

```output
order-wrong.c(7): error C2065: 'SQL_COPT_SS_BCP': undeclared identifier
```

On Windows, you must include `windows.h` before the ODBC headers. The Windows SDK copies of `sqltypes.h` and `sql.h` use Windows types such as `DWORD` and `LONG`. `msodbcsql.h` wraps its SQL Server structures in `pshpack8.h` and `poppack.h`. Without `windows.h`, the build fails inside the SDK headers themselves.

## Where the SDK files are installed

| Platform | `msodbcsql.h` | Bulk copy library |
| --- | --- | --- |
| Windows | `%ProgramFiles%\Microsoft SQL Server\Client SDK\ODBC\180\SDK\Include` | `%ProgramFiles%\Microsoft SQL Server\Client SDK\ODBC\180\SDK\Lib\<architecture>\msodbcsql18.lib` |
| Linux | `/opt/microsoft/msodbcsql18/include` | `/opt/microsoft/msodbcsql18/lib64`, with a `/usr/lib/libmsodbcsql-18.so` symlink |
| macOS | `$(brew --prefix msodbcsql18)/include/msodbcsql18` | `$(brew --prefix)/lib/libmsodbcsql.18.dylib` |

On Windows, the `Lib` folder contains a subfolder for each processor architecture the installer placed on the machine, such as `x64`, `x86`, or `arm64`. Add the `Include` folder to the compiler's include path and the architecture subfolder to the linker's library path.

On Linux, the shared object is versioned, named like `libmsodbcsql-18.6.so.2.1`, and carries no `SONAME`. The package installs `/usr/lib/libmsodbcsql-18.so` pointing at it, which is what makes `-lmsodbcsql-18` resolve without a `-L` option. Link through that symlink rather than naming the versioned file, so a driver update doesn't break your build.

On macOS, Homebrew installs into its own prefix, which is `/opt/homebrew` on Apple silicon and `/usr/local` on Intel. Both prefixes are symlinks into the versioned Cellar directory. Use `brew --prefix msodbcsql18` and `brew --prefix unixodbc` in your build script instead of hardcoding either one.

The number in the path tracks the major driver version. Version 17 installs to `...\ODBC\170\SDK\` on Windows and `/opt/microsoft/msodbcsql17/` on Linux, and its import library is `msodbcsql17.lib`.

For the full file inventory per platform, see [System requirements, installation, and driver files (Windows)](windows/system-requirements-installation-and-driver-files.md), [Install the ODBC driver on Linux](linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md), and [Install the ODBC driver on macOS](linux-mac/install-microsoft-odbc-driver-sql-server-macos.md).

## Verify your build setup

This program compiles against the headers, links against the driver manager, and lists the drivers the driver manager can see. It doesn't connect, so it separates a build or registration problem from a network or credential problem.

```c
#include <stdio.h>
#include <wchar.h>

#ifdef _WIN32
#include <windows.h>
#endif

#include <sql.h>
#include <sqlext.h>
#include <sqltypes.h>
#include <msodbcsql.h>

static void PrintDiagnostics(SQLSMALLINT handleType, SQLHANDLE handle)
{
    SQLCHAR state[6];
    SQLINTEGER native;
    SQLCHAR message[SQL_MAX_MESSAGE_LENGTH];
    SQLSMALLINT length;

    for (SQLSMALLINT record = 1;
         SQL_SUCCEEDED(SQLGetDiagRec(handleType, handle, record, state, &native,
                                     message, sizeof(message), &length));
         ++record)
    {
        fprintf(stderr, "  [%s] (%ld) %s\n", state, (long)native, message);
    }
}

int main(void)
{
    SQLHENV environment = SQL_NULL_HENV;
    SQLRETURN rc = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &environment);

    if (!SQL_SUCCEEDED(rc))
    {
        fprintf(stderr, "SQLAllocHandle for the environment failed.\n");
        return 1;
    }

    rc = SQLSetEnvAttr(environment, SQL_ATTR_ODBC_VERSION,
                       (SQLPOINTER)SQL_OV_ODBC3_80, 0);
    if (!SQL_SUCCEEDED(rc))
    {
        fprintf(stderr, "SQLSetEnvAttr for SQL_OV_ODBC3_80 failed.\n");
        PrintDiagnostics(SQL_HANDLE_ENV, environment);
        SQLFreeHandle(SQL_HANDLE_ENV, environment);
        return 1;
    }

    printf("Driver name from msodbcsql.h: %s\n", SQLODBC_DRIVER_NAME);
    printf("Installed drivers:\n");

    SQLCHAR description[256];
    SQLSMALLINT descriptionLength = 0;
    SQLUSMALLINT direction = SQL_FETCH_FIRST;

    while (SQL_SUCCEEDED(SQLDrivers(environment, direction,
                                    description, sizeof(description), &descriptionLength,
                                    NULL, 0, NULL)))
    {
        printf("  %s\n", description);
        direction = SQL_FETCH_NEXT;
    }

    SQLFreeHandle(SQL_HANDLE_ENV, environment);
    return 0;
}
```

Build it as a narrow character program. `SQLODBC_DRIVER_NAME` expands to a wide string when `UNICODE` or `_UNICODE` is defined, which `printf` with `%s` can't take.

### [Windows](#tab/windows)

```console
cl /W4 /I "%ProgramFiles%\Microsoft SQL Server\Client SDK\ODBC\180\SDK\Include" odbc-build-check.c /link odbc32.lib
```

### [Linux](#tab/linux)

```bash
gcc -Wall -o odbc-build-check odbc-build-check.c \
    -I/opt/microsoft/msodbcsql18/include -lodbc
```

### [macOS](#tab/macos)

```bash
clang -Wall -o odbc-build-check odbc-build-check.c \
    -I"$(brew --prefix unixodbc)/include" \
    -I"$(brew --prefix msodbcsql18)/include/msodbcsql18" \
    -L"$(brew --prefix unixodbc)/lib" -lodbc
```

---

On Windows, `/W4` reports two `C4201: nonstandard extension used: nameless struct/union` warnings from the Windows SDK copy of `sqlext.h`. These warnings come from the SDK header, not from your code, and the build succeeds.

The first line reports the driver name compiled into your binary. The rest is the driver manager's own list, so a driver you expect to see and don't is a registration problem, not a build problem. Your list will differ, and it includes every installed ODBC driver, not just the SQL Server ones:

```output
Driver name from msodbcsql.h: ODBC Driver 18 for SQL Server
Installed drivers:
  SQL Server
  ODBC Driver 17 for SQL Server
  ODBC Driver 18 for SQL Server
  Microsoft Access Driver (*.mdb, *.accdb)
  Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)
  Microsoft Access Text Driver (*.txt, *.csv)
  Microsoft Access dBASE Driver (*.dbf, *.ndx, *.mdx)
```

Build the connection string from `SQLODBC_DRIVER_NAME` rather than from a string literal. The macro tracks the header you compiled against, so upgrading the SDK updates the driver name in one place.

## What msodbcsql.h adds to the ODBC API

`msodbcsql.h` extends the standard ODBC API with SQL Server specifics. Each family occupies a contiguous numeric range counted from a base constant. The ranges aren't unique across families, so the function you pass the value to is what tells them apart.

| Family | Base constant | Value |
| --- | --- | --- |
| Connection attributes for `SQLSetConnectAttr` | `SQL_COPT_SS_BASE` | 1200 |
| Statement attributes for `SQLSetStmtAttr` | `SQL_SOPT_SS_BASE` | 1225 |
| Column attributes for `SQLColAttribute` | `SQL_CA_SS_BASE` | 1200 |
| Information types for `SQLGetInfo` | `SQL_INFO_SS_FIRST` | 1199 |
| Diagnostic fields for `SQLGetDiagField` | `SQL_DIAG_SS_BASE` | -1150 |
| Diagnostic dynamic function codes | `SQL_DIAG_DFC_SS_BASE` | -200 |

The header also declares:

- Authentication attributes, including `SQL_COPT_SS_AUTHENTICATION` and `SQL_COPT_SS_ACCESS_TOKEN`, which carry Microsoft Entra ID settings and access tokens.
- SQL type identifiers in the range -150 to -199 for SQL Server types that ODBC doesn't define: `SQL_SS_VARIANT`, `SQL_SS_UDT`, `SQL_SS_XML`, `SQL_SS_TABLE`, `SQL_SS_TIME2`, `SQL_SS_TIMESTAMPOFFSET`, and `SQL_SS_VECTOR`. These name a *SQL* type, so you pass them where ODBC expects a SQL type, such as the `ParameterType` argument of `SQLBindParameter`.
- Three matching *C* types for the buffer side: `SQL_C_SS_TIME2`, `SQL_C_SS_TIMESTAMPOFFSET`, and `SQL_C_SS_VECTOR`. The other SQL Server types bind to a standard ODBC C type such as `SQL_C_BINARY` or `SQL_C_WCHAR`, so they have no `SQL_C_SS_*` counterpart.
- The structures the `SQL_C_SS_*` types bind to: `SQL_SS_TIME2_STRUCT`, `SQL_SS_TIMESTAMPOFFSET_STRUCT`, and `SQL_SS_VECTOR_STRUCT`.
- Bulk copy prototypes and macros, including `bcp_init`, `bcp_bind`, `bcp_sendrow`, `bcp_batch`, and `bcp_done`. The `BCP_ENCRYPT_OFF`, `BCP_ENCRYPT_ON`, and `BCP_ENCRYPT_STRICT` options are in the Windows header only.

Each platform ships its own copy of `msodbcsql.h`, and they don't all declare the same symbols. The `SQLPERF` structure and the performance connection attributes that fill it in, such as `SQL_COPT_SS_PERF_DATA` and `SQL_COPT_SS_PERF_QUERY`, are in the Windows header only. The Linux and macOS headers don't declare them, and the driver doesn't collect performance data on those platforms. See [Programming guidelines (Linux and macOS)](linux-mac/programming-guidelines.md).

For the connection string keywords these attributes correspond to, see [DSN and connection string keywords and attributes](dsn-connection-string-attribute.md). For Microsoft Entra ID setup, see [Use Microsoft Entra ID with the ODBC driver](using-azure-active-directory.md). For the **vector** type, see [Vector data type](vector-data-type.md).

## Choose between asynchronous execution and threads

Some ODBC functions can run either synchronously or asynchronously. In synchronous mode, the driver doesn't return control until the server answers. In asynchronous mode, the driver returns `SQL_STILL_EXECUTING` right away, and the application repeats the same call with the same arguments until it gets a different return code. Any other return code, including `SQL_ERROR`, means the operation finished.

Asynchronous mode has two forms, and you use one of them. Call `SQLGetInfo` with `SQL_ASYNC_MODE` to find out which one the driver supports. It returns `SQL_AM_STATEMENT` if the driver supports per-statement control, `SQL_AM_CONNECTION` if the setting applies to the whole connection, or `SQL_AM_NONE` if the driver doesn't run functions asynchronously at all.

The statement form turns asynchronous mode on for one statement handle. Every other statement on the connection stays synchronous, so you can run both kinds at the same time:

```c
SQLSetStmtAttr(hStmt, SQL_ATTR_ASYNC_ENABLE,
               (SQLPOINTER)SQL_ASYNC_ENABLE_ON, SQL_IS_INTEGER);
```

If `SQL_ASYNC_MODE` returned `SQL_AM_CONNECTION`, the statement attribute is read only and this call returns `SQL_ERROR` with SQLSTATE `HYC00`. Use the connection form instead.

The connection form turns asynchronous mode on for every statement handle you allocate on that connection afterward. Whether it also affects handles that already exist is driver defined, so set it before you allocate any statements:

```c
SQLSetConnectAttr(hDbc, SQL_ATTR_ASYNC_ENABLE,
                  (SQLPOINTER)SQL_ASYNC_ENABLE_ON, SQL_IS_INTEGER);
```

The call returns `SQL_ERROR` with SQLSTATE `HY010` if a function is still executing asynchronously on a statement for that connection. An open cursor on its own doesn't block the call. Passing `SQL_ASYNC_ENABLE_OFF` puts every statement on the connection back in synchronous mode.

To find how many asynchronous statements the driver supports at once on one connection, call `SQLGetInfo` with `SQL_MAX_ASYNC_CONCURRENT_STATEMENTS`. Microsoft ODBC Driver 18 for SQL Server returns 1, so plan on one outstanding asynchronous operation per connection and open more connections or use threads beyond that. See [Asynchronous execution (polling method)](../../odbc/reference/develop-app/asynchronous-execution-polling-method.md).

Threads are the other way to keep several operations in flight. ODBC requires drivers on multithreaded operating systems to be thread safe, so a thread can make a blocking ODBC call while other threads keep working. That avoids the polling loop and the repeated function calls that asynchronous mode needs. Give each thread its own statement handle. A driver is likely to serialize two threads that use the same handle at the same time, so sharing one costs you the concurrency. See [Multithreading](../../odbc/reference/develop-app/multithreading.md). Prefer threads for new code, and measure your own workload before converting asynchronous code that already works.

On Windows, the driver manager also supports the notification method, which removes the polling loop. You associate a Win32 event with the connection or statement handle. The function still returns `SQL_STILL_EXECUTING` immediately, and the driver manager signals the event when the operation completes. Polling is disabled in this mode: calling the original function again returns `SQL_ERROR` with SQLSTATE `IM017`. Call `SQLCompleteAsync` to retrieve the result instead. This needs driver manager version ODBC 3.81 and later versions, and the driver has to support it as well. Call `SQLGetInfo` with `SQL_ASYNC_NOTIFICATION` to check. The value you get back depends on the ODBC version your application declares: with Microsoft ODBC Driver 18 for SQL Server, an application that sets `SQL_ATTR_ODBC_VERSION` to `SQL_OV_ODBC3_80` gets `SQL_ASYNC_NOTIFICATION_CAPABLE`, and one that declares `SQL_OV_ODBC3` gets `SQL_ASYNC_NOTIFICATION_NOT_CAPABLE` from that same driver. Declare `SQL_OV_ODBC3_80` before you allocate the connection. See [Asynchronous execution (notification method)](../../odbc/reference/develop-app/asynchronous-execution-notification-method.md) and the [notification method sample](windows/asynchronous-execution-notification-method-sample.md).

### Cancel an outstanding operation

`SQLCancel` cancels an operation that's still running on a statement handle. Call it from another thread, or from the polling loop, passing the handle of the outstanding call.

Use `SQLCancel` only for that. To abandon a result set you no longer want to read, call `SQLCloseCursor` or `SQLMoreResults` instead.

## Migrate from sqlncli.h to msodbcsql.h

SQL Server Native Client is retired, so applications that use it should move to the Microsoft ODBC Driver for SQL Server. The API is the same ODBC API, so most of the work involves renaming build inputs and the driver name in the connection string.

| SQL Server Native Client | Microsoft ODBC Driver 18 for SQL Server |
| --- | --- |
| `sqlncli.h` | `msodbcsql.h` |
| `sqlncli11.lib` | `msodbcsql18.lib` |
| `sqlncli11.dll` | `msodbcsql18.dll` |
| `Driver={SQL Server Native Client 11.0}` | `Driver={ODBC Driver 18 for SQL Server}` |
| `SQLNCLI_VER` | `SQLODBC_VER` |

The `msodbcsql.h` header still defines the `SQLNCLI_*` name macros, so source code that uses them keeps compiling. These definitions are guarded by `#ifndef __sqlncli_h__`, which means you can't include both headers in the same translation unit. Remove the `sqlncli.h` include.

Two things don't carry over:

- The distributed query metadata API functions that return lists of linked servers and their catalogs aren't declared in `msodbcsql.h`. They were specific to SQL Server Native Client.
- Version 18 encrypts connections by default and validates the server certificate. Native Client didn't. A connection string that worked with Native Client can fail on the first connect until you fix certificate trust or set `Encrypt` explicitly. See [Connection encryption troubleshooting](connection-troubleshooting.md).

For the rest of the version 17 to version 18 changes, see [Major version differences](major-version-differences.md).

## Related content

- [Connect to and query a database with C++](cpp-code-example-app-connect-access-sql-db.md)
- [DSN and connection string keywords and attributes](dsn-connection-string-attribute.md)
- [Programming guidelines (Linux and macOS)](linux-mac/programming-guidelines.md)
- [ODBC Programmer's Reference](../../odbc/reference/odbc-programmer-s-reference.md)
- [Microsoft ODBC Driver for SQL Server](microsoft-odbc-driver-for-sql-server.md)
