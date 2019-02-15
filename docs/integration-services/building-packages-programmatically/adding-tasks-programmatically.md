---
title: "Adding Tasks Programmatically | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services 
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "tasks [Integration Services], packages"
  - "adding package tasks"
ms.assetid: 5d4652d5-228c-4238-905c-346dd8503fdf
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Adding Tasks Programmatically
  Tasks can be added to the following types of objects in the run-time engine:  
  
-   <xref:Microsoft.SqlServer.Dts.Runtime.Package>  
  
-   <xref:Microsoft.SqlServer.Dts.Runtime.Sequence>  
  
-   <xref:Microsoft.SqlServer.Dts.Runtime.ForLoop>  
  
-   <xref:Microsoft.SqlServer.Dts.Runtime.ForEachLoop>  
  
-   <xref:Microsoft.SqlServer.Dts.Runtime.DtsEventHandler>  
  
 These classes are considered containers, and they all inherit the <xref:Microsoft.SqlServer.Dts.Runtime.IDTSSequence.Executables%2A> property. Containers can contain a collection of tasks, which are executable objects processed by the runtime during execution of the container. The order of execution of the objects in the collection is determined any <xref:Microsoft.SqlServer.Dts.Runtime.PrecedenceConstraint> set on each task in the containers. Precedence constraints enable execution branching based on the success, failure, or completion of an <xref:Microsoft.SqlServer.Dts.Runtime.Executable> in the collection.  
  
 Each container has an <xref:Microsoft.SqlServer.Dts.Runtime.Executables> collection that contains the individual <xref:Microsoft.SqlServer.Dts.Runtime.Executable> objects. Each executable task inherits and implements the <xref:Microsoft.SqlServer.Dts.Runtime.Executable.Execute%2A> method and <xref:Microsoft.SqlServer.Dts.Runtime.Executable.Validate%2A> method. These two methods are called by the run-time engine to process each <xref:Microsoft.SqlServer.Dts.Runtime.Executable>.  
  
 To add a task to a package, you need a container with an <xref:Microsoft.SqlServer.Dts.Runtime.Executables> existing collection. Most of the time, the task that you will add to the collection is a package. To add the new task executable into the collection for that container, you call the <xref:Microsoft.SqlServer.Dts.Runtime.Executables.Add%2A> method . The method has a single parameter, a string, that contains the CLSID, PROGID, STOCK moniker, or <xref:Microsoft.SqlServer.Dts.Runtime.TaskInfo.CreationName%2A> of the task you are adding.  
  
## Task Names  
 Although you can specify a task by name or by ID, the **STOCK** moniker is the parameter used most often in the <xref:Microsoft.SqlServer.Dts.Runtime.Executables.Add%2A> method. To add a task to an executable identified by the **STOCK** moniker, use the following syntax:  
  
```csharp  
Executable exec = package.Executables.Add("STOCK:BulkInsertTask");  
  
```  
  
```vb  
Dim exec As Executable = package.Executables.Add("STOCK:BulkInsertTask")  
  
```  
  
 The following list shows the names for each task that are used after the **STOCK** moniker.  
  
-   ActiveXScriptTask  
  
-   BulkInsertTask  
  
-   ExecuteProcessTask  
  
-   ExecutePackageTask  
  
-   Exec80PackageTask  
  
-   FileSystemTask  
  
-   FTPTask  
  
-   MSMQTask  
  
-   PipelineTask  
  
-   ScriptTask  
  
-   SendMailTask  
  
-   SQLTask  
  
-   TransferStoredProceduresTask  
  
-   TransferLoginsTask  
  
-   TransferErrorMessagesTask  
  
-   TransferJobsTask  
  
-   TransferObjectsTask  
  
-   TransferDatabaseTask  
  
-   WebServiceTask  
  
-   WmiDataReaderTask  
  
-   WmiEventWatcherTask  
  
-   XMLTask  
  
 If you prefer a more explicit syntax, or if the task that you want to add does not have a STOCK moniker, you can add the task to the executable using its long name. This syntax requires that you also specify the version number of the task.  
  
```csharp  
Executable exec = package.Executables.Add(  
  "Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptTask, " +  
  "Microsoft.SqlServer.ScriptTask, Version=10.0.000.0, " +  
  "Culture=neutral, PublicKeyToken=89845dcd8080cc91");  
```  
  
```vb  
Dim exec As Executable = package.Executables.Add( _  
  "Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptTask, " & _  
  "Microsoft.SqlServer.ScriptTask, Version=10.0.000.0, " & _  
  "Culture=neutral, PublicKeyToken=89845dcd8080cc91")  
```  
  
 You can obtain the long name for the task programmatically, without having to specify the task version, by using the **AssemblyQualifiedName** property of the class, as shown in the following example. This example requires a reference to the Microsoft.SqlServer.SQLTask assembly.  
  
