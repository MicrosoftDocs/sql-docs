---
title: "Add Iteration to a Control Flow | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "repeating workflows"
  - "adding iterations"
  - "control flow [Integration Services], iterations"
  - "expressions [Integration Services], control flow"
  - "iterations [Integration Services]"
  - "For Loop containers"
ms.assetid: eb3a7494-88ae-4165-9d0f-58715eb1734a
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Add Iteration to a Control Flow
  [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] includes the For Loop container, a control flow element that makes it simple to include looping that conditionally repeats a control flow in a package. For more information, see [For Loop Container](control-flow/for-loop-container.md).  
  
 The For Loop container evaluates a condition on each iteration of the loop, and stops when the condition evaluates to false. The For Loop container includes expressions for initializing the loop, specifying the evaluation condition that stops execution of the repeating control flow, and assigning a value to an expression that updates the value against which the evaluation condition is compared. You must provide an evaluation condition, but initialization and assignment expressions are optional.  
  
 The For Loop container provides no functionality; it provides only the structure in which you build the repeatable control flow. To provide container functionality, you must include at least one task in the For Loop container. For more information, see [Integration Services Tasks](control-flow/integration-services-tasks.md).  
  
 The For Loop container can include a control flow with multiple tasks, and can include other containers. Adding tasks and containers to a For Loop container is similar to adding them to a package, except you drag the tasks and containers to the For Loop container instead of to the package. If the For Loop container includes more than one task or container, you can connect them using precedence constraints just as you do in a package. For more information, see [Precedence Constraints](control-flow/precedence-constraints.md).  
  
## Using Expressions in For Loop Configuration  
 When you configure the For Loop container by specifying an evaluation condition, initialization value, or assignment value, you can use either literals or expressions.  
  
 The expressions can include variables. The advantage of using variables is that they can be updated at run time, making the packages more flexible and easier to manage. The maximum length of an expression is 4000 characters.  
  
 When you specify a variable in an expression, you must preface the variable name with the at sign (@). For example, for a variable named `Counter`, enter @Counter in the expression that the For Loop container uses. If you include the namespace property on the variable, you must enclose the variable and namespace in brackets. For example, for a `Counter` variable in the `MyNamespace` namespace, type [@MyNamespace::Counter].  
  
 The variables that the For Loop container uses must be defined in the scope of the For Loop container or in the scope of any container that is higher in the package container hierarchy. For example, a For Loop container can use variables defined in its scope and also variables defined in package scope. For more information, see [Integration Services &#40;SSIS&#41; Variables](integration-services-ssis-variables.md) and [Use Variables in Packages](../../2014/integration-services/use-variables-in-packages.md).  
  
 The [!INCLUDE[ssIS](../includes/ssis-md.md)] expression grammar provides a complete set of operators and functions for implementing complex expressions used for evaluation, initialization, or assignment. For more information, see [Integration Services &#40;SSIS&#41; Expressions](expressions/integration-services-ssis-expressions.md).  
  
### To implement a For Loop container in a control flow  
  
1.  Add the For Loop container to the package. For more information, see [Add or Delete a Task or a Container in a Control Flow](control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md)  
  .  
  
2.  Add tasks and containers to the For Loop container. For more information, see [Add or Delete a Task or a Container in a Control Flow](control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md)  
  .  
  
3.  Connect tasks and containers in the For Loop container using precedence constraints. For more information, see [Connect Tasks and Containers by Using a Default Precedence Constraint](../../2014/integration-services/connect-tasks-and-containers-by-using-a-default-precedence-constraint.md).  
  
4.  Configure the For Loop container. For more information, see [Configure a For Loop Container](../../2014/integration-services/configure-a-for-loop-container.md).  
  
## See Also  
 [Add or Delete a Task or a Container in a Control Flow](control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md)   
 [Group or Ungroup Components](group-or-ungroup-components.md)   
 [Connect Tasks and Containers by Using a Default Precedence Constraint](../../2014/integration-services/connect-tasks-and-containers-by-using-a-default-precedence-constraint.md)   
 [Add Enumeration to a Control Flow](../../2014/integration-services/add-enumeration-to-a-control-flow.md)   
 [Control Flow](control-flow/control-flow.md)  
  
  
