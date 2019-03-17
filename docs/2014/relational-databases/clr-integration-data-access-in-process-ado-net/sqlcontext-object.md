---
title: "SqlContext Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "Windows identity [CLR integration]"
  - "SqlContext object"
  - "context [CLR integration]"
ms.assetid: 67437853-8a55-44d9-9337-90689ebba730
author: rothja
ms.author: jroth
manager: craigg
---
# SqlContext Object
  You invoke managed code in the server when you call a procedure or function, when you call a method on a common language runtime (CLR) user-defined type, or when your action fires a trigger defined in any of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework languages. Because execution of this code is requested as part of a user connection, access to the context of the caller from the code running in the server is required. In addition, certain data access operations may only be valid if run under the context of the caller. For example, access to inserted and deleted pseudo-tables used in trigger operations is only valid under the context of the caller.  
  
 The context of the caller is abstracted in a `SqlContext` object. For more information about the `SqlTriggerContext` methods and properties, see the `Microsoft.SqlServer.Server.SqlTriggerContext` class reference documentation in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] SDK.  
  
 `SqlContext` provides access to the following components:  
  
-   `SqlPipe`: The `SqlPipe` object represents the "pipe" through which results flow to the client. For more information about the `SqlPipe` object, see [SqlPipe Object](sqlpipe-object.md).  
  
-   `SqlTriggerContext`: The `SqlTriggerContext` object can only be retrieved from within a CLR trigger. It provides information about the operation that caused the trigger to fire and a map of the columns that were updated. For more information about the `SqlTriggerContext` object, see [SqlTriggerContext Object](sqltriggercontext-object.md).  
  
-   `IsAvailable`: The `IsAvailable` property is used to determine context availability.  
  
-   `WindowsIdentity`: The `WindowsIdentity` property is used to retrieve the Windows identity of the caller.  
  
## Determining Context Availability  
 Query the `SqlContext` class to see if the currently executing code is running in-process. To do this, check the `IsAvailable` property of the `SqlContext` object. The `IsAvailable` property is read-only, and returns `True` if the calling code is running inside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and if other `SqlContext` members can be accessed. If the `IsAvailable` property returns `False`, all the other `SqlContext` members throw an `InvalidOperationException`, if used. If `IsAvailable` returns `False`, any attempt to open a connection object that has "context connection=true" in the connection string fails.  
  
## Retrieving Windows Identity  
 CLR code executing inside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is always invoked in the context of the process account. If the code should perform certain actions using the identity of the calling user, instead of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process identity, then an impersonation token should be obtained through the `WindowsIdentity` property of the `SqlContext` object. The `WindowsIdentity` property returns a `WindowsIdentity` instance representing the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows identity of the caller, or null if the client was authenticated using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. Only assemblies marked with `EXTERNAL_ACCESS` or `UNSAFE` permissions can access this property.  
  
 After obtaining the `WindowsIdentity` object, callers can impersonate the client account and perform actions on their behalf.  
  
 The identity of the caller is only available through `SqlContext.WindowsIdentity` if the client that initiated execution of the stored-procedure or function connected to the server using Windows Authentication. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication was used instead, this property is null and the code is unable to impersonate the caller.  
  
### Example  
 The following example shows how to get the Windows identity of the calling client and impersonate the client.  
  
 C#  
  
```  
[Microsoft.SqlServer.Server.SqlProcedure]  
public static void WindowsIDTestProc()  
{  
    WindowsIdentity clientId = null;  
    WindowsImpersonationContext impersonatedUser = null;  
  
    // Get the client ID.  
    clientId = SqlContext.WindowsIdentity;  
  
    // This outer try block is used to thwart exception filter   
    // attacks which would prevent the inner finally   
    // block from executing and resetting the impersonation.  
    try  
    {  
        try  
        {  
            impersonatedUser = clientId.Impersonate();  
            if (impersonatedUser != null)  
            {  
                // Perform some action using impersonation.  
            }  
        }  
        finally  
        {  
            // Undo impersonation.  
            if (impersonatedUser != null)  
                impersonatedUser.Undo();  
        }  
    }  
    catch  
    {  
        throw;  
    }  
}  
```  
  
 Visual Basic  
  
```  
<Microsoft.SqlServer.Server.SqlProcedure()> _  
Public Shared Sub  WindowsIDTestProcVB ()  
    Dim clientId As WindowsIdentity  
    Dim impersonatedUser As WindowsImpersonationContext  
  
    ' Get the client ID.  
    clientId = SqlContext.WindowsIdentity  
  
    ' This outer try block is used to thwart exception filter   
    ' attacks which would prevent the inner finally   
    ' block from executing and resetting the impersonation.  
  
    Try  
        Try  
  
            impersonatedUser = clientId.Impersonate()  
  
            If impersonatedUser IsNot Nothing Then  
                ' Perform some action using impersonation.  
            End If  
  
        Finally  
            ' Undo impersonation.  
            If impersonatedUser IsNot Nothing Then  
                impersonatedUser.Undo()  
            End If  
        End Try  
  
    Catch e As Exception  
  
        Throw e  
  
    End Try  
End Sub  
```  
  
## See Also  
 [SqlPipe Object](sqlpipe-object.md)   
 [SqlTriggerContext Object](sqltriggercontext-object.md)   
 [CLR Triggers](../../database-engine/dev-guide/clr-triggers.md)   
 [SQL Server In-Process Specific Extensions to ADO.NET](sql-server-in-process-specific-extensions-to-ado-net.md)  
  
  
