---
title: "Define Assignments and Other Script Commands | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Define Assignments and Other Script Commands
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  On the **Calculations** tab of Cube Designer, click the **New ScriptCommand** icon on the toolbar to create an empty script. When you create a new script, it initially is displayed with a blank title in the **Script Organizer** pane of the Calculations tab. The characters you type in the Calculation Expressions pane will be visible as the name of the item in **Script Organizer**. Therefore, you may want to type a commented name on the first line to more easily identify the script in the **Script Organizer** pane. For more information, see [Introduction to MDX Scripting in Microsoft SQL Server 2005](http://go.microsoft.com/fwlink/?LinkId=81892).  
  
> [!IMPORTANT]  
>  When you initially switch to the **Calculations** tab of Cube Designer, the **Script Organizer** pane contains a single script with a CALCULATE command. The CALCULATE command controls the aggregation of the cells in the cube and should be edited only if you intend to manually specify how the cube is to be aggregated.  
  
 You can use the Calculation Expressions pane to build an expression in Multidimensional Expressions (MDX) syntax. While you build the expression, you can drag or copy cube components, functions, and templates from the **Calculation Tools** pane to the Calculation Expressions pane. Doing so adds the script for the item to the Calculation Expressions pane in the location that you drop or paste it. Replace arguments and their delimiters (« and ») with the appropriate values.  
  
> [!IMPORTANT]  
>  When writing an expression that contains multiple statements using the Calculation Expressions pane, ensure that all lines except the last line of the MDX script end with a semi-colon (;). Calculations are concatenated into a single MDX script, and each script has a semi-colon appended to it to ensure that the MDX script compiles correctly. If you add a semi-colon to the last line of the script in the Calculation Expressions pane, the cube will build and deploy correctly but you will be unable to run queries against it.  
  
## See Also  
 [Calculations in Multidimensional Models](../../analysis-services/multidimensional-models/calculations-in-multidimensional-models.md)  
  
  
