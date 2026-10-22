---
title: "Debug Common Language Runtime (CLR) Database Objects"
description: SQL Server provides support for debugging Transact-SQL and CLR objects in the database integrating SQL Server debugger with Microsoft Visual Studio debugger.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: how-to
helpviewer_keywords:
  - "database objects [CLR integration], debugging"
  - "permissions [CLR integration]"
  - "debugging [CLR integration]"
  - "building database objects [CLR integration], debugging"
  - "common language runtime [SQL Server], debugging"
---
# How to debug CLR database objects

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides support for debugging [!INCLUDE [tsql](../../includes/tsql-md.md)] and common language runtime (CLR) objects in the database. The key aspects of debugging in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] are the ease of setup and use, and the integration of the SQL Server debugger with the Microsoft Visual Studio debugger. Furthermore, debugging works across languages. Users can step seamlessly into CLR objects from [!INCLUDE [tsql](../../includes/tsql-md.md)], and vice versa.

The Transact-SQL debugger in SQL Server Management Studio can't be used to debug managed database objects, but you can debug the objects by using the debuggers in Visual Studio. Managed database object debugging in Visual Studio supports all common debugging features, such as *step into* and *step over* statements within routines executing on the server. Debuggers can set breakpoints, inspect the call stack, inspect variables, and modify variable values while debugging.

## Debug permissions and restrictions

Debugging is a highly privileged operation, and therefore only members of the **sysadmin** fixed server role are allowed to do so in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

The following restrictions apply while debugging:

- Debugging CLR routines is restricted to one debugger instance at a time. This limitation applies because all CLR code execution freezes when a break point is hit, and execution doesn't continue until the debugger advances from the break point. However, you can continue debugging [!INCLUDE [tsql](../../includes/tsql-md.md)] in other connections. Although [!INCLUDE [tsql](../../includes/tsql-md.md)] debugging doesn't freeze other executions on the server, it can cause other connections to wait by holding a lock.

- Existing connections can't be debugged, only new connections, as [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] requires information about the client and debugger environment before the connection can be made.

We recommend therefore that [!INCLUDE [tsql](../../includes/tsql-md.md)] and CLR code is debugged on a test server, and not on a production server.

## Overview

Debugging in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] follows a per-connection model. A debugger can detect and debug activities only on the client connection it attaches to. Because the functionality of the debugger isn't limited by the type of connection, both tabular data stream (TDS) and HTTP connections can be debugged. However, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't allow debugging existing connections. Debugging supports all common debugging features within routines executing on the server. The interaction between a debugger and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] happens through distributed Component Object Model (COM).

For more information and scenarios about debugging managed stored procedures, functions, triggers, user-defined types, and aggregates, see [SQL Server CLR Integration Database Debugging](/previous-versions/ms165050(v=vs.100)) in the Visual Studio documentation.

The TCP/IP network protocol must be enabled on the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance in order to use Visual Studio for remote development, debugging, and development. For more information about enabling TCP/IP protocol on the server, see [Configure Client Protocols](../../database-engine/configure-windows/configure-client-protocols.md).

## Debug steps

Use the following steps to debug a CLR database object in Microsoft Visual Studio:

1. Open Microsoft Visual Studio, and create a new SQL Server project. You can use the SQL LocalDB instance that comes with Visual Studio.

1. Create a new SQL CLR type ([!INCLUDE [c-sharp-md](../../includes/c-sharp-md.md)]):

   1. In **Solution Explorer**, right-click the project, and select **Add**, **New Item...**.
   1. From the **Add New Item** window, select **SQL CLR C# Stored Procedure**, **SQL CLR C# User-Defined Function**, **SQL CLR C# User-Defined Type**, **SQL CLR C# Trigger**, **SQL CLR C# Aggregate**, or **Class**.
   1. Specify a name for the source file of the new type, and then select **Add**.

1. Add code for the new type to the text editor. For sample code for an example stored procedure, see the following Example section in this article.

1. Add a script that tests the type:

   1. In **Solution Explorer**, right-click on the project node and select **Add**, **Script...**.
   1. In the **Add New Item** window, select **Script (Not in build)**, and specify a name, such as `Test.sql`. Select the **Add** button.
   1. In **Solution Explorer**, double-click the `Test.sql` node to open the default test script source file.
   1. Add the test script (one that invokes the code to be debugged) to the text editor. See the example in the next section for a sample script.

1. Place one or more breakpoints in the source code. Right-click on a line of code in the text editor on the function or routine that you want to debug. Select **Breakpoint**, **Insert Breakpoint**. The breakpoint is added, highlighting the line of code in red.

1. In the **Debug** menu, select **Start Debugging** to compile, deploy, and test the project. The test script in `Test.sql` is run, and the managed database object is invoked.

1. When the yellow arrow (designating the instruction pointer) appears at the breakpoint, code execution pauses. You can then debug your managed database object:

   1. Use **Step Over** from the **Debug** menu to advance the instruction pointer to the next line of code.
   1. Use the **Locals** window to observe the state of the objects currently highlighted by the instruction pointer.
   1. Add variables to the **Watch** window. You can observe the state of watched variables throughout the debugging session, even when the variable isn't at the line of code currently highlighted by the instruction pointer.
   1. Select **Continue** from the **Debug** menu to advance the instruction pointer to the next breakpoint or to complete execution of the routine if there are no more breakpoints.

## Example code

The following [!INCLUDE [c-sharp-md](../../includes/c-sharp-md.md)] example returns the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] version to the caller.

```csharp
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

public class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void GetVersion()
    {
        using (var connection = new SqlConnection("context connection=true"))
        {
            connection.Open();
            var command = new SqlCommand("select @@version", connection);
            SqlContext.Pipe.ExecuteAndSend(command);
        }
    }
}
```

## Example test script

The following test script shows how to invoke the `GetVersion` stored procedure defined in the previous example.

```sql
EXECUTE GetVersion;
```

## Related content

- [Debugging Managed Code](/visualstudio/debugger/debugging-managed-code)
- [Common language runtime (CLR) integration programming concepts](common-language-runtime-clr-integration-programming-concepts.md)
