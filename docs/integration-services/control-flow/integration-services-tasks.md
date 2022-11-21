---
description: "Integration Services Tasks"
title: "Integration Services Tasks | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "scripts [Integration Services], tasks"
  - "files [Integration Services], task options"
  - "workflow tasks [Integration Services]"
  - "Integration Services, tasks"
  - "adding package tasks"
  - "tasks [Integration Services], listed"
  - "SSIS tasks"
  - "SSIS, tasks"
  - "control flow [Integration Services], tasks"
  - "tasks [Integration Services]"
  - "grouping tasks"
  - "tasks [Integration Services], about tasks"
  - "SQL Server Integration Services tasks"
  - "data preparation tasks [Integration Services]"
  - "directory operations [Integration Services]"
ms.assetid: 75c8901d-6966-4af3-abe5-10af6dd9313b
author: chugugrace
ms.author: chugu
---
# Integration Services Tasks

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Tasks are control flow elements that define units of work that are performed in a package control flow. An [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package is made up of one or more tasks. If the package contains more than one task, they are connected and sequenced in the control flow by precedence constraints.  
  
 You can also write custom tasks using a programming language that supports COM, such as Visual Basic, or a .NET programming language, such as C#.  
  
 The [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, the graphical tool in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] for working with packages, provides the design surface for creating package control flow, and provides custom editors for configuring tasks. You can also program the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model to create packages programmatically.  
  
## Types of Tasks  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes the following types of tasks.  
  
 Data Flow Task  
 The task that runs data flows to extract data, apply column level transformations, and load data.  
  
 Data Preparation Tasks  
 These tasks do the following processes: copy files and directories; download files and data; run Web methods; apply operations to XML documents; and profile data for cleansing.  
  
 Workflow Tasks  
 The tasks that communicate with other processes to run packages, run programs or batch files, send and receive messages between packages, send e-mail messages, read Windows Management Instrumentation (WMI) data, and watch for WMI events.  
  
 SQL Server Tasks  
 The tasks that access, copy, insert, delete, and modify [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects and data.  
  
 Scripting Tasks  
 The tasks that extend package functionality by using scripts.  
  
 Analysis Services Tasks  
 The tasks that create, modify, delete, and process [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects.  
  
 Maintenance Tasks  
 The tasks that perform administrative functions such as backing up and shrinking [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases, rebuilding and reorganizing indexes, and running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs.  
  
 Custom Tasks  
 Additionally, you can write custom tasks using a programming language that supports COM, such as Visual Basic, or a .NET programming language, such as C#. If you want to access your custom task in the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, you can create and register a user interface for the task. For more information, see [Developing a Custom Task](../../integration-services/extending-packages-custom-objects/task/developing-a-custom-task.md).  
  
## Configuration of Tasks  
 An [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package can contain a single task, such as an Execute SQL task that deletes records in a database table when the package runs. However, packages typically contain several tasks, and each task is set to run within the context of the package control flow. Event handlers, which are workflows that run in response to run-time events, can also have tasks.  
  
 For more information about adding a task to a package using [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Add or Delete a Task or a Container in a Control Flow](../../integration-services/control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md).  
  
 For more information about adding a task to a package programmatically, see [Adding Tasks Programmatically](../../integration-services/building-packages-programmatically/adding-tasks-programmatically.md).  
  
 Each task can be configured individually using the custom dialog boxes for each task that [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides, or the Properties window included in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. A package can include multiple tasks of the same type-for example, six Execute SQL tasks-and each task can be configured differently. For more information, see [Set the Properties of a Task or Container](./add-or-delete-a-task-or-a-container-in-a-control-flow.md).  
  
## Tasks Connections and Groups  
 If the task contains more than one task, they are connected and sequenced in the control flow by precedence constraints. For more information, see [Precedence Constraints](../../integration-services/control-flow/precedence-constraints.md).  
  
 Tasks can be grouped together and performed as a single unit of work, or repeated in a loop. For more information, see [Foreach Loop Container](../../integration-services/control-flow/foreach-loop-container.md), [For Loop Container](../../integration-services/control-flow/for-loop-container.md), and [Sequence Container](../../integration-services/control-flow/sequence-container.md).  
  
## Related Tasks  
 [Add or Delete a Task or a Container in a Control Flow](../../integration-services/control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md)  
  
