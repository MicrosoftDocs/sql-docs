---
title: "Developing a Custom ForEach Enumerator | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "custom foreach enumerators [Integration Services]"
  - "custom foreach enumerators [Integration Services], about custom foreach enumerators"
  - "foreach enumerators [Integration Services]"
ms.assetid: bffe26e0-1b9a-47ad-bae6-6b708cb4cf4f
author: janinezhang
ms.author: janinez
manager: craigg
---
# Developing a Custom ForEach Enumerator
  [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] uses foreach enumerators to iterate over the items in a collection and perform the same tasks for each element. [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] includes a variety of foreach enumerators that support the most commonly used collections, such as all the files in a folder, all the tables in a database, or all the elements of a list stored in a package variable. If the foreach enumerators and collections that are provided do not entirely meet your requirements, you can create a custom foreach enumerator.  
  
 To create a custom foreach enumerator, you have to create a class that inherits from the <xref:Microsoft.SqlServer.Dts.Runtime.ForEachEnumerator> base class, apply the <xref:Microsoft.SqlServer.Dts.Runtime.DtsForEachEnumeratorAttribute> attribute to your new class, and override the important methods and properties of the base class, including the <xref:Microsoft.SqlServer.Dts.Runtime.ForEachEnumerator.GetEnumerator%2A> method.  
  
## In This Section  
 This section describes how to create, configure, and code a custom foreach enumerator and its custom user interface.  
  
 [Creating a Custom Foreach Enumerator](../../../integration-services/extending-packages-custom-objects/foreach-enumerator/creating-a-custom-foreach-enumerator.md)  
 Describes how to create the classes for a custom foreach enumerator project.  
  
 [Coding a Custom Foreach Enumerator](../../../integration-services/extending-packages-custom-objects/foreach-enumerator/coding-a-custom-foreach-enumerator.md)  
 Describes how to implement a custom foreach enumerator by overriding the methods and properties of the base class.  
  
 [Developing a User Interface for a Custom ForEach Enumerator](../../../integration-services/extending-packages-custom-objects/foreach-enumerator/developing-a-user-interface-for-a-custom-foreach-enumerator.md)  
 Describes how to implement the user interface class and the form that is used to configure the custom foreach enumerator.  
  
## Related Topics  
  
### Information Common to all Custom Objects  
 For information that is common to all the type of custom objects that you can create in [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)], see the following topics:  
  
 [Developing Custom Objects for Integration Services](../../../integration-services/extending-packages-custom-objects/developing-custom-objects-for-integration-services.md)  
 Describes the basic steps in implementing all types of custom objects for [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)].  
  
 [Persisting Custom Objects](../../../integration-services/extending-packages-custom-objects/persisting-custom-objects.md)  
 Describes custom persistence and explains when it is necessary.  
  
 [Building, Deploying, and Debugging Custom Objects](../../../integration-services/extending-packages-custom-objects/building-deploying-and-debugging-custom-objects.md)  
 Describes the techniques for building, signing, deploying, and debugging custom objects.  
  
### Information about Other Custom Objects  
 For information on the other types of custom objects that you can create in [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)], see the following topics:  
  
 [Developing a Custom Task](../../../integration-services/extending-packages-custom-objects/task/developing-a-custom-task.md)  
 Discusses how to program custom tasks.  
  
 [Developing a Custom Connection Manager](../../../integration-services/extending-packages-custom-objects/connection-manager/developing-a-custom-connection-manager.md)  
 Discusses how to program custom connection managers.  
  
 [Developing a Custom Log Provider](../../../integration-services/extending-packages-custom-objects/log-provider/developing-a-custom-log-provider.md)  
 Discusses how to program custom log providers.  
  
 [Developing a Custom Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/developing-a-custom-data-flow-component.md)  
 Discusses how to program custom data flow sources, transformations, and destinations.  
 
