---
description: "Sequence Container"
title: "Sequence Container | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.sequencecontainer.f1"
helpviewer_keywords: 
  - "Sequence container"
  - "grouping control flows"
  - "containers [Integration Services], Sequence"
  - "subset control flow [Integration Services]"
ms.assetid: 7731f91e-b8b3-4d96-a0d9-73f568547cb3
author: chugugrace
ms.author: chugu
---
# Sequence Container

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The Sequence container defines a control flow that is a subset of the package control flow. Sequence containers group the package into multiple separate control flows, each containing one or more tasks and containers that run within the overall package control flow.  
  
 The Sequence container can include multiple tasks in addition to other containers. Adding tasks and containers to a Sequence container is similar to adding them to a package, except you drag the tasks and containers to the Sequence container instead of to the package container. If the Sequence container includes more than one task or container, you can connect them using precedence constraints just as you do in a package. For more information, see [Precedence Constraints](../../integration-services/control-flow/precedence-constraints.md).  
  
 There are many benefits of using a Sequence container:  
  
-   Disabling groups of tasks to focus package debugging on one subset of the package control flow.  
  
-   Managing properties on multiple tasks in one location by setting properties on a Sequence container instead of on the individual tasks.  
  
     For example, you can set the **Disable** property of the Sequence container to **True** to disable all the tasks and containers in the Sequence container.  
  
-   Providing scope for variables that a group of related tasks and containers use.  
  
-   Grouping many tasks so you can more easily managed them by collapsing and expanding the Sequence container.  
  
     You can also create task groups, which expand and collapse using the **Group** box. However, the **Group** box is a design-time feature that has no properties or run-time behavior. For more information, see [Group or Ungroup Components](../../integration-services/group-or-ungroup-components.md)  
  
-   Set a transaction attribute on the Sequence container to define a transaction for a subset of the package control flow. In this way, you can manage transactions at a more granular level.  
  
     For example, if a Sequence container includes two related tasks, one task that deletes data in a table and another task that inserts data into a table, you can configure a transaction to ensure that the delete action is rolled back if the insert action fails. For more information, see [Integration Services Transactions](../../integration-services/integration-services-transactions.md).  
  
## Configuration of the Sequence Container  
 The Sequence container has no custom user interface and you can configure it only in the **Properties** window of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or programmatically.  
  
 For information about programmatically setting these properties, see documentation for the **T:Microsoft.SqlServer.Dts.Runtime.Sequence** class in the Developer Guide.  
  
## Related Tasks  
 For information about how to set properties of the component in the [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], see [Set the Properties of a Task or Container](./add-or-delete-a-task-or-a-container-in-a-control-flow.md).  
  
## See Also  
 [Add or Delete a Task or a Container in a Control Flow](../../integration-services/control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md)   
 [Connect Tasks and Containers by Using a Default Precedence Constraint](./precedence-constraints.md)   
 [Integration Services Containers](../../integration-services/control-flow/integration-services-containers.md)  
  
