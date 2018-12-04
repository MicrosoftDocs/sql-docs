---
title: "Parent-Child Hierarchy | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "hierarchies [Analysis Services], parent-child"
  - "dimensions [Analysis Services], parent-child"
  - "parent attributes [Analysis Services]"
  - "data members [Analysis Services]"
  - "hierarchies [Analysis Services], multilevel"
  - "self-joins"
  - "self-referencing relationships"
  - "members [Analysis Services], data"
  - "parent-child dimensions [Analysis Services]"
ms.assetid: 4657f5dc-d88e-48d2-a448-08f79bc89546
author: minewiskan
ms.author: owend
manager: craigg
---
# Parent-Child Hierarchy
  A parent-child hierarchy is a hierarchy in a standard dimension that contains a parent attribute. A parent attribute describes a *self-referencing relationship*, or *self-join*, within a dimension main table. Parent-child hierarchies are constructed from a single parent attribute. Only one level is assigned to a parent-child hierarchy, because the levels present in the hierarchy are drawn from the parent-child relationships between members associated with the parent attribute. The position of a member in a parent-child hierarchy is determined by the `KeyColumns` and `RootMemberIf` properties of the parent attribute, whereas the position of a member in a level is determined by the `OrderBy` property of the parent attribute. For more information about attribute properties, see [Attributes and Attribute Hierarchies](../multidimensional-models-olap-logical-dimension-objects/attributes-and-attribute-hierarchies.md).  
  
 Because of parent-child relationships between levels in a parent-child hierarchy, some nonleaf members can also have data derived from underlying data sources, in addition to data aggregated from child members.  
  
## Dimension Schema  
 The dimension schema of a parent-child hierarchy depends on a self-referencing relationship present on the dimension main table. For example, the following diagram illustrates the **DimOrganization** dimension main table in the [!INCLUDE[ssSampleDBDWobject](../../includes/sssampledbdwobject-md.md)] sample database.  
  
 ![Self-referencing join in DimOrganization table](../dev-guide/media/dimorganization.gif "Self-referencing join in DimOrganization table")  
  
 In this dimension table, the **ParentOrganizationKey** column has a foreign key relationship with the **OrganizationKey** primary key column. In other words, each record in this table can be related through a parent-child relationship with another record in the table. This kind of self-join is generally used to represent organization entity data, such as the management structure of employees in a department.  
  
## Hierarchies and Levels  
 Dimensions that do not have a parent-child relationship construct hierarchies by grouping and ordering attributes. These dimensions derive the level names for their hierarchies from the attribute names.  
  
 However, parent-child dimensions construct parent-child hierarchies by examining the data that the dimension main table contains, and then evaluating the parent-child relationships between the records in the table. For more information about parent-child hierarchies, see [User Hierarchies](../multidimensional-models-olap-logical-dimension-objects/user-hierarchies.md).  
  
 Parent-child hierarchies do not derive the names for the levels in a parent-child hierarchy from the attributes that are used to create the hierarchy. Instead, these dimensions create level names automatically by using a naming template-a string expression you can specify at the level of the parent attribute that controls how the attribute generates the attribute hierarchy. For more information about how to set the naming template for a parent attribute, see [Attributes and Attribute Hierarchies](../multidimensional-models-olap-logical-dimension-objects/attributes-and-attribute-hierarchies.md).  
  
## Data Members  
 Typically, leaf members in a dimension contain data derived directly from underlying data sources, whereas nonleaf members contain data derived from aggregations performed on child members.  
  
 However, parent-child hierarchies might have some nonleaf members whose data is derived from underlying data sources, in addition to data aggregated from child members. For these nonleaf members in a parent-child hierarchy, special system-generated child members can be created that contain the underlying fact table data. Referred to as *data members*, these special child members contain a value that is directly associated with a nonleaf member and is independent of the summary value calculated from the descendants of the nonleaf member. For more information about data members, see [Attributes in Parent-Child Hierarchies](parent-child-dimension-attributes.md).  
  
## See Also  
 [Attributes in Parent-Child Hierarchies](parent-child-dimension-attributes.md)   
 [Database Dimension Properties](../multidimensional-models-olap-logical-dimension-objects/database-dimension-properties.md)  
  
  
