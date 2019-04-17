---
title: "Group or Ungroup Components | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "grouping containers"
  - "tasks [Integration Services], grouping"
  - "containers [Integration Services], grouping"
  - "grouping tasks"
ms.assetid: 34320838-c271-4f8c-90b3-1254690890bb
author: janinezhang
ms.author: janinez
manager: craigg
---
# Group or Ungroup Components
  The **Control Flow**, **Data Flow**, and **Event Handlers** tabs in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer supports collapsible grouping. If a package has many components, the tabs can become crowded, making it difficult to view all the components at one time and to locate the item with which you want to work. The collapsible grouping feature can conserve space on the work surface and make it easier to work with large packages.  
  
 You select the components that you want to group, group them, and then expand or collapse the groups to suit your work. Expanding a group provides access to the properties of the components in the group. The precedence constraints that connect tasks and containers are automatically included in the group.  
  
 The following are considerations for grouping components.  
  
-   To group components, the control flow, data flow, or event handler must contain more than one component.  
  
-   Groups can also be nested, making it possible to create groups within groups. The design surface can implement multiple un-nested groups, but a component can belong to only one group, unless the groups are nested.  
  
-   When a package is saved, [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer saves the grouping, but the grouping has no effect on package execution. The ability to group components is a design-time feature; it does not affect the run-time behavior of the package.  
  
### To group components  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow**, **Data Flow**, or **Event Handlers** tab.  
  
4.  On the design surface of the tab, select the components you want to group, right-click a selected component, and then click **Group**.  
  
5.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
### To ungroup components  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow**, **Data Flow**, or **Event Handlers** tab.  
  
4.  On the design surface of the tab, select the group that contains the component you want to ungroup, right-click, and then click **Ungroup**.  
  
5.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Add or Delete a Task or a Container in a Control Flow](../integration-services/control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md)   
 [Connect Tasks and Containers by Using a Default Precedence Constraint](https://msdn.microsoft.com/library/8f31f15f-98ff-4c35-b41f-8b8cfd148d75)  
  
  
