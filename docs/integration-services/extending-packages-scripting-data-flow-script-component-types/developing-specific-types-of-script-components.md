---
title: "Developing Specific Types of Script Components | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "Script component [Integration Services], examples"
ms.assetid: dfbbe959-6b4e-4b47-b9dd-bcc31929482d
author: janinezhang
ms.author: janinez
manager: craigg
---
# Developing Specific Types of Script Components
  The Script component is a configurable tool that you can use in the data flow of a package to fill almost any requirement that is not met by the sources, transformations, and destinations that are included with [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. This section contains Script component code samples that demonstrate the four options for configuring the Script component:  
  
-   As a source.  
  
-   As a transformation with synchronous outputs.  
  
-   As a transformation with asynchronous outputs.  
  
-   As a destination.  
  
 For additional examples of the Script component, see [Additional Script Component Examples](../../integration-services/extending-packages-scripting-data-flow-script-component-examples/additional-script-component-examples.md).  
  
## In This Section  
 [Creating a Source with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-a-source-with-the-script-component.md)  
 Explains and demonstrates how to create a data flow source by using the Script component.  
  
 [Creating a Synchronous Transformation with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-a-synchronous-transformation-with-the-script-component.md)  
 Explains and demonstrates how to create a data flow transformation with synchronous outputs by using the Script component. This kind of transformation modifies rows of data in place as they pass through the component.  
  
 [Creating an Asynchronous Transformation with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-an-asynchronous-transformation-with-the-script-component.md)  
 Explains and demonstrates how to create a data flow transformation with asynchronous outputs by using the Script component. This kind of transformation has to read all rows of data before it can add more information, such as calculated aggregates, to the data that passes through the component.  
  
 [Creating a Destination with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-a-destination-with-the-script-component.md)  
 Explains and demonstrates how to create a data flow destination by using the Script component.  
  
## See Also  
 [Comparing Scripting Solutions and Custom Objects](../../integration-services/extending-packages-scripting/comparing-scripting-solutions-and-custom-objects.md)   
 [Developing Specific Types of Data Flow Components](../../integration-services/extending-packages-custom-objects-data-flow-types/developing-specific-types-of-data-flow-components.md)  
  
  
