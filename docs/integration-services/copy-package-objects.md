---
title: "Copy Package Objects | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "control flow [Integration Services], copying objects"
  - "copying package objects [Integration Services]"
  - "data flow [Integration Services], copying objects"
  - "connection managers [Integration Services], copying"
ms.assetid: 99b85e5c-d6bd-4e7c-afe4-51f6ce151c2f
author: janinezhang
ms.author: janinez
manager: craigg
---
# Copy Package Objects
  This topic describes how to copy control flow items, data flow items, and connection managers within a package or between packages.  
  
### To copy control and data flow items  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the packages that you want work with.  
  
2.  In Solution Explorer, double-click the packages that you want to copy between.  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click the tab for the package that contains the items to copy and click the **Control Flow**, **Data Flow**, or **Event Handlers** tab.  
  
4.  Select the control flow or data flow items to copy. You can either select items one at a time by pressing the Shift key and clicking the item or select items as a group by dragging the pointer across the items you want to select.  
  
    > [!IMPORTANT]  
    >  The precedence constraints and paths that connect items are not selected automatically when you select the two items that they connect. To copy an ordered workflow-a segment of control flow or data flow-make sure to also copy the precedence constrains and the paths.  
  
5.  Right-click a selected item and click **Copy**.  
  
6.  If copying items to a different package, click the package that you want to copy to, and then click the appropriate tab for the item type.  
  
    > [!IMPORTANT]  
    >  You cannot copy a data flow to a package unless the package contains at least one Data Flow task.  
  
7.  Right-click and click **Paste**.  
  
### To copy connection managers  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package that you want to work with.  
  
2.  In Solution Explorer, double-click the package.  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click the **Control Flow**, **Data Flow**, or **Event Handler** tab.  
  
4.  In the **Connection Managers** area, right-click the connection manager, and then click **Copy**. You can copy only one connection manager at a time.  
  
5.  If you are copying items to a different package, click the package that you want to copy to and then click the **Control Flow**, **Data Flow**, or **Event Handler** tab.  
  
6.  Right-click in the **Connection Managers** area and click **Paste**.  
  
## See Also  
 [Control Flow](../integration-services/control-flow/control-flow.md)   
 [Data Flow](../integration-services/data-flow/data-flow.md)   
 [Integration Services &#40;SSIS&#41; Connections](../integration-services/connection-manager/integration-services-ssis-connections.md)   
 [Copy Project Items](https://msdn.microsoft.com/library/1606c54d-20f9-49f3-a4ef-caad83a772aa)  
  
  