```csharp  
using Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask;  
...  
      Executable exec = package.Executables.Add(  
        typeof(Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask.ExecuteSQLTask).AssemblyQualifiedName);  
```  
  
```vb  
Imports Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask  
...  
    Dim exec As Executable = package.Executables.Add( _  
      GetType(Microsoft.SqlServer.Dts.Tasks.ExecuteSQLTask.ExecuteSQLTask).AssemblyQualifiedName)  
```  
  
 The following code example shows how to create an <xref:Microsoft.SqlServer.Dts.Runtime.Executables> collection from a new package, and then add a File System task and a Bulk Insert task to the collection, by using their **STOCK** monikers. This example requires a reference to the Microsoft.SqlServer.FileSystemTask and Microsoft.SqlServer.BulkInsertTask assemblies.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Runtime;  
using Microsoft.SqlServer.Dts.Tasks.FileSystemTask;  
using Microsoft.SqlServer.Dts.Tasks.BulkInsertTask;  
  
namespace Microsoft.SqlServer.Dts.Samples  
{  
  class Program  
  {  
    static void Main(string[] args)  
    {  
      Package p = new Package();  
      // Add a File System task to the package.  
      Executable exec1 = p.Executables.Add("STOCK:FileSystemTask");  
      TaskHost thFileSystemTask = exec1 as TaskHost;  
      // Add a Bulk Insert task to the package.  
      Executable exec2 = p.Executables.Add("STOCK:BulkInsertTask");  
      TaskHost thBulkInsertTask = exec2 as TaskHost;  
  
      // Iterate through the package Executables collection.  
      Executables pExecs = p.Executables;  
      foreach (Executable pExec in pExecs)  
      {  
        TaskHost taskHost = (TaskHost)pExec;  
        Console.WriteLine("Type {0}", taskHost.InnerObject.ToString());  
      }  
      Console.Read();  
    }  
  }  
}  
```  
  
```vb  
Imports Microsoft.SqlServer.Dts.Runtime  
Imports Microsoft.SqlServer.Dts.Tasks.FileSystemTask  
Imports Microsoft.SqlServer.Dts.Tasks.BulkInsertTask  
  
Module Module1  
  
  Sub Main()  
  
    Dim p As Package = New Package()  
    ' Add a File System task to the package.  
    Dim exec1 As Executable = p.Executables.Add("STOCK:FileSystemTask")  
    Dim thFileSystemTask As TaskHost = CType(exec1, TaskHost)  
    ' Add a Bulk Insert task to the package.  
    Dim exec2 As Executable = p.Executables.Add("STOCK:BulkInsertTask")  
    Dim thBulkInsertTask As TaskHost = CType(exec2, TaskHost)  
  
    ' Iterate through the package Executables collection.  
    Dim pExecs As Executables = p.Executables  
    Dim pExec As Executable  
    For Each pExec In pExecs  
      Dim taskHost As TaskHost = CType(pExec, TaskHost)  
      Console.WriteLine("Type {0}", taskHost.InnerObject.ToString())  
    Next  
    Console.Read()  
  
  End Sub  
  
