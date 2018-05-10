---
title: "Tuples | Microsoft Docs"
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
# Tuples
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  A tuple uniquely identifies a slice of data from a cube. The tuple is formed by a combination of dimension members, as long as there are no two or more members that belong to the same hierarchy.  
  
## Implicit or default attribute members in a tuple  
 When defining a tuple in an MDX query or expression, you do not need to explicitly include the attribute member from every attribute hierarchy. If a member from an attribute hierarchy is not explicitly included in a query or an expression, the default member for that attribute hierarchy is the attribute member implicitly included in the tuple. Unless otherwise explicitly defined in a cube, the default member for every attribute hierarchy is the (All) member, if an (All) member exists. If an (All) member does not exist within an attribute hierarchy, the default member is a member of the attribute hierarchy's top level. The default measure is the first measure specified in the cube, unless a default measure is explicitly defined. For more information, see [Define a Default Member](../../../analysis-services/multidimensional-models/attribute-properties-define-a-default-member.md) and [DefaultMember &#40;MDX&#41;](../../../mdx/defaultmember-mdx.md).  
  
 For example, the following tuple identifies a single cell in the Adventure Works database by explicitly defining only a single member of the Measures dimension.  
  
```  
(Measures.[Reseller Sales Amount])  
```  
  
 The previous example uniquely identifies the cell consisting of the Reseller Sales Amount member from the Measures dimension and the default member from every attribute hierarchy in the cube. The default member is the (All) member for every attribute hierarchy other than the Destination Currency attribute hierarchy. The default member for the Destination Currency hierarchy is the US Dollar member (this default member is defined in the MDX script for the Adventure Works cube).  
  
> [!IMPORTANT]  
>  The member of an attribute hierarchy in a tuple is also affected by relationships that are defined between attributes within a dimension.  
  
 The following query returns the value for the cell referenced by the tuple specified in the previous example, ($80,450.596.98).  
  
```  
SELECT   
Measures.[Reseller Sales Amount] ON COLUMNS   
FROM [Adventure Works]  
```  
  
> [!NOTE]  
>  When you specify an axis for a set (in this case composed of a single tuple) in a query, you must begin by specifying a set for the column axis before specifying a set for the row axis. The column axis can also be referred to as *axis(0)* or simply *0*. For more information about MDX queries, see [The Basic MDX Query &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-query-the-basic-query.md).  
  
### Tuples as values or member references  
 You can use a tuple in a query to return the value in the cell that is referenced by the tuple, as in the previous example. Or you can use a tuple in an expression to explicitly refer to the members specified in the tuple. The query or the expression can utilize functions that either return or consume tuples. A tuple can be used to either refer to the value of the cell that the tuple specifies, or to specify a combination of members when utilized in a function.  
  
### Tuple dimensionality  
 The *dimensionality* of a tuple refers to the sequence or order of the members in the tuple. Since the implicit members always occur in the same order, dimensionality is most often thought of in terms of the explicitly defined members of the tuple. The ordering of the members of the tuple is important when you define a set of tuples. The following example includes two members in a tuple on the column axis.  
  
```  
SELECT   
([Measures].[Reseller Sales Amount],[Date].[Calendar Year].[CY 2004]) ON COLUMNS   
FROM [Adventure Works]  
```  
  
> [!NOTE]  
>  When you explicitly specify a member in a tuple from more than one dimension, you must include the entire tuple in parentheses. When only specifying a single member in a tuple, parentheses are optional.  
  
 The tuple in the query in the previous example specifies the return of the cube cell at the intersection of the Reseller Sales Amount Measure of the Measures dimension and the CY 2004 member of the Calendar Year attribute hierarchy in the Date dimension.  
  
> [!NOTE]  
>  An attribute member can be referred by either its member name or its member key. In the previous example, you could replace the reference to [CY 2004] with &[2004].  
  
## See Also  
 [Key Concepts in MDX &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/mdx/key-concepts-in-mdx-analysis-services.md)   
 [Cube Space](../../../analysis-services/multidimensional-models/mdx/cube-space.md)   
 [Autoexists](../../../analysis-services/multidimensional-models/mdx/autoexists.md)   
 [Working with Members, Tuples, and Sets &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/working-with-members-tuples-and-sets-mdx.md)  
  
  
