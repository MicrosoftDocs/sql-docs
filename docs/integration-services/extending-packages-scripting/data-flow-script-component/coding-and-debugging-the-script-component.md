---
description: "Coding and Debugging the Script Component"
title: "Coding and Debugging the Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "SSIS Script task, development environment"
  - "Script component [Integration Services], debugging"
  - "Script component [Integration Services], coding"
  - "SSIS Script component, debugging"
  - "Script component [Integration Services], development environment"
  - "debugging [Integration Services], scripts"
  - "SSIS Script component, coding"
  - "VSTA"
ms.assetid: c3913c15-66aa-4b61-89b5-68488fa5f0a4
author: chugugrace
ms.author: chugu
---
# Coding and Debugging the Script Component

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  In [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer, the Script component has two modes: metadata design mode and code design mode. When you open the **Script Transformation Editor**, the component enters metadata design mode, in which you configure metadata and set component properties. After you have set the properties of the Script component and configured the input and outputs in metadata design mode, you can switch to code design mode to write your custom script. For more information about metadata design mode and code design mode, see [Configuring the Script Component in the Script Component Editor](../../../integration-services/extending-packages-scripting/data-flow-script-component/configuring-the-script-component-in-the-script-component-editor.md).  
  
## Writing the Script in Code Design Mode  
  
### Script Component Development Environment  
 To write your script, click **Edit Script** on the **Script** page of the **Script Transformation Editor** to open the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] Tools for Applications (VSTA) IDE. The VSTA IDE includes all the standard features of the [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] .NET environment, such as the color-coded [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] editor, IntelliSense, and Object Browser.  
  
 Script code is written in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual Basic or [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual C#. You specify the script language by setting the **ScriptLanguage** property in the **Script Transformation Editor**. If you prefer to use another programming language, you can develop a custom assembly in your language of choice and call its functionality from the code in the Script component.  
  
 The script that you create in the Script component is stored in the package definition. There is no separate script file. Therefore, the use of the Script component does not affect package deployment.  
  
> [!NOTE]  
>  While you design the package, the script code is temporarily written to a project file. Because storing sensitive information in a file is a potential security risk, we recommended that you do not include sensitive information such as passwords in the script code.  
  
 By default, **Option Strict** is disabled in the IDE.  
  
### Script Component Project Structure  
 The power of the Script component is that it can generate infrastructure code that reduces the amount of code that you must write. This feature relies on the fact that inputs and outputs and their columns and properties are fixed and known in advance. Therefore, any subsequent changes that you make to the component's metadata may invalidate the code that you have written. This causes compilation errors during execution of the package.  
  
#### Project Items and Classes in the Script Component Project  
 When you switch to code design mode, the VSTA IDE opens and displays the **ScriptMain** project item. The **ScriptMain** project item contains the editable **ScriptMain** class, which serves as the entry point for the script and which is where you write your code. The code elements in the class vary depending on the programming language that you selected for the Script task.  
  
 The script project contains two additional auto-generated read-only project items:  
  
-   The **ComponentWrapper** project item contains three classes:  
  
    -   The **UserComponent** class, which inherits from <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent> and contains the methods and properties that you will use to process data and to interact with the package. The **ScriptMain** class inherits from the **UserComponent** class.  
  
    -   A **Connections** collection class that contains references to the connections selected on the Connection Manager page of the Script Transformation Editor.  
  
    -   A **Variables** collection class that contains references to the variables entered in the **ReadOnlyVariable** and **ReadWriteVariables** properties on the **Script** page of the **Script Transformation Editor**.  
  
-   The **BufferWrapper** project item contains a class that inherits from <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptBuffer> for each input and output configured on the **Inputs and Outputs** page of the **Script Transformation Editor**. Each of these classes contains typed accessor properties that correspond to the configured input and output columns, and the data flow buffers that contain the columns.  
  
 For information about how to use these objects, methods, and properties, see [Understanding the Script Component Object Model](../../../integration-services/extending-packages-scripting/data-flow-script-component/understanding-the-script-component-object-model.md). For information about how to use the methods and properties of these classes in a particular type of Script component, see the section [Additional Script Component Examples](../../../integration-services/extending-packages-scripting-data-flow-script-component-examples/additional-script-component-examples.md). The example topics also contain complete code samples.  
  
 When you configure the Script component as a transformation, the **ScriptMain** project item contains the following autogenerated code. The code template also provides an overview of the Script component, and additional information on how to retrieve and manipulate SSIS objects, such as variables, events, and connections.  
  
```vb  
' Microsoft SQL Server Integration Services Script Component  
' Write scripts using Microsoft Visual Basic 2008.  
' ScriptMain is the entry point class of the script.  
  
Imports System  
Imports System.Data  
Imports System.Math  
Imports Microsoft.SqlServer.Dts.Pipeline.Wrapper  
Imports Microsoft.SqlServer.Dts.Runtime.Wrapper  
  
<Microsoft.SqlServer.Dts.Pipeline.SSISScriptComponentEntryPointAttribute> _  
<CLSCompliant(False)> _  
Public Class ScriptMain  
    Inherits UserComponent  
  
    Public Overrides Sub PreExecute()  
        MyBase.PreExecute()  
        '  
        ' Add your code here for preprocessing or remove if not needed  
        '  
    End Sub  
  
    Public Overrides Sub PostExecute()  
        MyBase.PostExecute()  
        '  
        ' Add your code here for postprocessing or remove if not needed  
        ' You can set read/write variables here, for example:  
        ' Me.Variables.MyIntVar = 100  
        '  
    End Sub  
  
    Public Overrides Sub Input0_ProcessInputRow(ByVal Row As Input0Buffer)  
        '  
        ' Add your code here  
        '  
    End Sub  
  
End Class  
```  
  
```csharp  
/* Microsoft SQL Server Integration Services user script component  
*  Write scripts using Microsoft Visual C# 2008.  
*  ScriptMain is the entry point class of the script.*/  
  
using System;  
using System.Data;  
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;  
using Microsoft.SqlServer.Dts.Runtime.Wrapper;  
  
[Microsoft.SqlServer.Dts.Pipeline.SSISScriptComponentEntryPointAttribute]  
public class ScriptMain : UserComponent  
{  
  
    public override void PreExecute()  
    {  
        base.PreExecute();  
        /*  
          Add your code here for preprocessing or remove if not needed  
        */  
    }  
  
    public override void PostExecute()  
    {  
        base.PostExecute();  
        /*  
          Add your code here for postprocessing or remove if not needed  
          You can set read/write variables here, for example:  
          Variables.MyIntVar = 100  
        */  
    }  
  
    public override void Input0_ProcessInputRow(Input0Buffer Row)  
    {  
        /*  
          Add your code here  
        */  
    }  
  
}  
```  
  
#### Additional Project Items in the Script Component Project  
 The Script component project can include items other than the default **ScriptMain** item. You can add classes, modules, code files, and folders to the project, and you can use folders to organize groups of items.  
  
 All the items that you add are persisted inside the package.  
  
#### References in the Script Component Project  
 You can add references to managed assemblies by right-clicking the Script task project in **Project Explorer**, and then clicking **Add Reference**. For more information, see [Referencing Other Assemblies in Scripting Solutions](../../../integration-services/extending-packages-scripting/referencing-other-assemblies-in-scripting-solutions.md).  
  
> [!NOTE]  
>  You can view project references in the VSTA IDE in **Class View** or in **Project Explorer**. You open either of these windows from the **View** menu. You can add a new reference from the **Project** menu, from **Project Explorer**, or from **Class View**.  
  
## Interacting with the Package in the Script Component  
 The custom script that you write in the Script component can access and use variables and connection managers from the containing package through strongly-typed accessors in the auto-generated base classes. However, you must configure both variables and connection managers before entering code-design mode if you want to make them available to your script. You can also raise events and perform logging from your Script component code.  
  
 The autogenerated project items in the Script component project provide the following objects, methods, and properties for interacting with the package.  
  
|Package Feature|Access Method|  
|---------------------|-------------------|  
|Variables|Use the named and typed accessor properties in the **Variables** collection class in the **ComponentWrapper** project item, exposed through the **Variables** property of the **ScriptMain** class.<br /><br /> The **PreExecute** method can access only read-only variables. The **PostExecute** method can access both read-only and read/write variables.|  
|Connections|Use the named and typed accessor properties in the **Connections** collection class in the **ComponentWrapper** project item, exposed through the **Connections** property of the **ScriptMain** class.|  
|Events|Raise events by using the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.ComponentMetaData%2A> property of the **ScriptMain** class and the **Fire\<X>** methods of the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface.|  
|Logging|Perform logging by using the <xref:Microsoft.SqlServer.Dts.Pipeline.ScriptComponent.Log%2A> method of the **ScriptMain** class.|  
  
## Debugging the Script Component  
 To debug the code in your Script component, set at least one breakpoint in the code, and then close the VSTA IDE to run the package in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)]. When package execution enters the Script component, the VSTA IDE reopens and displays your code in read-only mode. After execution reaches your breakpoint, you can examine variable values and step through the remaining code.  
  
