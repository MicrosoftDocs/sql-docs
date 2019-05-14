---
title: "SSIS Designer | Microsoft Docs"
ms.custom: ""
ms.date: "08/31/2016"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.controlflowwindow.f1"
  - "sql13.dts.designer.dataflowwindow.f1"
  - "sql13.dts.designer.eventhandlerwindow.f1"
  - "sql13.dts.designer.treeview.f1"
  - "sql13.dts.designer.progress.f1"
  - "sql13.dts.designer.connectionstray.f1"
helpviewer_keywords: 
  - "SQL Server Integration Services, SSIS Designer"
  - "Business Intelligence Development Studio, Integration Services in"
  - "tools [Integration Services], SSIS Designer"
  - "SSIS, SSIS Designer"
  - "SSIS Designer, about SSIS Designer"
  - "Integration Services, SSIS Designer"
ms.assetid: 006d68ea-1b5c-4c1e-8079-3910288e012c
author: janinezhang
ms.author: janinez
manager: craigg
---
# SSIS Designer

[!INCLUDE[ssis-appliesto](../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer is a graphical tool that you can use to create and maintain [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages. [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer is available in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] as part of an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project.  
  
 You can use [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer to perform the following tasks:  
  
-   Constructing the control flow in a package.  
  
-   Constructing the data flows in a package.  
  
-   Adding event handlers to the package and package objects.  
  
-   Viewing the package content.  
  
-   At run time, viewing the execution progress of the package.  
  
 The following diagram shows [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer and the **Toolbox** window.  
  
 ![Screenshot of SSIS Designer and Toolbox](../integration-services/media/denali-designerandtoolbox.gif "Screenshot of SSIS Designer and Toolbox")  
  
 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] includes additional dialog boxes and windows for adding functionality to packages, and [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] provides windows and dialog boxes for configuring the development environment and working with packages. For more information, see [Integration Services User Interface](../integration-services/integration-services-user-interface.md).  
  
 [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer has no dependency on the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service, the service that manages and monitors packages, and it is not required that the service be running to create or modify packages in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer. However, if you stop the service while [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer is open, you can no longer open the dialog boxes that [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer provides and you may receive the error message "RPC server is unavailable." To reset [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer and continue working with the package, you must close the designer, exit [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], and then reopen [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project, and the package.  
  
## Undo and Redo  
 You can undo and redo up to 20 actions in the [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer. For a package, undo /redo is available in the **Control Flow**, **Data Flow**, **Event Handlers**, and **Parameters** tabs, and in the **Variables** window. For a project, undo/redo is available in the **Project Parameters** window.  
  
 You can't undo/redo changes to the new **SSIS Toolbox**.  
  
 When you make changes to a component using the component editor, you undo and redo the changes as a set rather than undoing and redoing individual changes. The set of changes appears as a single action in the undo and redo drop-down list.  
  
 To undo an action, click the undo toolbar button, **Edit/Undo** menu item, or press CTRL+Z. To redo an action, click the redo toolbar button, **Edit/Redo** menu item or press CTRL + Y. You can undo and redo multiple actions, by clicking the arrow next to the toolbar button, highlighting multiple actions in the drop-down list, and then clicking in the list.  
  
## Parts of the SSIS Designer  
 [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer has five permanent tabs: one each for building package control flow, data flows, parameters, and event handlers, and one tab for viewing the contents of a package. At run time a sixth tab appears that shows the execution progress of a package while it is running and the execution results after it finishes.  
  
 In addition, [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer includes the Connection Managers area for adding and configuring the connection managers that a package uses to connect to data.  
  
### Control Flow Tab  
 You construct the control flow in a package on the design surface of the **Control Flow** tab. Drag items from **Toolbox** to the design surface and connect them into a control flow by clicking the icon for the item, and then dragging the arrow from one item to another.  
  
 For more information, see [Control Flow](../integration-services/control-flow/control-flow.md).  
  
### Data Flow Tab  
 If a package contains a Data flow task, you can add data flows to the package. You construct the data flows in a package on the design surface of the **Data Flow** tab. Drag items from **Toolbox** to the design surface and connect them into a data flow by clicking the icon for the item, and then dragging the arrow from one item to another.  
  
 For more information, see [Data Flow](../integration-services/data-flow/data-flow.md).  
  
### Parameters Tab  
 Integration Services (SSIS) parameters allow you to assign values to properties within packages at the time of package execution. You can create project parameters at the project level and package parameters at the package level. Project parameters are used to supply any external input the project receives to one or more packages in the project. Package parameters allow you to modify package execution without having to edit and redeploy the package. This tab allows you to manage package parameters.  
  
 For more information about parameters, see [Integration Services (SSIS) Parameters](integration-services-ssis-package-and-project-parameters.md).  
  
> **IMPORTANT!!**  Parameters are available only to projects developed for the project deployment model. Therefore, you will see the Parameters tab only for packages that are part of a project configured to use the project deployment model.  
  
### Event Handlers Tab  
 You construct the events in a package on the design surface of the **Event Handlers** tab. On the **Event Handlers** tab, you select the package or package object that you want to create an event handler for, and then select the event to associate with the event handler. An event handler has a control flow and optional data flows.  
  
 For more information, see [Add an Event Handler to a Package](https://msdn.microsoft.com/library/5e56885d-8658-480a-bed9-3f2f8003fd78).  
  
### Package Explorer Tab  
 Packages can be complex, including many tasks, connection managers, variables, and other elements. The explorer view of the package lets you see a complete list of package elements.  
  
 For more information, see [View Package Objects](../integration-services/view-package-objects.md).  
  
### Progress/Execution Result Tab  
 While a package is running, the **Progress** tab shows the execution progress of the package. After the package has finished running, the execution results remain available on the **Execution Result** tab.  
  
> **NOTE:** To enable or disable the display of messages on the **Progress** tab, toggle the **Debug Progress Reporting** option on the **SSIS** menu.  
  
#### Connection Managers Area  
 You add and modify the connection managers that a package uses in the **Connection Managers** area. [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] includes connection managers to connect to a variety of data sources, such as text files, OLE DB databases, and .NET providers.  
  
 For more information, see [Integration Services &#40;SSIS&#41; Connections](../integration-services/connection-manager/integration-services-ssis-connections.md) and [Create Connection Managers](https://msdn.microsoft.com/library/6ca317b8-0061-4d9d-b830-ee8c21268345).  
 
## Control Flow tab
Use the **Control Flow** tab of [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer to build the control flow in a [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package.  
  
 Create the control flow by dragging graphical objects that represent [!INCLUDE[ssIS](../includes/ssis-md.md)] tasks and containers from the **Toolbox** to the design surface of the **Control Flow** tab, and then connecting the objects by dragging the connector on an object to another object. Each connecting line represents a precedence constraint that specifies the order in which the tasks and containers run  
  
 Additionally, you can use [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer to add the following functionality from the **Control Flow** tab:  
  
-   Implement logging  
  
-   Create package configurations  
  
-   Sign the package with a certificate  
  
-   Manage variables  
  
-   Add annotations  
  
-   Configure breakpoints  
  
 To add these functions to individual tasks or containers in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, right-click the object on the design surface, and then select the option.  
 
## Data Flow tab
Use the **Data Flow** tab of [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer to create data flows in a [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package.  
  
 Create the data flows by dragging graphical objects that represent sources, transformations, and destinations from the **Toolbox** to the design surface of the **Data Flow** tab, and then connecting the objects to create paths that determine the sequence in which the transformations run.  
  
 Right-click a path, and then click **Data Viewers,** to add data viewers to view the data before and after each data flow object.  
  
 You can also use [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer to add the following functionality from the **Data Flow** tab:  
  
-   Manage variables  
  
-   Add annotations  
  
 To add these functions in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, right-click the design surface, and then select the option you want.  
 
## Event Handlers tab
  Use the **Event Handlers** tab of [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer to build a control flow in an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package. An event handler runs in response to an event raised by the package or by a task or container in the package.  
  
## Options  
 **Executable**  
 Select the executable for which you want to build an event handler. The executable can be the package, or a task or containers in the package.  
  
 **Event handler**  
 Select a type of event handler. Create the event handler by dragging items from the **Toolbox**.  
  
 **Delete**  
 Select an event handler, and remove it from the package by clicking **Delete**.  
  
 **Click here to create an \<event handler name\> for the executable \<executable name\>**  
 Click to create the event handler.  
  
 Create the control flow by dragging graphical objects that represent [!INCLUDE[ssIS](../includes/ssis-md.md)] tasks and containers from the **Toolbox** to the design surface of the **Event Handlers** tab, and then connecting the objects by using precedence constraints to define the sequence in which they run.  
  
 Additionally, to add annotations, right-click the design surface, and then on the menu, click **Add Annotation**.  
 
## Package Explorer tab
Use the **Package Explorer** tab of [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer to see a hierarchical view of all of the elements in a package: configurations, connections, event handlers, executable objects such as tasks and containers, log providers, precedence constraints, and variables. If a package contains a Data Flow task, the **Package Explorer** tab includes a node that contains a hierarchical view of the data flow components.  
  
 Right-click a package element, and then click **Properties** to show the properties of the element in the **Properties** window, or click **Delete** to delete the element. 
 
## Progress tab
Use the **Progress** tab of [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer to view the progress of execution of an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package when you run the package in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. The **Progress** tab lists the start time, the finish time, and the elapsed time for validation and execution of the package and its executables; any information or warnings for the package; progress notifications; the success or failure of the package; and any error messages that are generated during package execution.  
  
 To enable or disable the display of messages on the **Progress** tab, toggle the **Debug Progress Reporting** option on the **SSIS** menu. Disabling progress reporting can help improve performance while running a complex package in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)].  
  
 After the package stops running, the **Progress** tab becomes the **Execution Results** tab.  
 
## Connection Managers area
Packages use connection managers to connect to data sources such as files, relational databases, and servers.  
  
 Use the **Connections Managers** area of [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer to add, delete, modify, rename, and copy and paste the connection managers.  
  
 Right-click in this area, and then on the menu, click the option for the task you want to perform.
 
## Related Tasks  
  
-   [Create Packages in SQL Server Data Tools](../integration-services/create-packages-in-sql-server-data-tools.md)  
  
## See Also  
 [Integration Services User Interface](../integration-services/integration-services-user-interface.md)  
  
  
