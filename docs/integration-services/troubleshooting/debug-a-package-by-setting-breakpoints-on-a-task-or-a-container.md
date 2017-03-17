---
title: "Debug a Package by Setting Breakpoints on a Task or a Container | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "containers [Integration Services], breakpoints"
  - "breakpoints [Integration Services]"
  - "tasks [Integration Services], breakpoints"
ms.assetid: e7fa106a-2221-403a-bb74-efc9f12bb450
caps.latest.revision: 35
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Debug a Package by Setting Breakpoints on a Task or a Container
  This procedure describes how to set breakpoints in a package, a task, a For Loop container, a Foreach Loop container, or a Sequence container.  
  
### To set breakpoints in a package, a task, or a container  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  Double-click the package in which you want to set breakpoints.  
  
3.  In SSIS Designer, do the following:  
  
    -   To set breakpoints in the package object, click the **Control Flow** tab, place the cursor anywhere on the background of the design surface, right-click, and then click **Edit Breakpoints**.  
  
    -   To set breakpoints in a package control flow, click the **Control Flow** tab, right-click a task, a For Loop container, a Foreach Loop container, or a Sequence container, and then click **Edit Breakpoints**.  
  
    -   To set breakpoints in an event handler, click the **Event Handler** tab, right-click a task, a For Loop container, a Foreach Loop container, or a Sequence container, and then click **Edit Breakpoints**.  
  
4.  In the **Set Breakpoints \<container name>** dialog box, select the breakpoints to enable.  
  
5.  Optionally, modify the hit count type and the hit count number for each breakpoint.  
  
6.  To save the package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Troubleshooting Tools for Package Development](../../integration-services/troubleshooting/troubleshooting-tools-for-package-development.md)   
 [Debug a Script by Setting Breakpoints in a Script Task and Script Component](../../integration-services/extending-packages-scripting/debug-a-script-by-setting-breakpoints-in-a-script-task-and-script-component.md)   
 [Coding and Debugging the Script Task](../../integration-services/extending-packages-scripting/task/coding-and-debugging-the-script-task.md)   
 [Coding and Debugging the Script Component](../../integration-services/extending-packages-scripting/data-flow-script-component/coding-and-debugging-the-script-component.md)  
  
  