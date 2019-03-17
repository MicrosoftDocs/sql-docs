---
title: "Set the Properties of a Task or Container | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "tasks [Integration Services], properties"
ms.assetid: 52d47ca4-fb8c-493d-8b2b-48bb269f859b
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Set the Properties of a Task or Container
  You can set most properties of tasks and containers by using the **Properties** window. The exceptions are properties of task collections and properties that are too complex to set by using the **Properties** window. For example, you cannot configure the enumerator that the Foreach Loop container uses in the **Properties** window. You must use a task or container editor to set these complex properties. Most task and container editors have multiple nodes and each node contains related properties. The name of the node indicates the subject of the properties that the node contains.  
  
 The following procedures describe how to set the properties of a task or container by using either the **Properties** windows, or the corresponding task or container editor.  
  
### To set the properties of a task or container by using the Properties window  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab.  
  
4.  On the design surface of the **Control Flow** tab, right-click the task or container, and then click **Properties**.  
  
5.  In the **Properties** window, update the property value.  
  
    > [!NOTE]  
    >  Most properties can be set by typing a value directly in the text box, or by selecting a value from a list. However, some properties are more complex and have a custom property editor. To set the property, click in the text box, and then click the build **(...)** button to open the custom editor.  
  
6.  Optionally, create property expressions to dynamically update the properties of the task or container. For more information, see [Add or Change a Property Expression](expressions/add-or-change-a-property-expression.md).  
  
7.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
### To set the properties of a task or container by using a task or container editor  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab.  
  
4.  On the design surface of the **Control Flow** tab, right-click the task or container, and then click **Edit** to open the corresponding task or container editor.  
  
     For information about how to configure the For Loop container, see [Configure a For Loop Container](control-flow/for-loop-container.md).  
  
     For information about how to configure the Foreach Loop container, see [Configure a Foreach Loop Container](control-flow/foreach-loop-container.md).  
  
    > [!NOTE]  
    >  The Sequence container has no custom editor.  
  
5.  If the task or container editor has multiple nodes, click the node that contains the property that you want to set.  
  
6.  Optionally, click **Expressions** and, on the **Expressions** page, create property expressions to dynamically update the properties of the task or container. For more information, see [Add or Change a Property Expression](expressions/add-or-change-a-property-expression.md).  
  
7.  Update the property value.  
  
8.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Integration Services Tasks](control-flow/integration-services-tasks.md)   
 [Integration Services Containers](control-flow/integration-services-containers.md)  
  
  
