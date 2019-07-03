---
title: "User Hierarchies | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "members [Analysis Services], hierarchies"
  - "dimensions [Analysis Services], hierarchies"
  - "user hierarchies [Analysis Services]"
  - "hierarchies [Analysis Services], multilevel"
  - "hierarchies [Analysis Services], attribute"
  - "attributes [Analysis Services], hierarchies"
  - "parent-child hierarchies [Analysis Services]"
  - "hierarchies [Analysis Services], user"
  - "ragged hierarchies [Analysis Services]"
  - "balanced hierarchies [Analysis Services]"
  - "hierarchies [Analysis Services]"
  - "OLAP objects [Analysis Services], hierarchies"
  - "multilevel hierarchies [Analysis Services]"
  - "unbalanced hierarchies [Analysis Services]"
ms.assetid: 9394e9a3-2242-4f0e-85e0-25d499d2d3b6
author: minewiskan
ms.author: owend
manager: craigg
---
# User Hierarchies
  User-defined hierarchies are user-defined hierarchies of attributes that are used in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to organize the members of a dimension into hierarchical structures and provide navigation paths in a cube. For example, the following table defines a dimension table for a time dimension. The dimension table supports three attributes, named Year, Quarter, and Month.  
  
|Year|Quarter|Month|  
|----------|-------------|-----------|  
|1999|Quarter 1|Jan|  
|1999|Quarter 1|Feb|  
|1999|Quarter 1|Mar|  
|1999|Quarter 2|Apr|  
|1999|Quarter 2|May|  
|1999|Quarter 2|Jun|  
|1999|Quarter 3|Jul|  
|1999|Quarter 3|Aug|  
|1999|Quarter 3|Sep|  
|1999|Quarter 4|Oct|  
|1999|Quarter 4|Nov|  
|1999|Quarter 4|Dec|  
  
 The Year, Quarter, and Month attributes are used to construct a user-defined hierarchy, named Calendar, in the time dimension. The relationship between the levels and members of the Calendar dimension (a regular dimension) is shown in the following diagram.  
  
 ![Level and member hierarchy for a time dimension](../../../2014/analysis-services/dev-guide/media/as-levelconcepts.gif "Level and member hierarchy for a time dimension")  
  
> [!NOTE]  
>  Any hierarchy other than the default two-level attribute hierarchy is called a user-defined hierarchy. For more information about attribute hierarchies, see [Attributes and Attribute Hierarchies](../multidimensional-models-olap-logical-dimension-objects/attributes-and-attribute-hierarchies.md).  
  
## Member Structures  
 With the exception of parent-child hierarchies, the positions of members within the hierarchy are controlled by the order of the attributes in the hierarchy's definition. Each attribute in the hierarchy definition constitutes a level in the hierarchy. The position of a member within a level is determined by the ordering of the attribute used to create the level. The member structures of user-defined hierarchies can take one of four basic forms, depending on how the members are related to each other.  
  
### Balanced Hierarchies  
 In a balanced hierarchy, all branches of the hierarchy descend to the same level, and each member's logical parent is the level immediately above the member. The Product Categories hierarchy of the Product dimension in the [!INCLUDE[ssAWDWsp](../../includes/ssawdwsp-md.md)] sample [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database is a good example of a balanced hierarchy. Each member in the Product Name level has a parent member in the Subcategory level, which in turn has a parent member in the Category level. Also, every branch in the hierarchy has a leaf member in the Product Name level.  
  
