---
title: "Developing Specific Types of Data Flow Components | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "data flow task [Integration Services], components"
  - "components [Integration Services], data flow"
  - "data flow [Integration Services], components"
ms.assetid: 348e219a-b8ff-425e-b9c6-811880101c54
author: janinezhang
ms.author: janinez
manager: craigg
---
# Developing Specific Types of Data Flow Components
  This section covers the specifics of developing source components, transformation components with synchronous outputs, transformation components with asynchronous outputs, and destination components.  
  
 For information about component development in general, see [Developing a Custom Data Flow Component](../../integration-services/extending-packages-custom-objects/data-flow/developing-a-custom-data-flow-component.md).  
  
## In This Section  
 [Developing a Custom Source Component](../../integration-services/extending-packages-custom-objects-data-flow-types/developing-a-custom-source-component.md)  
 Contains information on developing a component that accesses data from an external data source and supplies it to downstream components in the data flow.  
  
 [Developing a Custom Transformation Component with Synchronous Outputs](../../integration-services/extending-packages-custom-objects-data-flow-types/developing-a-custom-transformation-component-with-synchronous-outputs.md)  
 Contains information on developing a transformation component whose outputs are synchronous to its inputs. These components do not add data to the data flow, but process data as it passes through.  
  
 [Developing a Custom Transformation Component with Asynchronous Outputs](../../integration-services/extending-packages-custom-objects-data-flow-types/developing-a-custom-transformation-component-with-asynchronous-outputs.md)  
 Contains information on developing a transformation component whose outputs are not synchronous to its inputs. These components receive data from upstream components, but also add data to the dataflow.  
  
 [Developing a Custom Destination Component](../../integration-services/extending-packages-custom-objects-data-flow-types/developing-a-custom-destination-component.md)  
 Contains information on developing a component that receives rows from upstream components in the data flow and writes them to an external data source.  
  
## Reference  
 <xref:Microsoft.SqlServer.Dts.Pipeline>  
 Contains the classes and interfaces used to create custom data flow components.  
  
 <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper>  
 Contains the unmanaged classes and interfaces of the data flow task. The developer uses these, and the managed <xref:Microsoft.SqlServer.Dts.Pipeline> namespace, when building a data flow programmatically or creating custom data flow components.  
  
## See Also  
 [Comparing Scripting Solutions and Custom Objects](../../integration-services/extending-packages-scripting/comparing-scripting-solutions-and-custom-objects.md)   
 [Developing Specific Types of Script Components](../../integration-services/extending-packages-scripting-data-flow-script-component-types/developing-specific-types-of-script-components.md)  
  
  
