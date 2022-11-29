---
description: "Developing a User Interface for a Custom Task"
title: "Developing a User Interface for a Custom Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "custom user interfaces [Integration Services]"
  - "IDtsTaskUI interface"
  - "DtsTaskAttribute attribute"
  - "custom tasks [Integration Services], user interface"
  - "custom user interface [Integration Services], custom tasks"
  - "user interface [Integration Services]"
  - "SSIS custom tasks, user interface"
ms.assetid: 1e940cd1-c5f8-4527-b678-e89ba5dc398a
author: chugugrace
ms.author: chugu
---
# Developing a User Interface for a Custom Task

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  The [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] object model provides custom task developers the ability to easily create a custom user interface for a task that can then be integrated and displayed in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)]. The user interface can provide helpful information to the user in [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer, and guide users to correctly configure the properties and settings of the custom task.  
  
 Developing a custom user interface for a task involves using two important classes. The following table describes those classes.  
  
|Class|Description|  
|-----------|-----------------|  
|<xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute>|An attribute that identifies a managed task, and supplies design-time information through its properties to control how [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer displays and interacts with the object.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsTaskUI>|An interface used by the task to associate the task with its custom user interface.|  
  
 This section describes the role of the <xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute> attribute and the <xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsTaskUI> interface when you are developing a user interface for a custom task, and provides details about how to create, integrate, deploy, and debug the task within [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer.  
  
 The [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer provides multiple entry points to the user interface for the task: the user can select **Edit** on the shortcut menu, double-click the task, or click the **Show Editor** link at the bottom of the property sheet. When the user accesses one of these entry points, [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer locates and loads the assembly that contains the user interface for the task. The user interface for the task is responsible for creating the properties dialog box that is displayed to the user in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)].  
  
 A task and its user interface are separate entities. They should be implemented in separate assemblies to reduce localization, deployment, and maintenance work. The task DLL does not load, call, or generally contain any knowledge of its user interface, except for the information that is contained in the <xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute> attribute values coded in the task. This is the only way that a task and its user interface are associated.  
  
## The DtsTask Attribute  
 The <xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute> attribute is included in the task class code to associate a task with its user interface. The [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer uses the properties of the attribute to determine how to display the task in the designer. These properties include the name to display and the icon, if any.  
  
 The following table describes the properties of the <xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute> attribute.  
  
|Property|Description|  
|--------------|-----------------|  
|<xref:Microsoft.SqlServer.Dts.Runtime.Localization.DtsLocalizableAttribute.DisplayName%2A>|Displays the task name in the Control Flow toolbox.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.Localization.DtsLocalizableAttribute.Description%2A>|The task description (inherited from <xref:Microsoft.SqlServer.Dts.Runtime.Localization.DtsLocalizableAttribute>). This property is shown in ToolTips.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute.IconResource%2A>|The icon displayed in [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute.RequiredProductLevel%2A>|If used, set it to one of the values in the <xref:Microsoft.SqlServer.Dts.Runtime.DTSProductLevel> enumeration. For example, `RequiredProductLevel = DTSProductLevel.None`.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute.TaskContact%2A>|Holds contact information for occasions when the task requires technical support.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute.TaskType%2A>|Assigns a type to the task.|  
|Attribute.TypeId|When implemented in a derived class, gets a unique identifier for this Attribute. For more information, see **Attribute.TypeID** property in the .NET Framework Class Library.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute.UITypeName%2A>|The type name of the assembly that is used by [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer to load the assembly. This property is used to find the user interface assembly for the task.|  
  
 The following code example shows the <xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute> as it would look, coded above the class definition.  
  
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
  
 The [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer uses the <xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute.UITypeName%2A> property of the attribute that includes the assembly name, type name, version, culture, and public key token, to locate the assembly in the Global Assembly Cache (GAC) and load it for use by the designer.  
  
 After the assembly has been located, [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer uses the other properties in the attribute to display additional information about the task in [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer, such as the name, icon, and description of the task.  
  
 The <xref:Microsoft.SqlServer.Dts.Runtime.Localization.DtsLocalizableAttribute.DisplayName%2A>, <xref:Microsoft.SqlServer.Dts.Runtime.Localization.DtsLocalizableAttribute.Description%2A>, and <xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute.IconResource%2A> properties specify how the task is presented to the user. The <xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute.IconResource%2A> property contains the resource ID of the icon embedded in the user interface assembly. The designer loads the icon resource by ID from the assembly, and displays it next to the task name in the toolbox and on the designer surface when the task is added to a package. If a task does not provide an icon resource, the designer uses a default icon for the task.  
  
## The IDTSTaskUI Interface  
 The <xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsTaskUI> interface defines the collection of methods and properties called by [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer to initialize and display the user interface associated with the task. When the user interface for a task is invoked, the designer calls the <xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsTaskUI.Initialize%2A> method, implemented by the task user interface when you wrote it, and then provides the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> and <xref:Microsoft.SqlServer.Dts.Runtime.Connections> collections of the task and package, respectively, as parameters. These collections are stored locally, and used subsequently in the <xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsTaskUI.GetView%2A> method.  
  
 The designer calls the <xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsTaskUI.GetView%2A> method to request the window that is displayed in [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer. The task creates an instance of the window that contains the user interface for the task, and returns the user interface to the designer for display. Typically, the <xref:Microsoft.SqlServer.Dts.Runtime.TaskHost> and <xref:Microsoft.SqlServer.Dts.Runtime.Connections> objects are provided to the window through an overloaded constructor so they can be used to configure the task.  
  
 The [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer calls the <xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsTaskUI.GetView%2A> method of the task UI to display the user interface for the task. The task user interface returns the Windows form from this method, and [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer shows this form as a modal dialog box. When the form is closed, [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer examines the value of the **DialogResult** property of the form to determine whether the task has been modified and if these modifications should be saved. If the value of the **DialogResult** property is **OK**, the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer calls the persistence methods of the task to save the changes; otherwise, the changes are discarded.  
  
 The following code sample implements the <xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsTaskUI> interface, and assumes the existence of a Windows form class named SampleTaskForm.  
  
```csharp  
using System;  
using System.Windows.Forms;  
using Microsoft.SqlServer.Dts.Runtime;  
using Microsoft.SqlServer.Dts.Runtime.Design;  
  
namespace Sample  
{  
   public class HelloWorldTaskUI : IDtsTaskUI  
   {  
      TaskHost   taskHost;  
      Connections connections;  
      public void Initialize(TaskHost taskHost, IServiceProvider serviceProvider)  
      {  
         this.taskHost = taskHost;  
         IDtsConnectionService cs = serviceProvider.GetService  
         ( typeof( IDtsConnectionService ) ) as   IDtsConnectionService;   
         this.connections = cs.GetConnections();  
      }  
      public ContainerControl GetView()  
      {  
        return new HelloWorldTaskForm(this.taskHost, this.connections);  
      }  
     public void Delete(IWin32Window parentWindow)  
     {  
     }  
     public void New(IWin32Window parentWindow)  
     {  
     }  
   }  
}  
```  
  
```vb  
Imports System  
Imports Microsoft.SqlServer.Dts.Runtime  
Imports Microsoft.SqlServer.Dts.Runtime.Design  
Imports System.Windows.Forms  
  
Public Class HelloWorldTaskUI  
  Implements IDtsTaskUI  
  
  Dim taskHost As TaskHost  
  Dim connections As Connections  
  
  Public Sub Initialize(ByVal taskHost As TaskHost, ByVal serviceProvider As IServiceProvider) _  
    Implements IDtsTaskUI.Initialize  
  
    Dim cs As IDtsConnectionService  
  
    Me.taskHost = taskHost  
    cs = DirectCast(serviceProvider.GetService(GetType(IDtsConnectionService)), IDtsConnectionService)  
    Me.connections = cs.GetConnections()  
  
  End Sub  
  
  Public Function GetView() As ContainerControl _  
    Implements IDtsTaskUI.GetView  
  
    Return New HelloWorldTaskForm(Me.taskHost, Me.connections)  
  
  End Function  
  
  Public Sub Delete(ByVal parentWindow As IWin32Window) _  
    Implements IDtsTaskUI.Delete  
  
  End Sub  
  
  Public Sub [New](ByVal parentWindow As IWin32Window) _  
    Implements IDtsTaskUI.[New]  
  
  End Sub  
  
End Class  
```  
 
## See Also  
 [Creating a Custom Task](../../../integration-services/extending-packages-custom-objects/task/creating-a-custom-task.md)   
 [Coding a Custom Task](../../../integration-services/extending-packages-custom-objects/task/coding-a-custom-task.md)   
 [Developing a User Interface for a Custom Task](../../../integration-services/extending-packages-custom-objects/task/developing-a-user-interface-for-a-custom-task.md)  
  
  
