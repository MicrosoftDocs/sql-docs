---
title: "Add or Delete a Task or a Container in a Control Flow | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "containers [Integration Services], adding"
  - "adding tasks"
  - "adding containers"
  - "tasks [Integration Services], adding"
ms.assetid: 653084c6-87a3-45d5-b458-914ecf24d56a
author: janinezhang
ms.author: janinez
manager: craigg
---
# Add or Delete a Task or a Container in a Control Flow

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  When you are working in the control flow designer, the Toolbox in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer lists the tasks that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides for building control flow in a package. For more information about the Toolbox, see [SSIS Toolbox](../../integration-services/ssis-toolbox.md).  
  
 A package can include multiple instances of the same task. Each instance of a task is uniquely identified in the package, and you can configure each instance differently.  
  
 If you delete a task, the precedence constraints that connect the task to other tasks and containers in the control flow are also deleted.  
  
 The following procedures describe how to add or delete a task or container in the control flow of a package.  
  
## Add a task or a container to a control flow  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab.  
  
4.  To open the **Toolbox**, click **Toolbox** on the **View** menu.  
  
5.  Expand **Control Flow Items** and **Maintenance Tasks**.  
  
6.  Drag tasks and containers from the **Toolbox** to the design surface of the **Control Flow** tab.  
  
7.  Connect a task or container within the package control flow by dragging its connector to another component in the control flow.  
  
8.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## Delete a task or a container from a control flow  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it. Do one of the following:  
  
    -   Click the **Control Flow** tab, right-click the task or container that you want to remove, and then click **Delete**.  
  
    -   Open **Package Explorer**. From the **Executables** folder, right-click the task or container that you want to remove, and then click **Delete**.  
  
3.  To save the updated package, click **Save Selected Items** on the **File** menu.  

## Set the properties of a task or container
You can set most properties of tasks and containers by using the **Properties** window. The exceptions are properties of task collections and properties that are too complex to set by using the **Properties** window. For example, you cannot configure the enumerator that the Foreach Loop container uses in the **Properties** window. You must use a task or container editor to set these complex properties. Most task and container editors have multiple nodes and each node contains related properties. The name of the node indicates the subject of the properties that the node contains.  
  
 The following procedures describe how to set the properties of a task or container by using either the **Properties** windows, or the corresponding task or container editor.  
  
### Set the properties of a task or container with the Properties window  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab.  
  
4.  On the design surface of the **Control Flow** tab, right-click the task or container, and then click **Properties**.  
  
5.  In the **Properties** window, update the property value.  
  
    > [!NOTE]  
    >  Most properties can be set by typing a value directly in the text box, or by selecting a value from a list. However, some properties are more complex and have a custom property editor. To set the property, click in the text box, and then click the build **(...)** button to open the custom editor.  
  
6.  Optionally, create property expressions to dynamically update the properties of the task or container. For more information, see [Add or Change a Property Expression](../../integration-services/expressions/add-or-change-a-property-expression.md).  
  
7.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
### Set the properties of a task or container with the task or container editor  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab.  
  
4.  On the design surface of the **Control Flow** tab, right-click the task or container, and then click **Edit** to open the corresponding task or container editor.  
  
     For information about how to configure the For Loop container, see [Configure a For Loop Container](https://msdn.microsoft.com/library/b9cd7ea7-b198-4a35-8b16-6acf09611ca5).  
  
     For information about how to configure the Foreach Loop container, see [Configure a Foreach Loop Container](https://msdn.microsoft.com/library/519c6f96-5e1f-47d2-b96a-d49946948c25).  
  
    > [!NOTE]  
    >  The Sequence container has no custom editor.  
  
5.  If the task or container editor has multiple nodes, click the node that contains the property that you want to set.  
  
6.  Optionally, click **Expressions** and, on the **Expressions** page, create property expressions to dynamically update the properties of the task or container. For more information, see [Add or Change a Property Expression](../../integration-services/expressions/add-or-change-a-property-expression.md).  
  
7.  Update the property value.  
  
8.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Integration Services Containers](../../integration-services/control-flow/integration-services-containers.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)  
  
  
