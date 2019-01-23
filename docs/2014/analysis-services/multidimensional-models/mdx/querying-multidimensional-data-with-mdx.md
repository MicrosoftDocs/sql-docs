---
title: "Querying Multidimensional Data with MDX | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "multidimensional data [Analysis Services], querying"
ms.assetid: e0a5dd60-35a3-4a4f-b36f-52ecea814886
author: minewiskan
ms.author: owend
manager: craigg
---
# Querying Multidimensional Data with MDX
  Multidimensional Expressions (MDX) is the query language that you use to work with and retrieve multidimensional data in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. MDX is based on the XML for Analysis (XMLA) specification, with specific extensions for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. MDX utilizes expressions composed of identifiers, values, statements, functions, and operators that [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] can evaluate to retrieve an object (for example a set or a member), or a scalar value (for example, a string or a number).  
  
 MDX queries and expressions in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] are used to do the following:  
  
-   Return data to a client application from a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] cube.  
  
-   Format query results.  
  
-   Perform cube design tasks, including the definition of calculated members, named sets, scoped assignments, and key performance indicators (KPIs).  
  
-   Perform administrative tasks, including dimension and cell security.  
  
 MDX is superficially similar in many ways to the SQL syntax that is typically used with relational databases. However, MDX is not an extension of the SQL language and is different from SQL in many ways. In order to create MDX expressions used to design or secure cubes, or to create MDX queries to return and format multidimensional data, you need to understand basic concepts in MDX and dimensional modeling, MDX syntax elements, MDX operators, MDX statements, and MDX functions.  
  
> [!NOTE]  
>  For more information, see the Additional Resources section on the [SQL Server 2005 - Analysis Services](https://go.microsoft.com/fwlink/?LinkId=80853) page on the Microsoft TechNet Web site. For more information about performance issues related to MDX queries and calculations, see the section "Writing Efficient MDX" in the [SQL Server 2005 Analysis Services Performance Guide](https://go.microsoft.com/fwlink/?LinkId=81621).  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Key Concepts in MDX &#40;Analysis Services&#41;](../key-concepts-in-mdx-analysis-services.md)|You can use Multidimensional Expressions (MDX) to query multidimensional data or to create MDX expressions for use within a cube, but first you should understand [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] dimension concepts and terminology.|  
|[MDX Query Fundamentals &#40;Analysis Services&#41;](mdx-query-fundamentals-analysis-services.md)|Multidimensional Expressions (MDX) enables you to query multidimensional objects, such as cubes, and return multidimensional cellsets that contain the cube's data. This topic and its subtopics provide an overview of MDX queries.|  
|[MDX Scripting Fundamentals &#40;Analysis Services&#41;](mdx-scripting-fundamentals-analysis-services.md)|In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], a Multidimensional Expressions (MDX) script is made up of one or more MDX expressions or statements that populate a cube with calculations.<br /><br /> An MDX script defines the calculation process for a cube. An MDX script is also considered part of the cube itself. Therefore, changing an MDX script associated with a cube immediately changes the calculation process for the cube.<br /><br /> To create MDX scripts, you can use Cube Designer in the [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)].|  
  
## See Also  
 [MDX Syntax Elements &#40;MDX&#41;](/sql/mdx/mdx-syntax-elements-mdx)   
 [MDX Language Reference &#40;MDX&#41;](/sql/mdx/mdx-language-reference-mdx)  
  
  