> [!NOTE]  
>  You cannot debug a Script component when you run the Script component as part of a child package that is run from an Execute Package task. Breakpoints that you set in the Script component in the child package are disregarded in these circumstances. You can debug the child package normally by running it separately.  
  
> [!NOTE]  
>  When you debug a package that contains multiple Script components, the debugger debugs one Script component. The system can debug another Script component if the debugger completes, as in the case of a Foreach Loop or For Loop container.  
  
 You can also monitor the execution of the Script component by using the following methods:  
  
-   Interrupt execution and display a modal message by using the **MessageBox.Show** method in the **System.Windows.Forms** namespace. (Remove this code after you complete the debugging process.)  
  
-   Raise events for informational messages, warnings, and errors. The FireInformation, FireWarning, and FireError methods display the event description in the Visual Studio **Output** window. However, the FireProgress method, the Console.Write method, and Console.WriteLine method do not display any information in the **Output** window. Messages from the FireProgress event appear on the **Progress** tab of [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer. For more information, see [Raising Events in the Script Component](../../../integration-services/extending-packages-scripting/data-flow-script-component/raising-events-in-the-script-component.md).  
  
-   Log events or user-defined messages to enabled logging providers. For more information, see [Logging in the Script Component](../../../integration-services/extending-packages-scripting/data-flow-script-component/logging-in-the-script-component.md).  
  
 If you just want to examine the output of a Script component configured as a source or as a transformation, without saving the data to a destination, you can stop the data flow with a [Row Count Transformation](../../../integration-services/data-flow/transformations/row-count-transformation.md) and attach a data viewer to the output of the Script component. For information about data viewers, see [Debugging Data Flow](../../../integration-services/troubleshooting/debugging-data-flow.md).  
  
## In This Section  
 For more information about coding the Script component, see the following topics in this section.  
  
 [Understanding the Script Component Object Model](../../../integration-services/extending-packages-scripting/data-flow-script-component/understanding-the-script-component-object-model.md)  
 Explains how to use the objects, methods, and properties available in the Script component.  
  
 [Referencing Other Assemblies in Scripting Solutions](../../../integration-services/extending-packages-scripting/referencing-other-assemblies-in-scripting-solutions.md)  
 Explains how to reference objects from the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] class library in the Script component.  
  
 [Simulating an Error Output for the Script Component](../../../integration-services/extending-packages-scripting-data-flow-script-component-examples/simulating-an-error-output-for-the-script-component.md)  
 Explains how to simulate an error output for rows that raise errors during processing by the Script component.  
  
## External Resources  
  
-   Blog entry, [VSTA setup and configuration troubles for SSIS 2008 and R2 installations](/archive/blogs/jason_howell/vsta-setup-and-configuration-troubles-for-ssis-2008-and-r2-installations), on blogs.msdn.com.  
  
## See Also  
 [Configuring the Script Component in the Script Component Editor](../../../integration-services/extending-packages-scripting/data-flow-script-component/configuring-the-script-component-in-the-script-component-editor.md)  
  
