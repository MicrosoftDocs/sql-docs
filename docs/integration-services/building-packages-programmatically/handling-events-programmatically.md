---
title: "Handling Events Programmatically | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services 
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "Integration Services packages, events"
  - "SQL Server Integration Services packages, events"
  - "SSIS events, programmatically handling"
  - "packages [Integration Services], events"
  - "DtsEventHandler object"
  - "SSIS packages, events"
  - "SSIS events"
  - "events [Integration Services], programmatically"
  - "tasks [Integration Services], events"
  - "IDTSEvents interface"
ms.assetid: 0f00bd66-efd5-4f12-9e1c-36195f739332
author: janinezhang
ms.author: janinez
manager: craigg
---
# Handling Events Programmatically
  The [!INCLUDE[ssIS](../../includes/ssis-md.md)] runtime provides a collection of events that occur before, during, and after the validation and execution of a package. These events can be captured in two ways. The first method is by implementing the <xref:Microsoft.SqlServer.Dts.Runtime.IDTSEvents> interface in a class, and supplying the class as a parameter to the **Execute** and **Validate** methods of the package. The second method is by creating <xref:Microsoft.SqlServer.Dts.Runtime.DtsEventHandler> objects, which can contain other [!INCLUDE[ssIS](../../includes/ssis-md.md)] objects, such as tasks and loops, that are executed when an event in <xref:Microsoft.SqlServer.Dts.Runtime.IDTSEvents> occurs. This section describes these two methods and provides code examples to demonstrate their use.  
  
## Receiving IDTSEvents Callbacks  
 Developers who build and execute packages programmatically can receive event notifications during the validation and execution process using the <xref:Microsoft.SqlServer.Dts.Runtime.IDTSEvents> interface. This is done by creating a class that implements the <xref:Microsoft.SqlServer.Dts.Runtime.IDTSEvents> interface and providing this class as a parameter to the **Validate** and **Execute** methods of a package. The methods of the class are then called by the run-time engine when the events occur.  
  
 The <xref:Microsoft.SqlServer.Dts.Runtime.DefaultEvents> class is a class that already implements the <xref:Microsoft.SqlServer.Dts.Runtime.IDTSEvents> interface; therefore, another alternative to implementing <xref:Microsoft.SqlServer.Dts.Runtime.IDTSEvents> directly is to derive from <xref:Microsoft.SqlServer.Dts.Runtime.DefaultEvents> and override the specific events that you want to respond to. You then provide your class as a parameter to the **Validate** and **Execute** methods of the <xref:Microsoft.SqlServer.Dts.Runtime.Package> to receive event callbacks.  
  
 The following code sample demonstrates a class that derives from <xref:Microsoft.SqlServer.Dts.Runtime.DefaultEvents>, and overrides the <xref:Microsoft.SqlServer.Dts.Runtime.IDTSEvents.OnPreExecute%2A> method. The class is then provided as aparameter to the **Validate** and **Execute** methods of the package.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Runtime;  
  
namespace Microsoft.SqlServer.Dts.Samples  
{  
  class Program  
  {  
    static void Main(string[] args)  
    {  
      Package p = new Package();  
      MyEventsClass eventsClass = new MyEventsClass();  
  
      p.Validate(null, null, eventsClass, null);  
      p.Execute(null, null, eventsClass, null, null);  
  
      Console.Read();  
    }  
  }  
  class MyEventsClass : DefaultEvents  
  {  
    public override void OnPreExecute(Executable exec, ref bool fireAgain)  
    {  
      // TODO: Add custom code to handle the event.  
      Console.WriteLine("The PreExecute event of the " +  
        exec.ToString() + " has been raised.");  
    }  
  }  
}  
```  
  
```vb  
Imports Microsoft.SqlServer.Dts.Runtime  
  
Module Module1  
  
  Sub Main()  
  
    Dim p As Package = New Package()  
    Dim eventsClass As MyEventsClass = New MyEventsClass()  
  
    p.Validate(Nothing, Nothing, eventsClass, Nothing)  
    p.Execute(Nothing, Nothing, eventsClass, Nothing, Nothing)  
  
    Console.Read()  
  
  End Sub  
  
