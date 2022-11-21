---
description: "MDX Syntax Elements (MDX)"
title: "MDX Syntax Elements (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MDX Syntax Elements (MDX)


  Multidimensional Expressions (MDX) has several elements that are used by, or influence, most statements:  
  
|Term|Definition|  
|----------|----------------|  
|[Identifiers](../mdx/identifiers-mdx.md)|Identifiers are the names of objects such as cubes, dimensions, members, and measures.|  
|**Data Types**|Define the types of data that are contained by cells, member properties, and cell properties. MDX supports only the OLE VARIANT data type. For more information about the coercion, conversion, and manipulation of the VARIANT data type, see "VARIANT and VARIANTARG" in the Platform SDK documentation.|  
|[Expressions &#40;MDX&#41;](../mdx/expressions-mdx.md)|Expressions are units of syntax that Analysis Services can resolve to single (scalar) values or objects. Expressions include functions that return a single value, a set expression, and so on.|  
|[Operators](../mdx/operators-mdx-syntax.md)|Operators are syntax elements that work with one or more simple MDX expressions to make more complex MDX expressions.|  
|[Functions](../mdx/functions-mdx-syntax.md)|Functions are syntax elements that take zero, one, or more input values, and return a scalar value or an object. Examples include the [Sum](../mdx/sum-mdx.md) function for adding several values, the [Members](../mdx/members-set-mdx.md) function for returning a set of members from a dimension or level, and so on.|  
|[Comments](../mdx/comments-mdx-syntax.md)|Comments are pieces of text that are inserted into MDX statements or scripts to explain the purpose of the statement. [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] does not execute comments .|  
|[Reserved Keywords](../mdx/reserved-keywords-mdx-syntax.md)|Reserved keywords are words that are reserved for the use of MDX and should not be used for object names used in MDX statements.|  
|[Members, Tuples, and Sets](/analysis-services/multidimensional-models/mdx/working-with-members-tuples-and-sets-mdx)|Members, tuples and sets are core concepts of multidimensional data that you must understand before you create an MDX query.|  
  
## See Also  
 [Multidimensional Expressions &#40;MDX&#41; Reference](../mdx/multidimensional-expressions-mdx-reference.md)  
  
