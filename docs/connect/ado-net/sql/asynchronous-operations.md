---
title: "Asynchronous operations"
description: "Describes how to perform asynchronous database operations by using an API that is modeled after the asynchronous model used by the .NET Framework."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: "09/30/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Asynchronous operations

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

Some database operations, such as command executions, can take significant time to complete. In such a case, single-threaded applications must block other operations and wait for the command to finish before they can continue their own operations. In contrast, being able to assign the long-running operation to a background thread allows the foreground thread to remain active throughout the operation. In a Windows application, for example, delegating the long-running operation to a background thread allows the user interface thread to remain responsive while the operation is executing.  
  
The .NET provides several standard asynchronous design patterns that developers can use to take advantage of background threads and free the user interface or high-priority threads to complete other operations in its <xref:Microsoft.Data.SqlClient.SqlCommand> class. Specifically, the <xref:Microsoft.Data.SqlClient.SqlCommand.BeginExecuteNonQuery%2A>, <xref:Microsoft.Data.SqlClient.SqlCommand.BeginExecuteReader%2A>, and <xref:Microsoft.Data.SqlClient.SqlCommand.BeginExecuteXmlReader%2A> methods, paired with the <xref:Microsoft.Data.SqlClient.SqlCommand.EndExecuteNonQuery%2A>, <xref:Microsoft.Data.SqlClient.SqlCommand.EndExecuteReader%2A>, and <xref:Microsoft.Data.SqlClient.SqlCommand.EndExecuteXmlReader%2A> methods, provide the asynchronous support.  
  
> [!NOTE]
>  Asynchronous programming is a core feature of the .NET. For more information about the different asynchronous techniques available to developers, see [Calling Synchronous Methods Asynchronously](/dotnet/standard/asynchronous-programming-patterns/calling-synchronous-methods-asynchronously).  
  
Although using asynchronous techniques with ADO.NET features does not add any special considerations, it is important to be aware of the benefits and pitfalls of creating multithreaded applications. The examples that follow in this section point out several important issues that developers will need to take into account when building applications that incorporate multithreaded functionality.  
  
## In this section  
[Windows applications using callbacks](windows-applications-callbacks.md)  
Provides an example demonstrating how to execute an asynchronous command safely, correctly handling interaction with a form and its contents from a separate thread.  
  
[ASP.NET applications using wait handles](aspnet-apps-use-wait-handles.md)  
Provides an example demonstrating how to execute multiple concurrent commands from an ASP.NET page, using Wait handles to manage the operation at completion of all the commands.  
  
[Polling in console applications](poll-console-applications.md)  
Provides an example demonstrating the use of polling to wait for the completion of an asynchronous command execution from a console application. This technique is also valid in a class library or other application without a user interface.  
  
## Next steps
- [SQL Server and ADO.NET](index.md)
- [Calling Synchronous Methods Asynchronously](/dotnet/standard/asynchronous-programming-patterns/calling-synchronous-methods-asynchronously)