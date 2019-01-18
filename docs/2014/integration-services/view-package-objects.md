---
title: "View Package Objects | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Integration Services packages, properties"
  - "properties [Integration Services]"
  - "Package Explorer window [Integration Services]"
  - "packages [Integration Services], properties"
  - "explorer view [Integration Services]"
  - "SSIS packages, properties"
  - "viewing package objects"
  - "SQL Server Integration Services packages, properties"
ms.assetid: a85c0245-0a68-4eb0-83b1-9b11df80bd10
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# View Package Objects
  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, the **Package Explorer** tab provides an explorer view of the package. The view reflects the container hierarchy of the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] architecture. The package container is at the top of the hierarchy, and you expand the package to view the connections, executables, event handlers, log providers, precedence constraints, and variables in the package.  
  
 The executables, which are the containers and tasks in the package, can include event handlers, precedence constraints, and variables. [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] supports a nested hierarchy of containers, and the For Loop, Foreach Loop, and Sequence containers can include other executables.  
  
 If a package includes a data flow, the **Package Explorer** lists the Data Flow task and includes a **Components** folder that lists the data flow components.  
  
 From the **Package Explorer** tab, you can delete objects in a package and access the **Properties** window to view object properties.  
  
 The following diagram shows a tree view of a simple package.  
  
 ![Screenshot of the Package Explorer tab](media/packageexplorer.gif "Screenshot of the Package Explorer tab")  
  
### To view package content  
  
-   [View Package Objects in Package Explorer](../../2014/integration-services/view-package-objects-in-package-explorer.md)  
  
## See Also  
 [Integration Services Tasks](control-flow/integration-services-tasks.md)   
 [Integration Services Containers](control-flow/integration-services-containers.md)   
 [Precedence Constraints](control-flow/precedence-constraints.md)   
 [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md)   
 [Integration Services &#40;SSIS&#41; Event Handlers](integration-services-ssis-event-handlers.md)   
 [Integration Services &#40;SSIS&#41; Logging](performance/integration-services-ssis-logging.md)  
  
  
