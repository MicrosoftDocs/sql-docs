---
title: "DefaultMember (MDX)"
description: "DefaultMember (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# DefaultMember (MDX)


  Returns the default member of a hierarchy.  
  
## Syntax  
  
```  
  
Hierarchy_Expression.DefaultMember  
```  
  
## Arguments  
 *Hierarchy_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a hierarchy.  
  
## Remarks  
 The default member on an attribute is used to evaluate expressions when an attribute is not included in a query.  
  
## Example  
 The following example uses the **DefaultMember** function, in conjunction with the **Name** function, to return the default member for the Destination Currency dimension in the Adventure Works cube. The example returns **US Dollar**. The **Name** function is used to return the name of the measure rather than the default property of the measure, which is **value**.  
  
```  
WITH MEMBER Measures.x AS   
   [Destination Currency].[Destination Currency].DefaultMember.Name  
SELECT Measures.x ON 0  
FROM [Adventure Works]  
```  
  
## Related content

- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
- [Define a Default Member](/analysis-services/multidimensional-models/attribute-properties-define-a-default-member)
