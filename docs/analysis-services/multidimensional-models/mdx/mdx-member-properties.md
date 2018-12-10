---
title: "Using Member Properties (MDX) | Microsoft Docs"
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
# MDX Member Properties
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Member properties cover the basic information about each member in each tuple. This basic information includes the member name, parent level, the number of children, and so on. Member properties are available for all members at a given level. In terms of organization, member properties are treated as dimensionally organized data, stored on a single dimension.  
  
> [!NOTE]
>  In [!INCLUDE[msCoName](../../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], member properties are know as attribute relationships. For more information, see [Attribute Relationships](../../../analysis-services/multidimensional-models-olap-logical-dimension-objects/attribute-relationships.md).  
  
 Member properties are either *intrinsic* or *custom*:  
  
 Intrinsic member properties  
 All members support intrinsic member properties, such as the formatted value of a member, while dimensions and levels supply additional intrinsic dimension and level member properties, such as the ID of a member.  
  
 For more information, see [Intrinsic Member Properties &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-member-properties-intrinsic-member-properties.md).  
  
 User-defined member properties  
 Members often have additional properties associated with them. For example, the Products level may offer the SKU, SRP, Weight, and Volume properties for each product. These properties are not members, but contain additional information about members at the Products level.  
  
 For more information, see [User-Defined Member Properties &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-member-properties-user-defined-member-properties.md).  
  
 Both intrinsic and user-defined member properties can be retrieved through the use of the **PROPERTIES** keyword or the [Properties](../../../mdx/properties-mdx.md) function.  
  
## Using the PROPERTIES Keyword  
 The **PROPERTIES** keyword specifies the member properties that are to be used for a given axis dimension. The **PROPERTIES** keyword is buried within the `<axis specification>` clause of the MDX [SELECT](../../../mdx/mdx-data-manipulation-select.md) statement:  
  
```  
SELECT [<axis_specification>  
       [, <axis_specification>...]]  
  FROM [<cube_specification>]  
[WHERE [<slicer_specification>]]  
```  
  
 The `<axis_specification>` clause includes an optional `<dim_props>` clause, as shown in the following syntax:  
  
```  
<axis_specification> ::= <set> [<dim_props>] ON <axis_name>  
```  
  
> [!NOTE]  
>  For more information about the `<set>` and `<axis_name>` values, see [Specifying the Contents of a Query Axis &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-query-and-slicer-axes-specify-the-contents-of-a-query-axis.md).  
  
 The `<dim_props>` clause lets you query dimension, level, and member properties using the **PROPERTIES** keyword. The following syntax shows the formatting of the `<dim_props>` clause:  
  
```  
<dim_props> ::= [DIMENSION] PROPERTIES <property> [,<property>...]  
```  
  
 The breakdown of the `<property>` syntax varies depending on the property that you are querying:  
  
-   Context sensitive intrinsic member properties must be preceded with the name of the dimension or level. However, non-context sensitive intrinsic member properties cannot be qualified by the dimension or level name. For more information about how to use the **PROPERTIES** keyword with intrinsic member properties, see [Intrinsic Member Properties &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-member-properties-intrinsic-member-properties.md).  
  
-   User-defined member properties should be preceded by the name of the level in which they reside. For more information about how to use the **PROPERTIES** keyword with user-defined member properties, see [User-Defined Member Properties &#40;MDX&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-member-properties-user-defined-member-properties.md).  
  
## See Also  
 [Creating and Using Property Values &#40;MDX&#41;](http://msdn.microsoft.com/library/0cafb269-03c8-4183-b6e9-220f071e4ef2)  
  
  