End Module  
```  
  
 **Sample Output:**  
  
 `Type Microsoft.SqlServer.Dts.Tasks.FileSystemTask.FileSystemTask`  
  
 `Type Microsoft.SqlServer.Dts.Tasks.BulkInsertTask.BulkInsertTask`  
  
## TaskHost Container  
 The <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> class is a container that does not appear in the graphical user interface, but is very important in programming. This class is a wrapper for every task. Tasks that are added to the package by using the <xref:Microsoft.SqlServer.Dts.Runtime.Executables.Add%2A> method as an <xref:Microsoft.SqlServer.Dts.Runtime.Executable> object can be cast as a <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> object. When a task is cast as a <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost>, you can use additional properties and methods on the task. Also, the task itself can be accessed through the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost.InnerObject%2A> property of the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost>. Depending on your needs, you may decide to keep the task as a <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> object so that you can use the properties of the task through the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost.Properties%2A> collection. The advantage of using the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost.Properties%2A> is that you can write more generic code. If you need very specific code for a task, then you should cast the task to its appropriate object.  
  
 The following code example shows how to cast a <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost>, thBulkInsertTask, that contains a <xref:Microsoft.SqlServer.Dts.Tasks.BulkInsertTask.BulkInsertTask>, to a <xref:Microsoft.SqlServer.Dts.Tasks.BulkInsertTask.BulkInsertTask> object.  
  
```csharp  
BulkInsertTask myTask = thBulkInsertTask.InnerObject as BulkInsertTask;  
```  
  
```vb  
Dim myTask As BulkInsertTask = CType(thBulkInsertTask.InnerObject, BulkInsertTask)  
```  
  
 The following code example shows how to cast the executable to a <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost>, and then use the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost.InnerObject%2A> property to determine which type of executable is contained by the host.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Runtime;  
using Microsoft.SqlServer.Dts.Tasks.FileSystemTask;  
using Microsoft.SqlServer.Dts.Tasks.BulkInsertTask;  
  
namespace Microsoft.SqlServer.Dts.Samples  
{  
  class Program  
  {  
    static void Main(string[] args)  
    {  
      Package p = new Package();  
      // Add a File System task to the package.  
      Executable exec1 = p.Executables.Add("STOCK:FileSystemTask");  
      TaskHost thFileSystemTask1 = exec1 as TaskHost;  
      // Add a Bulk Insert task to the package.  
      Executable exec2 = p.Executables.Add("STOCK:BulkInsertTask");  
      TaskHost thFileSystemTask2 = exec2 as TaskHost;  
  
      // Iterate through the package Executables collection.  
      Executables pExecs = p.Executables;  
      foreach (Executable pExec in pExecs)  
      {  
        TaskHost taskHost = (TaskHost)pExec;  
        if (taskHost.InnerObject is Microsoft.SqlServer.Dts.Tasks.FileSystemTask.FileSystemTask)  
        {  
          // Do work with FileSystemTask here.  
          Console.WriteLine("Found task of type {0}", taskHost.InnerObject.ToString());  
        }  
        else if (taskHost.InnerObject is Microsoft.SqlServer.Dts.Tasks.BulkInsertTask.BulkInsertTask)  
        {  
          // Do work with BulkInsertTask here.  
          Console.WriteLine("Found task of type {0}", taskHost.InnerObject.ToString());  
        }  
        // Add additional statements to check InnerObject, if desired.  
      }  
      Console.Read();  
    }  
  }  
}  
```  
  
```vb  
Imports Microsoft.SqlServer.Dts.Runtime  
Imports Microsoft.SqlServer.Dts.Tasks.FileSystemTask  
Imports Microsoft.SqlServer.Dts.Tasks.BulkInsertTask  
  
Module Module1  
  
  Sub Main()  
  
    Dim p As Package = New Package()  
    ' Add a File System task to the package.  
    Dim exec1 As Executable = p.Executables.Add("STOCK:FileSystemTask")  
    Dim thFileSystemTask1 As TaskHost = CType(exec1, TaskHost)  
    ' Add a Bulk Insert task to the package.  
    Dim exec2 As Executable = p.Executables.Add("STOCK:BulkInsertTask")  
    Dim thFileSystemTask2 As TaskHost = CType(exec2, TaskHost)  
  
    ' Iterate through the package Executables collection.  
    Dim pExecs As Executables = p.Executables  
    Dim pExec As Executable  
    For Each pExec In pExecs  
      Dim taskHost As TaskHost = CType(pExec, TaskHost)  
      If TypeOf taskHost.InnerObject Is Microsoft.SqlServer.Dts.Tasks.FileSystemTask.FileSystemTask Then  
        ' Do work with FileSystemTask here.  
        Console.WriteLine("Found task of type {0}", taskHost.InnerObject.ToString())  
      ElseIf TypeOf taskHost.InnerObject Is Microsoft.SqlServer.Dts.Tasks.BulkInsertTask.BulkInsertTask Then  
        ' Do work with BulkInsertTask here.  
        Console.WriteLine("Found task of type {0}", taskHost.InnerObject.ToString())  
      End If  
      ' Add additional statements to check InnerObject, if desired.  
    Next  
    Console.Read()  
  
  End Sub  
  
End Module  
```  
  
 **Sample Output:**  
  
 `Found task of type Microsoft.SqlServer.Dts.Tasks.FileSystemTask.FileSystemTask`  
  
 `Found task of type Microsoft.SqlServer.Dts.Tasks.BulkInsertTask.BulkInsertTask`  
  
 The <xref:Microsoft.SqlServer.Dts.Runtime.Executables.Add%2A> statement returns an executable that is cast to a <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> object from the newly created <xref:Microsoft.SqlServer.Dts.Runtime.Executable> object.  
  
 To set properties or to call methods on the new object, you have two options:  
  
1.  Use the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost.Properties%2A> collection of the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost>. For example, to obtain a property from the object, use `th.Properties["propertyname"].GetValue(th))`. To set a property, use `th.Properties["propertyname"].SetValue(th, <value>);`.  
  
