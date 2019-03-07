---
title: "Developing a Custom Data Flow Component | Microsoft Docs"
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
  - "data flow task [Integration Services], extending"
  - "data flow [Integration Services], extending"
  - "extending data flow task [Integration Services]"
  - "components [Integration Services], data flow"
ms.assetid: be126913-2a9a-41c9-9bf5-a7b0a0aea2f8
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Developing a Custom Data Flow Component
  The data flow task consists of components that connect to a variety of data sources and then transform and route that data at high speed. [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] provides an extensible object model that lets developers create custom sources, transformations, and destinations that you can use in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)] and in deployed packages. This section contains topics that will guide you in developing custom data flow components.  
  
## In This Section  
 [Creating a Custom Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/creating-a-custom-data-flow-component.md)  
 Describes the initial steps in creating a custom data flow component.  
  
 [Design-time Methods of a Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/design-time-methods-of-a-data-flow-component.md)  
 Describes the design-time methods to implement in a custom data flow component.  
  
 [Run-time Methods of a Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/run-time-methods-of-a-data-flow-component.md)  
 Describes the run-time methods to implement in a custom data flow component.  
  
 [Execution Plan and Buffer Allocation](../../../integration-services/extending-packages-custom-objects/data-flow/execution-plan-and-buffer-allocation.md)  
 Describes the data flow execution plan and the allocation of data buffers.  
  
 [Working with Data Types in the Data Flow](../../../integration-services/extending-packages-custom-objects/data-flow/working-with-data-types-in-the-data-flow.md)  
 Explains how the data flow maps [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] data types to .NET Framework managed data types.  
  
 [Validating a Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/validating-a-data-flow-component.md)  
 Explains the methods used to validate component configuration and to reconfigure component metadata.  
  
 [Implementing External Metadata](../../../integration-services/extending-packages-custom-objects/data-flow/implementing-external-metadata.md)  
 Explains how to use external metadata columns for data validation.  
  
 [Raising and Defining Events in a Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/raising-and-defining-events-in-a-data-flow-component.md)  
 Explains how to raise predefined and custom events.  
  
 [Logging and Defining Log Entries in a Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/logging-and-defining-log-entries-in-a-data-flow-component.md)  
 Explains how to create and write to custom log entries.  
  
 [Using Error Outputs in a Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/using-error-outputs-in-a-data-flow-component.md)  
 Explains how to redirect error rows to an alternative output.  
  
 [Upgrading the Version of a Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/upgrading-the-version-of-a-data-flow-component.md)  
 Explains how to update saved component metadata when a new version of your component is first used.  
  
 [Developing a User Interface for a Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/developing-a-user-interface-for-a-data-flow-component.md)  
 Explains how to implement a custom editor for a component.  
  
 [Developing Specific Types of Data Flow Components](../../../integration-services/extending-packages-custom-objects-data-flow-types/developing-specific-types-of-data-flow-components.md)  
 Contains information about developing the three types of data flow components: sources, transformations, and destinations.  
  
## Reference  
 <xref:Microsoft.SqlServer.Dts.Pipeline>  
 Contains the classes and interfaces used to create custom data flow components.  
  
 <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper>  
 Contains the classes and interfaces that make up the data flow task object model, and is used to create custom data flow components or build a data flow task.  
  
 <xref:Microsoft.SqlServer.Dts.Pipeline.Design>  
 Contains the classes and interfaces used to create the user interface for data flow components.  
  
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)  
 Lists the predefined [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] error codes with their symbolic names and descriptions.  
  
## Related Sections  
  
### Information Common to All Custom Objects  
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
  
 [Developing a Custom ForEach Enumerator](../../../integration-services/extending-packages-custom-objects/foreach-enumerator/developing-a-custom-foreach-enumerator.md)  
 Discusses how to program custom enumerators.  
  
## See Also  
 [Extending the Data Flow with the Script Component](../../../integration-services/extending-packages-scripting/data-flow-script-component/extending-the-data-flow-with-the-script-component.md)   
 [Comparing Scripting Solutions and Custom Objects](../../../integration-services/extending-packages-scripting/comparing-scripting-solutions-and-custom-objects.md)  
  
  
