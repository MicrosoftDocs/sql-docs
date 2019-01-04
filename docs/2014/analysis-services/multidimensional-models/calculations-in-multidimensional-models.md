---
title: "Calculations in Multidimensional Models | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "calculations [Analysis Services], creating"
  - "deleting calculations"
  - "calculations [Analysis Services], scripts"
  - "Cube Designer"
  - "modifying scripts"
  - "removing calculations"
  - "calculations [Analysis Services], deleting"
  - "scripts [Analysis Services], calculations"
  - "cubes [Analysis Services], calculations"
  - "solve orders [Analysis Services]"
ms.assetid: c21b3459-9bef-45a2-aba5-c992eba5b66e
author: minewiskan
ms.author: owend
manager: craigg
---
# Calculations in Multidimensional Models
  Use the **Calculations** tab of Cube Designer to create calculated members, named sets, and other Multidimensional Expressions (MDX) calculations.  
  
 The **Calculations** tab has the following three panes:  
  
-   The **Script Organizer** pane lists the calculations in a cube. Use this pane to create, organize, and select calculations for editing.  
  
-   The **Calculation Tools** pane provides metadata, functions, and templates with which to create calculations.  
  
-   The Calculation Expressions pane supports a form view and a script view.  
  
> [!NOTE]  
>  For more information about MDX scripting, see [Introduction to MDX Scripting in Microsoft SQL Server 2005](https://go.microsoft.com/fwlink/?LinkId=81892), and see the Additional Resources section on the [SQL Server 2005 - Analysis Services](https://go.microsoft.com/fwlink/?LinkId=80853) page on the Microsoft TechNet Web site. For more information about performance issues related to cube design, see the [SQL Server 2005 Analysis Services Performance Guide](https://go.microsoft.com/fwlink/?LinkId=81621).  
  
## Creating a New Calculation  
 To create a new calculation, on the **Calculations** tab of Cube Designer, on the **Cube** menu, click either **New Calculated Member**, **New Named Set**, or **New Script Command**, depending on the type of calculation you want to create. You can also click any of the corresponding buttons on the toolbar, or right-click anywhere in the **Script Organizer** pane and then click one of the commands on the shortcut menu. This action adds a new calculation to the **Script Organizer** pane and displays fields for it in the calculations form in the Calculations Expressions pane. If you create a new script, this action opens the Script view in the Calculation Expressions pane. For more information about building the three types of calculations, see [Create Calculated Members](create-calculated-members.md), [Create Named Sets](create-named-sets.md), and [Define Assignments and Other Script Commands](define-assignments-and-other-script-commands.md).  
  
## Editing Scripts  
 Edit scripts in the Calculation Expressions pane of the **Calculations** tab. The Calculation Expressions pane has two views, Script view and Form view. The Form view shows the expressions and properties for a single command. When you edit an MDX script, an expression box fills the entire Form view.  
  
 The Script view provides a code editor in which to edit the scripts. While the Calculation Expressions pane is in Script view, the **Script Organizer** pane is hidden. The Script view provides color coding, parenthesis matching, auto-complete, and MDX code regions. The MDX code regions can be collapsed or expanded to facilitate editing.  
  
 You can switch between the Form view and the Script view by clicking the **Cube** menu, pointing to **Show Calculations in**, and then clicking **Script** or **Form**. You can also click either **Form view** or **Script view** on the toolbar.  
  
## Changing Solve Order  
 Calculations are solved in the order listed in the **Script Organizer** pane. You can reorder the calculations by right-clicking any calculation and then clicking **Move Up** or **Move Down** on the shortcut menu. You can also click a calculation and then click the **Move Up** or **Move Down** button on the toolbar.  
  
 You can override this order manually for calculated cells and calculated members. For more information about pass order and solve order, see [Understanding Pass Order and Solve Order &#40;MDX&#41;](mdx/mdx-data-manipulation-understanding-pass-order-and-solve-order.md).  
  
## Deleting a Calculation  
 To delete an existing calculation, on the **Calculations** tab, in the **Script Organizer** pane, select the calculation to be deleted, and then click **Delete** on the **Edit** menu or click the **Delete** icon on the toolbar. You can also right-click the calculation in the **Script Organizer** pane and select **Delete** from the short-cut menu.  
  
  
