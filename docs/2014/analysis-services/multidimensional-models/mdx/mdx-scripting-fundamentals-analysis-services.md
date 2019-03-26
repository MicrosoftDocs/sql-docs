---
title: "MDX Scripting Fundamentals (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "cubes [Analysis Services], scripts"
  - "calculations [Analysis Services], scripts"
  - "MDX [Analysis Services], scripts"
  - "scripts [MDX]"
  - "cubes [Analysis Services], calculations"
  - "Multidimensional Expressions [Analysis Services], scripts"
ms.assetid: fdecb3ce-7c87-4bab-8000-532ba7a29f96
author: minewiskan
ms.author: owend
manager: craigg
---
# MDX Scripting Fundamentals (Analysis Services)
  In [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], a Multidimensional Expressions (MDX) script is made up of one or more MDX expressions or statements that populate a cube with calculations.  
  
 An MDX script defines the calculation process for a cube. An MDX script is also considered part of the cube itself. Therefore, changing an MDX script associated with a cube immediately changes the calculation process for the cube.  
  
 To create MDX scripts, you can use Cube Designer in the [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)]. For more information, see [Define Assignments and Other Script Commands](../define-assignments-and-other-script-commands.md) and [Introduction to MDX Scripting in Microsoft SQL Server 2005](https://go.microsoft.com/fwlink/?LinkId=81892).  
  
 For performance issues related to MDX queries and calculations, see the MDX optimization section in the [SQL Server Analysis Services Performance Guide](https://go.microsoft.com/fwlink/p/?LinkId=399050).  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[The Basic MDX Script &#40;MDX&#41;](the-basic-mdx-script-mdx.md)|Details the basic MDX script, including the default MDX script provided in each cube, and how MDX scripts generally function within a cube in [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|[Managing Scope and Context &#40;MDX&#41;](managing-scope-and-context-mdx.md)|Describes how to use the [CALCULATE](/sql/mdx/mdx-scripting-calculate) statement, the [SCOPE](/sql/mdx/mdx-scripting-scope) statement, and the [This](/sql/mdx/this-mdx) function to manage context and scope within an MDX script.|  
|[Using Variables and Parameters &#40;MDX&#41;](using-variables-and-parameters-mdx.md)|Describes how to use variables and parameters in an MDX script.|  
|[Error Handling &#40;MDX&#41;](error-handling-mdx.md)|Explains error handling within an MDX script.|  
|[Supported MDX &#40;MDX&#41;](supported-mdx-mdx.md)|Provides a list of supported MDX operators, statements, and functions within an MDX script.|  
  
## See Also  
 [MDX Language Reference &#40;MDX&#41;](/sql/mdx/mdx-language-reference-mdx)  
  
  
