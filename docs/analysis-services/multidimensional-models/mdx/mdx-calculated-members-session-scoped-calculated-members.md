---
title: "Creating Session-Scoped Calculated Members (MDX) | Microsoft Docs"
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
# MDX Calculated Members - Session-Scoped Calculated Members
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  To create a calculated member that is available throughout a Multidimensional Expressions (MDX) session, you use the [CREATE MEMBER](../../../mdx/mdx-data-definition-create-member.md) statement. A calculated member that is created by using the CREATE MEMBER statement will not be removed until after the MDX session closes.  
  
 As discussed in this topic, the syntax of the CREATE MEMBER statement is straightforward and easy to use.  
  
> [!NOTE]  
>  For more information about calculated members, see [Building Calculated Members in MDX &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-calculated-members-building-calculated-members.md).  
  
## CREATE MEMBER Syntax  
 Use the following syntax to add the CREATE MEMBER statement to the MDX statement:  
  
```  
CREATE [SESSION] MEMBER [<cube-name>.]<fully-qualified-member-name> AS <expression> [,<property-definition-list>]  
<cube name> ::= CURRENTCUBE | <Cube Name>  
<property-definition-list> ::= <property-definition>  
  | <property-definition>, <property-definition-list>  
<property-definition> ::= <property-identifier> = <property-value>  
<property-identifier> ::= VISIBLE | SOLVEORDER | SOLVE_ORDER | FORMAT_STRING | NON_EMPTY_BEHAVIOR <ole db member properties>  
```  
  
 In the syntax for the CREATE MEMBER statement, the `fully-qualified-member-name` value is the fully qualified name of the calculated member. The fully qualified name includes the dimension or level to which the calculated member is associated. The `expression` value returns the value of the calculated member after the expression value has been evaluated,.  
  
## CREATE MEMBER Example  
 The following example uses the CREATE MEMBER statement to create the `LastFourStores` calculated member. This calculated member returns the sum of units sold for the last four stores, and will be available throughout the whole session of the cube.  
  
```  
Create Session Member [Store].[Measures].LastFourStores as   
sum(([Stores].[ByLocation].Lag(3) :  
[Stores].[ByLocation].NextMember), [Measures].[Units Sold])  
```  
  
## See Also  
 [Creating Query-Scoped Calculated Members &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-calculated-members-query-scoped-calculated-members.md)  
  
  
