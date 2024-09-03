---
title: "Programming extended stored procedures"
description: Learn about extended stored procedures in SQL Server, including how they work, and how to use them.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/29/2024
ms.service: sql
ms.topic: "reference"
helpviewer_keywords:
  - "gateway applications [SQL Server]"
  - "extended stored procedures [SQL Server], about extended stored procedures"
  - "Open Data Services [SQL Server]"
  - "ODS [SQL Server]"
---
# Programming Database Engine extended stored procedures

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CLR integration](../clr-integration/clr-integration-overview.md) instead.

## How extended stored procedures work

The process by which an extended stored procedure works is:

1. When a client executes an extended stored procedure, the request is transmitted in tabular data stream (TDS) or Simple Object Access Protocol (SOAP) format from the client application to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] searches for the DLL associated with the extended stored procedure, and loads the DLL if it isn't already loaded.

1. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] calls the requested extended stored procedure (implemented as a function inside the DLL).

1. The extended stored procedure passes result sets and return parameters back to the server by through the Extended Stored Procedure API.

In the past, Open Data Services was used to write server applications, such as gateways to non-SQL Server database environments. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support the obsolete portions of the Open Data Services API. The only part of the original Open Data Services API still supported by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] are the extended stored procedure functions, so the API was renamed to the Extended Stored Procedure API.

With the emergence of distributed queries and CLR integration, the need for Extended Stored Procedure API applications has largely been replaced.

If you have existing gateway applications, you can't use the `opends60.dll` that ships with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to run the applications. Gateway applications are no longer supported.

## Extended stored procedures vs. CLR integration

CLR integration provides a more robust alternative to writing server-side logic that was either hard to express or impossible to write in [!INCLUDE [tsql](../../includes/tsql-md.md)]. In earlier releases of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], extended stored procedures (XPs) provided the only mechanism that was available for database application developers to write such code.

With CLR integration, logic that used to be written in the form of stored procedures is often better expressed as table-valued functions, which allow the results constructed by the function to be queried in `SELECT` statements by embedding them in the `FROM` clause.

For more information, see [CLR integration overview](../clr-integration/clr-integration-overview.md).

## Execution characteristics of extended stored procedures

The execution of an extended stored procedure has these characteristics:

- The extended stored procedure function is executed under the security context of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- The extended stored procedure function runs in the process space of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- The thread associated with the execution of the extended stored procedure is the same one used for the client connection.

> [!IMPORTANT]  
> Before adding extended stored procedures to the server and granting execute permissions to other users, the system administrator should thoroughly review each extended stored procedure to make sure that it doesn't contain harmful or malicious code.

After the extended stored procedure DLL is loaded, the DLL remains loaded in the address space of the server until [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is stopped or the administrator explicitly unloads the DLL by using `DBCC <DLL_name> (FREE)`.

The extended stored procedure can be executed from [!INCLUDE [tsql](../../includes/tsql-md.md)] as a stored procedure by using the `EXECUTE` statement:

```sql
EXECUTE @retval = xp_extendedProcName @param1, @param2 OUTPUT;
```

### Parameters

#### @ *retval*

A return value.

#### @ *param1*

An input parameter.

#### @ *param2*

An input/output parameter.

> [!CAUTION]  
> Extended stored procedures offer performance enhancements and extend [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] functionality. However, because the extended stored procedure DLL and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] share the same address space, a problem procedure can adversely affect [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] functioning. Although exceptions thrown by the extended stored procedure DLL are handled by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], it's possible to damage [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data areas. As a security precaution, only [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system administrators can add extended stored procedures to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. These procedures should be thoroughly tested before they are installed.

## Send result sets to the server with Extended Stored Procedure API

When sending a result set to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the extended stored procedure should call the appropriate API as follows:

- The `srv_sendmsg` function might be called in any order before or after all rows (if any) are with `srv_sendrow`. All messages must be sent to the client before the completion status is sent with `srv_senddone`.

- The `srv_sendrow` function is called once for each row sent to the client. All rows must be sent to the client before any messages, status values, or completion statuses are sent with `srv_sendmsg`, the `srv_status` argument of `srv_pfield`, or `srv_senddone`.

- Sending a row that doesn't have all its columns defined with `srv_describe` causes the application to raise an informational error message and return `FAIL` to the client. In this case, the row isn't sent.

## Create extended stored procedures

An extended stored procedure is a C/C++ function with a prototype:

**SRVRETCODE *xp_extendedProcName* ( SRVPROC \*);**

