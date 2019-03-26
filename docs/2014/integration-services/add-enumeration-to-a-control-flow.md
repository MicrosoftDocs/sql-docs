---
title: "Add Enumeration to a Control Flow | Microsoft Docs"
ms.custom: ""
ms.date: "08/22/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "adding enumerations"
  - "Foreach Loop containers"
  - "control flow [Integration Services], enumerations"
  - "repeating workflows"
  - "enumerations [Integration Services]"
ms.assetid: f212b5fb-3cc4-422e-9b7c-89eb769a812a
author: janinezhang
ms.author: janinez
manager: craigg
---
# Add Enumeration to a Control Flow
  [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] includes the Foreach Loop container, a control flow element that makes it simple to include a looping construct that enumerates files and objects in the control flow of a package. For more information, see [Foreach Loop Container](control-flow/foreach-loop-container.md).  
  
 The Foreach Loop container provides no functionality; it provides only the structure in which you build the repeatable control flow, specify an enumerator type, and configure the enumerator. To provide container functionality, you must include at least one task in the Foreach Loop container. For more information, see [Integration Services Tasks](control-flow/integration-services-tasks.md).  
  
 The Foreach Loop container can include a control flow with multiple tasks and other containers. Adding tasks and containers to a Foreach Loop container is similar to adding them to a package, except you drag the tasks and containers to the Foreach Loop container instead of to the package. If the Foreach Loop container includes more than one task or container, you can connect them using precedence constraints just as you do in a package. For more information, see [Precedence Constraints](control-flow/precedence-constraints.md).  
  
### To implement a Foreach Loop container in a control flow  
  
1.  Add the Foreach Loop container to the package. For more information, see [Add or Delete a Task or a Container in a Control Flow](control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md)  
  .  
  
2.  Add tasks and containers to the Foreach Loop container. For more information, see [Add or Delete a Task or a Container in a Control Flow](control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md)  
  .  
  
3.  Connect tasks and containers in the Foreach Loop container using precedence constraints. For more information, see [Connect Tasks and Containers by Using a Default Precedence Constraint](../../2014/integration-services/connect-tasks-and-containers-by-using-a-default-precedence-constraint.md).  
  
4.  Configure the Foreach Loop container. For more information, see [Configure a Foreach Loop Container](../../2014/integration-services/configure-a-foreach-loop-container.md).  
  
## See Also  
 [Add or Delete a Task or a Container in a Control Flow](control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md)   
 [Group or Ungroup Components](group-or-ungroup-components.md)   
 [Precedence Constraints](control-flow/precedence-constraints.md)   
 [Add Iteration to a Control Flow](add-iteration-to-a-control-flow.md)   
 [Control Flow](control-flow/control-flow.md)  
  
  
