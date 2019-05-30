---
title: "Extending Packages with Custom Objects | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
ms.assetid: 26616eb8-9e80-434d-b22a-ece1b00f449d
author: janinezhang
ms.author: janinez
manager: craigg
---
# Extending Packages with Custom Objects

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  If you find that the components provided in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] do not meet your requirements, you can extend the power of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] by coding your own extensions. You have two discrete options for extending your packages: you can write code within the powerful wrappers provided by the Script task and the Script component, or you can create custom [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] extensions from scratch by deriving from the base classes provided by the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model.  
  
 This section explores the more advanced of the two options - extending packages with custom objects.  
  
 When your custom [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] solution requires more flexibility than the Script task and the Script component provide, or when you need a component that you can reuse in multiple packages, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model lets you build custom tasks, data flow components, and other package objects in managed code from the ground up.  
  
## In This Section  
 [Developing Custom Objects for Integration Services](../../integration-services/extending-packages-custom-objects/developing-custom-objects-for-integration-services.md)  
 Discusses the custom objects that can be created for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], and summarizes the essential steps and settings.  
  
 [Persisting Custom Objects](../../integration-services/extending-packages-custom-objects/persisting-custom-objects.md)  
 Discusses the default persistence of custom objects, and the process of implementing custom persistence.  
  
 [Building, Deploying, and Debugging Custom Objects](../../integration-services/extending-packages-custom-objects/building-deploying-and-debugging-custom-objects.md)  
 Discusses the common approaches to building, deploying and testing the various types of custom objects.  
  
 [Developing a Custom Task](../../integration-services/extending-packages-custom-objects/task/developing-a-custom-task.md)  
 Describes the process of coding a custom task.  
  
 [Developing a Custom Connection Manager](../../integration-services/extending-packages-custom-objects/connection-manager/developing-a-custom-connection-manager.md)  
 Describes the process of coding a custom connection manager.  
  
 [Developing a Custom Log Provider](../../integration-services/extending-packages-custom-objects/log-provider/developing-a-custom-log-provider.md)  
 Describes the process of coding a custom log provider.  
  
 [Developing a Custom ForEach Enumerator](../../integration-services/extending-packages-custom-objects/foreach-enumerator/developing-a-custom-foreach-enumerator.md)  
 Describes the process of coding a custom enumerator.  
  
 [Developing a Custom Data Flow Component](../../integration-services/extending-packages-custom-objects/data-flow/developing-a-custom-data-flow-component.md)  
 Discusses how to program custom data flow sources, transformations, and destinations.  
  
## Reference  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)  
 Lists the predefined [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] error codes with their symbolic names and descriptions.  
  
## Related Sections  
 [Extending Packages with Scripting](../../integration-services/extending-packages-scripting/extending-packages-with-scripting.md)  
 Discusses how to extend the control flow by using the Script task, or extend the data flow by using the Script component.  
  
 [Building Packages Programmatically](../../integration-services/building-packages-programmatically/building-packages-programmatically.md)  
 Describes how to create, configure, run, load, save, and manage [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages programmatically.  
  
## See Also  
 [Comparing Scripting Solutions and Custom Objects](../../integration-services/extending-packages-scripting/comparing-scripting-solutions-and-custom-objects.md)   
 [SQL Server Integration Services](../../integration-services/sql-server-integration-services.md)  
  
  