Using the prefix `xp_` is optional. Extended stored procedure names are case-sensitive when referenced in [!INCLUDE [tsql](../../includes/tsql-md.md)] statements, regardless of code page/sort order installed on the server. When you build a DLL:

- If an entry point is necessary, write a `DllMain` function.

  This function is optional. If you don't provide it in source code, the compiler links its own version, which does nothing but return `TRUE`. If you provide a `DllMain` function, the operating system calls this function when a thread or process attaches to or detaches from the DLL.

- All functions called from outside the DLL (all extended stored procedure Efunctions) must be exported.

  You can export a function by listing its name in the `EXPORTS` section of a `.def` file, or you can prefix the function name in the source code with `__declspec(dllexport)`, a Microsoft compiler extension (`__declspec()` begins with two underscores).

These files are required for creating an extended stored procedure DLL.

| File | Description |
| --- | --- |
| `srv.h` | Extended Stored Procedure API header file |
| `opends60.lib` | Import library for `opends60.dll` |

To create an extended stored procedure DLL, create a project of type Dynamic Link Library. For more information about creating a DLL, see the development environment documentation.

All extended stored procedure DLLs should implement and export the following function:

```cpp
__declspec(dllexport) ULONG __GetXpVersion()
{
   return ODS_VERSION;
}
```

`__declspec(dllexport)` is a Microsoft-specific compiler extension. If your compiler doesn't support this directive, you should export this function in your `DEF` file under the `EXPORTS` section.

When [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is started with the trace flag `-T260` or if a user with system administrator privileges runs `DBCC TRACEON (260)`, and if the extended stored procedure DLL doesn't support `__GetXpVersion()`, the following warning message is printed to the error log (`__GetXpVersion()` begins with two underscores).

```output
Error 8131: Extended stored procedure DLL '%' does not export __GetXpVersion().
```

If the extended stored procedure DLL exports `__GetXpVersion()`, but the version returned by the function is less than the version required by the server, a warning message stating the version returned by the function and the version expected by the server is printed to the error log. If you get this message, you're returning an incorrect value from `__GetXpVersion()`, or you're compiling with an older version of `srv.h`.

> [!NOTE]  
> `SetErrorMode`, a Win32 function, shouldn't be called in extended stored procedures.

Long-running extended stored procedures should call `srv_got_attention` periodically, so that the procedure can terminate itself if the connection is killed, or the batch is aborted.

To debug an extended stored procedure DLL, copy it to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] `\Binn` directory. To specify the executable for the debugging session, enter the path and file name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] executable file (for example, `C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Binn\sqlservr.exe`). For information about `sqlservr` arguments, see [sqlservr Application](../../tools/sqlservr-application.md).

## Add an extended stored procedure to SQL Server

A DLL that contains extended stored procedure functions acts as an extension to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. To install the DLL, copy the file to a directory, such as the one that contains the standard [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] DLL files (`C:\Program Files\Microsoft SQL Server\MSSQL16.0.<x>\MSSQL\Binn` by default).

After the extended stored procedure DLL is copied to the server, a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system administrator must register to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] each extended stored procedure function in the DLL. This is done using the `sp_addextendedproc` system stored procedure.

> [!IMPORTANT]  
> The system administrator should thoroughly review an extended stored procedure to ensure that it doesn't contain harmful or malicious code before adding it to the server and granting execute permissions to other users. Validate all user input. Don't concatenate user input before validating it. Never execute a command constructed from unvalidated user input.

The first parameter of `sp_addextendedproc` specifies the name of the function, and the second parameter specifies the name of the DLL in which that function resides. You should specify the complete path of the DLL.

> [!NOTE]  
> Existing DLLs that weren't registered with a complete path don't work after upgrading to [!INCLUDE [ssversion2005-md](../../includes/ssversion2005-md.md)] or a later version. To correct the problem, use `sp_dropextendedproc` to unregister the DLL, and then reregister it with `sp_addextendedproc,` specifying the complete path.

The name of the function specified in `sp_addextendedproc` must be exactly the same, including the case, as the function's name in the DLL. For example, this command registers a function `xp_hello,` located in a dll named `xp_hello.dll`, as a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] extended stored procedure:

```sql
sp_addextendedproc 'xp_hello', 'c:\Program Files\Microsoft SQL Server\MSSQL13.0.MSSQLSERVER\MSSQL\Binn\xp_hello.dll';
```

If the name of the function specified in `sp_addextendedproc` doesn't exactly match the function name in the DLL, the new name is registered in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], but the name isn't usable. For example, although `xp_Hello` is registered as a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] extended stored procedure located in `xp_hello.dll`, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can't find the function in the DLL if you use `xp_Hello` to call the function later.

