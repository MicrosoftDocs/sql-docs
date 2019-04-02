---
title: "Connect Tasks and Containers by Using a Default Precedence Constraint | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "tasks [Integration Services], precedence constraints"
  - "precedence constraints [Integration Services], connecting tasks"
  - "default precedence constraints"
  - "containers [Integration Services], precedence constraints"
ms.assetid: 8f31f15f-98ff-4c35-b41f-8b8cfd148d75
author: janinezhang
ms.author: janinez
manager: craigg
---
# Connect Tasks and Containers by Using a Default Precedence Constraint
  Precedence constraints connect two executables. An executable can be any task or a For Loop, Foreach Loop, or Sequence container. This procedure describes how to set the default behavior for precedence constraints, and how to connect executables using the default precedence constraints.  
  
## Creating Default Precedence Constraints  
 When you first use [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, the default value of a precedence constraint is `Success`. Follow these steps to configure [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer to use a different default value for precedence constraints.  
  
#### To set the default value for precedence constraints  
  
1.  Open [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
2.  On the **Tools** menu, click **Options**.  
  
3.  In the **Options** dialog box, expand **Business Intelligence Designers,** and then expand **Integration Services Designers**.  
  
4.  Click **Control Flow Auto Connect** and select **Connect a new shape to the selected shape by default**.  
  
5.  In the drop-down list, choose either **Use a Failure constraint for the new shape** or **Use a Completion constraint for the new shape**.  
  
6.  Click **OK**.  
  
#### To create a default precedence constraint  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab.  
  
4.  On the design surface of the **Control Flow** tab, click the task or container and drag its connector to the executable to which you want the precedence constraint to apply.  
  
5.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Precedence Constraints](control-flow/precedence-constraints.md)   
 [Set the Value of a Precedence Constraint by Using the Shortcut Menu](../../2014/integration-services/set-the-value-of-a-precedence-constraint-by-using-the-shortcut-menu.md)   
 [Set the Properties of a Precedence Constraint](../../2014/integration-services/set-the-properties-of-a-precedence-constraint.md)   
 [Use an Expression in a Precedence Constraint](../../2014/integration-services/use-an-expression-in-a-precedence-constraint.md)  
  
  
