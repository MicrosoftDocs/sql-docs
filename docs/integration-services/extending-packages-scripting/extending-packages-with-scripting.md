---
title: "Extending Packages with Scripting | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Integration Services, scripting"
  - "SSIS, scripting"
  - "scripts [Integration Services], about scripting"
ms.assetid: 67fe18ef-f3aa-41d4-9b9d-5defd4618c4b
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Extending Packages with Scripting
  If you find that the built-in components [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] do not meet your requirements, you can extend the power of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] by coding your own extensions. You have two discrete options for extending your packages: you can write code within the powerful wrappers provided by the Script task and the Script component, or you can create custom [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] extensions from scratch by deriving from the base classes provided by the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model.  
  
 This section explores the simpler of the two options - extending packages with scripting.  
  
 The Script task and the Script component let you extend both the control flow and the data flow of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package with very little coding. Both objects use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Tools for Applications (VSTA) development environment and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual C# programming languages, and benefit from all the functionality offered by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] class library, as well as custom assemblies. The Script task and the Script component let the developer create custom functionality without having to write all the infrastructure code that is typically required when developing a custom task or custom data flow component.  
  
## In This Section  
 [Comparing the Script Task and the Script Component](../../integration-services/extending-packages-scripting/comparing-the-script-task-and-the-script-component.md)  
 Discusses the similarities and differences between the Script task and the Script component.  
  
 [Comparing Scripting Solutions and Custom Objects](../../integration-services/extending-packages-scripting/comparing-scripting-solutions-and-custom-objects.md)  
 Discusses the criteria to use in choosing between a scripting solution and the development of a custom object.  
  
 [Referencing Other Assemblies in Scripting Solutions](../../integration-services/extending-packages-scripting/referencing-other-assemblies-in-scripting-solutions.md)  
 Discusses the steps required to reference and use external assemblies and namespaces in a scripting project.  
  
 [Extending the Package with the Script Task](../../integration-services/extending-packages-scripting/task/extending-the-package-with-the-script-task.md)  
 Discusses how to create custom tasks by using the Script task. A task is typically called one time per package execution, or one time for each data source opened by a package.  
  
 [Extending the Data Flow with the Script Component](../../integration-services/extending-packages-scripting/data-flow-script-component/extending-the-data-flow-with-the-script-component.md)  
 Discusses how to create custom data flow sources, transformations, and destinations by using the Script component. A data flow component is typically called one time for each row of data that is processed.  
  
## Reference  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)  
 Lists the predefined [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] error codes with their symbolic names and descriptions.  
  
## Related Sections  
 [Extending Packages with Custom Objects](../../integration-services/extending-packages-custom-objects/extending-packages-with-custom-objects.md)  
 Discusses how to create program custom tasks, data flow components, and other package objects for use in multiple packages.  
  
 [Building Packages Programmatically](../../integration-services/building-packages-programmatically/building-packages-programmatically.md)  
 Describes how to create, configure, run, load, save, and manage [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages programmatically.  
  
## See Also  
 [SQL Server Integration Services](../../integration-services/sql-server-integration-services.md)  
  
  
