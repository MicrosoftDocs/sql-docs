---
title: "Using Member Expressions | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Using Member Expressions


  A member expression contains a member identifier, a member function, or an expression that can be converted to a member.  
  
 Member identifiers can come in many different formats. The simplest form of a member identifier consists of the member's name. For example:  
  
```  
SELECT Amount ON 0  
FROM [Adventure Works]  
  
```  
  
 However, if there are several members with the same name on different hierarchies, there is no method to determine which member the query will return. For example, the following query requests data for a member with the name [CY 2004]. The queryexecutes successfully, but there are at least six members with that name in the Adventure Works cube:  
  
```  
SELECT [CY 2004] ON 0  
FROM [Adventure Works]  
  
```  
  
 Therefore, the most reliable form of member identifier is the member's unique name, which guarantees to identify a specific member in a cube. Analysis Services can generate unique names in several ways, but a unique name is always composed of at least two identifiers: the dimension name, and the member name or member key. A unique name appears in the following format:  
  
```  
  
Dimension_Name  
.[Hierarchy_Name.] [[{Member_Name | &Member_Key}.]... ] {Member_Name | &Member_Key}  
  
```  
  
 Here are some examples of member unique names from the Adventure Works cube:  
  
```  
[Measures].[Amount]  
[Date].[Calendar Year].&[2004]  
[Date].[Calendar].[Calendar Quarter].&[2004]&[1]  
[Employee].[Employees].&[112]  
[Product].[Product Categories].[All Products]  
  
```  
  
 Many MDX functions exist that return members. For a full list, see [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
> [!NOTE]  
>  For more information about member names and member keys, see [Working with Members, Tuples, and Sets &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/working-with-members-tuples-and-sets-mdx.md).  
  
## See Also  
 [Expressions &#40;MDX&#41;](../mdx/expressions-mdx.md)  
  
  
