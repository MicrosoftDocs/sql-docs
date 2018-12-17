---
title: "Developing Custom Objects for Integration Services | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "custom user interface [Integration Services]"
  - "custom objects [Integration Services]"
ms.assetid: ca1929a6-0ae6-47d7-b65f-08173b143720
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Developing Custom Objects for Integration Services
  When the control flow and data flow objects that are included with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] do not completely meet your requirements, you can develop many types of custom objects on your own including:  
  
-   **Custom tasks**.  
  
-   **Custom connection managers.** Connect to external data sources that are not currently supported.  
  
-   **Custom log providers.** Log package events in formats that are not currently supported.  
  
-   **Custom enumerators.** Support iteration over a set of objects or values formats that are not currently supported.  
  
-   **Custom data flow components.** Can be configured as sources, transformations, or destinations.  
  
 The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model facilitates this custom development with base classes that provide a consistent and reliable framework for your custom implementation.  
  
 If you do not have to reuse custom functionality across multiple packages, the Script task and the Script component give you the full power of a managed programming language with significantly less infrastructure code to write. For more information, see [Comparing Scripting Solutions and Custom Objects](../extending-packages-scripting/comparing-scripting-solutions-and-custom-objects.md).  
  
## Steps in Developing a Custom Object for Integration Services  
 When you develop a custom object for use in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], you develop a Class Library (a DLL) that will be loaded at design time and run time by SSIS Designer and by the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] runtime. The most important methods that you must implement are not methods that you call from your own code, but methods that the runtime calls at appropriate times to initialize and validate your component and to invoke its functionality.  
  
 Here are the steps that you follow in developing a custom object:  
  
1.  Create a new project of type Class Library in your preferred managed programming language.  
  
2.  Inherit from the appropriate base class, as shown in the following table.  
  
3.  Apply the appropriate attribute to your new class, as shown in the following table.  
  
4.  Override the methods of the base class as required and write code for the custom functionality of your object.  
  
5.  Optionally, build a custom user interface for your component. For ease of deployment, you may want to develop the user interface as a separate project within the same solution, and to build it as a separate assembly.  
  
6.  Optionally, display a link to samples and Help content for the custom object, in the **SSIS Toolbox**.  
  
7.  Build, deploy, and debug your new custom object as described in [Building, Deploying, and Debugging Custom Objects](building-deploying-and-debugging-custom-objects.md).  
  
## Base Classes, Attributes, and Important Methods  
 This table provides an easy reference to the most important elements in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model for each type of custom object that you can develop.  
  
|Custom object|Base class|Attribute|Important methods|  
|-------------------|----------------|---------------|-----------------------|  
|Task|<xref:Microsoft.SqlServer.Dts.Runtime.Task>|<xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute>|<xref:Microsoft.SqlServer.Dts.Runtime.Task.Execute%2A>|  
|Connection manager|<xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManagerBase>|<xref:Microsoft.SqlServer.Dts.Runtime.DtsConnectionAttribute>|<xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManagerBase.AcquireConnection%2A>, <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManagerBase.ReleaseConnection%2A>|  
|Log provider|<xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase>|<xref:Microsoft.SqlServer.Dts.Runtime.DtsLogProviderAttribute>|<xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.OpenLog%2A>, <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.Log%2A>, <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.CloseLog%2A>|  
|Enumerator|<xref:Microsoft.SqlServer.Dts.Runtime.ForEachEnumerator>|<xref:Microsoft.SqlServer.Dts.Runtime.DtsForEachEnumeratorAttribute>|<xref:Microsoft.SqlServer.Dts.Runtime.ForEachEnumerator.GetEnumerator%2A>|  
|Data flow component|<xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent>|<xref:Microsoft.SqlServer.Dts.Pipeline.DtsPipelineComponentAttribute>|<xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProvideComponentProperties%2A>, <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.PrimeOutput%2A>, <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ProcessInput%2A>|  
  