End Module  
  
Class MyEventsClass  
  Inherits DefaultEvents  
  
  Public Overrides Sub OnPreExecute(ByVal exec As Executable, ByRef fireAgain As Boolean)  
  
    ' TODO: Add custom code to handle the event.  
    Console.WriteLine("The PreExecute event of the " & _  
      exec.ToString() & " has been raised.")  
  
  End Sub  
  
End Class  
```  
  
## Creating DtsEventHandler Objects  
 The run-time engine provides a robust, highly flexible event handling and notification system through the <xref:Microsoft.SqlServer.Dts.Runtime.DtsEventHandler> object. These objects let you design whole workflows within the event handler, which execute only when the event that the event handler belongs to occurs. The <xref:Microsoft.SqlServer.Dts.Runtime.DtsEventHandler> object is a container that is executed when the corresponding event on its parent object fires. This architecture lets you create isolated workflows that are executed in response to events that occur on a container. Because <xref:Microsoft.SqlServer.Dts.Runtime.DtsEventHandler> objects are synchronous, execution does not resume until the event handlers that are attached to the event have returned.  
  
 The following code demonstrates how to create a <xref:Microsoft.SqlServer.Dts.Runtime.DtsEventHandler> object. The code adds a <xref:Microsoft.SqlServer.Dts.Tasks.FileSystemTask.FileSystemTask> to the <xref:Microsoft.SqlServer.Dts.Runtime.Package.Executables%2A> collection of the package, and then creates a <xref:Microsoft.SqlServer.Dts.Runtime.DtsEventHandler> object for the <xref:Microsoft.SqlServer.Dts.Runtime.IDTSEvents.OnError%2A> event of the task. A <xref:Microsoft.SqlServer.Dts.Tasks.FileSystemTask.FileSystemTask> is added to the event handler, which is executed when the <xref:Microsoft.SqlServer.Dts.Runtime.IDTSEvents.OnError%2A> event occurs for the first <xref:Microsoft.SqlServer.Dts.Tasks.FileSystemTask.FileSystemTask>. This example assumes that you have a file that is named C:\Windows\Temp\DemoFile.txt for testing. The first time that you run the sample, if copies the file successfully and the event handler is not called. The second time you run the sample, the first <xref:Microsoft.SqlServer.Dts.Tasks.FileSystemTask.FileSystemTask> fails to copy the file (because the value of <xref:Microsoft.SqlServer.Dts.Tasks.FileSystemTask.FileSystemTask.OverwriteDestinationFile%2A> is **false**), the event handler is called, the second <xref:Microsoft.SqlServer.Dts.Tasks.FileSystemTask.FileSystemTask> deletes the source file, and the package reports failure because of the error that occurred.  
  
## Example  
  
```csharp  
using System;  
using System.IO;  
using Microsoft.SqlServer.Dts.Runtime;  
using Microsoft.SqlServer.Dts.Tasks.FileSystemTask;  
  