2.  Cast the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost.InnerObject%2A> of the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> to the task class. For example, to cast the Bulk Insert task to a <xref:Microsoft.SqlServer.Dts.Tasks.BulkInsertTask.BulkInsertTask> after it has been added to a package as an <xref:Microsoft.SqlServer.Dts.Runtime.Executable> and subsequently cast to a <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost>, use `BulkInsertTask myTask = th.InnerObject as BulkInsertTask;`.  
  
 Using the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> class in code, instead of casting to the task-specific class has the following advantages:  
  
-   The <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost><xref:Microsoft.SqlServer.Dts.Runtime.TaskHost.Properties%2A> provider does not require a reference to the assembly in the code.  
  
-   You can code generic routines that work for any task, because you do not have to know the name of the task at compile time. Such generic routines include methods where you pass in the name of the task to the method, and the method code works for all tasks. This is a good method for writing test code.  
  
 Casting from the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> to the task-specific class has the following advantages:  
  
-   The Visual Studio project gives you statement completion (IntelliSense).  
  
-   The code may run faster.  
  
-   Task-specific objects enable early binding and the resulting optimizations. For more information about early and late binding, see the topic "Early and Late Binding" in Visual Basic Language Concepts.  
  
 The following code example expands on the concept of reusing task code. Instead of casting tasks to their specific class equivalents, the code example shows how to cast the executable to a <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost>, and then uses the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost.Properties%2A> to write generic code against all the tasks.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Runtime;  
  
namespace Microsoft.SqlServer.Dts.Samples  
{  
  class Program  
  {  
    static void Main(string[] args)  
    {  
      Package package = new Package();  
  
      string[] tasks = { "STOCK:SQLTask", "STOCK:ScriptTask",   
        "STOCK:ExecuteProcessTask", "STOCK:PipelineTask",   
        "STOCK:FTPTask", "STOCK:SendMailTask", "STOCK:MSMQTask" };  
  
      foreach (string s in tasks)  
      {  
        TaskHost taskhost = package.Executables.Add(s) as TaskHost;  
        DtsProperties props = taskhost.Properties;  
        Console.WriteLine("Enumerating properties on " + taskhost.Name);  
        Console.WriteLine(" TaskHost.InnerObject is " + taskhost.InnerObject.ToString());  
        Console.WriteLine();  
  
        foreach (DtsProperty prop in props)  
        {  
          Console.WriteLine("Properties for " + prop.Name);  
          Console.WriteLine("Name : " + prop.Name);  
          Console.WriteLine("Type : " + prop.Type.ToString());  
          Console.WriteLine("Readable : " + prop.Get.ToString());  
          Console.WriteLine("Writable : " + prop.Set.ToString());  
          Console.WriteLine();  
        }  
      }  
      Console.Read();  
    }  
  }  
}  
```  
  
```vb  
Imports Microsoft.SqlServer.Dts.Runtime  
  
Module Module1  
  
  Sub Main()  
  
    Dim package As Package = New Package()  
  
    Dim tasks() As String = New String() {"STOCK:SQLTask", "STOCK:ScriptTask", _  
              "STOCK:ExecuteProcessTask", "STOCK:PipelineTask", _  
              "STOCK:FTPTask", "STOCK:SendMailTask", "STOCK:MSMQTask"}  
  
    For Each s As String In tasks  
  
      Dim taskhost As TaskHost = CType(package.Executables.Add(s), TaskHost)  
      Dim props As DtsProperties = taskhost.Properties  
      Console.WriteLine("Enumerating properties on " & taskhost.Name)  
      Console.WriteLine(" TaskHost.InnerObject is " & taskhost.InnerObject.ToString())  
      Console.WriteLine()  
  
      For Each prop As DtsProperty In props  
        Console.WriteLine("Properties for " + prop.Name)  
        Console.WriteLine(" Name : " + prop.Name)  
        Console.WriteLine(" Type : " + prop.Type.ToString())  
        Console.WriteLine(" Readable : " + prop.Get.ToString())  
        Console.WriteLine(" Writable : " + prop.Set.ToString())  
        Console.WriteLine()  
      Next  
  
    Next  
    Console.Read()  
  
  End Sub  
  
End Module  
```  
  
## External Resources  
 Blog entry, [EzAPI - Updated for SQL Server 2012](https://go.microsoft.com/fwlink/?LinkId=243223), on blogs.msdn.com.  

## See Also  
 [Connecting Tasks Programmatically](../../integration-services/building-packages-programmatically/connecting-tasks-programmatically.md)  
  
  