## Providing Links to Samples and Help Content  
 To display a link in the **SSIS Toolbox** to samples and Help content for a custom object written in managed code, use the following properties.  
  
-   <xref:Microsoft.SqlServer.Dts.Pipeline.DTSPipelineComponentAttribute.SamplesTag%2A>  
  
-   <xref:Microsoft.SqlServer.Dts.Pipeline.DTSPipelineComponentAttribute.HelpCollection%2A>  
  
-   <xref:Microsoft.SqlServer.Dts.Pipeline.DTSPipelineComponentAttribute.HelpKeyword%2A>  
  
-   <xref:Microsoft.SqlServer.Dts.Runtime.DTSTaskAttribute.SamplesTag%2A>  
  
-   <xref:Microsoft.SqlServer.Dts.Runtime.DTSTaskAttribute.HelpCollection%2A>  
  
-   <xref:Microsoft.SqlServer.Dts.Runtime.DTSTaskAttribute.HelpKeyword%2A>  
  
 To display a link to samples and Help content for a custom object written in native code, add entries in the Registry Script (.rgs) file for SamplesTag, HelpKeyword, and HelpCollection. The following is an example.  
  
 `val HelpKeyword = s 'sql11.dts.designer.executepackagetask.F1'`  
  
 `val SamplesTag = s 'ExecutePackageTask'`  
  
## Providing a Custom User Interface  
 To allow users of your custom object to configure its properties, you may have to develop a custom user interface also. In those cases where a custom user interface is not strictly required, you may choose to create one to provide a more user-friendly interface than the default editor.  
  
 In a custom user interface project or assembly, you generally have two classes -a class that implements an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] interface for user interfaces for the specific type of custom object, and the Windows form that it displays to gather information from the user. The interfaces that you implement have only a few methods, and a custom user interface is not difficult to develop.  
  
> [!NOTE]  
>  Many [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] log providers have a custom user interface that implements <xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsLogProviderUI> and replaces the **Configuration** text box with a filtered drop-down list of available connection managers. However custom user interfaces for custom log providers are not implemented in this release of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. Specifying a value for the <xref:Microsoft.SqlServer.Dts.Runtime.DtsLogProviderAttribute.UITypeName%2A> property of the <xref:Microsoft.SqlServer.Dts.Runtime.DtsLogProviderAttribute> has no effect.  
  
 The following table provides an easy reference to the interfaces that you must implement when you develop a custom user interface for each type of custom object. It also explains what the user sees if you choose not to develop a custom user interface for your object, or if you fail to link your object to its user interface by using the `UITypeName` property in the object's attribute. Although the powerful Advanced Editor may be satisfactory for a data flow component, the Properties window is a less user-friendly solution for tasks and connection managers, and a custom ForEach enumerator cannot be configured at all without a custom form.  
  
|Custom object|Base class for user interface|Default editing behavior if no custom user interface is provided|  
|-------------------|-----------------------------------|----------------------------------------------------------------------|  
|Task|<xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsTaskUI>|Properties window only|  
|Connection manager|<xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsConnectionManagerUI>|Properties window only|  
|Log provider|<xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsLogProviderUI><br /><br /> (Not implemented in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)])|Text box in **Configuration** column|  
|Enumerator|<xref:Microsoft.SqlServer.Dts.Runtime.ForEachEnumeratorUI>|Properties window only. Enumerator Configuration area of editor is empty.|  
|Data flow component|<xref:Microsoft.SqlServer.Dts.Pipeline.Design.IDtsComponentUI>|Advanced Editor|  
  
## External Resources  
  
-   Blog entry, [Visual Studio solution build process give a warning about indirect dependency on the .NET Framework assembly due to SSIS references](https://go.microsoft.com/fwlink/?LinkId=215662), on blogs.msdn.com.  
  
![Integration Services icon (small)](../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Persisting Custom Objects](persisting-custom-objects.md)   
 [Building, Deploying, and Debugging Custom Objects](building-deploying-and-debugging-custom-objects.md)  
  
  
