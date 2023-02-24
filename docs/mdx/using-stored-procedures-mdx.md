---
description: "Using Stored Procedures (MDX)"
title: "Using Stored Procedures (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Using Stored Procedures (MDX)


  You can extend the functionality of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] and Multidimensional Expressions (MDX) by writing .NET stored procedures or user-defined functions. For more information, see [ADOMD.NET Server Programming](/analysis-services/adomd/multidimensional-models-adomd-net-server/adomd-net-server-programming)  
  
 When you reference or call a stored procedure, you specify the function name followed by parentheses. Within the parentheses, you can specify expressions called arguments that provide the data to be passed into the parameters. When you call a function, you must supply argument values for all of the parameters, and you must specify the argument values in the same sequence in which the parameters are defined in the user-defined function.  
  
 The following example query assumes that you have an assembly named SampleAssembly registered on your [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Server:  
  
```  
SELECT SampleAssembly.RandomSample([Geography].[State-Province].Members, 5) on ROWS,   
[Date].[Calendar].[Calendar Year] on COLUMNS  
FROM [Adventure Works]  
WHERE [Measures].[Reseller Freight Cost]  
```  
  
> [!NOTE]  
>  *Stored procedure* is the terminology used in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] for these types of functions. Earlier versions of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] called these types of functions as *user-defined functions*.  
  
## Types of stored procedures  
 [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] supports both COM and CLR assemblies. CLR assemblies are recommended because of the enhanced security available to CLR assemblies. If Microsoft Office Excel is installed on the server, Excel functions are also available.  
  
> [!NOTE]  
>  Microsoft Visual Basic for Applications (VBA) COM Assemblies are registered automatically.  
  
## See Also  
 [Functions &#40;MDX Syntax&#41;](../mdx/functions-mdx-syntax.md)  
  
