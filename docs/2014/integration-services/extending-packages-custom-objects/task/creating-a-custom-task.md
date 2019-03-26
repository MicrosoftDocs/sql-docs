---
title: "Creating a Custom Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "custom tasks [Integration Services], creating"
ms.assetid: 42965c09-1782-4cdb-9ce1-216af4c23e0a
author: janinezhang
ms.author: janinez
manager: craigg
---
# Creating a Custom Task
  The steps involved in creating a custom task are similar to the steps for creating any other custom object for [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)]:  
  
-   Create a new class that inherits from the base class. For a task, the base class is <xref:Microsoft.SqlServer.Dts.Runtime.Task>.  
  
-   Apply the attribute that identifies the type of object to the class. For a task, the attribute is <xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute>.  
  
-   Override the implementation of the base class's methods and properties. For a task, these include the <xref:Microsoft.SqlServer.Dts.Runtime.Task.Validate%2A> and <xref:Microsoft.SqlServer.Dts.Runtime.Task.Execute%2A> methods.  
  
-   Optionally, develop a custom user interface. For a task, this requires a class that implements the <xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsTaskUI> interface.  
  
## Getting Started with a Custom Task  
  
### Creating Projects and Classes  
 Because all managed tasks derive from the <xref:Microsoft.SqlServer.Dts.Runtime.Task> base class, the first step when you create a custom task is to create a class library project in your preferred managed programming language and create a class that inherits from the base class. In this derived class you will override the methods and properties of the base class to implement your custom functionality.  
  
 In the same solution, create a second class library project for the custom user interface. A separate assembly for the user interface is recommended for ease of deployment because it allows you to update and redeploy the connection manager or its user interface independently.  
  
 Configure both projects to sign the assemblies that will be generated at build time by using a strong name key file.  
  
### Applying the DtsTask Attribute  
 Apply the <xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute> attribute to the class that you have created to identify it as a task. This attribute provides design-time information such as the name, description, and task type of the task.  
  
 Use the <xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute.UITypeName%2A> property to link the task to its custom user interface. To obtain the public key token that is required for this property, you an use **sn.exe -t** to display the public key token from the key pair (.snk) file that you intend to use to sign the user interface assembly.  
  
```csharp  
using System;  
using Microsoft.SqlServer.Dts.Runtime;  
namespace Microsoft.SSIS.Samples  
{  
  [DtsTask  
  (  
   DisplayName = "MyTask",  
   IconResource = "MyTask.MyTaskIcon.ico",  
   UITypeName = "My Custom Task," +  
   "Version=1.0.0.0," +  
   "Culture = Neutral," +  
   "PublicKeyToken = 12345abc6789de01",  
   TaskType = "PackageMaintenance",  
   TaskContact = "MyTask; company name; any other information",  
   RequiredProductLevel = DTSProductLevel.None  
   )]  
  public class MyTask : Task  
  {  
    // Your code here.  
  }  
}  
```  
  
```vb  
Imports System  
Imports Microsoft.SqlServer.Dts.Runtime  
  
<DtsTask(DisplayName:="MyTask", _  
 IconResource:="MyTask.MyTaskIcon.ico", _  
 UITypeName:="My Custom Task," & _  
 "Version=1.0.0.0,Culture=Neutral," & _  
 "PublicKeyToken=12345abc6789de01", _  
 TaskType:="PackageMaintenance", _  
 TaskContact:="MyTask; company name; any other information", _  
 RequiredProductLevel:=DTSProductLevel.None)> _  
Public Class MyTask  
  Inherits Task  
  
  ' Your code here.  
  
End Class 'MyTask  
```  
  
## Building, Deploying, and Debugging a Custom Task  
 The steps for building, deploying, and debugging a custom task in [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] are similar to the steps required for other types of custom objects. For more information, see [Building, Deploying, and Debugging Custom Objects](../building-deploying-and-debugging-custom-objects.md).  
  
![Integration Services icon (small)](../../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Creating a Custom Task](creating-a-custom-task.md)   
 [Coding a Custom Task](coding-a-custom-task.md)   
 [Developing a User Interface for a Custom Task](developing-a-user-interface-for-a-custom-task.md)  
  
  
