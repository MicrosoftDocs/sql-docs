---
title: "Expressions (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "identifiers [MDX]"
  - "expressions [MDX]"
  - "expressions [MDX], about expressions"
  - "MDX [Analysis Services], expressions"
  - "Multidimensional Expressions [Analysis Services], expressions"
ms.assetid: e20c34bc-30fa-4ac5-b896-9d4c9925ef59
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Expressions (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  An expression is a combination of identifiers, values, and operators that [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] can evaluate to get a result. The data can be used in several different places when accessing or changing data. For example, you can use an expression as part of the data to be retrieved by a query or as a search condition to look for data that meets a set of criteria.  
  
## Simple and Complex Expressions  
 An expression can be simple or complex in MDX:  
  
 A simple expression can be one of the following expressions:  
  
 Constant  
 A constant is a symbol that represents a single, specific value in MDX. String, numeric, and date values can be rendered as constants. Unlike numeric constants, string and date constants must be delimited with single quote (') characters.  
  
 Scalar function  
 A scalar function returns a single value within the context of evaluation in MDX. This distinction is important to understanding how MDX resolves scalar functions, because most MDX expressions, statements, and scripts are evaluated not over a single data element, but iteratively over a group of data elements such as cells or members. At the time the scalar function is evaluated, however, the function is typically reviewing a single data element.  
  
 Object identifier  
 MDX is object-oriented because of the nature of multidimensional data. Object identifiers are considered simple expressions in MDX. For more information on identifiers, see [Identifiers &#40;MDX&#41;](../mdx/identifiers-mdx.md).  
  
 A complex expression can be built from combinations of these entities joined by operators.  
  
## Expression Results  
 For a simple expression built of a single constant, variable, scalar function, or column name, the data type, collation, precision, scale, and value of the expression is the data type, collation, precision, scale, and value of the referenced element. Because MDX directly supports only the OLE VARIANT data type, coercion should not occur when working with simple expressions.  
  
 For a complex expression, coercion can occur when using two or more simple expressions with different data types.  
  
## Expression Examples  
 The following query shows examples of calculated measures whose definitions are simple expressions:  
  
 `WITH`  
  
 `MEMBER MEASURES.CONSTANTVALUE AS 1`  
  
 `MEMBER MEASURES.SCALARFUNCTION AS [Date].[Calendar Year].CURRENTMEMBER.NAME`  
  
 `MEMBER MEASURES.OBJECTIDENTIFIER AS [Measures].[Internet Sales Amount]`  
  
 `SELECT {MEASURES.CONSTANTVALUE,MEASURES.SCALARFUNCTION,MEASURES.OBJECTIDENTIFIER } ON 0,`  
  
 `[Date].[Calendar Year].MEMBERS ON 1`  
  
 `FROM [Adventure Works]`  
  
 An expression can also be a calculation, such as `[Measures].[Discount Amount] * 1.5`. The following example demonstrates the use of a calculation to define a member in an MDX SELECT statement:  
  
```  
WITH   
   MEMBER [Measures].[Special Discount] AS  
   [Measures].[Discount Amount] * 1.5  
SELECT   
   [Measures].[Special Discount] on COLUMNS,  
   NON EMPTY [Product].[Product].MEMBERS  ON Rows  
FROM [Adventure Works]  
WHERE [Product].[Category].[Bikes]  
```  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Using Cube and Subcube Expressions](../mdx/using-cube-and-subcube-expressions.md)|Defines cube and subcube expressions.|  
|[Using Dimension Expressions](../mdx/using-dimension-expressions.md)|Defines dimension expressions.|  
|[Using Member Expressions](../mdx/using-member-expressions.md)|Defines member expressions.|  
|[Using Tuple Expressions](../mdx/using-tuple-expressions.md)|Defines tuple expressions.|  
|[Using Set Expressions](../mdx/using-set-expressions.md)|Defines set expressions.|  
|[Using Scalar Expressions](../mdx/using-scalar-expressions.md)|Defines scalar expressions.|  
|[Working with Empty Values](../mdx/working-with-empty-values.md)|Describes what an empty value is and how such values are handled.|  
  
## See Also  
 [MDX Language Reference &#40;MDX&#41;](../mdx/mdx-language-reference-mdx.md)   
 [MDX Query Fundamentals &#40;Analysis Services&#41;](../analysis-services/multidimensional-models/mdx/mdx-query-fundamentals-analysis-services.md)  
  
  
