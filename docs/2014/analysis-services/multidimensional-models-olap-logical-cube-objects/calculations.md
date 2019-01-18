---
title: "Calculations | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "calculations [Analysis Services]"
  - "OLAP objects [Analysis Services], calculations"
  - "MDX [Analysis Services], calculations"
  - "calculations [Analysis Services], about calculations"
  - "cubes [Analysis Services], calculations"
ms.assetid: 6be84916-fd05-4efc-ab98-6adbbad80154
author: minewiskan
ms.author: owend
manager: craigg
---
# Calculations
  A calculation is a Multidimensional Expressions (MDX) expression or script that is used to define a calculated member, a named set, or a scoped assignment in a cube in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Calculations let you add objects that are defined not by the data of the cube, but by expressions that can reference other parts of the cube, other cubes, or even information outside the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. Calculations let you extend the capabilities of a cube, adding flexibility and power to business intelligence applications. For more information about scripting calculations, see [Introduction to MDX Scripting in Microsoft SQL Server 2005](https://go.microsoft.com/fwlink/?LinkId=81892). For more information about performance issues related to MDX queries and calculations, see the [SQL Server 2005 Analysis Services Performance Guide](https://go.microsoft.com/fwlink/?LinkId=81621).  
  
## Calculated Members  
 A calculated member is a member whose value is calculated at run time using a Multidimensional Expressions (MDX) expression that you specify when you define the calculated member. A calculated member is available to business intelligence applications just like any other member. Calculated members do not increase the size of the cube because only the definitions are stored in the cube; values are calculated in memory as required to answer a query.  
  
 Calculated members can be defined for any dimension, including the measures dimension. Calculated members created on the Measures dimension are called calculated measures.  
  
 Although calculated members are typically based on data that already exists in the cube, you can create complex expressions by combining data with arithmetic operators, numbers, and functions. You can also use MDX functions, such as LookupCube, to access data in other cubes in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] includes standardized Visual Studio function libraries, and you can use stored procedures to retrieve data from sources other than the current [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. For more information about stored procedures, see [Defining Stored Procedures](../multidimensional-models-extending-olap-stored-procedures/defining-stored-procedures.md).  
  
 For example, suppose executives in a shipping company want to determine which types of cargo are more profitable to carry, based on profit per unit of volume. They use a Shipments cube that contains the dimensions Cargo, Fleet, and Time and the measures Price_to_Ship, Cost_to_Ship, and Volume_in_Cubic_Meters; however, the cube does not contain a measure for profitability. You can create a calculated member as a measure named Profit_per_Cubic_Meter in the cube by combining the existing measures in the following expression:  
  
```  
([Measures].[Price_to_Ship] - [Measures].[Cost_to_Ship]) /  
[Measures].[Volume_in_Cubic_Meters]  
```  
  
 After you create the calculated member, the Profit_per_Cubic_Meter appears together with the other measures the next time that the Shipments cube is browsed.  
  
 To create calculated members, use the **Calculation**s tab in Cube Designer. For more information, see [Create Calculated Members](../multidimensional-models/create-calculated-members.md)  
  
## Named Sets  
 A named set is a CREATE SET MDX statement expression that returns a set. The MDX expression is saved as part of the definition of a cube in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. A named set is created for reuse in Multidimensional Expressions (MDX) queries. A named set enables business users to simplify queries, and use a set name instead of a set expression for complex, frequently used set expressions. **Related topic:** [Create Named Sets](../multidimensional-models/create-named-sets.md)  
  
## Script Commands  
 A script command is an MDX script, included as part of the definition of the cube. Script commands let you perform almost any action that is supported by MDX on a cube, such as scoping a calculation to apply to only part of the cube. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], MDX scripts can apply either to the whole cube or to specific sections of the cube, at specific points throughout the execution of the script. The default script command, which is the CALCULATE statement, populates cells in the cube with aggregated data based on the default scope.  
  
 The default scope is the whole cube, but you can define a more limited scope, known as a subcube, and then apply an MDX script to only that particular cube space. The SCOPE statement defines the scope of all subsequent MDX expressions and statements in the calculation script until the scope is terminated or redefined. The THIS statement is then used to apply an MDX expression to the current scope. You can use the BACK_COLOR statement to specify a background cell color for the cells in the current scope, to help you during debugging.  
  
 For example, you can use a script command to allocate sales quotas to employees across time and sales territory based on the weighted values of sales for a prior time period.  
  
## See Also  
 [Calculations in Multidimensional Models](../multidimensional-models/calculations-in-multidimensional-models.md)  
  
  
