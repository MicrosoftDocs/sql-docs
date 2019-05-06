---
title: "Debug a Script by Setting Breakpoints in a Script Task and Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "breakpoints [Integration Services]"
  - "scripts [Integration Services], breakpoints"
ms.assetid: 6c03464f-3f7d-4882-b7f8-8e396f8e2944
author: janinezhang
ms.author: janinez
manager: craigg
---
# Debug a Script by Setting Breakpoints in a Script Task and Script Component
  This procedure describes how to set breakpoints in the scripts that are used in the Script task and Script component.  
  
 After you set breakpoints in the script, the **Set Breakpoints - \<object name>** dialog box lists the breakpoints, along with the built-in breakpoints.  
  
> [!IMPORTANT]  
>  Under certain circumstances, breakpoints in the Script task and Script component are ignored. For more information, see the **Debugging the Script Task** section in [Coding and Debugging the Script Task](../../integration-services/extending-packages-scripting/task/coding-and-debugging-the-script-task.md) and the **Debugging the Script Component** section in [Coding and Debugging the Script Component](../../integration-services/extending-packages-scripting/data-flow-script-component/coding-and-debugging-the-script-component.md).  
  
### To set a breakpoint in script  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  Double-click the package that contains the script in which you want to set breakpoints.  
  
3.  To open the Script task, click the **Control Flow** tab, and then double-click the Script task.  
  
4.  To open the Script component, click the **Data Flow** tab, and then double-click the Script component.  
  
5.  Click **Script** and then click **Edit Script**.  
  
6.  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Tools for Applications (VSTA), locate the line of script on which you want to set a breakpoint, right-click that line, point to **Breakpoint**, and then click **Insert Breakpoint**.  
  
     The breakpoint icon appears on the line of code.  
  
7.  On the **File** menu, click **Exit**.  
  
8.  Click **OK**.  
  
9. To save the package, click **Save Selected Items** on the **File** menu.  
  
  