namespace Microsoft.SqlServer.Dts.Samples  
{  
  class Program  
  {  
    static void Main(string[] args)  
    {  
      string f = "DemoFile.txt";  
      Executable e;  
      TaskHost th;  
      FileSystemTask fst;  
  
      Package p = new Package();  
  
      p.Variables.Add("sourceFile", true, String.Empty,  
        @"C:\Windows\Temp\" + f);  
      p.Variables.Add("destinationFile", true, String.Empty,  
        Path.Combine(Directory.GetCurrentDirectory(), f));  
  
      // Create a first File System task and add it to the package.  
      // This task tries to copy a file from one folder to another.  
      e = p.Executables.Add("STOCK:FileSystemTask");  
      th = e as TaskHost;  
      th.Name = "FileSystemTask1";  
      fst = th.InnerObject as FileSystemTask;  
  
      fst.Operation = DTSFileSystemOperation.CopyFile;  
      fst.OverwriteDestinationFile = false;  
      fst.Source = "sourceFile";  
      fst.IsSourcePathVariable = true;  
      fst.Destination = "destinationFile";  
      fst.IsDestinationPathVariable = true;  
  
      // Add an event handler for the FileSystemTask's OnError event.  
      DtsEventHandler ehOnError = (DtsEventHandler)th.EventHandlers.Add("OnError");  
  
      // Create a second File System task and add it to the event handler.  
      // This task deletes the source file if the event handler is called.  
      e = ehOnError.Executables.Add("STOCK:FileSystemTask");  
      th = e as TaskHost;  
      th.Name = "FileSystemTask2";  
      fst = th.InnerObject as FileSystemTask;  
  
      fst.Operation = DTSFileSystemOperation.DeleteFile;  
      fst.Source = "sourceFile";  
      fst.IsSourcePathVariable = true;  
  
      DTSExecResult vr = p.Validate(null, null, null, null);  
      Console.WriteLine("ValidationResult = " + vr.ToString());  
      if (vr == DTSExecResult.Success)  
      {  
        DTSExecResult er = p.Execute(null, null, null, null, null);  
        Console.WriteLine("ExecutionResult = " + er.ToString());  
        if (er == DTSExecResult.Failure)  
          if (!File.Exists(@"C:\Windows\Temp\" + f))  
            Console.WriteLine("Source file has been deleted by the event handler.");  
      }  
      Console.Read();  
    }  
  }  
}  
```  
  
```vb  
Imports System.IO  
Imports Microsoft.SqlServer.Dts.Runtime  
Imports Microsoft.SqlServer.Dts.Tasks.FileSystemTask  
  
Module Module1  
  
  Sub Main()  
  
    Dim f As String = "DemoFile.txt"  
    Dim e As Executable  
    Dim th As TaskHost  
    Dim fst As FileSystemTask  
  
    Dim p As Package = New Package()  
  
    p.Variables.Add("sourceFile", True, String.Empty, _  
      "C:\Windows\Temp\" & f)  
    p.Variables.Add("destinationFile", True, String.Empty, _  
      Path.Combine(Directory.GetCurrentDirectory(), f))  
  
    ' Create a first File System task and add it to the package.  
    ' This task tries to copy a file from one folder to another.  
    e = p.Executables.Add("STOCK:FileSystemTask")  
    th = CType(e, TaskHost)  
    th.Name = "FileSystemTask1"  
    fst = CType(th.InnerObject, FileSystemTask)  
  
    fst.Operation = DTSFileSystemOperation.CopyFile  
    fst.OverwriteDestinationFile = False  
    fst.Source = "sourceFile"  
    fst.IsSourcePathVariable = True  
    fst.Destination = "destinationFile"  
    fst.IsDestinationPathVariable = True  
  
    ' Add an event handler for the FileSystemTask's OnError event.  
    Dim ehOnError As DtsEventHandler = CType(th.EventHandlers.Add("OnError"), DtsEventHandler)  
  
    ' Create a second File System task and add it to the event handler.  
    ' This task deletes the source file if the event handler is called.  
    e = ehOnError.Executables.Add("STOCK:FileSystemTask")  
    th = CType(e, TaskHost)  
    th.Name = "FileSystemTask1"  
    fst = CType(th.InnerObject, FileSystemTask)  
  
    fst.Operation = DTSFileSystemOperation.DeleteFile  
    fst.Source = "sourceFile"  
    fst.IsSourcePathVariable = True  
  
    Dim vr As DTSExecResult = p.Validate(Nothing, Nothing, Nothing, Nothing)  
    Console.WriteLine("ValidationResult = " + vr.ToString())  
    If vr = DTSExecResult.Success Then  
      Dim er As DTSExecResult = p.Execute(Nothing, Nothing, Nothing, Nothing, Nothing)  
      Console.WriteLine("ExecutionResult = " + er.ToString())  
      If er = DTSExecResult.Failure Then  
        If Not File.Exists("C:\Windows\Temp\" + f) Then  
          Console.WriteLine("Source file has been deleted by the event handler.")  
        End If  
      End If  
    End If  
    Console.Read()  
  
  End Sub  
  
End Module  
```  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Event Handlers](../../integration-services/integration-services-ssis-event-handlers.md)   
 [Add an Event Handler to a Package](https://msdn.microsoft.com/library/5e56885d-8658-480a-bed9-3f2f8003fd78)  
  
  