```sql
-- Register the function (xp_hello) with an initial upper case
sp_addextendedproc 'xp_Hello', 'c:\xp_hello.dll';

-- Use the newly registered name to call the function
DECLARE @txt VARCHAR(33);
EXEC xp_Hello @txt OUTPUT;
```

Here's the error message:

```output
Server: Msg 17750, Level 16, State 1, Procedure xp_Hello, Line 1
Could not load the DLL xp_hello.dll, or one of the DLLs it references. Reason: 127(The specified procedure could not be found.).
```

If the name of the function specified in `sp_addextendedproc` matches exactly the function name in the DLL, and the collation of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance is case-insensitive, the user can call the extended stored procedure using any combination of lower- and upper-case letters of the name.

```sql
-- Register the function (xp_hello)
sp_addextendedproc 'xp_hello', 'c:\xp_hello.dll';

-- The following example succeeds in calling xp_hello
DECLARE @txt VARCHAR(33);
EXEC xp_Hello @txt OUTPUT;

DECLARE @txt VARCHAR(33);
EXEC xp_HelLO @txt OUTPUT;

DECLARE @txt VARCHAR(33);
EXEC xp_HELLO @txt OUTPUT;
```

When the collation of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance is case-sensitive, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can't call the extended stored procedure if the procedure is called with a different case. This is true even if it was registered with exactly the same name and collation as the function in the DLL.

```sql
-- Register the function (xp_hello)
sp_addextendedproc 'xp_hello', 'c:\xp_hello.dll';

-- The following example results in an error
DECLARE @txt VARCHAR(33);
EXEC xp_HELLO @txt OUTPUT;
```

Here's the error message:

```output
Server: Msg 2812, Level 16, State 62, Line 1
```

You don't need to stop and restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Query extended stored procedures installed in SQL Server

A [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authenticated user can display the currently defined extended stored procedures and the name of the DLL to which each belongs by running the `sp_helpextendedproc` system procedure. For example, the following example returns the DLL to which `xp_hello` belongs:

```sql
sp_helpextendedproc 'xp_hello';
```

If `sp_helpextendedproc` is executed without specifying an extended stored procedure, all the extended stored procedures and their DLLs are displayed.

## Remove an extended stored procedure from SQL Server

To drop each extended stored procedure function in a user-defined extended stored procedure DLL, a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system administrator must run the `sp_dropextendedproc` system stored procedure, specifying the name of the function and the name of the DLL in which that function resides. For example, this command removes the function `xp_hello`, located in a DLL named `xp_hello.dll,` from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]:

```sql
sp_dropextendedproc 'xp_hello';
```

`sp_dropextendedproc` doesn't drop system extended stored procedures. Instead, the system administrator should deny `EXECUTE` permission on the extended stored procedure to the **public** role.

## Unload an extended stored procedure DLL

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] loads an extended stored procedure DLL as soon as a call is made to one of the functions of the DLL. The DLL remains loaded until the server is shut down or until the system administrator uses the `DBCC` statement to unload it. For example, this command unloads the `xp_hello.dll`, allowing the system administrator to copy a newer version of this file to the directory without shutting down the server:

```sql
DBCC xp_hello(FREE);
```

## Related content

- [Common language runtime (CLR) integration](../clr-integration/common-language-runtime-integration-overview.md)
- [CLR Table-Valued Functions](../clr-integration-database-objects-user-defined-functions/clr-table-valued-functions.md)
- [Database Engine Extended Stored Procedures - Programming](database-engine-extended-stored-procedures-programming.md)
- [Querying Extended Stored Procedures Installed in SQL Server](../../relational-databases/extended-stored-procedures-programming/querying-extended-stored-procedures-installed-in-sql-server.md)
- [srv_got_attention (Extended Stored Procedure API)](../extended-stored-procedures-reference/srv-got-attention-extended-stored-procedure-api.md)
- [sp_addextendedproc (Transact-SQL)](../system-stored-procedures/sp-addextendedproc-transact-sql.md)
- [sp_dropextendedproc (Transact-SQL)](../system-stored-procedures/sp-dropextendedproc-transact-sql.md)
- [sp_helpextendedproc (Transact-SQL)](../system-stored-procedures/sp-helpextendedproc-transact-sql.md)
- [DBCC dllname (FREE) (Transact-SQL)](../../t-sql/database-console-commands/dbcc-dllname-free-transact-sql.md)
