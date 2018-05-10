---
title: "Creating Session-Scoped Named Sets (MDX) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# MDX Named Sets - Creating Session-Scoped Named Sets
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  To create a named set that is available throughout a Multidimensional Expressions (MDX) session, you use the [CREATE SET](../../../mdx/mdx-data-definition-create-set.md) statement. A named set that is created by using the CREATE SET statement will not be removed until after the MDX session closes.  
  
 As discussed in this topic, the syntax of the WITH keyword is straightforward and easy to use.  
  
> [!NOTE]  
>  For more information about named sets, see [Building Named Sets in MDX &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-named-sets-building-named-sets.md).  
  
## CREATE SET Syntax  
 Use the following syntax for the CREATE SET statement:  
  
```  
CREATE SESSION SET [CURRENTCUBE. | <cube name>.]<Set Identifier> AS <Set Expression>  
```  
  
 In the CREATE SET syntax, the `cube name` parameter contains the name of the cube that contains the members for the named set. If the `cube name` parameter is not specified, the current cube will be used as the cube that contains the member for the named set. Also, the `Set_Identifier` parameter contains the alias for the named set, and the `Set_Expression` parameter contains the set expression to which the named set alias will refer.  
  
## CREATE SET Example  
 The following example uses the CREATE SET statement to create the `SetCities_2_3` named set based on the Store cube. The members of the `SetCities_2_3` named set are the stores within City 2 and City 3.  
  
```  
create Session set [Store].[SetCities_2_3] as  
{[Data Stores].[ByLocation].[State].&[CA].&[City 02],  
[Data Stores].[ByLocation].[State].&[NH].&[City 03]}  
```  
  
 By using the CREATE SET statement to define the `SetCities_2_3` named set, this named set remains available for the length of the current MDX session. The following example is a valid query that would return City 2 and City 3 members, and that could be run anytime after you create the `SetCities_2_3` named set and before the session closes.  
  
```  
select SetCities_2_3 on 0 from [Store]  
```  
  
## See Also  
 [Creating Query-Scoped Named Sets &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-named-sets-creating-query-scoped-named-sets.md)  
  
  
