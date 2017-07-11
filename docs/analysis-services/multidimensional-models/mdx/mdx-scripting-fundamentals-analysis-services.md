---
title: "MDX Scripting Fundamentals (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "cubes [Analysis Services], scripts"
  - "calculations [Analysis Services], scripts"
  - "MDX [Analysis Services], scripts"
  - "scripts [MDX]"
  - "cubes [Analysis Services], calculations"
  - "Multidimensional Expressions [Analysis Services], scripts"
ms.assetid: fdecb3ce-7c87-4bab-8000-532ba7a29f96
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDX Scripting Fundamentals (Analysis Services)
  In [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], a Multidimensional Expressions (MDX) script is made up of one or more MDX expressions or statements that populate a cube with calculations.  
  
 An MDX script defines the calculation process for a cube. An MDX script is also considered part of the cube itself. Therefore, changing an MDX script associated with a cube immediately changes the calculation process for the cube.  
  
 To create MDX scripts, you can use Cube Designer in the [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)]. For more information, see [Define Assignments and Other Script Commands](../../../analysis-services/multidimensional-models/define-assignments-and-other-script-commands.md) and [Introduction to MDX Scripting in Microsoft SQL Server 2005](http://go.microsoft.com/fwlink/?LinkId=81892).  
  
 For performance issues related to MDX queries and calculations, see the MDX optimization section in the [SQL Server Analysis Services Performance Guide](http://go.microsoft.com/fwlink/p/?LinkId=399050).  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[The Basic MDX Script &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/the-basic-mdx-script-mdx.md)|Details the basic MDX script, including the default MDX script provided in each cube, and how MDX scripts generally function within a cube in [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|[Managing Scope and Context &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/managing-scope-and-context-mdx.md)|Describes how to use the [CALCULATE](../../../mdx/mdx-scripting-calculate.md) statement, the [SCOPE](../../../mdx/mdx-scripting-scope.md) statement, and the [This](../../../mdx/this-mdx.md) function to manage context and scope within an MDX script.|  
|[Using Variables and Parameters &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/using-variables-and-parameters-mdx.md)|Describes how to use variables and parameters in an MDX script.|  
|[Error Handling &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/error-handling-mdx.md)|Explains error handling within an MDX script.|  
|[Supported MDX &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/supported-mdx-mdx.md)|Provides a list of supported MDX operators, statements, and functions within an MDX script.|  
  
## See Also  
 [MDX Language Reference &#40;MDX&#41;](../../../mdx/mdx-language-reference-mdx.md)  
  
  