### Unbalanced Hierarchies  
 In an unbalanced hierarchy, branches of the hierarchy descend to different levels. Parent-child hierarchies are unbalanced hierarchies. For example, the Organization dimension in the [!INCLUDE[ssAWDWsp](../../includes/ssawdwsp-md.md)] sample [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database contains a member for each employee. The CEO is the top member in the hierarchy, and the division managers and executive secretary are immediately beneath the CEO. The division managers have subordinate members but the executive secretary does not.  
  
 It may be impossible for end users to distinguish between unbalanced and ragged hierarchies. However, you employ different techniques and properties in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to support these two types of hierarchies. For more information, see [Ragged Hierarchies](../multidimensional-models/user-defined-hierarchies-ragged-hierarchies.md), and [Attributes in Parent-Child Hierarchies](../multidimensional-models/parent-child-dimension-attributes.md).  
  
### Ragged Hierarchies  
 In a ragged hierarchy, the logical parent member of at least one member is not in the level immediately above the member. This can cause branches of the hierarchy to descend to different levels. For example, in a Geography dimension defined with the levels Continent, CountryRegion, and City, in that order, the member Europe appears in the top level of the hierarchy, the member France appears in the middle level, and the member Paris appears in the bottom level. France is more specific than Europe, and Paris is more specific than France. To this regular hierarchy, the following changes are made:  
  
-   The Vatican City member is added to the CountryRegion level.  
  
-   Members are added to the City level and are associated with the Vatican City member in the CountryRegion level.  
  
-   A level, named Province, is added between the CountryRegion and City levels.  
  
 The Province level is populated with members associated with other members in the CountryRegion level, and members in the City level are associated with their corresponding members in the Province level. However, because the Vatican City member in the CountryRegion level has no associated members in the Province level, members must be associated from the City level directly to the Vatican City member in the CountryRegion level. Because of the changes, the hierarchy of the dimension is now ragged. The parent of the city Vatican City is the country/region Vatican City, which is not in the level immediately above the Vatican City member in the City level. For more information, see [Ragged Hierarchies](../multidimensional-models/user-defined-hierarchies-ragged-hierarchies.md).  
  
### Parent-Child Hierarchies  
 Parent-child hierarchies for dimensions are defined by using a special attribute, called a parent attribute, to determine how members relate to each other. A parent attribute describes a *self-referencing relationship*, or *self-join*, within a dimension main table. Parent-child hierarchies are constructed from a single parent attribute. Only one level is assigned to a parent-child hierarchy, because the levels present in the hierarchy are drawn from the parent-child relationships between members associated with the parent attribute. The dimension schema of a parent-child hierarchy depends on a self-referencing relationship present on the dimension main table. For example, the following diagram illustrates the **DimOrganization** dimension main table in the [!INCLUDE[ssAWDWsp](../../includes/ssawdwsp-md.md)][!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] sample database.  
  
 ![Self-referencing join in DimOrganization table](../../../2014/analysis-services/dev-guide/media/dimorganization.gif "Self-referencing join in DimOrganization table")  
  
 In this dimension table, the **ParentOrganizationKey** column has a foreign key relationship with the **OrganizationKey** primary key column. In other words, each record in this table can be related through a parent-child relationship with another record in the table. This kind of self-join is generally used to represent organization entity data, such as the management structure of employees in a department.  
  
 When you create a parent-child hierarchy, the columns represented by both attributes must have the same data type. Both attributes must also be in the same table. By default, any member whose parent key equals its own member key, null, 0 (zero), or a value absent from the column for member keys is assumed to be a member of the top level (excluding the (All) level).  
  
 The depth of a parent-child hierarchy can vary among its hierarchical branches. In other words, a parent-child hierarchy is considered an unbalanced hierarchy.  
  
 Unlike user-defined hierarchies, in which the number of levels in the hierarchy determines the number of levels that can be seen by end users, a parent-child hierarchy is defined with the single level of an attribute hierarchy, and the values in this single level produce the multiple levels seen by users. The number of displayed levels depends on the contents of the dimension table columns that store the member keys and the parent keys. The number of levels can change when the data in the dimension tables change. For more information, see [Parent-Child Hierarchy](../multidimensional-models/parent-child-dimension.md), and [Attributes in Parent-Child Hierarchies](../multidimensional-models/parent-child-dimension-attributes.md).  
  
## See Also  
 [Create User-Defined Hierarchies](../multidimensional-models/user-defined-hierarchies-create.md)   
 [User Hierarchy Properties](../multidimensional-models-olap-logical-dimension-objects/user-hierarchies-properties.md)   
 [Dimension Attribute Properties Reference](../multidimensional-models/dimension-attribute-properties-reference.md)  
  
  
