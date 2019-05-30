---
title: "Creating a Custom Log Provider | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "custom log providers [Integration Services], creating"
ms.assetid: fc20af96-9eb8-4195-8d3f-8a4d7c753f24
author: janinezhang
ms.author: janinez
manager: craigg
---
# Creating a Custom Log Provider

[!INCLUDE[ssis-appliesto](../../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] run-time environment has extensive logging capabilities. A log lets you capture events that occur during package execution. [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] includes a variety of log providers that enable logs to be created and stored in multiple formats, such as XML, text, database, or in the Windows event log. If one of these providers or output formats does not fit your needs, you can create a custom log provider.  
  
 The steps involved in creating a custom log provider are similar to the steps for creating any other custom object for [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)]:  
  
-   Create a new class that inherits from the base class. For a log provider, the base class is <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase>.  
  
-   Apply the attribute that identifies the type of object to the class. For a log provider, the attribute is <xref:Microsoft.SqlServer.Dts.Runtime.DtsLogProviderAttribute>.  
  
-   Override the implementation of the base class's methods and properties. For a log provider, these include the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.ConfigString%2A> property and the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.OpenLog%2A>, <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.Log%2A>, and <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase.CloseLog%2A> methods.  
  
-   Custom user interfaces for custom log providers are not implemented in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)].  
  
## Getting Started with a Custom Log Provider  
  
### Creating Projects and Classes  
 Because all managed log providers derive from the <xref:Microsoft.SqlServer.Dts.Runtime.LogProviderBase> base class, the first step when you create a custom log provider is to create a class library project in your preferred managed programming language, and then create a class that inherits from the base class. In this derived class you will override the methods and properties of the base class to implement your custom functionality.  
  
 Configure the project to sign the assembly that will be generated with a strong name key file.  
  
> [!NOTE]  
>  Many [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] log providers have a custom user interface that implements <xref:Microsoft.SqlServer.Dts.Runtime.Design.IDtsLogProviderUI> and replaces the **Configuration** text box in the **Configure SSIS Logs** dialog box with a filtered dropdown list of available connection managers. However custom user interfaces for custom log providers are not implemented in [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)].  
  
### Applying the DtsLogProvider Attribute  
 Apply the <xref:Microsoft.SqlServer.Dts.Runtime.DtsLogProviderAttribute> attribute to the class that you have created to identify it as a log provider. This attribute provides design-time information such as the name and description of the log provider. The **DisplayName** and **Description** properties of the attribute correspond to the **Name** and **Description** columns displayed in the **Configure SSIS Logs** editor, which is displayed when configuring logging for a package in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)].  
  
> [!IMPORTANT]  
>  The <xref:Microsoft.SqlServer.Dts.Runtime.DtsLogProviderAttribute.LogProviderType%2A> property of the attribute is not used. However, you must enter a value for it, or the custom log provider will not appear in the list of available log providers.  
  
> [!NOTE]  
>  Since custom user interfaces for custom log providers are not implemented in [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)], specifying a value for the <xref:Microsoft.SqlServer.Dts.Runtime.DtsLogProviderAttribute.UITypeName%2A> property of the <xref:Microsoft.SqlServer.Dts.Runtime.DtsLogProviderAttribute> has no effect.  
  
```vb  
<DtsLogProvider(DisplayName:="MyLogProvider", Description:="A simple log provider.", LogProviderType:="Custom")> _  
Public Class MyLogProvider  
     Inherits LogProviderBase  
    ' TODO: Override the base class methods.  
End Class  
```  
  
```csharp  
[DtsLogProvider(DisplayName="MyLogProvider", Description="A simple log provider.", LogProviderType="Custom")]  
public class MyLogProvider : LogProviderBase  
{  
    // TODO: Override the base class methods.  
}  
```  
  
## Building, Deploying, and Debugging a Custom Log Provider  
 The steps for building, deploying, and debugging a custom log provider in [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] are very similar to the steps required for other types of custom objects. For more information, see [Building, Deploying, and Debugging Custom Objects](../../../integration-services/extending-packages-custom-objects/building-deploying-and-debugging-custom-objects.md).  
  
## See Also  
 [Coding a Custom Log Provider](../../../integration-services/extending-packages-custom-objects/log-provider/coding-a-custom-log-provider.md)   
 [Developing a User Interface for a Custom Log Provider](../../../integration-services/extending-packages-custom-objects/log-provider/developing-a-user-interface-for-a-custom-log-provider.md)  
  
  
