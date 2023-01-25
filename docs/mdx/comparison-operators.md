---
description: "Comparison Operators"
title: "Comparison Operators | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Comparison Operators


  You use comparison operators with scalar data. You can use comparison operators in any Multidimensional Expressions (MDX) expression.  
  
 To check for a condition, you can also use comparison operators in MDX statements and functions, such as the MDX [IIf](../mdx/iif-mdx.md) function. However, if you use comparison operators to check for a condition, make sure that you have appropriate permissions before trying to change data based upon that condition. Anyone that has access to the actual data and can query that data can use comparison operators in additional queries. But this access does not mean that these individuals have or should have the appropriate permissions to change data. Also, to maintain data integrity, limit the number of people that can query and change data.  
  
 Comparison operators evaluate to a Boolean data type, returning TRUE or FALSE based on the outcome of the tested condition.  
  
 MDX supports the comparison operators listed in the following table.  
  
|Operator|Description|  
|--------------|-----------------|  
|[= (Equal To)](../mdx/equal-to-mdx.md)|For non-null arguments, returns TRUE if the left argument is equal to the right argument; otherwise, FALSE.<br /><br /> If either or both arguments evaluate to a null value, the operator returns a null value, unless the comparison `0=null` is made, in which case the Boolean contains TRUE.|  
|[<> (Not Equal To)](../mdx/not-equal-to-mdx.md)|For non-null arguments, returns TRUE if the left argument is not equal to the right argument; otherwise, FALSE.<br /><br /> If either or both arguments evaluate to a null value, the operator returns a null value.|  
|[> (Greater Than)](../mdx/greater-than-mdx.md)|For non-null arguments, returns TRUE if the left argument has a value that is greater than the right argument; otherwise, FALSE.<br /><br /> If either or both arguments evaluate to a null value, the operator returns a null value.|  
|[>= (Greater Than or Equal To)](../mdx/greater-than-or-equal-to-mdx.md)|For non-null arguments, returns TRUE if the left argument has a value that is higher than or equal to the right argument; otherwise, FALSE.<br /><br /> If either or both arguments evaluate to a null value, the operator returns a null value.|  
|[< (Less Than)](../mdx/less-than-mdx.md)|For non-null arguments, returns TRUE if the left argument has a value that is less than the right argument; otherwise, FALSE.<br /><br /> If either or both arguments evaluate to a null value, the operator returns a null value.|  
|[<= (Less Than or Equal To)](../mdx/less-than-or-equal-to-mdx.md)|For non-null arguments, returns TRUE if the left argument has a value that is lower than or equal to the right argument; otherwise, FALSE.<br /><br /> If either or both arguments evaluate to a null value, the operator returns a null value.|  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)   
 [Operators &#40;MDX Syntax&#41;](../mdx/operators-mdx-syntax.md)  
  
  
