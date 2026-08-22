---
title: "IsEmpty (MDX)"
description: "IsEmpty (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# IsEmpty (MDX)


  Returns whether the evaluated expression is the empty cell value.  
  
## Syntax  
  
```  
  
IsEmpty(Value_Expression)   
```  
  
## Arguments  
 *Value_Expression*  
 A valid Multidimensional Expressions (MDX) expression that typically returns the cell coordinates of a member or a tuple.  
  
## Remarks  
 The **IsEmpty** function returns **true** if the evaluated expression is an empty cell value. Otherwise, this function returns **false**.  
  
> [!NOTE]  
>  The default property for a member is the value of the member.  
  
 The **IsEmpty** function is the only way to reliably test for an empty cell because the empty cell value has special meaning in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
> [!IMPORTANT]  
>  If the evaluation of the value expression returns an error, the function will return **false**. A value expression can return an error, for example, if a properties reference refers to an invalid or non-existent property.  
  
 For more information about empty cells, see the OLE DB documentation.  
  
## Example  
 The following example returns TRUE if the Internet Sales Amount for the current member on the Fiscal hierarchy of the Date dimension returns an empty cell:  
  
```  
WITH MEMBER MEASURES.ISEMPTYDEMO AS  
IsEmpty([Measures].[Internet Sales Amount])  
SELECT {[Measures].[Internet Sales Amount],MEASURES.ISEMPTYDEMO} ON 0,  
[Date].[Fiscal].MEMBERS ON 1  
FROM [Adventure Works]  
```  
  
## Related content

- [Working with Empty Values](working-with-empty-values.md)
- [MDX Function Reference (MDX)](mdx-function-reference-mdx.md)
