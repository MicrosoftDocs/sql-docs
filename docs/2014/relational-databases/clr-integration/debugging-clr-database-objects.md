---
title: "Debugging CLR Database Objects | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "database objects [CLR integration], debugging"
  - "permissions [CLR integration]"
  - "debugging [CLR integration]"
  - "building database objects [CLR integration], debugging"
  - "common language runtime [SQL Server], debugging"
ms.assetid: 1332035c-d6ed-424d-8234-46ad21168319
author: rothja
ms.author: jroth
manager: craigg
---
# Debugging CLR Database Objects
  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] provides support for debugging [!INCLUDE[tsql](../../../includes/tsql-md.md)] and common language runtime (CLR) objects in the database. The key aspects of debugging in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] are the ease of setup and use, and the integration of the SQL Server debugger with the Microsoft Visual Studio debugger. Furthermore, debugging works across languages. Users can step seamlessly into CLR objects from [!INCLUDE[tsql](../../../includes/tsql-md.md)], and vice versa. The Transact-SQL debugger in SQL Server Management Studio cannot be used to debug managed database objects, but you can debug the objects by using the debuggers in Visual Studio. Managed database object debugging in Visual Studio supports all common debugging features, such as "step into" and "step over" statements within routines executing on the server. Debuggers can set breakpoints, inspect the call stack, inspect variables, and modify variable values while debugging. Note that Visual Studio .NET 2003 cannot be used for CLR integration programming or debugging. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] includes the .NET Framework pre-installed, and Visual Studio .NET 2003 cannot use the .NET Framework 2.0 assemblies.  
  
 For more information about debugging managed code using Visual Studio, see the "[Debugging Managed Code](https://go.microsoft.com/fwlink/?LinkId=120377)" topic in the Visual Studio documentation.  
  
## Debugging Permissions and Restrictions  
 Debugging is a highly privileged operation, and therefore only members of the **sysadmin** fixed server role are allowed to do so in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 The following restrictions apply while debugging:  
  
-   Debugging CLR routines is restricted to one debugger instance at a time. This limitation applies because all CLR code execution freezes when a break point is hit and execution does not continue until the debugger advances from the break point. However, you can continue debugging [!INCLUDE[tsql](../../../includes/tsql-md.md)] in other connections. Although [!INCLUDE[tsql](../../../includes/tsql-md.md)] debugging does not freeze other executions on the server, it could cause other connections to wait by holding a lock.  
  
-   Existing connections cannot be debugged, only new connections, as [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] requires information about the client and debugger environment before the connection can be made.  
  
 Due to the above restrictions, we recommend that [!INCLUDE[tsql](../../../includes/tsql-md.md)] and CLR code be debugged on a test server, and not on a production server.  
  
## Overview of Debugging Managed Database Objects  
 Debugging in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] follows a per-connection model. A debugger can detect and debug activities only to the client connection to which it is attached. Because the functionality of the debugger is not limited by the type of connection, both tabular data stream (TDS) and HTTP connections can be debugged. However, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not allow debugging existing connections. Debugging supports all common debugging features within routines executing on the server. The interaction between a debugger and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] happens through distributed Component Object Model (COM).  
  
 For more information and scenarios about debugging managed stored procedures, functions, triggers, user-defined types, and aggregates, see the "[SQL Server CLR Integration Database Debugging](https://go.microsoft.com/fwlink/?LinkId=120378)" topic in the Visual Studio documentation.  
  
 The TCP/IP network protocol must be enabled on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance in order to use Visual Studio for remote development, debugging, and development. For more information about enabling TCP/IP protocol on the server, see [Configure Client Protocols](../../database-engine/configure-windows/configure-client-protocols.md).  
  
#### To debug a managed database object  
  
1.  Open Microsoft Visual Studio, create a new [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] project, and establish a connection to a database on an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
2.  Create a new type. In **Solution Explorer**, right-click the project, select **Add** and **New Item...** From the **Add New Item** window, select **Stored Procedure**, **User-Defined Function**, **User-Defined Type**, **Trigger**, **Aggregate**, or **Class**. Specify a name for the source file of the new type and click **Add**.  
  
3.  Add code for the new type to the text editor. For sample code for an example stored procedure, see the section later in this topic.  
  
4.  Add a script that tests the type. In **Solution Explorer**, expand the **TestScripts** directory double-click **Test.sql** to open the default test script source file. Add the test script, one that invokes the code to be debugged, to the text editor. See below for a sample script.  
  
5.  Place one or more breakpoints in the source code. Right-click on a line of code in the text editor, within the function or routine you want to debug, and select **Breakpoint** and **Insert Breakpoint**. The breakpoint is added, highlighting the line of code in red.  
  
6.  In the **Debug** menu, select **Start Debugging** to compile, deploy, and test the project. The test script in Test.sql will be run and the managed database object will be invoked.  
  
7.  When the yellow arrow designating the instruction pointer appears at the breakpoint code execution pauses and you can begin debugging your managed database object. You can **Step Over** from the **Debug** menu to advance the instruction pointer to the next line of code. The **Locals** window is used to observe the state of the objects currently highlighted by the instruction pointer. Variables can be added to the **Watch** window. The state of watched variables can be observed throughout the entire debugging session, not only when the variable is in the line of code currently highlighted by instruction pointer. Select Continue from the Debug menu to advance the instruction pointer to the next breakpoint or to complete execution of the routine if there are no more breakpoints.  
  
### Example  
 The following example returns the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] version to the caller.  
  
 C#  
  
```  
using System;  
using System.Data;  
using System.Data.SqlTypes;  
using System.Data.SqlClient;  
using Microsoft.SqlServer.Server;   
  
public class StoredProcedures   
{  
   [Microsoft.SqlServer.Server.SqlProcedure]  
   public static void GetVersion()  
   {  
   using(SqlConnection connection = new SqlConnection("context connection=true"))   
   {  
      connection.Open();  
      SqlCommand command = new SqlCommand("select @@version",  
                                           connection);  
      SqlContext.Pipe.ExecuteAndSend(command);  
      }  
   }  
}  
```  
  
 Visual Basic  
  
```  
Imports System  
Imports System.Data  
Imports System.Data.Sql  
Imports System.Data.SqlTypes  
Imports Microsoft.SqlServer.Server  
Imports System.Data.SqlClient  
  
Partial Public Class StoredProcedures   
    <Microsoft.SqlServer.Server.SqlProcedure> _  
    Public Shared Sub GetVersion()  
        Using connection As New SqlConnection("context connection=true")  
            connection.Open()  
            Dim command As New SqlCommand("SELECT @@VERSION", connection)  
            SqlContext.Pipe.ExecuteAndSend(command)  
        End Using  
    End Sub  
End Class  
```  
  
 The following is a test script that invokes the GetVersion stored procedure defined above.  
  
```  
EXEC GetVersion  
```  
  
## See Also  
 [Common Language Runtime &#40;CLR&#41; Integration Programming Concepts](common-language-runtime-clr-integration-programming-concepts.md)  
  
  
